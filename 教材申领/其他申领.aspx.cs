using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

public partial class 教材申领_其他申领 : System.Web.UI.Page
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
            }

            //显示Dropdownlist
            string year = DateTime.Now.Year.ToString();
            string y1 = (DateTime.Now.Year - 1).ToString() + "-" + year;
            string y2 = year + "-" + (DateTime.Now.Year + 1).ToString();
            DropDownList1.Items.Add(y1);
            DropDownList1.Items.Add(y2);

            //加载出版社名称
            string sql2 = "select press_name from press_message";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            while (read2.Read())
            {
                DropPressName.Items.Add(new ListItem(read2[0].ToString()));
            }
        }
    }

    protected void UpdateGridRb()
    {
        string ci = Regex.Replace(TextBox4.Text, @" ", ""); //去空格
        string cn = Regex.Replace(TextBox5.Text, @" ", "");
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string tc = Regex.Replace(TextBox1.Text, @" ", "");
        string ti = Regex.Replace(TextBox2.Text, @" ", "");
        string tn = Regex.Replace(TextBox3.Text, @" ", "");
        string cp = DropDownList3.SelectedValue;

        string sql3 = "select * from book_message where "
        +"course_id='" + ci + "' "
        +"and date_year='" + dy + "' "
        +"and semester='" + se + "' "
        +"and teacher_id='" + ti + "'";
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

            //判断book_message_mid中是否有这本书的记录
            string sql2 = "select id from book_message_mid where course_id='" + ci + "' and date_year='" + dy + "' and semester='" + se + "' and teacher_id='" + ti + "' and book_id='" + bi + "'";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            string fid = "";
            while (read2.Read())
            {
                fid = read2[0].ToString();
            }
            if (fid == "")//说明book_message_mid中没有未处理的记录
            {
                //将该课程的书的信息从Rbook_message写入book_message_mid中
                string sql4 = "insert into book_message_mid (date_year,semester,teacher_college,teacher_id,teacher_name,course_id,course_name,book_id,book_name,book_price,book_editor,press_name,press_time,press_date,university_advise,state_id,Campus)";
                sql4 += "values('" + dy + "','" + se + "','" + tc + "','" + ti + "','" + tn + "','" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bp + "','" + be + "','" + pn + "','" + pt + "','" + pd + "','',0,'" + cp + "')";
                SQLHelper.ExecuteNonQuery(sql4);
            }
        }

        string sql1 = "select id,book_id,book_name,press_name,press_time,press_date,book_editor,book_price from book_message_mid where date_year ='" + dy + "' and semester ='" + se + "' and teacher_id ='" + ti + "'and course_id ='" + ci + "'";
        DataTable dt = SQLHelper.GetDataTable(sql1);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        string ci = Regex.Replace(TextBox4.Text, @" ", ""); //去空格
        string cn = Regex.Replace(TextBox5.Text, @" ", "");
        if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "" && TextBox1.Text != "" && TextBox2.Text != "" && TextBox3.Text != ""&& DropDownList3.SelectedValue != "")
        {
            string crbsql = "select distinct course_id from book_message where course_id='" + ci + "' and course_name = '" + cn + "'";
            DataSet CountTable;
            CountTable = SQLHelper.GetDataSet(crbsql, "book_message");
            int count = CountTable.Tables[0].Rows.Count;
            if (count == 1)
            {
                UpdateGridRb();
            }
        }
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        string ci = Regex.Replace(TextBox4.Text, @" ", ""); //去空格
        string cn = Regex.Replace(TextBox5.Text, @" ", "");
        if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "" && TextBox1.Text != "" && TextBox2.Text != "" && TextBox3.Text != "" && DropDownList3.SelectedValue != "")
        {
            string crbsql = "select distinct course_id from book_message where course_id='" + ci + "' and course_name = '" + cn + "'";
            DataSet CountTable;
            CountTable = SQLHelper.GetDataSet(crbsql, "book_message");
            int count = CountTable.Tables[0].Rows.Count;
            if (count == 1)
            {
                UpdateGridRb();
            }
        }
    }

    //添加
    protected void Button1_Click(object sender, EventArgs e)
    {
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string tc = TextBox1.Text;
        string ti = TextBox2.Text;
        string tn = TextBox3.Text;
        string ci = TextBox4.Text;
        string cn = TextBox5.Text;
        string bi = TextBox7.Text;
        string bn = TextBox8.Text;
        string pn = DropPressName.SelectedValue;
        string pt = PressTime.SelectedValue;
        string pd = TBPressDate.Text;
        string bp = TextBox12.Text;
        string be = TextBox13.Text;
        string cp = DropDownList3.SelectedValue;

        //如果为空
        if (bn == "" || pn == "" || pt == "" || pd == "" || bp == ""|| be == "")
        {
            WebMessageBox.Show("除定价外，所有信息不能为空！");
            return;
        }
        //如果都填了
        else
        {
            //如果book_message_mid中已经有了这本书
            string tbi = "";
            SqlDataReader read = SQLHelper.ExecuteReader("select book_id from book_message_mid where date_year='" + dy + "' and semester='" + se + "' and course_id='" + ci + "' and teacher_id='" + ti + "'");
            for (; read.Read(); )
            {
                tbi = read[0].ToString();
                if (tbi == bi)
                {
                    WebMessageBox.Show("您已申请此书，不可添加！");
                    return;
                }
            }

            string ati = "";
            string atn = "";

            string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
            string sql1 = "select * from tbl_UserInformation where user_login= '" + username + "'";
            SqlDataReader read1 = SQLHelper.ExecuteReader(sql1);
            for (; read1.Read(); )
            {
                ati = read1["teacher_id"].ToString();
                atn = read1["teacher_name"].ToString();
            }

            //如果其中没有这本书 则要写入
            string str = "insert into book_message_mid (date_year,semester,teacher_college,teacher_id,teacher_name,course_id,course_name,book_id,book_name,book_price,book_editor,press_name,press_time,press_date,university_advise,state_id,Campus,apply_teacher_id,apply_teacher_name)";
            str += "values('" + dy + "','" + se + "','" + tc + "','" + ti + "','" + tn + "','" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bp + "','" + be + "','" + pn + "','" + pt + "','" + pd + "','',0,'" + cp + "','" + ati + "','" +atn + "')";
            SQLHelper.ExecuteNonQuery(str);

            string sql2 = "select id,book_id,book_name,press_name,press_time,press_date,book_editor,book_price from book_message_mid where "
            +"date_year ='" + dy + "' "
            +"and semester ='" + se + "' "
            +"and teacher_id ='" + ti + "' "
            +"and course_id ='" + ci + "'";
            DataTable dt = SQLHelper.GetDataTable(sql2);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TBPressDate.Text = "";
        DropPressName.SelectedValue = "";
        PressTime.SelectedValue = "";
    }

    //保存
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ati = "";
        string atn = "";
        string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
        string sql0 = "select * from tbl_UserInformation where user_login= '" + username + "'";
        SqlDataReader read1 = SQLHelper.ExecuteReader(sql0);
        for (; read1.Read(); )
        {
            ati = read1["teacher_id"].ToString();
            atn = read1["teacher_name"].ToString();
        }
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string tc = TextBox1.Text;
        string ti = TextBox2.Text;
        string tn = TextBox3.Text;
        string ci = TextBox4.Text;
        string cn = TextBox5.Text;
        string cp = DropDownList3.SelectedValue;

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

            //判断此书是否在book_message中已有
            string si = "";
            string tbi = "";
            int flag = 0;//原始状态insert
            string sql2 = "select book_id,state_id from book_message where "
            +"date_year='" + dy + "' "
            +"and semester='" + se + "' "
            +"and teacher_id='" + ti + "' "
            + "and apply_teacher_id='" + ati + "' "
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
                +"university_advise='' "
                +"where "
                +"date_year='" + dy + "' "
                +"and semester='" + se + "' "
                +"and teacher_id='" + ti + "' "
                +"and apply_teacher_id='" + ati + "' "
                +"and course_id='" + ci + "' "
                +"and book_id='" + bi + "'";
                SQLHelper.ExecuteNonQuery(sql);
            }
            else if (flag == 0)
            {
                string sql = "insert into book_message "
                    +"(date_year,semester,"
                +"teacher_college,teacher_id,teacher_name,"
                +"course_id,course_name,"
                +"book_id,book_name,"
                +"book_price,book_editor,"
                +"press_name,press_time,press_date,"
                +"university_advise,state_id,Campus,"
                +"apply_teacher_id,apply_teacher_name)";
                sql += "values('" + dy + "','" + se + "',"
                + "'" + tc + "','" + ti + "','" + tn + "',"
                + "'" + ci + "','" + cn + "',"
                + "'" + bi + "','" + bn + "','" + bp + "','" + be + "',"
                + "'" + pn + "','" + pt + "','" + pd + "',"
                + "'',1,'" + cp + "','" + ati + "','" + atn + "')";
                SQLHelper.ExecuteNonQuery(sql);
            }
        }
        //删除book_message_mid中该课程的记录
        string sql3 = "delete from book_message_mid where "
        +"date_year='" + dy + "' "
        +"and semester='" + se + "' "
        +"and teacher_id='" + ti + "' "
        +"and apply_teacher_id='" + ati + "' "
        +"and course_id='" + ci + "'";
        SQLHelper.ExecuteNonQuery(sql3);
        JScript.AjaxAlertAndLocationHref(this.Page, "保存成功", "提交申领.aspx?ti=" + ti + "&flag=1");
    }

    //提交
    protected void Button3_Click(object sender, EventArgs e)
    {
        string ati = "";
        string atn = "";
        string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
        string sql0 = "select * from tbl_UserInformation where user_login= '" + username + "'";
        SqlDataReader read1 = SQLHelper.ExecuteReader(sql0);
        for (; read1.Read(); )
        {
            ati = read1["teacher_id"].ToString();
            atn = read1["teacher_name"].ToString();
        }
        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string tc = TextBox1.Text;
        string ti = TextBox2.Text;
        string tn = TextBox3.Text;
        string ci = TextBox4.Text;
        string cn = TextBox5.Text;
        string cp = DropDownList3.SelectedValue;

        //将book_message_mid中的数据写入book_message中
        string sql1 = "select * from book_message_mid where "
        + "date_year='" + dy + "' "
        + "and semester='" + se + "' "
        + "and teacher_id='" + ti + "' "
        + "and apply_teacher_id='" + ati + "' "
        + "and course_id= '" + ci + "'";
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

            //判断此书是否在book_message中已有
            string si = "";
            string tbi = "";
            int flag = 0;//原始状态insert
            string sql2 = "select book_id,state_id from book_message where "
            + "date_year='" + dy + "' "
            + "and semester='" + se + "' "
            + "and teacher_id='" + ti + "' "
            + "and apply_teacher_id='" + ati + "' "
            + "and course_id='" + ci + "'";
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
                + "book_name='" + bn + "',"
                + "book_price='" + bp + "',"
                + "book_editor='" + be + "',"
                + "press_name='" + pn + "',"
                + "press_date='" + pd + "',"
                + "press_time='" + pt + "',"
                + "state_id=2,"
                + "university_advise='' "
                + "where "
                + "date_year='" + dy + "' "
                + "and semester='" + se + "' "
                + "and teacher_id='" + ti + "' "
                + "and apply_teacher_id='" + ati + "' "
                + "and course_id='" + ci + "' "
                + "and book_id='" + bi + "'";
                SQLHelper.ExecuteNonQuery(sql);
            }
            else if (flag == 0)
            {
                string sql = "insert into book_message "
                    + "(date_year,semester,"
                + "teacher_college,teacher_id,teacher_name,"
                + "course_id,course_name,"
                + "book_id,book_name,"
                + "book_price,book_editor,"
                + "press_name,press_time,press_date,"
                + "university_advise,state_id,Campus,"
                + "apply_teacher_id,apply_teacher_name)";
                sql += "values('" + dy + "','" + se + "',"
                + "'" + tc + "','" + ti + "','" + tn + "',"
                + "'" + ci + "','" + cn + "',"
                + "'" + bi + "','" + bn + "','" + bp + "','" + be + "',"
                + "'" + pn + "','" + pt + "','" + pd + "',"
                + "'',2,'" + cp + "','" + ati + "','" + atn + "')";
                SQLHelper.ExecuteNonQuery(sql);
            }
        }
        //删除book_message_mid中该课程的记录
        string sql3 = "delete from book_message_mid where "
        + "date_year='" + dy + "' "
        + "and semester='" + se + "' "
        + "and teacher_id='" + ti + "' "
        + "and apply_teacher_id='" + ati + "' "
        + "and course_id='" + ci + "'";
        SQLHelper.ExecuteNonQuery(sql3);
        JScript.AjaxAlertAndLocationHref(this.Page, "提交成功", "个人查看申领.aspx?ci=" + ci + "&ti=" + ti + "&dy=" + dy + "&se=" + se);
    }

    //已申领中的删除
    protected void Button4_Click(object sender, EventArgs e)
    {
        string id = (sender as Button).CommandArgument;
        string sql = "delete from book_message_mid where id =" + id;
        SQLHelper.ExecuteNonQuery(sql);

        string dy = DropDownList1.SelectedValue;
        string se = DropDownList2.SelectedValue;
        string ti = TextBox2.Text;
        string ci = TextBox4.Text;

        string sql1 = "select id,book_id,book_name,press_name,press_time,press_date,book_editor,book_price from book_message_mid where date_year ='" + dy + "' and semester ='" + se + "' and teacher_id ='" + ti + "'and course_id ='" + ci + "'";
        DataTable dt = SQLHelper.GetDataTable(sql1);
        GridView1.DataSource = dt;
        GridView1.DataBind();
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

    
    protected void OK_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == ""
            || DropDownList2.SelectedValue == ""
            || DropDownList3.SelectedValue == ""
            || TextBox1.Text == ""
            || TextBox2.Text == ""
            || TextBox3.Text == ""
            || TextBox4.Text == ""
            || TextBox5.Text == "")
            WebMessageBox.Show("请填入基本信息！");
        else
        {
            DropDownList1.Enabled = false;
            DropDownList2.Enabled = false;
            DropDownList3.Enabled = false;
            TextBox1.ReadOnly = true;
            TextBox2.ReadOnly = true;
            TextBox3.ReadOnly = true;
            TextBox4.ReadOnly = true;
            TextBox5.ReadOnly = true;
            string ci = Regex.Replace(TextBox4.Text, @" ", ""); //去空格
            string dy = DropDownList1.SelectedValue;
            string se = DropDownList2.SelectedValue;
            string ti = Regex.Replace(TextBox2.Text, @" ", "");

            string sql1 = "select id,book_id,book_name,press_name,press_time,press_date,book_editor,book_price from book_message_mid where "
            + "date_year ='" + dy + "' "
            + "and semester ='" + se + "' "
            + "and teacher_id ='" + ti + "' "
            + "and course_id ='" + ci + "'";
            DataTable dt = SQLHelper.GetDataTable(sql1);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}