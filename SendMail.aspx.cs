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

public partial class SendMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string attachmentPath = Request["path"];
        bool result = PageCommon.SendMailTo("jiaqi369@gmail.com", this.txtSendTo.Text, this.txtSubject.Text, this.hidContent.Value, "jiaqi369", "zjq369", @attachmentPath);
        if (result)
        {
            JScript.AjaxAlert(this.Page, "Send Mail Success");
            return;
        }
        else
        {
            JScript.AjaxAlert(this.Page, "Send Mail Failure");
            return;
        }
    }
}
