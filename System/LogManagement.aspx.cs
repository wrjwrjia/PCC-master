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

public partial class System_LogManagement : System.Web.UI.Page
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
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string whereClause = "";
        if (this.txtUserName.Text.Trim() != "")
        {
            whereClause += " and usr_name like '%" + Common.FormatParameter(this.txtUserName.Text) + "%' ";
        }
        if (this.txtOperationTimeStart.Text.Trim() != "")
        {
            whereClause += " and opt_date >= '" + Common.FormatParameter(this.txtOperationTimeStart.Text) + "' ";
        }
        if (this.txtOperationTimeEnd.Text.Trim() != "")
        {
            whereClause += " and opt_date <= '" + Common.FormatParameter(this.txtOperationTimeEnd.Text) + " 23:59:59' ";
        }
        if (this.txtOperationTimeStart.Text.Trim() != "" && !PageValidate.isDateTime(this.txtOperationTimeStart.Text.Trim()))
        {
            JScript.AjaxAlert(this.Page, "Start Time is wrong");
            return;
        }
        if (this.txtOperationTimeEnd.Text.Trim() != "" && !PageValidate.isDateTime(this.txtOperationTimeEnd.Text.Trim()))
        {
            JScript.AjaxAlert(this.Page, "End Time is wrong");
            return;
        }
        this.WebPager1.SqlField = " usr_name,opt,opt_date,detail ";
        this.WebPager1.TableName = " tbl_log ";
        this.WebPager1.WhereClause = whereClause;
        this.WebPager1.orderByID = " opt_date desc ";
    }
    private void initData()
    {
        this.WebPager1.SqlField = " usr_name,opt,opt_date,detail ";
        this.WebPager1.TableName = " tbl_log ";
        this.WebPager1.orderByID = " opt_date desc ";
        this.WebPager1.TblID = " id ";
    }
    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
    }
}
