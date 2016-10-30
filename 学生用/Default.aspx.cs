using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : PageBase 
{
    HtmlTableRow row;
    HtmlTableCell cell;
    protected void Page_Load(object sender, EventArgs e)
    {
        StartDate.Text = startDate;
        EndDate.Text = endDate;
        CurDate.Text = DateTime.Now.ToString();
        StuGrade.Text = stuGrade;
        LimitGrade.Text = limitGrade;

        if (!isOpen)
        {
            OpenSta.Text = "系统已关闭";
        }       
        else {
            if(orderKind == "0")
                OpenSta.Text = "系统开放,现在是非公选课教材订购阶段,只能订购非公选课教材";
            else if(orderKind == "1")
                OpenSta.Text = "系统开放,现在是公选课教材订购阶段，只能订购公选课教材";
            else
                OpenSta.Text = "系统开放,现在是教材补订阶段，只能进行教材的补订";
        }

        StuID.Text = stuID;
        StuName.Text = stuName;
        ClassName.Text = className;
        //Abook_message_stu中存放学生订书信息//
        table = SQLHelper.GetDataTable("select * from Abook_message_stu where term = '" + term + "' and stuID = '"+stuID+"'");
        if (table != null)
        {
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = Convert.ToString(i + 1);
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["courseID"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["courseName"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (table.Rows[i]["teacName"].ToString() == "")
                    {
                        cell.InnerText = "教师待定";
                    }
                    else
                    {
                        cell.InnerText = table.Rows[i]["teacName"].ToString();
                    }
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["BookName"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["author"].ToString();
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
                    else {
                        cell.InnerText = "非公选";
                    }
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (isOpen)
                    {
                        if (orderKind == additionalOrderKind)
                        {
                            if (table.Rows[i]["OrderKind"].ToString() == orderKind)
                            {
                                cell.InnerHtml = "<a href='DoCancel.aspx?id=" + table.Rows[i]["ID"].ToString() + "' onclick=\"return confirm('您确定要退订该教材吗?')\">退订</a>";
                            }
                            else
                            {
                                cell.InnerHtml = "<a href=\"javascript:onclick=alert('补订阶段不可退订前期订购教材！');\">退订</a>";
                            }
                        }
                        else if (table.Rows[i]["OrderKind"].ToString() == orderKind)
                        {
                            cell.InnerHtml = "<a href='DoCancel.aspx?id=" + table.Rows[i]["ID"].ToString() + "' onclick=\"return confirm('您确定要退订该教材吗?')\">退订</a>";
                        }
                        else {
                            cell.InnerHtml = "<a href=\"javascript:onclick=alert('"+OpenSta.Text+"');\">退订</a>";
                        }
                    }
                    else {
                        cell.InnerHtml = "<a href=\"javascript:onclick=alert('系统已关闭，不得退订');\">退订</a>";
                    }
                    row.Cells.Add(cell);
                    SelectedBookList.Rows.Add(row);
                }
            }
            else {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.ColSpan = 7;
                cell.InnerText = "您当前没有选定教材记录";
                row.Cells.Add(cell);
                SelectedBookList.Rows.Add(row);
            }
        }
        //数据表v_courselist存放的是学生选择课程信息
        table = SQLHelper.GetDataTable("select * from v_courselist where stuID = '" + stuID + "' and term = '"+term+"'");
        if (table != null)
        {
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = Convert.ToString(i + 1);
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["courseID"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = table.Rows[i]["courseName"].ToString();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (table.Rows[i]["teacName"].ToString() == "")
                    {
                        cell.InnerText = "教师待定";
                    }
                    else
                    {
                        cell.InnerText = table.Rows[i]["teacName"].ToString();
                    }
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (table.Rows[i]["SubKclb"].ToString().Trim() == System.Configuration.ConfigurationManager.AppSettings["gxklb"].ToString())
                    {
                        cell.InnerText = "公选";
                    }
                    else if (table.Rows[i]["SubKclb"].ToString().Trim() == "")
                    {
                        cell.InnerText = "";
                    }
                    else
                    {
                        cell.InnerText = "非公选";
                    }
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (isOpen)
                    {
                        if (table.Rows[i]["subkclb"].ToString().Trim() == "")
                        {
                            cell.InnerHtml = "<a href=\"javascript:onclick=alert('课程类型无效');\">订购教材</a>";
                        }
                        else
                        {
                            if (orderKind == additionalOrderKind)
                            {
                                cell.InnerHtml = "<a href=\"DoOrder.aspx?cid=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(table.Rows[i]["courseID"].ToString().Trim())).Replace("+", "%2B") + "\">订购教材</a>";
                            }
                            else
                            {
                                if (orderKind == GeneralOrderKind)
                                {
                                    if (table.Rows[i]["subkclb"].ToString().Trim() == System.Configuration.ConfigurationManager.AppSettings["gxklb"].ToString())
                                    {
                                        cell.InnerHtml = "<a href=\"DoOrder.aspx?cid=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(table.Rows[i]["courseID"].ToString().Trim())).Replace("+", "%2B") + "\">订购教材</a>";
                                    }
                                    else {
                                        cell.InnerHtml = "<a href=\"javascript:onclick=alert('" + OpenSta.Text + "');\">订购教材</a>";
                                    }
                                }
                                else if (orderKind == UnGeneralOrderKind)
                                {
                                    if (table.Rows[i]["subkclb"].ToString().Trim() != System.Configuration.ConfigurationManager.AppSettings["gxklb"].ToString())
                                    {
                                        cell.InnerHtml = "<a href=\"DoOrder.aspx?cid=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(table.Rows[i]["courseID"].ToString().Trim())).Replace("+", "%2B") + "\">订购教材</a>";
                                    }
                                    else
                                    {
                                        cell.InnerHtml = "<a href=\"javascript:onclick=alert('" + OpenSta.Text + "');\">订购教材</a>";
                                    }
                                }
                                else {
                                    cell.InnerHtml = "<a href=\"javascript:onclick=alert('数据异常');\">订购教材</a>";
                                }
                            }
                        }
                    }
                    else {
                        cell.InnerHtml = "<a href=\"javascript:onclick=alert('系统已关闭，不得选订教材');\">订购教材</a>";
                    }
                    row.Cells.Add(cell);
                    SelectedCourseList.Rows.Add(row);
                }
            }
            else
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.ColSpan = 4;
                cell.InnerText = "没有找到您当前的选课记录";
                row.Cells.Add(cell);
                SelectedCourseList.Rows.Add(row);
            }
        }
    }
}
