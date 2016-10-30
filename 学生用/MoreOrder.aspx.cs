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

public partial class MoreOrder : PageBase
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
        string courseID = "", courseName = "", kclb = "";
        if (CourseID.Text .Trim() == "")
        {
            WebMessageBox.Show("课程编号不能为空！");
        }
        else
        {
            courseID = CourseID.Text.Trim();
        }

        table = SQLHelper.GetDataTable("select distinct subkclb from v_CourseList where courseID = '" + courseID + "'");

        if (table != null && table.Rows.Count != 0)
        {
            if (table.Rows[0]["subkclb"].ToString().Trim() == "")
            {
               // Function.Debug("课程类别无效");
                WebMessageBox.Show("课程类别无效");
            }
            else
            {
                kclb = table.Rows[0]["subkclb"].ToString().Trim();
            }
        }
        else
        {
           // Function.Debug("课程类别无效");
            WebMessageBox.Show("课程类别无效");
        }

        string sqlQuery = "select top 1 * from v_courseList where courseID = '" + courseID + "'";

        table = SQLHelper.GetDataTable(sqlQuery);

        if (table != null || table.Rows.Count > 0)
        {
            courseName = table.Rows[0]["courseName"].ToString();
        }

        Title.Text = "课程名称：《" + courseName + "》推荐教材如下:";

        sqlQuery = "select * from Recommend where term = '" + term + "' and courseID = '" + courseID + "'";
        if (orderKind != additionalOrderKind)
        {
            sqlQuery += " and OrderKind='" + orderKind + "'";
        }

        try
        {
            table = SQLHelper.GetDataTable(sqlQuery);
        }
        catch
        {
            table = null;
        }

        HtmlTableRow row;
        HtmlTableCell cell;
        if (table != null)
        {
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    if (orderKind != additionalOrderKind)
                    {
                        cell.InnerHtml = "<input type=checkbox name='list' value='" + table.Rows[i]["ID"].ToString() + "'>";
                    }
                    else
                    {
                        if (table.Rows[i]["OrderKind"].ToString() == GeneralOrderKind)
                        {
                            if (kclb == System.Configuration.ConfigurationManager.AppSettings["gxklb"].ToString())
                            {
                                cell.InnerHtml = "<input type=checkbox name='list' value='" + table.Rows[i]["ID"].ToString() + "'>";
                            }
                            else
                            {
                                cell.InnerText = "";
                            }
                        }
                        else
                        {
                            if (kclb != System.Configuration.ConfigurationManager.AppSettings["gxklb"].ToString())
                            {
                                cell.InnerHtml = "<input type=checkbox name='list' value='" + table.Rows[i]["ID"].ToString() + "'>";
                            }
                            else
                            {
                                cell.InnerText = "";
                            }
                        }
                    }
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
                    cell.InnerText = table.Rows[i]["publish"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if ((decimal)table.Rows[i]["UnitPrice"] == 0)
                    {
                        cell.InnerText = "价格待定";
                    }
                    else
                    {
                        cell.InnerText = table.Rows[i]["UnitPrice"].ToString();
                    }
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    try
                    {
                        if ((int)table.Rows[i]["Version"] != 0)
                        {
                            cell.InnerText = table.Rows[i]["Version"].ToString();
                        }
                        else
                        {
                            cell.InnerText = "";
                        }
                    }
                    catch
                    {
                        cell.InnerText = "";
                    }
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["PublishDate"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (orderKind != additionalOrderKind)
                    {
                        cell.InnerText = table.Rows[i]["Remark"].ToString();
                    }
                    else
                    {
                        if (table.Rows[i]["OrderKind"].ToString() == GeneralOrderKind)
                        {
                            if (kclb == System.Configuration.ConfigurationManager.AppSettings["gxklb"].ToString())
                            {
                                cell.InnerText = table.Rows[i]["Remark"].ToString();
                            }
                            else
                            {
                                cell.InnerHtml = "<font style='color:red;font-weight:bold;font-size:7pt;'>该教材为非公选课教材</font>";
                            }
                        }
                        else
                        {
                            if (kclb != System.Configuration.ConfigurationManager.AppSettings["gxklb"].ToString())
                            {
                                cell.InnerText = table.Rows[i]["Remark"].ToString();
                            }
                            else
                            {
                                cell.InnerHtml = "<font style='color:red;font-weight:bold;font-size:7pt;'>该教材为公选课教材</font>";
                            }
                        }
                    }
                    row.Cells.Add(cell);

                    MoreBookList.Rows.Add(row);
                }

                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.ColSpan = 10;
                cell.Align = "middle";
                cell.InnerHtml = "<input type='submit' value='订购选定教材'>";
                row.Cells.Add(cell);
                MoreBookList.Rows.Add(row);
            }
            else
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.ColSpan = 10;
                cell.InnerText = "该课程当前没有可推荐的教材";
                row.Cells.Add(cell);
                MoreBookList.Rows.Add(row);
            }
        }
        else
        {
            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            cell.ColSpan = 10;
            cell.InnerText = "系统异常，请和管理员联系";
            row.Cells.Add(cell);
            MoreBookList.Rows.Add(row);
        }
    }
}
