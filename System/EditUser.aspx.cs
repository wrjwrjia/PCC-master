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
using System.Data.SqlClient;

public partial class System_EditUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Object.Equals(Request.Cookies["user"], null))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                InitUser();
                this.lblPath.Text = "Edit User";
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text == string.Empty)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "aa", "alert('Please input user name!')", true);
            return;
        }     
        if (ddlRole.SelectedValue == "0")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "aa", "alert('Please input role name!')", true);
            return;
        }
        SqlParameter[] parames = {
                new SqlParameter("@usr_login",Common.FormatParameter(txtUserName.Text.Trim())),              
                new SqlParameter("@role_id",ddlRole.SelectedValue)             
            };      
        SQLHelper.ExecuteNonQuery("update tbl_usr set usr_login = @usr_login,role_id = @role_id where id = '" + Request["uid"].ToString() + "' ", parames);       
        Response.Redirect("UserManagement.aspx");
    }
    private void InitUser()
    {
        //dropdownlist绑定
        ddlRole.DataSource = SQLHelper.GetDataTable(" select id,role_na from tbl_role ");
        ddlRole.DataTextField = "role_na";
        ddlRole.DataValueField = "id";
        ddlRole.DataBind();

        DataTable dt_usr = SQLHelper.GetDataTable("select id,usr_login,role_id from tbl_usr where id = '" + Request["uid"] + "'");
        ddlRole.SelectedValue = dt_usr.Rows[0]["role_id"].ToString();

        txtUserName.Text = dt_usr.Rows[0]["usr_login"].ToString();        
    }
}
