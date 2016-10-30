using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class 教材计划_提交订单 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //显示Dropdownlist
            string year = DateTime.Now.Year.ToString();
            string y1 = (DateTime.Now.Year - 1).ToString() + "-" + year;
            string y2 = year + "-" + (DateTime.Now.Year + 1).ToString();
            DropDownList1.Items.Add(y1);
            DropDownList1.Items.Add(y2);

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

    private void initData()
    {
        string date_year = DropDownList1.SelectedValue;
        string semester = DropDownList2.SelectedValue;
        string order_id = TextBox1.Text;
        string supply_person = DropSupplier.SelectedValue;
        string operate_person = TextBox3.Text;
        string campus = DropDownList3.SelectedValue;

        this.WebPager1.SqlField = " id,date_year,semester,order_id,order_date,operate_person,supply_person,campus ";
        this.WebPager1.TableName = " finally_order_information ";
        this.WebPager1.orderByID = " order_id asc ";
        this.WebPager1.TblID = " order_id ";
        this.WebPager1.WhereClause = " "
        + " and id in (select max(id) from finally_order_information group by order_id)"
        + "and state_id=1 "
        + "and date_year like '%" + date_year + "%' "
        + "and semester like '%" + semester + "%' "
        + "and order_id like '%" + order_id + "%' "
        + "and supply_person like '%" + supply_person + "%' "
        + "and operate_person like '%" + operate_person + "%' "
        + "and campus like '%" + campus + "%'";
    }

    //查询
    protected void Search_Click(object sender, EventArgs e)
    {
        initData();
    }

    //提交
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string t = DateTime.Now.ToString("yyyyMMdd");
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked)
                {
                    SQLHelper.ExecuteNonQuery("update finally_order_information set "
                    +"state_id=2,"
                    +"done_time='" + t + "' "
                    +"where "
                    +"order_id=" + GridView1.DataKeys[i].Value);
                }
            }
            JScript.AjaxAlertAndLocationHref(this.Page, "提交成功", "提交订单.aspx");
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }

    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[1].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }
}