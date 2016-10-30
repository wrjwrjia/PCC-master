using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class SystemInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Object.Equals(Request.Cookies["user"], null))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                this.Label1.Text = Request.Cookies["user"].Values["name"];
                intiData();
            }
        }
    }
    private void intiData()
    {
//        try
//        {            
//            this.lblLatestRMA.Text = "&nbsp;&nbsp;" + (string)SQLHelper.ExecuteScalar("select top 1 rma_no from rma_list  order by rma_issue_time desc,id desc ");
//            //VLRR
//            string[] customerName = { "Dell", "Toshiba", "HP", "Acer", "Lenovo", "Asus" };
//            string[] productCategory ={ "F3507g", "F3607gw", "F3307", "F5521gw" };
//            string[,] calculateResult = new string[4, 6];
//            DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
//            DateTime dtEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));         
//            for (int i = 0; i < productCategory.Length; i++)
//            {
//                for (int j = 0; j < customerName.Length; j++)
//                {
//                    calculateResult[i, j] = Report.CalculateVLRR(" and customer_name = '" + customerName[j] + "' and ericsson_pn in(select distinct(typeno) from product_category where category = '" + productCategory[i] + "') ", dtStart, dtEnd);
//                }
//            }
//            //将计算结果显示结果显示在 Literal 控件中
//            this.LiteralVID.Text = "<table style='font-family:Arial;color:DarkBlue'>";
//            for (int i = 0; i < productCategory.Length; i++)
//            {
//                this.LiteralVID.Text += "<tr>";
//                this.LiteralVID.Text += "<td Width='70px'>";
//                this.LiteralVID.Text += productCategory[i];
//                this.LiteralVID.Text += "</td>";
//                for (int j = 0; j < customerName.Length; j++)
//                {
//                    this.LiteralVID.Text += "<td Width='100px'>";
//                    this.LiteralVID.Text += customerName[j] + ":&nbsp;&nbsp;" + calculateResult[i, j];
//                    this.LiteralVID.Text += "</td>";
//                }
//                this.LiteralVID.Text += "</tr>";
//            }
//            this.LiteralVID.Text += "</table>";

//            DataTable dt = SQLHelper.GetDataTable(@"select top 1 sum(qty) as qty, datename(week, delivery_date),max(delivery_date) as delivery_date
//                                        from delivery_info 
//                                        where year(delivery_date) = year(getdate())                                          
//                                        group by datename(week, delivery_date)
//                                        order by delivery_date desc");
//            if (dt.Rows.Count > 0)
//                this.lblDeliveryQty.Text = "&nbsp;&nbsp;" + dt.Rows[0]["qty"].ToString();
//            DataTable dtContent = SQLHelper.GetDataTable("select top 20  hot_content from tbl_hot_issue order by id desc");
//            if (dtContent.Rows.Count > 0)
//            {
//                this.LiteralHotIssue.Text += "<font size = 2><marquee height=200 direction='up' scrolldelay=800 onMouseOut='this.start()' onMouseOver='this.stop()' loop = -1>";
//                for (int i = 0; i < dtContent.Rows.Count; i++)
//                {                    
//                    this.LiteralHotIssue.Text += "&nbsp;" + dtContent.Rows[i]["hot_content"].ToString();                    
//                    this.LiteralHotIssue.Text += "<br/>";
//                }
//                this.LiteralHotIssue.Text += "</marquee></font>";
//            }

//            DataTable dtProcessLink = SQLHelper.GetDataTable("select process_link_name,process_link_content from process_link order by id");            

//            int ProcessLinkNum = dtProcessLink.Rows.Count;
//            int columnCount = 2;
//            int rowCount = ProcessLinkNum / columnCount;
//            int totalCount = 0;
//            if (ProcessLinkNum % columnCount != 0)
//            {
//                rowCount++;
//            }
//            this.LiteralProcessLink.Text = "<table style='font-family:Arial;color:DarkBlue'>";
//            for (int i = 0; i < rowCount; i++)
//            {
//                this.LiteralProcessLink.Text += "<tr>";
//                for (int j = 0; j < columnCount && totalCount < ProcessLinkNum; j++)
//                {
//                    this.LiteralProcessLink.Text += "<td Width='400px'><a href=" + dtProcessLink.Rows[totalCount]["process_link_content"] + " target='_blank' >";
//                    this.LiteralProcessLink.Text += dtProcessLink.Rows[totalCount]["process_link_name"];
//                    this.LiteralProcessLink.Text += "</a></td>";
//                    totalCount++;
//                }
//                this.LiteralProcessLink.Text += "</tr>";
//            }
//            this.LiteralProcessLink.Text += "</table>";

//            //Contact Person
//            string ContactPersonEMail = (string)SQLHelper.ExecuteScalar("select type_item from dict_item  where type_name = 'MainPageMail'");
//            this.lblEMail.Text = "<a href='mailto:" + ContactPersonEMail + "'>" + ContactPersonEMail + "</a>";
//            this.lblTelephone.Text = (string)SQLHelper.ExecuteScalar("select type_item from dict_item  where type_name = 'MainPageTelephone'");      
//        }
//        catch(Exception){}
    } 
}
