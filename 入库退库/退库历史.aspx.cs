using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 库存管理_退库历史 : System.Web.UI.Page
{
    static string SqlStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["SortOrder"] = "date_year";
            ViewState["OrderDire"] = "ASC";
            GridViewBind();
            if (GridView1.Rows.Count != 0)
            {
                Button2.Visible = true;
                //Button3.Visible = true;
            }
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
        System.Data.SqlClient.SqlConnection sqlCon = new SqlConnection();
        sqlCon.ConnectionString = SQLHelper.connectionString;
        string supply_person = TextBox1.Text;
        string book_name = TextBox2.Text;
        //string return_date = TextBox3.Text;
        SqlStr = "select date_year,semester,supply_person,supplier_phone,order_id,book_id,book_price,book_name,order_discount,press_name,Storage_location,remain_num,return_num,return_date from return_history where supply_person like '%" + supply_person + "%' and book_name like'%" + book_name + "%'";
        SqlDataAdapter da = new SqlDataAdapter(SqlStr, sqlCon);
        DataSet ds = new DataSet();
        da.Fill(ds, "return_history");
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
        GridViewBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = "退库历史";
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
            WebMessageBox.Show(ex.Message);
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