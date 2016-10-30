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
using System.DirectoryServices;

public partial class bg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtUserName.Focus();
        }
    }

    /// <summary>
    /// 登陆按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtLogin_Click(object sender, ImageClickEventArgs e)
    {

        DataTable dt_login = SQLHelper.GetDataTable("select id,usr_login,usr_pwd,role_id from tbl_usr where usr_login = '" + Common.FormatParameter(this.txtUserName.Text.Trim()) + "' and usr_pwd = '" + Common.FormatParameter(this.txtPWD.Text.Trim()) + "'");//Common.WebEncrypt(this.txtPWD.Text.Trim()) + "'");

        try
        {
            HttpCookie user = new HttpCookie("user"); // Cookie
            user["id"] = dt_login.Rows[0]["id"].ToString();
            user["name"] = Server.UrlEncode(dt_login.Rows[0]["usr_login"].ToString()).Replace("+", " ");
            user["roleid"] = dt_login.Rows[0]["role_id"].ToString();
            Response.Cookies.Add(user);
            DataTable dt_login2 = SQLHelper.GetDataTable("select * from student where stuID = '" + Common.FormatParameter(this.txtUserName.Text.Trim()) + "'");//Common.WebEncrypt(this.txtPWD.Text.Trim()) + "'");
            if (dt_login2.Rows.Count>0)
            {
                HttpCookie StuInfo = new HttpCookie("StuInfo"); // Cookie
                StuInfo["ID"] = dt_login2.Rows[0]["ID"].ToString();
                StuInfo["stuID"] = dt_login2.Rows[0]["stuID"].ToString();
                StuInfo["stuName"] = dt_login2.Rows[0]["stuName"].ToString();
                StuInfo["className"] = dt_login2.Rows[0]["className"].ToString();
                StuInfo["stuGrade"] = dt_login2.Rows[0]["stuGrade"].ToString();
                StuInfo["DeptName"] = dt_login2.Rows[0]["DeptName"].ToString();
                StuInfo["campusName"] = dt_login2.Rows[0]["campusName"].ToString();
                Response.Cookies.Add(StuInfo);
            }
           //获取Output和UploadFiles文件夹下一天前的临时文件
            //DeleteFile.DeleteOverdueFile(Server.MapPath("~/Output"));
            //DeleteFile.DeleteOverdueFile(Server.MapPath("~/UploadFiles"));


            Response.Redirect("index.aspx");
           // Response.Redirect("学生用/Notice.aspx");

        }
        catch (Exception ex)
        {
            JScript.AjaxAlert(this.Page, "用户名或密码错误!");
        }


        //int index = this.txtUserName.Text.Trim().IndexOf('\\');
        //if (index < 0)
        //{
        //    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('The username does not exist!')</script>");
        //    return;
        //}
        ////获取"\"后面的真实的用户名
        //string RealName = this.txtUserName.Text.Remove(0, index + 1);

        ////LDAP验证
        //string strPath = "LDAP://147.128.18.10";
        //DirectoryEntry de = new DirectoryEntry(strPath, this.txtUserName.Text.Trim(), this.txtPWD.Text.Trim(), AuthenticationTypes.None);
        //DirectorySearcher deSearch = new DirectorySearcher();
        //deSearch.SearchRoot = de;

        ////验证LDAP用户名和密码
        //if (VerifyUser(deSearch))
        //{
        //    DataTable dt_login = SQLHelper.GetDataTable("select id,usr_login,usr_pwd,role_id from tbl_usr where usr_login = '" + RealName + "'");//Common.WebEncrypt(this.txtPWD.Text.Trim()) + "'");

        //    if (dt_login.Rows.Count > 0)
        //    {
        //        HttpCookie user = new HttpCookie("user"); // Cookie
        //        user["id"] = dt_login.Rows[0]["id"].ToString();
        //        user["name"] = Server.UrlEncode(dt_login.Rows[0]["usr_login"].ToString()).Replace("+", " ");
        //        user["roleid"] = dt_login.Rows[0]["role_id"].ToString();
        //        Response.Cookies.Add(user);

        //        //获取Output文件夹下一天前的临时文件
        //        DeleteFile.DeleteOverdueFile(Server.MapPath("~/Output"));
        //        DeleteFile.DeleteOverdueFile(Server.MapPath("~/UploadFiles"));

        //        Response.Redirect("index.aspx");
        //    }
        //    else
        //    {
        //        JScript.AjaxAlert(this.Page, "The username does not exist!");
        //        return;
        //    }
        //}
        //else
        //{
        //    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('LDAP Authentication failure!')</script>");
        //    return;
        //}
    }

    //LDAP验证
    public bool VerifyUser(DirectorySearcher searcher)
    {
        try
        {
            //执行以下方法时没抛出异常说明用户名密码正确
            SearchResultCollection rs = searcher.FindAll();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
