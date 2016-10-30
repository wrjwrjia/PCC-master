using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 教材选用_查看所有登记 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //加载出版社
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
        this.WebPager1.SqlField = " id,teacher_college,course_id,course_name,book_id,book_name,book_catagory,book_price,press_name,press_time,state_id,college_advise,university_advise ";
        this.WebPager1.TableName = " Rbook_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " and state_id <> 5 ";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string tc = DropDownList1.SelectedValue;
        string ci = TextBox2.Text;
        string cn = TextBox3.Text;
        string ti = TextBox4.Text;
        string tn = TextBox5.Text;
        string bn = TextBox1.Text;
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

        this.WebPager1.SqlField = " id,teacher_college,course_id,course_name,book_id,book_name,book_catagory,book_price,press_name,press_time,state_id,college_advise,university_advise ";
        this.WebPager1.TableName = " Rbook_message ";
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
            + "and semester like '%" + se + "%'"
            + "and state_id <> 5 "
            + "and state_id like'%" + st + "%'";
    }
    
    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            switch(e.Row.Cells[10].Text)
            {
                case "1": e.Row.Cells[10].Text = "保存";         
                    if (e.Row.Cells[11].Text != "") e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                    if (e.Row.Cells[12].Text != "") e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;  break;   
                case "2": e.Row.Cells[10].Text = "提交";break;
                case "3": e.Row.Cells[10].Text = "院审批通过"; break;
                case "4": e.Row.Cells[10].Text = "校审批通过"; break;
            }
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }
}