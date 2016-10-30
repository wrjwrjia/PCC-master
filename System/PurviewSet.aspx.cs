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
using System.Drawing;
using System.Text;

public partial class System_PurviewSet : System.Web.UI.Page
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
                hidId.Value = Request["id"].ToString();
                //展示数据
                initGvData();
                //将角色对应的权限勾选
                initData();
            }
        }
    }
    private void initGvData()
    {
        //显示前三列的数据
        GvData.DataSource = SQLHelper.GetDataTable("select * from menu order by orderid");
        GvData.DataBind(); 
    }

    //string txt = "├ ";
    string kg = "&nbsp;&nbsp;&nbsp;";

    protected void GvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string tempFlowId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "url"));

            if (string.IsNullOrEmpty(tempFlowId))
            {
                e.Row.Cells[1].ForeColor = Color.Red;
                e.Row.Cells[2].Visible = false;
            }
            else
            {
                e.Row.Cells[1].Text = kg + e.Row.Cells[1].Text;
                //e.Row.Cells[1].Text = kg + txt + e.Row.Cells[1].Text;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {        
        try
        {
            StringBuilder module = new StringBuilder();
            int isInsertitem = 0;
            for (int i = 0; i < this.GvData.Rows.Count; i++)
            {
                int moduleid = Convert.ToInt32(this.GvData.Rows[i].Cells[0].Text);
                DataTable dt = SQLHelper.GetDataTable("select id from UserPurview where moduleid = " + moduleid + " and roleid = " + hidId.Value + " ");
                if (dt.Rows.Count <= 0)   //不存在此角色的信息，先将roleid和moduleid写入
                {
                    SQLHelper.ExecuteNonQuery("insert into UserPurview (roleid,moduleid)values(" + hidId.Value + "," + moduleid + ")");
                }

                //IsSelected
                if ((this.GvData.Rows[i].Cells[2].FindControl("cbMenu") as CheckBox).Checked)
                {
                    SQLHelper.ExecuteNonQuery("update UserPurview set IsSelected=1 where moduleid = " + moduleid + " and roleid = " + hidId.Value);
                    isInsertitem++;
                }
                else
                {
                    SQLHelper.ExecuteNonQuery("update UserPurview set IsSelected=0 where moduleid = " + moduleid + " and roleid = " + hidId.Value);
                }

                if (isInsertitem > 0)
                {
                    module.Append(moduleid + ",");
                }
                isInsertitem = 0;
            }
            //循环结束

            //无任何权限,将purview表中的数据删除
            if (module.Length == 0)
            {
                SQLHelper.ExecuteNonQuery("delete from purview where roleid = " + hidId.Value);
                Response.Redirect("RoleManagement.aspx");
            }
            //增加父结点menuid
            string purview = module.Remove(module.Length - 1, 1).ToString();    //删除最后的逗号
            DataTable GetPID = SQLHelper.GetDataTable("select parentid  from menu where menuid in (" + purview + ") and parentid != 0 group by parentid");
            //三级菜单判断
            int menuid1 = 0;
            int menuid2 = 0;
            for (int i = 0; i < GetPID.Rows.Count; i++)
            {
                if (GetPID.Rows[i]["parentid"].ToString() == "1")
                    menuid1 = 1; // 1 已存在
                if (GetPID.Rows[i]["parentid"].ToString() == "2")
                    menuid2 = 1; // 1 已存在
            }
            for (int i = 0; i < GetPID.Rows.Count; i++)
            {
                purview = purview.Insert(0, GetPID.Rows[i]["parentid"].ToString() + ",");
            }
            if (menuid1 == 0 && menuid2 == 1)
            {
                purview = purview.Insert(0, "1,");
            }
            //权限更新
            if ((int)SQLHelper.ExecuteScalar("select count(*) from purview where roleid = " + hidId.Value) <= 0)
            {
                SQLHelper.ExecuteNonQuery("insert into purview(roleid) values( '" + hidId.Value + "')");
            }
            SQLHelper.ExecuteNonQuery("update purview set permission = '" + purview + "' where roleid = " + hidId.Value);

            //获取角色名
            string rolename = SQLHelper.GetDataTable("select role_na from tbl_role where id = '" + hidId.Value + "'").Rows[0][0].ToString();
            //记录日志
            Log.writeLog(Request.Cookies["user"].Values["id"], Request.Cookies["user"].Values["name"], "Change Authority", "Change Authority of Role:" + rolename + " by " + Request.Cookies["user"].Values["name"]);


            //给用户提示,并返回角色管理页面
            JScript.AjaxAlertAndLocationHref(this.Page, "Authority Successfully", "RoleManagement.aspx");
        }
        catch (System.Exception ex)
        {
            JScript.AjaxAlertAndLocationHref(this.Page, "Sorry,there is an error! Reasons:" + ex.Message.ToString(), "RoleManagement.aspx");
        }
        
    }

    private void initData()
    {
        DataTable dt = SQLHelper.GetDataTable("select id from UserPurview where roleid = " + hidId.Value);
        if (dt.Rows.Count > 0)
        {           
            for (int i = 0; i < this.GvData.Rows.Count; i++)
            {
                string index = this.GvData.Rows[i].Cells[0].Text.ToString();

                /*
                if ( SQLHelper.ReturnInteger("select IsSearch from UserPurview where moduleid = " + index + " and roleid = " + hidId.Value) == 1)
                {
                    (this.GvData.Rows[i].Cells[3].FindControl("cbQry") as CheckBox).Checked = true;

                }
                if ( SQLHelper.ReturnInteger("select IsAdd from UserPurview where moduleid = " + index + " and roleid = " + hidId.Value) == 1 )
                {
                    (this.GvData.Rows[i].Cells[3].FindControl("cbAdd") as CheckBox).Checked = true;

                }
                if ( SQLHelper.ReturnInteger("select IsDel from UserPurview where moduleid = " + index + " and roleid = " + hidId.Value) == 1)
                {
                    (this.GvData.Rows[i].Cells[3].FindControl("cbDelete") as CheckBox).Checked = true;

                }
                if ( SQLHelper.ReturnInteger("select IsUpdate from UserPurview where moduleid = " + index + " and roleid = " + hidId.Value) == 1)
                {
                    (this.GvData.Rows[i].Cells[3].FindControl("cbUpdate") as CheckBox).Checked = true;

                }
                if ( SQLHelper.ReturnInteger("select IsIMP from UserPurview where moduleid = " + index + " and roleid = " + hidId.Value) == 1)
                {
                    (this.GvData.Rows[i].Cells[3].FindControl("cbIMP") as CheckBox).Checked = true;

                }
                if ( SQLHelper.ReturnInteger("select IsEXP from UserPurview where moduleid = " + index + " and roleid = " + hidId.Value) == 1)
                {
                    (this.GvData.Rows[i].Cells[3].FindControl("cbEXP") as CheckBox).Checked = true;
                }
                if ( SQLHelper.ReturnInteger("select IsMail from UserPurview where moduleid = " + index + " and roleid = " + hidId.Value) == 1)
                {
                    (this.GvData.Rows[i].Cells[3].FindControl("cbMail") as CheckBox).Checked = true;
                }

                */

                if (SQLHelper.ReturnInteger("select IsSelected from UserPurview where moduleid = " + index + " and roleid = " + hidId.Value) == 1)
                {
                    (this.GvData.Rows[i].Cells[2].FindControl("cbMenu") as CheckBox).Checked = true;
                }         
            }
        }
       
    }

}
