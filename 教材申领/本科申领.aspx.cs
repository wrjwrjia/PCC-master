using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class 教材选用_t : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //显示该老师的工号和姓名
            string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
            string sql1 = "select * from tbl_UserInformation where user_login= '" + username + "'";
            SqlDataReader read = SQLHelper.ExecuteReader(sql1);
            for (; read.Read(); )
            {
                TextBox2.Text = read["teacher_id"].ToString();
                TextBox3.Text = read["teacher_name"].ToString();
                TextBox1.Text = read["college"].ToString();
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

            //显示Dropdownlist
            string year = DateTime.Now.Year.ToString();
            string y1 = (DateTime.Now.Year - 1).ToString() + "-" + year;
            string y2 = year + "-" + (DateTime.Now.Year + 1).ToString();
            DropDownList1.Items.Add(y1);
            DropDownList1.Items.Add(y2);
        }        
    }

    protected void UpdateGridRb()
    {
        string ci = DropCourseId.SelectedValue;
        string cn = DropCourseName.SelectedValue;
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string tc = TextBox1.Text;
        string ti = TBTeacherId.Text;
        string ati=TextBox2.Text;

        //显示登记过的书单
        string rbsql = "select id,book_id,book_name,press_name,press_time,book_editor,book_price,addition,book_catagory from Rbook_message where date_year ='" + dy + "' and semester ='" + se + "' and course_id ='" + ci + "' and state_id=4";
        DataTable rbdt = SQLHelper.GetDataTable(rbsql);
        GridView3.DataSource = rbdt;
        GridView3.DataBind();

        //显示本次领书单
        string sql1 = "select id,book_id,book_name,press_name,press_time,book_editor,book_price "
        +"from book_message_mid where "
        +"date_year ='" + dy + "' "
        +"and semester ='" + se + "' "
        +"and teacher_id ='" + ti + "' "
        +"and apply_teacher_id ='" + ati + "' "
        +"and course_id ='" + ci + "'";
        DataTable dt = SQLHelper.GetDataTable(sql1);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }

    //已申领书单中的确定
    protected void Button3_Click(object sender, EventArgs e)
    {
        string id = (sender as Button).CommandArgument;
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string tc = TextBox1.Text;
        string ati = TextBox2.Text;
        string atn = TextBox3.Text;
        string ti = TBTeacherId.Text;
        string tn = TBTeacherName.Text;
        string ci = DropCourseId.SelectedValue;
        string cn = DropCourseName.SelectedValue;

        string sql3 = "select * from Rbook_message where id='" + id + "'";
        SqlDataReader read3 = SQLHelper.ExecuteReader(sql3);
        while (read3.Read())
        {
            string bn = read3["book_name"].ToString();
            string bi = read3["book_id"].ToString();
            string bp = read3["book_price"].ToString();
            string be = read3["book_editor"].ToString();
            string pn = read3["press_name"].ToString();
            string pt = read3["press_time"].ToString();
            string pd = read3["press_date"].ToString();
            string ad = read3["addition"].ToString();
            string asi = read3["add_state_id"].ToString();

            //判断book_message_mid中是否有这本书的记录
            string sql2 = "select id from book_message_mid where "
            +"course_id='" + ci + "' "
            +"and date_year='" + dy + "' "
            +"and semester='" + se + "' "
            +"and apply_teacher_id='" + ati + "' "
            +"and teacher_id='" + ti + "' "
            +"and book_id='" + bi + "'";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            string fid = "";
            while (read2.Read())
            {
                fid = read2[0].ToString();
            }
            if (fid == "")//说明book_message_mid中没有未处理的记录
            {
                //将该课程的书的信息从Rbook_message写入book_message_mid中
                string sql4 = "insert into book_message_mid (date_year,semester,teacher_college,teacher_id,teacher_name,course_id,course_name,book_id,book_name,book_price,book_editor,press_name,press_time,press_date,university_advise,state_id,addition,add_state_id,apply_teacher_id,apply_teacher_name)";
                sql4 += "values('" + dy + "','" + se + "','" + tc + "','" + ti + "','" + tn + "','" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bp + "','" + be + "','" + pn + "','" + pt + "','" + pd + "','',0,'" + ad + "','" + asi + "','" + ati + "','" + atn + "')";
                SQLHelper.ExecuteNonQuery(sql4);
            }
            else
            {
                WebMessageBox.Show("您已为此教师申领了此书!");
            }
        }
        string sql1 = "select id,book_id,book_name,press_name,press_time,book_editor,book_price from book_message_mid where date_year ='" + dy + "' and semester ='" + se + "' and apply_teacher_id ='" + ati + "' and teacher_id ='" + ti + "'and course_id ='" + ci + "'";
        DataTable dt = SQLHelper.GetDataTable(sql1);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }

    //本次申领书目中的删除
    protected void Button4_Click(object sender, EventArgs e)
    {
        string id = (sender as Button).CommandArgument;
        string sql = "delete from book_message_mid where id=" + id;
        SQLHelper.ExecuteNonQuery(sql);

        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string ati = TextBox2.Text;
        string ti = TBTeacherId.Text;
        string ci = DropCourseId.SelectedValue;
        string sql1 = "select id,book_id,book_name,press_name,press_time,book_editor,book_price from book_message_mid where date_year ='" + dy + "' and semester ='" + se + "' and teacher_id ='" + ti + "' and apply_teacher_id ='" + ati + "' and course_id ='" + ci + "'";
        DataTable dt = SQLHelper.GetDataTable(sql1);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }

    //保存
    protected void Button7_Click(object sender, EventArgs e)
    {
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string tc = TextBox1.Text;
        string ati = TextBox2.Text;
        string atn = TextBox3.Text;
        string ci = DropCourseId.SelectedValue;
        string cn = DropCourseName.SelectedValue;
        string cp = DropDownList3.Text;
        string ti = TBTeacherId.Text;
        string tn = TBTeacherName.Text;

        //将book_message_mid中的数据写入book_message中
        string sql1 = "select * from book_message_mid where "
        +"date_year='" + dy + "' "
        +"and semester='" + se + "' "
        +"and teacher_id='" + ti + "' "
        +"and apply_teacher_id='" + ati + "' "
        +"and course_id= '" + ci + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql1);
        while (read.Read())
        {
            string bi = read["book_id"].ToString();
            string bn = read["book_name"].ToString();
            string be = read["book_editor"].ToString();
            string pn = read["press_name"].ToString();
            string pt = read["press_time"].ToString();
            string pd = read["press_date"].ToString();
            string bp = read["book_price"].ToString();
            string ad = read["addition"].ToString();
            string asi = read["add_state_id"].ToString();

            //判断此书是否在book_message中已有
            string si = "";
            string tbi = "";
            int flag = 0;//原始状态insert
            string sql2 = "select book_id,state_id from book_message where "
            +"date_year='" + dy + "' "
            +"and semester='" + se + "' "
            +"and teacher_id='" + ti + "' "
            +"and apply_teacher_id='" + ati + "' "
            +"and course_id='" + ci + "'";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            for (; read2.Read(); )
            {
                tbi = read2["book_id"].ToString();
                si = read2["state_id"].ToString();
                if (tbi == bi)
                {
                    if (si == "1")
                    {
                        flag = 1;//update
                        break;
                    }
                    else
                    {
                        flag = 2;//nothing
                        break;
                    }
                }
            }
            if (flag == 1)
            {
                string sql = "update book_message set "
                +"book_name='" + bn + "',"
                +"book_price='" + bp + "',"
                +"book_editor='" + be + "',"
                +"press_name='" + pn + "',"
                +"press_date='" + pd + "',"
                +"press_time='" + pt + "',"
                +"state_id=1,"
                +"university_advise='',"
                +"addition='" + ad + "',"
                +"add_state_id='" + asi + "' "
                +"where date_year='" + dy + "' "
                +"and semester='" + se + "' "
                +"and teacher_id='" + ti + "' "
                +"and apply_teacher_id='" + ati + "' "
                +"and course_id='" + ci + "' "
                +"and book_id='" + bi + "' "
                +"and Campus='" + cp + "'";
                SQLHelper.ExecuteNonQuery(sql);
            }
            else if (flag == 0)
            {
                string sql = "insert into book_message (date_year,semester,teacher_college,teacher_id,teacher_name,course_id,course_name,book_id,book_name,book_price,book_editor,press_name,press_time,press_date,university_advise,state_id,addition,add_state_id,Campus,apply_teacher_id,apply_teacher_name)";
                sql += "values('" + dy + "','" + se + "','" + tc + "','" + ti + "','" + tn + "','" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bp + "','" + be + "','" + pn + "','" + pt + "','" + pd + "','',1,'" + ad + "','0','" + cp + "','" + ati + "','" + atn + "')";
                SQLHelper.ExecuteNonQuery(sql);
            }
        }
        //删除book_message_mid中该课程的记录
        string sql3 = "delete from book_message_mid where date_year='" + dy + "' and semester='" + se + "' and teacher_id='" + ti + "' and apply_teacher_id='" + ati + "' and course_id='" + ci + "'";
        SQLHelper.ExecuteNonQuery(sql3);
        JScript.AjaxAlertAndLocationHref(this.Page, "保存成功", "提交申领.aspx?ti=" + ti);
    }

    //提交
    protected void Button8_Click(object sender, EventArgs e)
    {
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string tc = TextBox1.Text;
        string ati = TextBox2.Text;
        string atn = TextBox3.Text;
        string ci = DropCourseId.SelectedValue;
        string cn = DropCourseName.SelectedValue;
        string cp = DropDownList3.SelectedValue;
        string ti = TBTeacherId.Text;
        string tn = TBTeacherName.Text;

        //将book_message_mid中的数据写入book_message中
        string sql1 = "select * from book_message_mid where "
        +"date_year='" + dy + "' and "
        +"semester='" + se + "' "
        +"and teacher_id='" + ti + "' "
        +"and apply_teacher_id='" + ati + "' "
        +"and course_id= '" + ci + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql1);
        while (read.Read())
        {
            string bi = read["book_id"].ToString();
            string bn = read["book_name"].ToString();
            string be = read["book_editor"].ToString();
            string pn = read["press_name"].ToString();
            string pt = read["press_time"].ToString();
            string pd = read["press_date"].ToString();
            string bp = read["book_price"].ToString();
            string ad = read["addition"].ToString();
            string asi = read["add_state_id"].ToString();

            //判断此书是否在book_message中已有
            string si = "";
            string tbi = "";
            int flag = 0;//原始状态insert
            string sql2 = "select book_id,state_id from book_message where "
            +"date_year='" + dy + "' "
            +"and semester='" + se + "' "
            +"and teacher_id='" + ti + "' "
            +"and apply_teacher_id='" + ati + "' "
            +"and course_id='" + ci + "'";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            for (; read2.Read(); )
            {
                tbi = read2["book_id"].ToString();
                si = read2["state_id"].ToString();
                if (tbi == bi)
                {
                    if (si == "1")
                    {
                        flag = 1;//update
                        break;
                    }
                    else
                    {
                        flag = 2;//nothing
                        break;
                    }
                }
            }
            if (flag == 1)
            {
                string sql = "update book_message set "
                +"book_name='" + bn + "',"
                +"book_price='" + bp + "',"
                +"book_editor='" + be + "',"
                +"press_name='" + pn + "',"
                +"press_date='" + pd + "',"
                +"press_time='" + pt + "',"
                +"state_id=2,"
                +"university_advise='',"
                +"addition='" + ad + "',"
                +"add_state_id='" + asi + "' "
                +"where "
                +"date_year='" + dy + "' "
                +"and semester='" + se + "' "
                +"and teacher_id='" + ti + "' "
                +"and apply_teacher_id='" + ati + "' "
                +"and course_id='" + ci + "' "
                +"and Campus='" + cp + "'";
                SQLHelper.ExecuteNonQuery(sql);
            }
            else if (flag == 0)
            {
                string sql = "insert into book_message (date_year,semester,teacher_college,teacher_id,teacher_name,course_id,course_name,book_id,book_name,book_price,book_editor,press_name,press_time,press_date,university_advise,state_id,addition,add_state_id,Campus,apply_teacher_id,apply_teacher_name)";
                sql += "values('" + dy + "','" + se + "','" + tc + "','" + ti + "','" + tn + "','" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bp + "','" + be + "','" + pn + "','" + pt + "','" + pd + "','',2,'" + ad + "','0','" + cp + "','" + ati + "','" + atn + "')";
                SQLHelper.ExecuteNonQuery(sql);
            }
        }
        JScript.AjaxAlertAndLocationHref(this.Page, "提交成功", "个人查看申领.aspx?ci=" + ci + "&ti=" + ti + "&dy=" + dy + "&se=" + se);
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

    //基本信息的确定
    protected void OK_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "" 
            || DropDownList2.SelectedValue == ""
            || DropDownList3.SelectedValue == "" 
            || DropCourseId.SelectedValue == "" 
            || DropCourseName.SelectedValue == ""
            || TextBox1.Text==""
            || TextBox2.Text == ""
            || TextBox3.Text == ""
            || TBTeacherId.Text==""
            || TBTeacherName.Text=="" )
            WebMessageBox.Show("请填入基本信息！");
        else
        {
            DropCourseId.Enabled = false;
            DropCourseName.Enabled = false;
            DropDownList1.Enabled = false;
            DropDownList2.Enabled = false;
            DropDownList3.Enabled = false;
            TextBox1.ReadOnly = true;
            TextBox2.ReadOnly = true;
            TextBox3.ReadOnly = true;
            TBTeacherId.ReadOnly = true;
            TBTeacherName.ReadOnly = true;
        }
        UpdateGridRb();
    }
}