using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 库存管理_入库历史 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //End_Time.Attributes.Add("onfocus", "javascript:myclick()");
        }
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        Button1_Click(sender, e);
    }
    public void GridViewBind()
    {
        SqlConnection sqlCon = new SqlConnection();
        sqlCon.ConnectionString = SQLHelper.connectionString;
        string book_name, press_name, start_time = "", end_time = "";
        string SqlStr = "";
        //DateTime d1,d2;
        book_name = TextBox1.Text;
        press_name = TextBox2.Text;
        if (Start_Time.Text != null)
        {
            start_time = Start_Time.Text.ToString();
        }
        if (End_Time.Text != null)
        {
            end_time = End_Time.Text.ToString();
        }
        //d1 = Convert.ToDateTime(start_time);
        //d2 = Convert.ToDateTime(end_time);
        if (start_time != "" && end_time != "")
        {
            SqlStr = "select date_year,semester,order_id,book_id,book_name,press_name,book_price,arrived_discount,arrived_date,arrival_amount from Storage_management where book_name like '%" + book_name + "%' and press_name like '%" + press_name + "%'and book_editor like '%" + Book_Editor.Text + "%' and arrived_date>='" + start_time + "' and arrived_date<='" + end_time + "'";
        }
        else if (start_time != ""&&end_time=="")
        {
            SqlStr = "select date_year,semester,order_id,book_id,book_name,press_name,book_price,arrived_discount,arrived_date,arrival_amount from Storage_management where book_name like '%" + book_name + "%' and press_name like '%" + press_name + "%'and book_editor like '%" + Book_Editor.Text + "%' and arrived_date>='" + start_time + "'";
        }
        else if(start_time==""&&end_time!="")
        {
            SqlStr = "select date_year,semester,order_id,book_id,book_name,press_name,book_price,arrived_discount,arrived_date,arrival_amount from Storage_management where book_name like '%" + book_name + "%' and press_name like '%" + press_name + "%'and book_editor like '%" + Book_Editor.Text + "%' and arrived_date<='" + end_time + "'";
        }
        else if (start_time == "" && end_time == "")
        {
            SqlStr = "select date_year,semester,order_id,book_id,book_name,press_name,book_price,arrived_discount,arrived_date,arrival_amount from Storage_management where book_name like '%" + book_name + "%' and press_name like '%" + press_name + "%'and book_editor like '%" + Book_Editor.Text + "%'";
        }
        SqlDataAdapter da = new SqlDataAdapter(SqlStr, sqlCon);
        DataSet ds = new DataSet();
        da.Fill(ds, "Storage_management");
        DataView dv = ds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        dv.Sort = sort;
        GridView1.DataSource = dv;
        GridView1.DataBind();

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
        // GridView1.Focus();
        GridViewBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {
            //DataGrid2Excel(GridView1);
            string FileName = "入库历史";
            // PrepareGridViewForExport(MachineList);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView1.AllowSorting = false;
            GridView1.AllowPaging = false;
            GridViewBind();
            GridView1.RenderControl(htmlWrite);
            //Response.Write("<style> .text { vnd.ms-excel.numberformat:@; } .cndate {vnd.ms-excel.numberformat:yyyy年m月d日} </style>");
            HttpContext.Current.Response.Write(stringWrite.ToString());
            HttpContext.Current.Response.End();
            GridView1.AllowSorting = true;
            GridView1.AllowPaging = true;
            GridViewBind();
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm. control is rendered for
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