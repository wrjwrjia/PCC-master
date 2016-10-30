using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// PageBase 的摘要说明
/// </summary>
public class PageBase : System.Web.UI.Page 
{
    protected string startDate,endDate,term,orderKind,limitGrade;
    protected DataTable table,table2;
    protected string stuID, stuName, className,stuGrade,campusName,DeptName;
    protected bool isOpen = true;
    protected string additionalOrderKind = "2";//指定默认补订教材订购类型
    protected string GeneralOrderKind = "1";   //指定默认公选课教材订购类型
    protected string UnGeneralOrderKind = "0";//指定默认非公选课教材订购类型

	public PageBase()
	{
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        HttpContext.Current.Request.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); 

        table = SQLHelper.GetDataTable("select * from param");
        if (table != null)
        {
            startDate = table.Rows[0]["StartDate"].ToString();
            endDate = table.Rows[0]["EndDate"].ToString();
            term = table.Rows[0]["Term"].ToString();
            orderKind = table.Rows[0]["orderKind"].ToString();
            limitGrade = table.Rows[0]["limitGrade"].ToString();
            try
            {
                if (DateTime.Parse(startDate) > DateTime.Now || DateTime.Parse(endDate) < DateTime.Now)
                {
                    isOpen = true;
                }
                else
                {
                    isOpen = true;
                }
            }
            catch
            {
                isOpen = true;
            }
        }
        else
        {
            Function.Debug("系统参数尚未设置，请设置");
        }

        if (HttpContext.Current.Request.Cookies["StuInfo"] != null)
        {
            HttpCookie aCookie = HttpContext.Current.Request.Cookies["StuInfo"];
            //stuID = HttpContext.Current.Request.Cookies["user"].Values["name"];
            //table2 = SQLHelper.GetDataTable("select * from student where stuID='" + stuID + "'");
            //if (table2 != null)
            //{
            //    stuName =table2.Rows[0]["stuName"].ToString();
            //    className = table2.Rows[0]["className"].ToString();
            //    stuGrade = table2.Rows[0]["stuGrade"].ToString();
            //    DeptName = table2.Rows[0]["DeptName"].ToString();
            //    campusName = table2.Rows[0]["campusName"].ToString();
            //}
            stuID = HttpUtility.UrlDecode(aCookie.Values["stuID"].ToString(), System.Text.Encoding.Default);
            stuName = HttpUtility.UrlDecode(aCookie.Values["stuName"].ToString(), System.Text.Encoding.Default);
            className = HttpUtility.UrlDecode(aCookie.Values["className"].ToString(), System.Text.Encoding.Default);
            stuGrade = HttpUtility.UrlDecode(aCookie.Values["stuGrade"].ToString(), System.Text.Encoding.Default);
            DeptName = HttpUtility.UrlDecode(aCookie.Values["DeptName"].ToString(), System.Text.Encoding.Default);
            campusName = HttpUtility.UrlDecode(aCookie.Values["campusName"].ToString(), System.Text.Encoding.Default);
            //强制用户必须阅读教材订购说明
            if (HttpContext.Current.Request.Cookies["ReadSta"] == null)
            {
                HttpContext.Current.Response.Redirect("Notice.aspx");
            }
        }
        else
        {
            ///学号信息请使用Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(a)).Replace("+", "%2B");加密
            stuID = HttpContext.Current.Request.QueryString["sid"];
            try
            {
                stuID = System.Text.Encoding.Default.GetString(Convert.FromBase64String(stuID.Replace("%2B", "+")));
            }
            catch
            {
                stuID = "";
            }
        }

        if (stuID == "" || stuID == null)
        {
            Function.Debug("用户身份认证失败，请重新登录");
        }
        else {
            if (HttpContext.Current.Request.Cookies["StuInfo"] == null)
            {
                HttpCookie aCookie = new HttpCookie("StuInfo");
                stuID = HttpContext.Current.Request.Cookies["user"].Values["name"];
                table2 = SQLHelper.GetDataTable("select * from student where stuid = '" + stuID + "'");
                if (table != null && table.Rows.Count > 0)
                {
                    stuID = table2.Rows[0]["stuID"].ToString();
                    stuName = table2.Rows[0]["stuName"].ToString();
                    className = table2.Rows[0]["className"].ToString();
                    stuGrade = table2.Rows[0]["grade"].ToString();
                    aCookie["stuID"] = HttpUtility.UrlEncode(stuID, System.Text.Encoding.Default);
                    aCookie["stuName"] = HttpUtility.UrlEncode(stuName, System.Text.Encoding.Default);
                    aCookie["className"] = HttpUtility.UrlEncode(className, System.Text.Encoding.Default);
                    aCookie["stuGrade"] = HttpUtility.UrlEncode(stuGrade, System.Text.Encoding.Default);

                    HttpContext.Current.Response.Cookies.Add(aCookie);

                    //强制用户必须阅读教材订购说明
                    if (HttpContext.Current.Request.Cookies["ReadSta"] == null)
                    {
                        HttpContext.Current.Response.Redirect("Notice.aspx");
                    }
                }
                else
                {
                    Function.Debug("学号无效");
                }
            }
        }

        if (limitGrade.IndexOf(stuGrade) == -1)
        {
            isOpen = true;
        }
	}

    protected override void OnInit(EventArgs e)
    {
        //此方法将减少用户受到一次单击攻击的几率
        base.OnInit(e);
    }
}
