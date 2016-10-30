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

public partial class Cancel : PageBase
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
            else
            {
                SearchBtn.Enabled = false;
            }
        }
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        decimal totalPrice = 0;
        if (SelectTerm.Items.Count > 0)
        {
            if (SelectTerm.Items[SelectTerm.SelectedIndex] != null)
            {
                try
                {
                    table = SQLHelper.GetDataTable("select * from CancelInfo where stuid = '" + stuID + "' and term = '" + SelectTerm.Items[SelectTerm.SelectedIndex].ToString() + "'");
                }
                catch
                {

                }

                if (table != null)
                {
                    HtmlTableRow row = null;
                    HtmlTableCell cell = null;
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
                        try
                        {
                            if ((decimal)table.Rows[i]["UnitPrice"] == 0)
                            {
                                cell.InnerText = "价格待定";
                            }
                            else
                            {
                                cell.InnerText = table.Rows[i]["UnitPrice"].ToString();
                            }
                        }
                        catch {
                            cell.InnerText = "价格待定";
                        }
                        row.Cells.Add(cell);
                        totalPrice += (decimal)table.Rows[i]["UnitPrice"];

                        cell = new HtmlTableCell();
                        try
                        {
                            if ((int)table.Rows[i]["Version"] != 0)
                            {
                                cell.InnerText = table.Rows[i]["Version"].ToString();
                            }
                            else {
                                cell.InnerText = "";
                            }
                        }
                        catch {
                            cell.InnerText = "";
                        }
                        row.Cells.Add(cell);

                        cell = new HtmlTableCell();
                        cell.InnerText = table.Rows[i]["InfoDate"].ToString();
                        row.Cells.Add(cell);

                        CanceledBookList.Rows.Add(row);
                    }

                    row = new HtmlTableRow();

                    cell = new HtmlTableCell();
                    cell.ColSpan = 2;
                    cell.InnerText = "总册数：";
                    cell.Align = "center";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.ColSpan = 3;
                    cell.InnerText = table.Rows.Count.ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.ColSpan = 2;
                    cell.InnerText = "总金额：";
                    cell.Align = "center";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.ColSpan = 3;
                    cell.InnerText = totalPrice.ToString();
                    row.Cells.Add(cell);

                    CanceledBookList.Rows.Add(row);
                }
            }
        }
    }
}
