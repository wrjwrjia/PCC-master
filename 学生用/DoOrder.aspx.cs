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

public partial class DoOrder : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string courseID = "", courseName = "", kclb = "";
        if (Request.QueryString["CID"] == null)
        {
            Function.Debug("课程编号不能为空");
        }
        else
        {
            courseID = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["CID"].ToString().Trim().Replace("%2B", "+")));
        }

        table = SQLHelper.GetDataTable("select distinct subkclb from v_CourseList where stuid = '" + stuID + "' and courseID = '" + courseID + "'");

        if (table != null && table.Rows.Count != 0)
        {
            if (table.Rows[0]["subkclb"].ToString().Trim() == "")
            {
                Function.Debug("课程类别无效");
            }
            else
            {
                kclb = table.Rows[0]["subkclb"].ToString().Trim();
            }
        }
        else
        {
            Function.Debug("课程类别无效");
        }

        if (orderKind != additionalOrderKind)
        {
            if (orderKind == GeneralOrderKind)
            {
                if (kclb != System.Configuration.ConfigurationManager.AppSettings["gxklb"].ToString())
                {
                    Function.Debug("现在是公选课教材订购阶段，非公选课教材暂停订购");
                }
            }
            else
            {
                if (kclb == System.Configuration.ConfigurationManager.AppSettings["gxklb"].ToString())
                {
                    Function.Debug("现在是非公选课教材订购阶段，公选课教材暂停订购");
                }
            }
        }
        string sqlQuery = "select top 1 * from v_courseList where courseID = '" + courseID + "'";

        table = SQLHelper.GetDataTable(sqlQuery);

        if (table != null || table.Rows.Count > 0)
        {
            courseName = table.Rows[0]["courseName"].ToString();
        }

        Title.Text = "课程名称：《" + courseName + "》推荐教材如下:";
        /////////////////////////////////////////////////////////
        ///////////////////////////////////////////////
        ///////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        sqlQuery = "select * from Rbook_message where term = '" + term + "' and course_id = '" + courseID + "'";
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
                    cell.InnerText = table.Rows[i]["book_id"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["book_name"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["book_editor"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["press_name"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["press_date"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    try
                    {
                        if ((int)table.Rows[i]["press_time"] != 0)
                        {
                            cell.InnerText = table.Rows[i]["press_time"].ToString();
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
                    if ((decimal)table.Rows[i]["book_price"] == 0)
                    {
                        cell.InnerText = "价格待定";
                    }
                    else
                    {
                        cell.InnerText = table.Rows[i]["book_price"].ToString();
                    }
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["teacher_name"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["remark"].ToString();
                    row.Cells.Add(cell);

                    RecommendBookList.Rows.Add(row);
                }

                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.ColSpan = 10;
                cell.Align = "middle";
                cell.InnerHtml = "<input type='submit' value='订购选定教材'>";
                row.Cells.Add(cell);
                RecommendBookList.Rows.Add(row);
            }
        }
            ///////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////
            if (table.Rows.Count == 0)
            {
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
                            cell.InnerText = table.Rows[i]["BookID"].ToString();
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
                            cell.InnerText = table.Rows[i]["PublishDate"].ToString();
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
                            cell.InnerText = table.Rows[i]["teacName"].ToString();
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = table.Rows[i]["remark"].ToString();
                            row.Cells.Add(cell);

                            RecommendBookList.Rows.Add(row);
                        }

                        row = new HtmlTableRow();
                        cell = new HtmlTableCell();
                        cell.ColSpan = 10;
                        cell.Align = "middle";
                        cell.InnerHtml = "<input type='submit' value='订购选定教材'>";
                        row.Cells.Add(cell);
                        RecommendBookList.Rows.Add(row);
                    }
                    else
                    {
                        row = new HtmlTableRow();
                        cell = new HtmlTableCell();
                        cell.ColSpan = 10;
                        cell.InnerText = "该课程当前没有可推荐的教材";
                        row.Cells.Add(cell);
                        RecommendBookList.Rows.Add(row);
                    }
                }
                else
                {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.ColSpan = 10;
                    cell.InnerText = "系统异常，请和管理员联系";
                    row.Cells.Add(cell);
                    RecommendBookList.Rows.Add(row);
                }
            }
        }
}
