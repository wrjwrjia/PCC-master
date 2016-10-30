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

public partial class System_RoleManagement : System.Web.UI.Page
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
                Bind();
                //lblPath.Text = PageCommon.getPathbymoduleid(Request["moduid"].ToString());      
            }
        }
        this.PopupWin1.Visible = false;
    }
    private void Bind()
    {
        gvData.DataSource = SQLHelper.GetDataTable("select a.id,a.role_na,(select count(*) from tbl_usr where role_id=a.id) as ProNum from tbl_role a order by a.id asc");
        gvData.DataBind();        
    }
    //Search
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = "select a.id,a.role_na,(select count(*) from tbl_usr where role_id=a.id) as ProNum from tbl_role a where 1=1 ";
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            sql += " and role_na like '%" + Common.FormatParameter(TextBox1.Text) + "%' ";
        }
        sql += " order by a.id asc";
        gvData.DataSource = SQLHelper.GetDataTable(sql);
        gvData.DataBind();
    }
    //Edit   注：Edit和Add都是跳转到AddRole.aspx页面，当Edit时传递rid，但是Add时不传递，加以区分
    protected void lnkbtnEdit_Click(object sender, EventArgs e)
    {
        string roleid = (sender as LinkButton).CommandArgument;
        Response.Redirect("AddRole.aspx?rid=" + roleid);
    }
    //Change
    protected void lnkbtnSet_Click(object sender, EventArgs e)
    {
        string roleid = (sender as LinkButton).CommandArgument;
        Response.Redirect("PurviewSet.aspx?id=" + roleid + "&t=1&moduid=" + Request["moduid"]);
    } 
    //Delete   注：删除角色并没有删除角色下的用户
    protected void btnDel_Click1(object sender, EventArgs e)
    {       
        int count = 0;
        string DelRoleName = "";
        if (this.gvData.Rows.Count > 0)
        {
            for (int i = 0; i < this.gvData.Rows.Count; i++)
            {
                CheckBox ck = this.gvData.Rows[i].Cells[4].FindControl("chkDelete") as CheckBox;
                Label lb = this.gvData.Rows[i].Cells[0].FindControl("lbID") as Label;
                if (ck.Checked)
                {
                    //获取被删除角色名
                    DelRoleName = SQLHelper.GetDataTable("select role_na from tbl_role where id = '" + lb.Text + "'").Rows[0][0].ToString();

                    //删除角色,tbl_role
                    SQLHelper.ExecuteNonQuery("delete from tbl_role where id = " + lb.Text);

                    //删除角色对应的权限信息,userpurview purview
                    SQLHelper.ExecuteNonQuery("delete from userpurview where roleid = " + lb.Text);
                    SQLHelper.ExecuteNonQuery("delete from purview where roleid = " + lb.Text);

                    //记录日志
                    Log.writeLog(Request.Cookies["user"].Values["id"], Request.Cookies["user"].Values["name"], "Delete Role", "Delete Role:" + DelRoleName + " by " + Request.Cookies["user"].Values["name"]);

                    count++;
                }
            }
        }
        JScript.ShowMsg(this.PopupWin1, "Delete " + count.ToString() + " Role(s)!");
        Bind();
    }
    //Add     注：Edit和Add都是跳转到AddRole.aspx页面，当Edit时传递rid，但是Add时不传递，加以区分
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddRole.aspx?moduid=" + Request["moduid"]);
    }
    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
    }  
}
