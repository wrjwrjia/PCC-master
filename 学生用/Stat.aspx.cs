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

public partial class Stat : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            table = SQLHelper.GetDataTable("select distinct term from recommend ");
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        SelectTerm.Items.Add(new ListItem(table.Rows[i][0].ToString()));
                    }
                }
                else
                {
                    SearchBtn.Enabled = false;
                }
            }
            else {
                SearchBtn.Enabled = false;
            }

            OrderType.Items.Add(new ListItem("全部", ""));
            OrderType.Items.Add(new ListItem("非公选课","0"));
            OrderType.Items.Add(new ListItem("公选课", "1"));
            OrderType.Items.Add(new ListItem("补订", "2"));
        }
    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        if (SelectTerm.Items[SelectTerm.SelectedIndex] != null)
        {
            try
            {
                string sqlQuery = "select className,bookName,CourseID,CourseName,Author,TeacName,Term,Publish,orderKind,count(*) num,unitprice from Abook_message_stu where term = '" + SelectTerm.Items[SelectTerm.SelectedIndex].ToString() + "' and className = '" + className + "'";
                ListItem item = OrderType.Items[OrderType.SelectedIndex];
                if (item.Value != "")
                {
                    sqlQuery += " and orderKind = " + item.Value;
                }
                sqlQuery += " group by className,bookName,CourseID,CourseName,Author,TeacName,Term,Publish,OrderKind,unitprice";
                table = SQLHelper.GetDataTable(sqlQuery);
            }
            catch
            {

            }

            if (table != null)
            {
                HtmlTableRow row = null;
                HtmlTableCell cell = null;
                decimal totalPrice = 0,total = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = Convert.ToString(i + 1);
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["CourseID"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["CourseName"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["TeacName"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["BookName"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["Author"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["Publish"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (table.Rows[i]["OrderKind"].ToString() == GeneralOrderKind)
                    {
                        cell.InnerText = "公选";
                    }
                    else if (table.Rows[i]["OrderKind"].ToString() == additionalOrderKind)
                    {
                        cell.InnerText = "补订";
                    }
                    else
                    {
                        cell.InnerText = "非公选";
                    }
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerHtml = table.Rows[i]["num"].ToString() + " <a href=\"detail.aspx?id=" + Server.UrlEncode(table.Rows[i]["CourseID"].ToString()) + "&kid=" + table.Rows[i]["OrderKind"].ToString() + "&n=" + Server.UrlEncode(table.Rows[i]["BookName"].ToString()) + "\">详细</a>";
                    total += (int)table.Rows[i]["num"];
                    row.Cells.Add(cell);

                    totalPrice += (decimal)table.Rows[i]["UnitPrice"] * (int)table.Rows[i]["num"];

                    StatedBookList.Rows.Add(row);
                }

                row = new HtmlTableRow();

                cell = new HtmlTableCell();
                cell.ColSpan = 2;
                cell.InnerText = "总册数：";
                cell.Align = "center";
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.ColSpan = 3;
                cell.InnerText = total .ToString();
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.ColSpan = 2;
                cell.InnerText = "总金额：";
                cell.Align = "center";
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.ColSpan = 4;
                cell.InnerText = totalPrice.ToString();
                row.Cells.Add(cell);

                StatedBookList.Rows.Add(row);
            }
        }
    }
}
