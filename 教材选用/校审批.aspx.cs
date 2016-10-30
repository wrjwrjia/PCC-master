using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class 教材选用_校审批 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = "select xsm from CODE_XSB";
            SqlDataReader read = SQLHelper.ExecuteReader(sql);
            while (read.Read())
            {
                DropDownList1.Items.Add(new ListItem(read[0].ToString()));
            }
            if (Object.Equals(Request.Cookies["user"], null))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                this.WebPager1.SqlField = " id,teacher_college,teacher_name,course_id,course_name,book_id,book_name ";
                this.WebPager1.TableName = " Rbook_message ";
                this.WebPager1.orderByID = " id asc ";
                this.WebPager1.TblID = " id ";
                this.WebPager1.WhereClause = " and 2<1 ";
            }
        }
    }

    private void initData()
    {
        string ci = TextBox1.Text;
        string cn = TextBox2.Text;
        string ti = TextBox3.Text;
        string tn = TextBox4.Text;
        string tc = DropDownList1.SelectedValue;
        string bi = TBISBN.Text;
        string bn = TBBookName.Text;

        this.WebPager1.SqlField = " id,teacher_college,teacher_name,course_id,course_name,book_id,book_name ";
        this.WebPager1.TableName = " Rbook_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
            + "and state_id=3 "
            + "and course_id like '%" + ci + "%' "
            + "and course_name like '%" + cn + "%' "
            + "and teacher_id like '%" + ti + "%' "
            + "and teacher_name like '%" + tn + "%' "
            + "and teacher_college like '%" + tc + "%'"
            + "and book_id like '%" + bi + "%' "
            + "and book_name like '%" + bn + "%' ";
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
    }

    //进入
    protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SaveID.Text = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string id = SaveID.Text;
        SqlDataReader read = SQLHelper.ExecuteReader("select * from Rbook_message where id =" + id);

        for (; read.Read(); )
        {
            TBYear.Text = read["date_year"].ToString();
            TBSemester.Text = read["semester"].ToString();
            TextBox5.Text = read["teacher_college"].ToString();
            TextBox6.Text = read["teacher_major"].ToString();
            TextBox7.Text = read["teacher_id"].ToString();
            TextBox8.Text = read["teacher_name"].ToString();
            TextBox9.Text = read["phone_number"].ToString();
            TextBox10.Text = read["course_id"].ToString();
            TextBox11.Text = read["course_name"].ToString(); 
            TextBox12.Text = read["book_id"].ToString();
            TextBox13.Text = read["book_name"].ToString();
            TextBox14.Text = read["book_catagory"].ToString();
            TextBox15.Text = read["book_award"].ToString();
            TextBox16.Text = read["book_editor"].ToString();
            TextBox17.Text = read["book_price"].ToString();
            TextBox18.Text = read["press_name"].ToString();
            TextBox19.Text = read["press_time"].ToString();
            TextBox20.Text = read["addition"].ToString();
            TextBox21.Text = read["college_advise"].ToString();
            TextBox22.Text = read["press_date"].ToString();
        }
    }

    //全部同意
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count-1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked)
                {
                    //Rbook_message中更新university_advise
                    SQLHelper.ExecuteNonQuery("update Rbook_message set university_advise='同意',state_id=4 where id=" + GridView1.DataKeys[i].Value);

                    string num = "select * from Rbook_message where id=" + GridView1.DataKeys[i].Value;
                    SqlDataReader read = SQLHelper.ExecuteReader(num);
                    for (; read.Read(); )
                    {
                        string tc = read["teacher_college"].ToString();
                        string tm = read["teacher_major"].ToString();
                        string ti = read["teacher_id"].ToString();
                        string tn = read["teacher_name"].ToString();
                        string ph = read["phone_number"].ToString();
                        string ci = read["course_id"].ToString();
                        string cn = read["course_name"].ToString();
                        string bi = read["book_id"].ToString();
                        string bn = read["book_name"].ToString();
                        string bc = read["book_catagory"].ToString();
                        string bp = read["book_price"].ToString();
                        string ba = read["book_award"].ToString();
                        string be = read["book_editor"].ToString();
                        string pn = read["press_name"].ToString();
                        string pt = read["press_time"].ToString();
                        string pd = read["press_date"].ToString();
                        string ca = read["college_advise"].ToString();
                        string ua = read["university_advise"].ToString();
                        string ad = read["addition"].ToString();
                        string dy = read["date_year"].ToString();
                        string se = read["semester"].ToString();
                        string ct = DateTime.Now.ToString("yyyy/MM/dd");// 2008/9/4 时间
                        //历史记录
                        string sql1 = "insert into Rbook_history(teacher_college,teacher_major,teacher_id,teacher_name,phone_number,course_id,course_name,book_id,book_name,book_catagory,book_price,book_award,book_editor,press_name,press_time,press_date,college_advise,university_advise,state_id,addition,check_time,date_year,semester)";
                        sql1 += "values('" + tc + "','" + tm + "','" + ti + "','" + tn + "','" + ph + "','" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bc + "','" + bp + "','" + ba + "','" + be + "','" + pn + "','" + pt + "','" + pd + "','" + ca + "','" + ua + "',4,'" + ad + "','" + ct + "','" + dy + "','" + se + "')";
                        SQLHelper.ExecuteNonQuery(sql1);
                        //更新Abook_message中的书 根据book_id
                        string sql2 = "update Abook_message set book_name='" + bn + "',book_catagory='" + bc + "',book_price='" + bp + "',book_editor='" + be + "',press_name='" + pn + "',press_time='" + pt + "',press_date='" + pd + "' where book_id='" + bi + "' and course_id='" + ci + "'";
                        SQLHelper.ExecuteNonQuery(sql2);
                        //更新Abook_message_stu中的书
                        string sql3 = "update Abook_message_stu set BookName='" + bn + "',UnitPrice='" + bp + "',Publish='" + pn + "',Version='" + pt + "',InfoDate='" + pd + "' where BookID='" + bi + "'and CourseID='" + ci + "'";
                        SQLHelper.ExecuteNonQuery(sql3);
                    }
                }
            }
            initData();
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }

    //全部不同意
    protected void Button3_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
                SQLHelper.ExecuteNonQuery("update Rbook_message set university_advise='不同意',state_id=1 where id=" + GridView1.DataKeys[i].Value);
        }
        initData();
    }

    //同意
    protected void Button4_Click(object sender, EventArgs e)
    {
        //自动填入意见
        string ua;
        if (TextBox23.Text == "")
        {
            ua = "同意";
        }
        else
        {
            ua = TextBox23.Text;
        }

        string tc = TextBox5.Text;
        string tm = TextBox6.Text;
        string ti = TextBox7.Text;
        string tn = TextBox8.Text;
        string ph = TextBox9.Text;
        string ci = TextBox10.Text;
        string cn = TextBox11.Text;
        string bi = TextBox12.Text;
        string bn = TextBox13.Text;
        string bc = TextBox14.Text;
        string ba = TextBox15.Text;
        string be = TextBox16.Text;
        string bp = TextBox17.Text;
        string pn = TextBox18.Text;
        string pt = TextBox19.Text; 
        string ad = TextBox20.Text;
        string pd = TextBox22.Text;
        string ca = TextBox21.Text;
        string dy = TBYear.Text;
        string se = TBSemester.Text;
        string adsi="";
        string ct = DateTime.Now.ToString("yyyy/MM/dd");// 2008/9/4  时间
        //更新Rbook_message
        string id = SaveID.Text;
        string sql1 = "update Rbook_message set university_advise='" + ua + "',state_id=4 where id =" + id;
        SQLHelper.ExecuteNonQuery(sql1);
        if (ad == "")
            adsi = "0";
        else
            adsi = "1";
            
        //历史记录
        string sql2 = "insert into Rbook_history(teacher_college,teacher_major,teacher_id,teacher_name,phone_number,course_id,course_name,book_id,book_name,book_catagory,book_price,book_award,book_editor,press_name,press_time,press_date,college_advise,university_advise,addition,add_state_id,state_id,check_time,date_year,semester)";
        sql2 += "values('" + tc + "','" + tm + "','" + ti + "','" + tn + "','" + ph + "','" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bc + "','" + bp + "','" + ba + "','" + be + "','" + pn + "','" + pt + "','" + pd + "','" + ca + "','" + ua + "','" + ad + "','" + adsi + "',4,'" + ct + "','" + dy + "','" + se + "')";
        SQLHelper.ExecuteNonQuery(sql2);
        //更新Abook_message中的书 根据book_id
        string sql3 = "update Abook_message set book_name='" + bn + "',book_catagory='" + bc + "',book_price='" + bp + "',book_editor='" + be + "',press_name='" + pn + "',press_time='" + pt + "',press_date='" + pd + "' where book_id='" + bi + "' and course_id='" + ci + "'";
        SQLHelper.ExecuteNonQuery(sql3);
        //更新Abook_message_stu中的书
        string sql4 = "update Abook_message_stu set BookName='" + bn + "',UnitPrice='" + bp + "',Publish='" + pn + "',Version='" + pt + "',Term='" + pd + "' where BookName='" + bi + "'and CourseID='" + ci + "'";
        SQLHelper.ExecuteNonQuery(sql4);

        initData();
    }

    //不同意
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (TextBox23.Text == "")
        {
            WebMessageBox.Show("请输入审批意见");
            return;
        }
        string id = SaveID.Text;
        string sql = "update Rbook_message set university_advise='" + TextBox23.Text + "',state_id=1 where id =" + id;
        System.Diagnostics.Debug.WriteLine(sql);
        SQLHelper.ExecuteNonQuery(sql);
        initData();
    }

    //鼠标经过背景色改变
    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[1].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }
}