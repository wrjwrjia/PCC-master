using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 教材选用_允许更改 : System.Web.UI.Page
{
    //只有从Copy页面返回时，value才有值
    string value = string.Empty;
    static string oldProcessName;
    static string xh;
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
        this.WebPager1.SqlField = " id,course_id,course_name,book_id,book_name ";
        this.WebPager1.TableName = " Rbook_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " and 2<1";
    }

    //查询
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
        this.WebPager1.SqlField = " id,course_id,course_name,book_id,book_name ";
        this.WebPager1.TableName = " Rbook_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " and state_id=4 and state_id<>5";

        this.WebPager1.WhereClause += " "
            + "and teacher_college like '%" + tc + "%' "
            + "and course_id like '%" + ci + "%' "
            + "and course_name like '%" + cn + "%' "
            + "and teacher_id like '%" + ti + "%' "
            + "and teacher_name like '%" + tn + "%' "
            + "and book_name like '%" + bn + "%' "
            + "and date_year like '%" + dy + "%' "
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

    //更改
    protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
    }

    //取消
    protected void gvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
    }

    //原因确定
    protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string Reason = ((TextBox)this.GridView1.Rows[e.RowIndex].FindControl("TBReason")).Text.ToString();

        //判断合法性
        if (Reason=="")
        {
            JScript.AjaxAlert(this.Page, "请输入要求更改的原因!");
            return;
        }

        //更改Rbook_message中的状态
        SQLHelper.ExecuteNonQuery("update Rbook_message set state_id=1,college_advise='',university_advise='"
            + Reason + "' where id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");

        //记录历史到Rbook_AloChaHis
        string num = "select * from Rbook_message where id=" + GridView1.DataKeys[e.RowIndex].Value.ToString();
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
            string dy = read["date_year"].ToString();
            string se = read["semester"].ToString();
            string ct = DateTime.Now.ToString("yyyy/MM/dd");// 2008/9/4 时间

            string sql1 = "insert into Rbook_AloChaHis(teacher_college,teacher_major,teacher_id,teacher_name,phone_number,course_id,course_name,book_id,book_name,book_catagory,book_price,book_award,book_editor,press_name,press_time,press_date,college_advise,university_advise,change_time,date_year,semester)";
            sql1 += "values('" + tc + "','" + tm + "','" + ti + "','" + tn + "','" + ph + "','" + ci + "','" + cn + "','" + bi + "','" + bn + "','" + bc + "','" + bp + "','" + ba + "','" + be + "','" + pn + "','" + pt + "','" + pd + "','" + ca + "','" + ua + "','" + ct + "','" + dy + "','" + se + "')";
            SQLHelper.ExecuteNonQuery(sql1);
        }
        GridView1.EditIndex = -1;
    }
}