using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 库存管理_退库 : System.Web.UI.Page
{
    static string book_id = null;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        string sql = "select order_id,order_date,date_year,operate_person,supply_person,supplier_phone,book_id,book_name,book_price,remain_num from Storage_management_end where book_id like'%" + TextBox1.Text + "%' and book_name like '%" + TextBox2.Text + "%' and press_name like '%" + Press_Name.Text + "%' and supply_person like '%" + Supplier_Text.Text + "%'";
        DataTable dt = SQLHelper.GetDataTable(sql);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            Check_All.Visible = true;
            Return_Button.Visible = true;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        book_id = (sender as Button).CommandArgument;
        string sql = "select order_id,order_date,date_year,operate_person,supply_person,supplier_phone,book_id,book_name,book_price,remain_num from Storage_management_end where book_id=" + book_id;
        DataTable dt = SQLHelper.GetDataTable(sql);
        Button3.Enabled = true;
        Button4.Enabled = true;
        GridView1.AllowSorting = false;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        SqlDataReader read = SQLHelper.ExecuteReader("select * from Storage_management_end where book_id =" + book_id);
        for (; read.Read(); )
        {
            TextBox4.Text = read["remain_num"].ToString();
            TextBox16.Text = read["arrived_amount"].ToString();
            TextBox17.Text = read["real_num"].ToString();
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            string sql1 = "select order_id,order_date,date_year,operate_person,supply_person,supplier_phone,book_id,book_name,book_price,remain_num from Storage_management_end where book_id=" + book_id;
            DataTable dt = SQLHelper.GetDataTable(sql1);
            GridView1.AllowSorting = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            SqlDataReader read = SQLHelper.ExecuteReader("select * from Storage_management_end where book_id =" + book_id);
            //记录下书号和订单号，作为一本书的唯一标示
            int amount = 0;
            for (; read.Read(); )
            {

                //amount = int.Parse(read["remain_num"].ToString());
                amount = int.Parse(TextBox17.Text);
                if (TextBox3.Text == "" && TextBox3.Text == "0")
                {
                    WebMessageBox.Show("不能输入空值或零！");
                }
                if (int.Parse(TextBox3.Text) > amount)
                {
                    WebMessageBox.Show("输入的数量有错！");
                }
                else
                {

                    int remain = amount - Convert.ToInt32(TextBox3.Text);
                    string sql = "insert into return_history (order_id,operate_person,operater_phone,date_year ,semester ,supply_person,supplier_phone,book_id ,book_name,press_name,book_price,plan_num,auto_num,total_price,order_discount ,arrived_discount ,sell_discount ,arrived_amount ,unarrived_amount ,arrival_amount ,arrived_date ,Campus ,Storage_location ,out_num,remain_num ,order_date, press_time,state_id,total_num,return_num,return_date)";
                    sql += "values('" + read["order_id"].ToString() + "','" + read["operate_person"].ToString() + "','" + read["operater_phone"].ToString() + "','" + read["date_year"].ToString() + "','" + read["semester"].ToString() + "','" + read["supply_person"].ToString() + "','" + read["supplier_phone"].ToString() + "','" + read["book_id"].ToString() + "','" + read["book_name"].ToString() + "','" + read["press_name"].ToString() + "','" + read["book_price"].ToString() + "','" + read["plan_num"].ToString() + "','" + read["auto_num"].ToString() + "','" + read["total_price"].ToString() + "','" + read["order_discount"].ToString() + "','" + read["arrived_discount"].ToString() + "','" + read["sell_discount"].ToString() + "','" + read["arrived_amount"].ToString() + "','" + read["unarrived_amount"].ToString() + "','" + read["arrival_amount"].ToString() + "','" + read["arrived_date"].ToString() + "','" + read["Campus"].ToString() + "','" + read["Storage_location"].ToString() + "','" + read["out_num"].ToString() + "','" + read["remain_num"].ToString() + "','" + read["order_date"].ToString() + "','" + read["press_time"].ToString() + "','" + read["state_id"].ToString() + "','" + read["total_num"].ToString() + "','" + TextBox3.Text.ToString() + "','" + System.DateTime.Now.ToString("d") + "')";
                    SQLHelper.ExecuteNonQuery(sql);
                    SQLHelper.ExecuteReader("update Storage_management_end set remain_num='" + remain.ToString() + "'where book_id= '" + book_id + "' ");
                    string sql2 = "select date_year,semester,supply_person,book_id,book_name,press_name,return_num,return_date,Storage_location from return_history where book_id =" + book_id;
                    DataTable dt1 = SQLHelper.GetDataTable(sql2);
                    GridView2.DataSource = dt1;
                    GridView2.DataBind();
                    string sql3 = "delete from Storage_management_end where remain_num='0'";
                    SQLHelper.ExecuteNonQuery(sql3);
                    Button4.Visible = true;
                }
            }
            Button3.Focus();
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView2.DataSource = null;
        GridView1.AllowSorting = true;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        Button1_Click(sender, e);
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
        Button3_Click(sender, e);
    }
    protected void Return_Button_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox cb = GridView1.Rows[i].Cells[0].FindControl("CheckBox1") as CheckBox;
                if (cb.Checked == true)
                {
                    Label order_id = GridView1.Rows[i].Cells[0].FindControl("Label1") as Label;
                    Label book_id = GridView1.Rows[i].Cells[0].FindControl("Label7") as Label;
                    string sql4 = "select * from Storage_management_end where book_id=" + book_id.Text + " and order_id=" + order_id.Text + "";
                    SqlDataReader read = SQLHelper.ExecuteReader(sql4);
                    for (; read.Read(); )
                    {
                        string sql2 = "insert into return_history (order_id,operate_person,operater_phone,date_year ,semester ,supply_person,supplier_phone,book_id ,book_name,press_name,book_price,plan_num,auto_num,total_price,order_discount ,arrived_discount ,sell_discount ,arrived_amount ,unarrived_amount ,arrival_amount ,arrived_date ,Campus ,Storage_location ,out_num,remain_num ,order_date, press_time,state_id,total_num,return_num,return_date)";
                        sql2 += "values('" + read["order_id"].ToString() + "','" + read["operate_person"].ToString() + "','" + read["operater_phone"].ToString() + "','" + read["date_year"].ToString() + "','" + read["semester"].ToString() + "','" + read["supply_person"].ToString() + "','" + read["supplier_phone"].ToString() + "','" + read["book_id"].ToString() + "','" + read["book_name"].ToString() + "','" + read["press_name"].ToString() + "','" + read["book_price"].ToString() + "','" + read["plan_num"].ToString() + "','" + read["auto_num"].ToString() + "','" + read["total_price"].ToString() + "','" + read["order_discount"].ToString() + "','" + read["arrived_discount"].ToString() + "','" + read["sell_discount"].ToString() + "','" + read["arrived_amount"].ToString() + "','" + read["unarrived_amount"].ToString() + "','" + read["arrival_amount"].ToString() + "','" + read["arrived_date"].ToString() + "','" + read["Campus"].ToString() + "','" + read["Storage_location"].ToString() + "','" + read["out_num"].ToString() + "','" + read["remain_num"].ToString() + "','" + read["order_date"].ToString() + "','" + read["press_time"].ToString() + "','" + read["state_id"].ToString() + "','" + read["total_num"].ToString() + "','" + TextBox3.Text.ToString() + "','" + System.DateTime.Now.ToString("d") + "')";
                        SQLHelper.ExecuteNonQuery(sql2);
                    }
                    string sql = "delete from Storage_management_end where order_id=" + order_id.Text + " and book_id=" + book_id.Text;
                    SQLHelper.ExecuteNonQuery(sql);

                }
            }
            // Button1_Click(sender,e);
            //刷新数据表
            //Button1_Click(sender, e);
            string sql5 = "select order_id,order_date,date_year,operate_person,supply_person,supplier_phone,book_id,book_name,book_price,remain_num from Storage_management_end where book_id like'%" + TextBox1.Text + "%'and book_name like '%" + TextBox2.Text + "%'";
            DataTable dt = SQLHelper.GetDataTable(sql5);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            JScript.AjaxAlertAndLocationHref(this.Page, "退书成功", "退库.aspx");
            // WebMessageBox.Show("退货成功！");
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }
    protected void Check_All_CheckedChanged(object sender, EventArgs e)
    {

        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (Check_All.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }
    //鼠标经过背景色改变
    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
    }
}