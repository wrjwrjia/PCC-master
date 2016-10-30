using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 库存管理_盘库 : System.Web.UI.Page
{
    static string SqlStr = null;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["SortOrder"] = "date_year";
            ViewState["OrderDire"] = "ASC";
            GridViewBind();
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.ToString());
        }
    }
    public void GridViewBind()
    {
        SqlConnection sqlCon = new SqlConnection();
        sqlCon.ConnectionString = SQLHelper.connectionString;
        string supply_person = Supplier.Text;
        string book_name = Book_Name.Text;
        string book_id = Book_ID.Text;
        string press_name = Press_Name.Text;
        SqlStr = "select id,order_id,date_year,semester,supply_person,book_id,book_name,press_name,remain_num,Storage_location from Storage_management_end where supply_person like '%" + supply_person + "%' and book_name like'%" + book_name + "%' and press_name like'%" + press_name + "%' and book_id like'%" + book_id + "%'";
        SqlDataAdapter da = new SqlDataAdapter(SqlStr, sqlCon);
        DataSet ds = new DataSet();
        da.Fill(ds, "Storage_management_end");
        DataView dv = ds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        dv.Sort = sort;
        GridView1.DataSource = dv;
        GridView1.DataBind();
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lb = this.GridView1.Rows[i].Cells[0].FindControl("Label9") as Label;
            TextBox tb = this.GridView1.Rows[i].Cells[0].FindControl("TextBox9") as TextBox;
            tb.Text = lb.Text;
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sPage = e.SortExpression;
        if (ViewState["SortOrder"].ToString() == sPage)
        {
            if (ViewState["OrderDire"].ToString() == "Desc")
                ViewState["OrderDire"] = "ASC";
            else
                ViewState["OrderDire"] = "Desc";
        }
        else
        {
            ViewState["SortOrder"] = e.SortExpression;
        }
        GridViewBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        Button1_Click(sender, e);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label lb = this.GridView1.Rows[i].Cells[0].FindControl("Label1") as Label;
                TextBox tb = this.GridView1.Rows[i].Cells[0].FindControl("TextBox9") as TextBox;
                string sql = "update Storage_management_end set real_num='" + tb.Text + "' where id='" + lb.Text + "'";
                SQLHelper.ExecuteNonQuery(sql);
            }
            GridViewBind();
            WebMessageBox.Show("修改成功！");
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message.ToString());
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
    }
}