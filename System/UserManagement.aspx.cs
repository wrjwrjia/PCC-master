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

public partial class System_UserManagement : System.Web.UI.Page
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
                initData();
                //lblPath.Text = PageCommon.getPathbymoduleid(Request["moduid"].ToString());
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = "select a.id,usr_login,role_na from tbl_usr a left join tbl_role b on a.role_id = b.id where 1=1 ";
        if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
        {
            sql += "  and usr_login like '%" + Common.FormatParameter(this.txtUserName.Text) + "%'";
        }
        this.gvData.DataSource = SQLHelper.GetDataTable(sql);
        this.gvData.DataBind();       
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {        
        Response.Redirect("AddUser.aspx");
    }
    //删除
    protected void lnkbtnDel_Click(object sender, EventArgs e)
    {
        string userid = (sender as LinkButton).CommandArgument;
        try
        {
            //获取被删除用户名
            string DelUsername = SQLHelper.GetDataTable("select usr_login from tbl_usr where id = '" + userid + "'").Rows[0][0].ToString();

            //删除上传文件表中的数据
            SQLHelper.ExecuteNonQuery("delete from UploadFile where username = '" + DelUsername+"'");

            //删除用户表数据
            SQLHelper.ExecuteNonQuery("delete from tbl_usr where id = '" + userid + "' ");
          

            //记录日志
            Log.writeLog(Request.Cookies["user"].Values["id"], Request.Cookies["user"].Values["name"], "Delete user", "Delete user:"+DelUsername +" by " + Request.Cookies["user"].Values["name"]);

            this.gvData.DataSource = SQLHelper.GetDataTable("select a.id,usr_login,role_na from tbl_usr a left join tbl_role b on a.role_id = b.id where 1=1 ");
            this.gvData.DataBind();

            JScript.ShowMsg(this.PopupWin1, "Delete user successfully!");
        }
        catch (System.Exception ex)
        {
            JScript.AjaxAlert(this.Page, "Sorry,there is an error! Reasons:" + ex.Message.ToString());
        }

    }




    protected void lnkbtnEdit_Click(object sender, EventArgs e)
    {
        string userid = (sender as LinkButton).CommandArgument;
        Response.Redirect("EditUser.aspx?uid=" + userid + "&moduid=" + HidModuId.Value);       
    }

    private void initData()
    {
        this.gvData.DataSource = SQLHelper.GetDataTable("select a.id,usr_login,role_na from tbl_usr a left join tbl_role b on a.role_id = b.id where 1=1 ");
        this.gvData.DataBind(); 
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
    }
}
