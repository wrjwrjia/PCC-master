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

public partial class Notice : System.Web.UI.Page 
{
    public string regularText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["ReadSta"] != null)
        {
            ReadSta.Enabled = false;
        }

        DataTable table = SQLHelper.GetDataTable("select * from param");
        if (table != null && table.Rows.Count > 0)
        {
            regularText = table.Rows[0]["RegularText"].ToString();
        }
    }
    protected void ReadSta_Click(object sender, EventArgs e)
    {
        HttpCookie aCookie = new HttpCookie("ReadSta");
        aCookie.Value = "true";
        Response.Cookies.Add(aCookie);

        Response.Redirect("Default.aspx");
    }
}
