using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

/// <summary>
/// MailHelper 的摘要说明
/// </summary>
public class MailHelper
{
	public MailHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}   
    public static string SendMail(DataTable dt)
    {
        string emailString = string.Empty;
        StringBuilder sbEmail = new StringBuilder();
        sbEmail.Append("mailto:");

        sbEmail.Append(HttpUtility.UrlEncode(" ", System.Text.Encoding.Default));

        sbEmail.Append("?subject=");
        sbEmail.Append(HttpUtility.UrlEncode(" ", System.Text.Encoding.Default));
        sbEmail.Append("&body=");
        string body = "可以\t是一个\t链接, 也\t\r可以\t是具体的内\t容";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                body += dt.Rows[i][j].ToString()+"\t";
            }
            body += "\n";
        }           

        sbEmail.Append(HttpUtility.UrlEncode(body, System.Text.Encoding.Default));
        emailString = sbEmail.ToString();
        return emailString;        
    }   
}
