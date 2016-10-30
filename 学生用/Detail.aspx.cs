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

public partial class Detail : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || Request.QueryString["n"] == null)
        {
            Function.Debug("对不起，操作不合法，操作终止");
        }

        string courseID = Request.QueryString["id"].ToString();
        string bookName = Server.UrlDecode(Request.QueryString["n"].ToString());
        string orderKind = Server.UrlDecode(Request.QueryString["kid"].ToString());

        HtmlTableRow row;
        HtmlTableCell cell;
        table = SQLHelper.GetDataTable("select * from Abook_message_stu where term = '" + term + "' and classname = '" + className + "' and courseID = '" + courseID + "' and bookName = '" + bookName + "' and OrderKind = " + orderKind + " order by stuid");
        if (table == null || table.Rows.Count == 0)
        {
            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            cell.ColSpan = 8;
            cell.InnerText = "该课程当前没有教材订购记录";
            row.Cells.Add(cell);
            DetailBookList.Rows.Add(row);
        }
        else {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.InnerText = Convert.ToString(i + 1);
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = table.Rows[i]["stuID"].ToString();
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = table.Rows[i]["stuName"].ToString();
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = table.Rows[i]["courseID"].ToString();
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = table.Rows[i]["courseName"].ToString();
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = table.Rows[i]["teacName"].ToString();
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = table.Rows[i]["bookName"].ToString();
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = table.Rows[i]["author"].ToString();
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = table.Rows[i]["term"].ToString();
                row.Cells.Add(cell);

                DetailBookList.Rows.Add(row);
            }
        }
    }
}
