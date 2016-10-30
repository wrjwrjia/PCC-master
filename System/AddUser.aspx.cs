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

public partial class System_AddUser : System.Web.UI.Page
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
                InitInfo();
                this.lblPath.Text = "Add User";
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text == string.Empty)
        {
            JScript.AjaxAlert(this.Page, "Please input user name!");
            return;
        }
        //else if (txtPwd.Text == string.Empty)
        //{
        //    JScript.AjaxAlert(this.Page, "Please input password!");
        //    return;
        //}
        if (ddlRole.SelectedValue == "0")
        {
            JScript.AjaxAlert(this.Page, "Please choose role name!");
            return;
        }

        //判断用户名是否已存在
        if (SQLHelper.ReturnInteger("select count(*) from tbl_usr where usr_login = '" + Common.FormatParameter(this.txtUserName.Text) + "' ") > 0)
        {
            JScript.AjaxAlert(this.Page, "User Name has exist");
            return;
        }

        try
        {
            //使用LDAP验证，系统中使用默认的密码123
            this.txtPwd.Text = "123";
            SqlParameter[] parames = {
                new SqlParameter("@usr_login",Common.FormatParameter(txtUserName.Text.Trim())),
                //new SqlParameter("@usr_pwd",Common.WebEncrypt(txtPwd.Text.Trim())),
                new SqlParameter("@usr_pwd",Common.FormatParameter(txtPwd.Text.Trim())),
                new SqlParameter("@role_id",ddlRole.SelectedValue)             
            };
            SQLHelper.ExecuteNonQuery("insert into tbl_usr(usr_login,usr_pwd,role_id) values(@usr_login,@usr_pwd,@role_id)", parames);

            //向用户上传文件表插入数据
            SQLHelper.ExecuteNonQuery("insert into UploadFile values('" + this.txtUserName.Text.Trim() + "','a.xls');");

            //记录日志
            Log.writeLog(Request.Cookies["user"].Values["id"], Request.Cookies["user"].Values["name"], "Add user", "Add user:" + this.txtUserName.Text.Trim() + " by " + Request.Cookies["user"].Values["name"]);
          
            Response.Redirect("UserManagement.aspx");
        }
        catch (System.Exception ex)
        {
            JScript.AjaxAlert(this.Page, "Sorry,there is an error! Reasons:" + ex.Message.ToString());
        }
    }
    private void InitInfo()
    {
        ddlRole.DataSource = SQLHelper.GetDataTable(" select id,role_na from tbl_role ");
        ddlRole.DataTextField = "role_na";
        ddlRole.DataValueField = "id";
        ddlRole.DataBind();

        ddlRole.Items.Insert(0, new ListItem(">>Select Role Name", "0"));
    }
}
   
