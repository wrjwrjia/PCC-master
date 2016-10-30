using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 库存管理_查询教师 : System.Web.UI.Page
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
                this.WebPager1.WhereClause = " and 2<1 ";
            }
        }
    }

    private void initData()
    {
        string ti = TbTeacherId.Text;
        string tn = TbTeacherName.Text;
        string bi = TbBookId.Text;
        string bn = TbBookName.Text;
        this.WebPager1.SqlField = " * ";
        this.WebPager1.TableName = " Abook_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
            +"and teacher_id like '%" + ti + "%' "
            +"and teacher_name like '%" + tn + "%' "
            +"and book_id like '%" + bn + "%' "
            +"and book_name like '%" + bi + "%' ";
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
    }


    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            string get_id = e.Row.Cells[8].Text;
            if (get_id== "0")
            {
                e.Row.Cells[8].Text = "未领";
                e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
            }
            if (get_id == "1")
            {
                e.Row.Cells[8].Text = "已领";
            }
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }
}