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

public partial class Loginout : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["StuInfo"];
        cookie.Expires = DateTime.Now.AddDays(-5);
        Response.Cookies.Add(cookie);

        Response.Write("<script>window.opener = null;window.close();</script>");
    }
}
