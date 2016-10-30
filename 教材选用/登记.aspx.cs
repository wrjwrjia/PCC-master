using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

public partial class 教材选用_登记 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //显示该老师的工号和姓名
            string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
            string sql = "select * from tbl_UserInformation where user_login= '" + username + "'";
            SqlDataReader read = SQLHelper.ExecuteReader(sql);
            for (; read.Read(); )
            {
                TextBox1.Text = read["college"].ToString();
                TextBox2.Text = read["department"].ToString();
                TextBox3.Text = read["teacher_id"].ToString();
                TextBox4.Text = read["teacher_name"].ToString();
                TextBox5.Text = read["phone_number"].ToString();
            }

            //显示课程编号
            string sqli = "select course_id from tbl_CourseInformation where college='" + TextBox1.Text + "'";
            SqlDataReader readi = SQLHelper.ExecuteReader(sqli);
            while (readi.Read())
            {
                DropCourseId.Items.Add(new ListItem(readi[0].ToString()));
            }

            //显示课程名称
            string sqln = "select course_name from tbl_CourseInformation where college='" + TextBox1.Text + "'";
            SqlDataReader readn = SQLHelper.ExecuteReader(sqln);
            while (readn.Read())
            {
                DropCourseName.Items.Add(new ListItem(readn[0].ToString()));
            }
            
            //加载出版社名
            string sql2 = "select press_name from press_message";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            while (read2.Read())
            {
                DropDownList3.Items.Add(new ListItem(read2[0].ToString()));
            }

            string year = DateTime.Now.Year.ToString();
            string year2 = (Convert.ToInt32(year) - 1).ToString() + "-" + year;
            DropDownList1.Items.Add(new ListItem(year2));
            string year1 = year + "-" + (Convert.ToInt32(year) + 1).ToString();
            DropDownList1.Items.Add(new ListItem(year1));

            RadioButtonList1.SelectedIndex = 0;
            Addition.SelectedIndex = 0;
        }
    }

    protected void UpdateGrid()//更新历史表+登记表
    {
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string ci = DropCourseId.SelectedValue;
        string cn = DropCourseName.SelectedValue;

        //显示历史
        string sql3 = "select * from Rbook_history where course_id=" + ci + " and ( date_year <>'" + dy + "' or semester <>'" + se + "') ";
        DataTable dthistory = SQLHelper.GetDataTable(sql3);
        GridView2.DataSource = dthistory;
        GridView2.DataBind();

        //将本学期的信息读到Rbook_message_mid中
        //判断当前学年学期中Rbook_message_mid中有无这门课程所用教材
        string sql4 = "select id from Rbook_message_mid where course_id='" + ci + "'  and date_year='" + dy + "' and semester='" + se + "'";
        SqlDataReader read1 = SQLHelper.ExecuteReader(sql4);
        string id = "";
        while (read1.Read())
        {
            id = read1[0].ToString();
        }
        //如果Rbook_message_mid中不存在这门课程所用教材
        if (id == "")
        {
            //仅将Rbook_message中的此课程的书写入Rbook_message_mid
            string sql5 = "select * from Rbook_message where course_id='" + ci + "' and date_year='" + dy + "' and semester='" + se + "'";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql5);
            while (read2.Read())
            {
                string bn = read2["book_name"].ToString();
                string bi = read2["book_id"].ToString();
                string bp = read2["book_price"].ToString();
                string be = read2["book_editor"].ToString();
                string ba = read2["book_award"].ToString();
                string bc = read2["book_catagory"].ToString();
                string pn = read2["press_name"].ToString();
                string pt = read2["press_time"].ToString();
                string pd = read2["press_date"].ToString();
                string si = read2["state_id"].ToString();
                string ad = read2["addition"].ToString();
                string ads = read2["add_state_id"].ToString();
                string sql6 = "insert into Rbook_message_mid(course_id,course_name,book_id,book_name,book_price,book_editor,book_award,book_catagory,press_name,press_time,press_date,state_id,addition,add_state_id,date_year,semester)";
                sql6 += "values('" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bp + "','" + be + "','" + ba + "','" + bc + "','" + pn + "','" + pt + "','" + pd + "','" + si + "','" + ad + "','" + ads + "','" + dy + "','" + se + "')";
                SQLHelper.ExecuteNonQuery(sql6);
            }
        }
        UpdateGridNow();
    }

    //更新Rbook_message_mid
    protected void UpdateGridNow()
    {
        string sql4 = "select id,book_id,book_name,press_name,press_time,press_date,book_editor,book_price,book_catagory,date_year,semester from Rbook_message_mid"
        + " where course_id ='" + DropCourseId.SelectedValue + "'and date_year='" + DropDownList1.SelectedValue + "' and semester='" + DropDownList2.SelectedValue + "'";
        DataTable dt = SQLHelper.GetDataTable(sql4);
        GridView3.DataSource = dt;
        GridView3.DataBind();
    }


    //添加
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            //判断是否为自编讲义，自编讲义可以不填ISBN和出版社
            if (RadioButtonList1.SelectedIndex == 1)
            {
                if (TextBox9.Text == "" || PressTime.SelectedValue == "" || TextBox13.Text == "")
                {
                    WebMessageBox.Show("除ISBN 出版社 主编 定价，所有信息必填，不能为空！");
                    return;
                }
            }
            else
            {
                if (TextBox8.Text == "" || TextBox9.Text == "" || TextBox10.Text == "" || PressTime.Text == "" || TextBox13.Text == "" || DropDownList3.SelectedValue == "")
                    WebMessageBox.Show("请填入必要信息！");
            }
            string ba = "";
            if (CheckBox1.Checked)
                ba += "国家级获奖  ";
            if (CheckBox2.Checked)
                ba += "国家级立项/规划  ";
            if (CheckBox3.Checked)
                ba += "省（部）级获奖  ";
            if (CheckBox4.Checked)
                ba += "省（部）级立项/规划  ";
            if (CheckBox5.Checked)
                ba += "校级出版  ";
            if (CheckBox6.Checked)
                ba += "校级讲义  ";
            if (CheckBox7.Checked)
                ba += "21世纪  ";
            if (CheckBox8.Checked)
                ba += "学校指导委员会推荐  ";
            if (CheckBox9.Checked)
                ba += "统编  ";
            if (CheckBox10.Checked)
                ba += "其他  ";

            string bc;
            string pn = DropDownList3.SelectedValue;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                bc = "出版教材";
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                bc = "自编讲义";
                pn = "南京航空航天大学";
            }
            else
            {
                bc = "翻印教材";
            }

            string ad = "";
            if (Addition.SelectedIndex == 0)
            {
                ad = "一般";
            }
            else if (Addition.SelectedIndex == 1)
            {
                ad = "特殊适用班级";
            }
            else
            {
                ad = "新生";
            }

            //将更改内容写入Rbook_message_mid，并替换原来内容
            string ci = DropCourseId.SelectedValue;
            string cn = DropCourseName.SelectedValue;
            string bi = TextBox8.Text;
            string bn = TextBox9.Text;
            string be = TextBox10.Text;
            string bp = TextBox11.Text;
            string pd = TextBox13.Text;
            if (bp == "")
                bp = "0";
            string pt = PressTime.SelectedValue;
            if (pt == "最新版")
                pt = "0";
            string dy = DropDownList1.SelectedValue;
            string se = DropDownList2.SelectedValue;
            string adsi = "";

            //先判断这门课程
            if (SaveID.Text == "0")
            {
                //如果Rbook_message_mid中已经有了这本书
                string tbi = "";
                SqlDataReader read = SQLHelper.ExecuteReader("select book_id from Rbook_message_mid where course_id='" + ci + "'");
                for (; read.Read(); )
                {
                    tbi = read[0].ToString();
                    if (tbi == bi)
                    {
                        WebMessageBox.Show("您已申请此书，不可添加！");
                        return;
                    }
                }
            }

            if (ad != "")
            {
                adsi = "1";
            }
            else
            {
                adsi = "0";
            }


            if (SaveID.Text == "0")
            {
                string sql = "insert into Rbook_message_mid (course_id,course_name,book_id,book_name,book_price,book_catagory,book_award,book_editor,press_name,press_date,press_time,state_id,add_state_id,addition,date_year,semester)";
                sql += "values('" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bp + "','" + bc + "','" + ba + "','" + be + "','" + pn + "','" + pd + "','" + pt + "',1,'" + adsi + "','" + ad + "','" + dy + "','" + se + "')";
                SQLHelper.ExecuteNonQuery(sql);
            }
            else
            {
                string sql2 = "update Rbook_message_mid set book_name='" + bn + "',book_id='" + bi + "',book_catagory='" + bc + "',book_editor='" + be + "',book_price='" + bp + "',press_name='" + pn + "',press_time='" + pt + "',press_date='" + pd + "',book_award='" + ba + "',state_id=1,add_state_id='" + adsi + "',addition='" + ad + "',date_year='" + dy + "',semester='" + se + "' where id =" + SaveID.Text;
                SQLHelper.ExecuteNonQuery(sql2);
            }

            SaveID.Text = "0";
            UpdateGridNow();

            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            PressTime.SelectedValue = "";
            TextBox13.Text = "";
            Addition.SelectedIndex = 0;
            DropDownList3.SelectedValue = "";
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            CheckBox6.Checked = false;
            CheckBox7.Checked = false;
            CheckBox8.Checked = false;
            CheckBox9.Checked = false;
            CheckBox10.Checked = false;
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }

    }

    //修改
    protected void Button3_Click(object sender, EventArgs e)
    {
        //判断这本书的状态
        string id = (sender as Button).CommandArgument;
        string si = "";
        string adsi = "";
        string sql1 = "select state_id,add_state_id from Rbook_message_mid where id=" + id;
        SqlDataReader read1 = SQLHelper.ExecuteReader(sql1);
        for (; read1.Read(); )
        {
            si = read1[0].ToString();
            adsi = read1[1].ToString();
        }
        //如果此信息之前不是保存状态，则不可以编辑
        if (si != "1")
        {
            WebMessageBox.Show("此教材信息已提交，不可更改！");
        }
        //否则可以编辑
        else
        {
            string book_catagory = "";
            string book_award = "";
            string addition = "";
            SqlDataReader read = SQLHelper.ExecuteReader("select * from Rbook_message_mid where id =" + id);
            //将原来内容显示出来
            for (; read.Read(); )
            {
                TextBox8.Text = read["book_id"].ToString();
                TextBox9.Text = read["book_name"].ToString();
                book_catagory = read["book_catagory"].ToString();
                TextBox10.Text = read["book_editor"].ToString();
                TextBox11.Text = read["book_price"].ToString();
                DropDownList3.SelectedValue = read["press_name"].ToString();
                PressTime.SelectedValue = read["press_time"].ToString();
                TextBox13.Text = read["press_date"].ToString();
                book_award = read["book_award"].ToString();
                addition = read["addition"].ToString();
                SaveID.Text = id;
            }

            //addition
            if (book_catagory == "一般")
            {
                RadioButtonList1.SelectedIndex = 0;
            }
            else if (book_catagory == "特殊适用班级")
            {
                RadioButtonList1.SelectedIndex = 1;
            }
            else
            {
                RadioButtonList1.SelectedIndex = 2;
            }


            //book_catagory
            if (book_catagory == "出版教材")
            {
                RadioButtonList1.SelectedIndex = 0;
            }
            else if (book_catagory == "自编讲义")
            {
                RadioButtonList1.SelectedIndex = 1;
            }
            else
            {
                RadioButtonList1.SelectedIndex = 2;
            }
            //book_award
            string[] ba = book_award.Split(' ');
            for (int i = 0; i < ba.Length; i++)
            {
                if (ba[i] == "国家级获奖")
                    CheckBox1.Checked = true;
                if (ba[i] == "国家级立项/规划")
                    CheckBox2.Checked = true;
                if (ba[i] == "省（部）级获奖")
                    CheckBox3.Checked = true;
                if (ba[i] == "省（部）级立项/规划")
                    CheckBox4.Checked = true;
                if (ba[i] == "校级出版")
                    CheckBox5.Checked = true;
                if (ba[i] == "校级讲义")
                    CheckBox6.Checked = true;
                if (ba[i] == "21世纪")
                    CheckBox7.Checked = true;
                if (ba[i] == "学校指导委员会推荐")
                    CheckBox8.Checked = true;
                if (ba[i] == "统编")
                    CheckBox9.Checked = true;
                if (ba[i] == "其他")
                    CheckBox10.Checked = true;
            }
        }
    }

    //删除
    protected void Button4_Click(object sender, EventArgs e)
    {
        string id = (sender as Button).CommandArgument;
        string si = "";
        string sql = "select state_id from Rbook_message_mid where id=" + id;
        SqlDataReader read = SQLHelper.ExecuteReader(sql);
        for (; read.Read(); )
        {
            si = read[0].ToString();
        }
        //如果此信息之前只是保存状态，就可以删除
        if (si == "1")
        {
            SQLHelper.ExecuteNonQuery("delete from Rbook_message_mid where id=" + id);
            UpdateGridNow();
        }
        //如果此信息已提交或者审批，不可删除
        else
        {
            WebMessageBox.Show("此教材信息已提交，不可删除！");
        }
    }

    //提交
    protected void Button6_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            string mbi = "";
            string bi = "";
            string bn = "";
            string bp = "";
            string be = "";
            string ba = "";
            string bc = "";
            string pn = "";
            string pt = "";
            string pd = "";
            string ad = "";
            string adsi = "";
            string dy = DropDownList1.SelectedValue;
            string se = DropDownList2.SelectedValue;

            //读出Rbook_message_mid中的信息
            string sql1 = "select * from Rbook_message_mid where course_id='" + DropCourseId.SelectedValue + "' and date_year='" + dy + "' and semester='" + se + "'";
            SqlDataReader read1 = SQLHelper.ExecuteReader(sql1);
            while (read1.Read())
            {
                mbi = read1["book_id"].ToString();
                bn = read1["book_name"].ToString();
                bp = read1["book_price"].ToString();
                be = read1["book_editor"].ToString();
                ba = read1["book_award"].ToString();
                bc = read1["book_catagory"].ToString();
                pn = read1["press_name"].ToString();
                pt = read1["press_time"].ToString();
                pd = read1["press_date"].ToString();
                ad = read1["addition"].ToString();
                adsi = read1["add_state_id"].ToString();

                //判断此书是否在Rbook_message中已有
                string si = "";
                string sql2 = "select book_id,state_id from Rbook_message where course_id='" + DropCourseId.SelectedValue + "' and date_year='" + dy + "' and semester='" + se + "'";
                SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
                for (; read2.Read(); )
                {
                    bi = read2["book_id"].ToString();
                    si = read2["state_id"].ToString();
                    if (mbi == bi)
                    {
                        if (si == "1")
                            flag = 1;//update
                        else
                            flag = 2;//nothing
                    }
                }
                //update
                if (flag == 1)
                {
                    string sql = "update Rbook_message set book_name='" + bn + "',"
                    + "book_price='" + bp + "',"
                    + "book_catagory='" + bc + "',"
                    + "book_award='" + ba + "',"
                    + "book_editor='" + be + "',"
                    + "press_name='" + pn + "',"
                    + "press_date='" + pd + "',"
                    + "press_time='" + pt + "',"
                    + "state_id=1,"
                    + "college_advise='',"
                    + "university_advise='',"
                    + "addition='" + ad + "',"
                    + "add_state_id=" + adsi + " "
                    + "where course_id='" + DropCourseId.SelectedValue + "'"
                    + "and book_id='" + mbi + "' "
                    + "and date_year='" + dy + "' "
                    + "and semester='" + se + "'";
                    SQLHelper.ExecuteNonQuery(sql);
                }
                //insert
                else if (flag == 0)
                {
                    string tc = TextBox1.Text;
                    string tm = TextBox2.Text;
                    string ti = TextBox3.Text;
                    string tn = TextBox4.Text;
                    string ph = TextBox5.Text;
                    string ci = DropCourseId.SelectedValue;
                    string cn = DropCourseName.SelectedValue;
                    string sql = "insert into Rbook_message(teacher_college,teacher_major,teacher_id,teacher_name,phone_number,course_id,course_name,book_id,book_name,book_price,book_catagory,book_award,book_editor,press_name,press_date,press_time,state_id,addition,add_state_id,date_year,semester)";
                    sql += "values('" + tc + "','" + tm + "','" + ti + "','" + tn + "','" + ph + "','" + ci + "','" + cn + "','" + mbi + "','" + bn + "','" + bp + "','" + bc + "','" + ba + "','" + be + "','" + pn + "','" + pd + "','" + pt + "',2,'" + ad + "','" + adsi + "','" + dy + "','" + se + "')";
                    SQLHelper.ExecuteNonQuery(sql);
                }
                flag = 0;
            }
            //最终都删去这门课程对应的信息
            SQLHelper.ExecuteNonQuery("delete from Rbook_message_mid where course_id=" + DropCourseId.SelectedValue);
            JScript.AjaxAlertAndLocationHref(this.Page, "提交成功", "查看登记.aspx?ci=" + DropCourseId.SelectedValue + "&cn=" + DropCourseName.SelectedValue);
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }

    //保存
    protected void Button5_Click(object sender, EventArgs e)
    {
        int flag = 0;
        string mbi = "";
        string bi = "";
        string bn = "";
        string bp = "";
        string be = "";
        string ba = "";
        string bc = "";
        string pn = "";
        string pt = "";
        string pd = "";
        string ad = "";
        string adsi = "";
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;

        //读出Rbook_message_mid中的信息
        string sql1 = "select * from Rbook_message_mid where course_id='" + DropCourseId.SelectedValue + "' and date_year='" + dy + "' and semester='" + se + "'";
        SqlDataReader read1 = SQLHelper.ExecuteReader(sql1);
        while (read1.Read())
        {
            mbi = read1["book_id"].ToString();
            bn = read1["book_name"].ToString();
            bp = read1["book_price"].ToString();
            be = read1["book_editor"].ToString();
            ba = read1["book_award"].ToString();
            bc = read1["book_catagory"].ToString();
            pn = read1["press_name"].ToString();
            pt = read1["press_time"].ToString();
            pd = read1["press_date"].ToString();
            ad = read1["addition"].ToString();
            adsi = read1["add_state_id"].ToString();

            //判断此书是否在Rbook_message中已有
            string si = "";
            string sql2 = "select book_id,state_id from Rbook_message where course_id='" + DropCourseId.SelectedValue + "' and date_year='" + dy + "' and semester='" + se + "'";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            for (; read2.Read(); )
            {
                bi = read2["book_id"].ToString();
                si = read2["state_id"].ToString();
                if (mbi == bi)
                {
                    if (si == "1")
                        flag = 1;//update
                    else
                        flag = 2;//nothing
                }
            }
            //update
            if (flag == 1)
            {
                string sql = "update Rbook_message set book_name='" + bn + "',book_price='" + bp + "',book_catagory='" + bc + "',book_award='" + ba + "',book_editor='" + be + "',press_name='" + pn + "',press_date='" + pd + "',press_time='" + pt + "',state_id=1,college_advise='',university_advise='',addition='" + ad + "',add_state_id=" + adsi + " where course_id='" + DropCourseId.SelectedValue + "'and book_id='" + mbi + "' and date_year='" + dy + "' and semester='" + se + "'";
                SQLHelper.ExecuteNonQuery(sql);
            }
            //insert
            else if (flag == 0)
            {
                string tc = TextBox1.Text;
                string tm = TextBox2.Text;
                string ti = TextBox3.Text;
                string tn = TextBox4.Text;
                string ph = TextBox5.Text;
                string ci = DropCourseId.SelectedValue;
                string cn = DropCourseName.SelectedValue;
                string sql = "insert into Rbook_message(teacher_college,teacher_major,teacher_id,teacher_name,phone_number,course_id,course_name,book_id,book_name,book_price,book_catagory,book_award,book_editor,press_name,press_date,press_time,state_id,addition,add_state_id,date_year,semester)";
                sql += "values('" + tc + "','" + tm + "','" + ti + "','" + tn + "','" + ph + "','" + ci + "','" + cn + "','" + mbi + "','" + bn + "','" + bp + "','" + bc + "','" + ba + "','" + be + "','" + pn + "','" + pd + "','" + pt + "',1,'" + ad + "','" + adsi + "','" + dy + "','" + se + "')";
                SQLHelper.ExecuteNonQuery(sql);
            }
            flag = 0;
        }
        //最终都删去这门课程对应的信息
        SQLHelper.ExecuteNonQuery("delete from Rbook_message_mid where course_id=" + DropCourseId.SelectedValue);
        JScript.AjaxAlertAndLocationHref(this.Page, "保存成功", "查看登记.aspx?ci=" + DropCourseId.SelectedValue + "&cn=" + DropCourseName.SelectedValue);
    }

    //鼠标经过背景色改变
    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
    }

    //从历史中选择教材
    protected void SelectHistory_Click(object sender, EventArgs e)
    {
        //把这条记录copy到Rbook_message_mid中
        string id = (sender as Button).CommandArgument;
        SqlDataReader read = SQLHelper.ExecuteReader("select * from Rbook_history where id =" + id);
        //将原来内容得到
        for (; read.Read(); )
        {
            string cn = read["course_name"].ToString();
            string ci = read["course_id"].ToString();
            string bn = read["book_name"].ToString();
            string bi = read["book_id"].ToString();
            string bc = read["book_catagory"].ToString();
            string be = read["book_editor"].ToString();
            string ba = read["book_award"].ToString();
            string bp = read["book_price"].ToString();
            string pn = read["press_name"].ToString();
            string pt = read["press_time"].ToString();
            string pd = read["press_date"].ToString();
            string ad = read["addition"].ToString();
            string dy = DropDownList1.SelectedValue;
            string se = DropDownList2.SelectedValue;

            string sql = "insert into Rbook_message_mid (course_id,course_name,book_id,book_name,book_price,book_catagory,book_award,book_editor,press_name,press_date,press_time,state_id,addition,date_year,semester)";
            sql += "values('" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bp + "','" + bc + "','" + ba + "','" + be + "','" + pn + "','" + pd + "','" + pt + "',1,'" + ad + "','" + dy + "','" + se + "')";
            SQLHelper.ExecuteNonQuery(sql);
        }
        UpdateGridNow();
    }


    //课程编号内容改变
    protected void DropCourseId_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select course_name from tbl_CourseInformation where course_id= '" + DropCourseId.SelectedValue + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql);
        for (; read.Read(); )
        {
            DropCourseName.SelectedValue = read[0].ToString();
        }
    }

    //课程名称内容改变
    protected void DropCourseName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select course_id from tbl_CourseInformation where course_name= '" + DropCourseName.SelectedValue + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql);
        for (; read.Read(); )
        {
            DropCourseId.SelectedValue = read[0].ToString();
        }
    }

    //确定
    protected void OK_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "" || DropDownList2.SelectedValue == "" || DropCourseId.SelectedValue == "" || DropCourseName.SelectedValue == "")
            WebMessageBox.Show("请填入基本信息！");
        else
        {
            DropCourseId.Enabled = false;
            DropCourseName.Enabled = false;
            DropDownList1.Enabled = false;
            DropDownList2.Enabled = false;
            TextBox1.ReadOnly = true;
            TextBox2.ReadOnly = true;
            TextBox3.ReadOnly = true;
            TextBox4.ReadOnly = true;
            TextBox5.ReadOnly = true;
            UpdateGrid();
        }
    }
}