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

public partial class System_AddRole : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            if (Object.Equals(Request.Cookies["user"], null))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                this.lblPath.Text = "Add Role Name ";
                if (!string.IsNullOrEmpty(Request["rid"]))
                {
                    InitInfo();
                    this.hidID.Value = Request["rid"];
                }
            }
        }
    }
    private void InitInfo()
    {
        this.lblPath.Text = "Edit Role Name ";
        string roleid = Request["rid"].ToString();
        txtRole.Text = SQLHelper.ExecuteScalar("select role_na from tbl_role where id = " + roleid).ToString();
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtRole.Text.Trim() == "")
        {
            JScript.AjaxAlert(this.Page, "Please input role name!");
            return;
        }

        if (string.IsNullOrEmpty(Request["rid"]))  //Add
        {            
            try
            {
                if (SQLHelper.ReturnInteger("select count(*) from tbl_role where role_na = '" + Common.FormatParameter(this.txtRole.Text) + "' ") > 0)
                {
                    JScript.AjaxAlert(this.Page, "Role name has exist");
                    return;
                }
                SQLHelper.ExecuteNonQuery("insert into tbl_role(role_na) values('" + Common.FormatParameter(this.txtRole.Text) + "')");

                //记录日志
                Log.writeLog(Request.Cookies["user"].Values["id"], Request.Cookies["user"].Values["name"], "Add Role", "Add Role:" + this.txtRole.Text.Trim() + " by " + Request.Cookies["user"].Values["name"]);
            }
            catch (Exception ex)
            {
                JScript.AjaxAlert(this.Page, "Sorry,there is an error! Reasons:" + ex.Message.ToString());
            }
        }
        else                                              //Edit
        {
            if (SQLHelper.ReturnInteger("select count(*) from tbl_role where role_na = '" + Common.FormatParameter(this.txtRole.Text) + "' and id <>  '" + this.hidID.Value + "' ") > 0)
            {
                JScript.AjaxAlert(this.Page, "Role name has exist");
                return;
            }
            try
            {               
                SQLHelper.ExecuteNonQuery("update tbl_role set role_na = '" + Common.FormatParameter(this.txtRole.Text) + "' where id = '" + hidID.Value+"' ");
            }
            catch (Exception ex)
            {
                JScript.AjaxAlert(this.Page, "Sorry,there is an error! Reasons:" + ex.Message.ToString());
            }
        }
        Response.Redirect("RoleManagement.aspx");        
    }
}
