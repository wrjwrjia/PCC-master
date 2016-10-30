using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

/// <summary>
/// PageValidate 的摘要说明
/// </summary>
public class PageValidate
{
    public PageValidate()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static bool isDateTime(string date)
    {
        try
        {
            Convert.ToDateTime(date);
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;

    }
    //public static bool isNumber(string num)
    //{
    //    try
    //    {
    //        Convert.ToInt32(num);
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //    return true;
    //}
    public static bool isEmail(string mailStr)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(mailStr))
            return true;
        else
            return false;
    }

    public static bool isNumber(string str)
    {
        System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");

        if (reg1.IsMatch(str))
        {
            //是数字
            return true;
        }
        else
        {
            //非数字
            return false;
        }
    }
}
