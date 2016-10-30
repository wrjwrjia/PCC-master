using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 库存管理_查询学生 : System.Web.UI.Page
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
        string si = TbStuId.Text;
        string cn = TbClassName.Text;
        string bi = TbBookId.Text;
        string bn = TbBookName.Text;
        this.WebPager1.SqlField = " id,StuID,StuName,ClassName,BookID,BookName,Author,Publish,get_id ";
        this.WebPager1.TableName = " Abook_message_stu ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
            + "and StuID like '%" + si + "%' "
            + "and ClassName like '%" + cn + "%' "
            + "and BookID like '%" + bi + "%' "
            + "and BookName like '%" + bn + "%' ";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
    }


    //鼠标经过背景色改变
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