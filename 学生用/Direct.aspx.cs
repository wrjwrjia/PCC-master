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

public partial class Direct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        object stuid = Request.QueryString["sid"];
        if (stuid == null)
        {
            Function.Debug("学号无效");
        }
        else {
            Response.Redirect("default.aspx?sid=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(stuid.ToString())).Replace("+", "%2B"));
        }
    }
}
