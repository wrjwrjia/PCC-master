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

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblStackTrace.Text = this.Application["StackTrace"] as string;
        this.lblMessageError.Text = this.Application["MessageError"] as string;
        this.lblSourceError.Text = this.Application["SourceError"] as string;
        this.lblTagetSiteError.Text = this.Application["TargetSite"] as string;
    }
}
