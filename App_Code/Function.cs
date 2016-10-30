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
/// 公共函数方法
/// </summary>
public class Function
{
	public Function()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static void Debug(string message)
    {
        System.Web.HttpContext.Current.Response.Write(message);
        System.Web.HttpContext.Current.Response.End();
    }
}
