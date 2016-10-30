using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 教材选用_历史查看 : System.Web.UI.Page
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

            //加载学年
            string year = DateTime.Now.Year.ToString();
            string year2 = (Convert.ToInt32(year) - 1).ToString() + "-" + year;
            DropDataYear.Items.Add(new ListItem(year2));
            string year1 = year + "-" + (Convert.ToInt32(year) + 1).ToString();
            DropDataYear.Items.Add(new ListItem(year1));

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
        this.WebPager1.SqlField = " id,course_id,course_name,book_id,book_name,check_time "; 
        this.WebPager1.TableName = " Rbook_history ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " and 1>2";
    }

    protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlDataReader read = SQLHelper.ExecuteReader("select * from Rbook_history where id =" +  GridView1.DataKeys[e.RowIndex].Value.ToString());
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
            TextBox21.Text = read["college_advise"].ToString();
            TextBox23.Text = read["university_advise"].ToString();
            TextBox22.Text = read["press_date"].ToString();
            TextBox24.Text = read["check_time"].ToString();
        }
    }

    //查看
    protected void Button1_Click(object sender, EventArgs e)
    {
        string tc = DropDownList1.SelectedValue;
        string ci = TBCourseId.Text;
        string cn = TBCourseName.Text;
        string ti = TBTeacherId.Text;
        string tn = TBTeacherName.Text;
        string bn = TBBookName.Text;
        string dy = DropDataYear.SelectedValue;
        string se = DropSemester.SelectedValue;
        string st = DropState.SelectedValue;
        switch (st)
        {
            case "已保存": st = "1"; break;
            case "已提交": st = "2"; break;
            case "院审批已通过": st = "3"; break;
            case "校审批已通过": st = "4"; break;
        }
        this.WebPager1.SqlField = " id,course_id,course_name,book_id,book_name,check_time ";
        this.WebPager1.TableName = " Rbook_history ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
            + "and teacher_college like '%" + tc + "%' "
            + "and course_id like '%" + ci + "%' "
            + "and course_name like '%" + cn + "%' "
            + "and teacher_id like '%" + ti + "%' "
            + "and teacher_name like '%" + tn + "%' "
            + "and book_name like '%" + bn + "%' "
            + "and date_year like '%" + dy + "%' "
            + "and state_id like '%" + st + "%' "
            + "and semester like '%" + se + "%'";
    }

    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }
}