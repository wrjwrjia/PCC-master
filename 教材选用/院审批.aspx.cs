using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class 教材选用_院审批 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Object.Equals(Request.Cookies["user"], null))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                initData();
            }
        }
    }

    private void initData()
    {
        //这个老师只能看到自己院的信息
        string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
        string sql = "select * from tbl_UserInformation where user_login= '" + username + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql);
        string tc = "";
        for (; read.Read(); )
        {
            tc = read["college"].ToString();
        }
        string ci = TextBox1.Text;
        string cn = TextBox2.Text;
        string ti = TextBox3.Text;
        string tn = TextBox4.Text;
        string bi = TBISBN.Text;
        string bn = TBBookName.Text;

        this.WebPager1.SqlField = " id,teacher_id,teacher_name,course_id,course_name,book_id,book_name,book_catagory ";
        this.WebPager1.TableName = " Rbook_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
            + "and teacher_college='" + tc + "' "//老师的院
            + "and state_id=2 "//提交的
            + "and course_id like '%" + ci + "%' "
            + "and course_name like '%" + cn + "%' "
            + "and teacher_id like '%" + ti + "%' "
            + "and teacher_name like '%" + tn + "%' "
            + "and book_id like '%" + bi + "%' "
            + "and book_name like'%" + bn + "%'";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
    }

    //全部同意
    protected void Button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
            {
                string sql = "update Rbook_message set college_advise='同意',state_id=3 where id=" + GridView1.DataKeys[i].Value;
                SQLHelper.ExecuteNonQuery(sql);
            }
        }
        initData();
    }

    //全部不同意
    protected void Button3_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
                SQLHelper.ExecuteNonQuery("update Rbook_message set college_advise='不同意',state_id=1 where id=" + GridView1.DataKeys[i].Value);
        }
        initData();
    }

    //同意
    protected void Button4_Click(object sender, EventArgs e)
    {
        string advise;
        if (TextBox21.Text == "")
            advise = "同意";
        else
            advise = TextBox21.Text;

        string sql = "update Rbook_message set college_advise='" + advise + "',state_id=3 where id =" + SaveID.Text;
        System.Diagnostics.Debug.WriteLine(sql);
        SQLHelper.ExecuteNonQuery(sql);

        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";
        TextBox15.Text = "";
        TextBox16.Text = "";
        TextBox17.Text = "";
        TextBox18.Text = "";
        TextBox19.Text = "";
        TextBox20.Text = "";
        TextBox21.Text = "";
        TextBox22.Text = "";
        initData();
    }

    //不同意
    protected void Button5_Click(object sender, EventArgs e)
    {
        string advise="";
        if (TextBox21.Text == "")
            WebMessageBox.Show("请输入审批意见！");
        else
            advise = TextBox21.Text;
        string sql = "update Rbook_message set college_advise='" + advise + "',state_id=1 where id =" + SaveID.Text;
        System.Diagnostics.Debug.WriteLine(sql);
        SQLHelper.ExecuteNonQuery(sql);

        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";
        TextBox15.Text = "";
        TextBox16.Text = "";
        TextBox17.Text = "";
        TextBox18.Text = "";
        TextBox19.Text = "";
        TextBox20.Text = "";
        TextBox21.Text = "";
        TextBox22.Text = "";
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
            TextBox22.Text = read["press_date"].ToString();
        }
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