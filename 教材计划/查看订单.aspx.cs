using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class 教材计划_查看订单 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //显示学年
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
                initListData();
            }
        }
    }


    //对于GridView1，即某一订单下的信息
    private void initOrderData()
    {
        this.WebPager1.SqlField = " id,book_id,book_name,press_name,press_time,book_editor,book_price,total_num,total_price ";
        this.WebPager1.TableName = " finally_order_information ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.BookId = "book_id";
        this.WebPager1.Number = "total_num";
        this.WebPager1.Price = "total_price";
        string id = SaveOrderID.Text;
        this.WebPager1.WhereClause = " and order_id ='" + id + "' ";
        this.WebPager1.FlagForOrder = "Yes";
    }
    
    //对于GridView2，整个订单列表
    private void initListData()
    {
        string date_year = DropDownList1.SelectedValue;
        string semester = DropDownList2.SelectedValue;
        string order_id = TextBox1.Text;
        string supply_person = DropSupplyPerson.SelectedValue;
        string operate_person = TextBox3.Text;
        string campus = DropDownList3.SelectedValue;
        string dt1 = TextBox4.Text;
        string dt2 = TextBox5.Text;
        string st = DropState.SelectedValue;
        switch (st)
        {
            case "已保存": st = "1"; break;
            case "已提交": st = "2"; break;
            case "院审批已通过": st = "3"; break;
            case "校审批已通过": st = "4"; break;
        }

        this.WebPager2.SqlField = "id,order_id,order_date,operate_person,supply_person,supplier_phone,campus,state_id ";
        this.WebPager2.TableName = " finally_order_information ";
        this.WebPager2.orderByID = " id asc ";
        this.WebPager2.TblID = " id ";
        this.WebPager2.WhereClause = " "
        + " and id in (select max(id) from finally_order_information group by order_id)"
        + " and state_id <>5"
        + " and state_id like '%" + st + "%'"
        + " and date_year like '%" + date_year + "%'"
        + " and semester like '%" + semester + "%'"
        + " and order_id like '%" + order_id + "%'"
        + " and supply_person like '%" + supply_person + "%'"
        + " and operate_person like '%" + operate_person + "%'"
        + " and operate_person like '%" + operate_person + "%'"
        + " and campus like '%" + campus + "%'";
        if(dt1 !="")
        {
            dt1=Regex.Replace(dt1, @"-", "");
            this.WebPager2.WhereClause += "and done_time>=" + dt1;
        }
        if (dt2 != "")
        {
            dt2 = Regex.Replace(dt2, @"-", "");
            this.WebPager2.WhereClause += "and done_time<=" + dt2;
        }
        this.WebPager2.Flag = "Yes";
    }


    //查询
    protected void Search_Click(object sender, EventArgs e)
    {
        initListData();
    }



    //订单列表中的删除
    protected void gv2Data_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string order_id = GridView2.DataKeys[e.RowIndex].Value.ToString();
        string flag = "";

        string sql0 = "select state_id from finally_order_information where order_id='" + order_id + "'";
        SqlDataReader read0 = SQLHelper.ExecuteReader(sql0);
        while (read0.Read())
        {
            flag = read0[0].ToString();
        }
        if (flag == "2")
        {
            WebMessageBox.Show("此订单已提交，不能删除！");
            return;
        }

        string sql1 = "select book_id,campus,plan_num from finally_order_information where order_id='" + order_id + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql1);
        while (read.Read())
        {
            string book_id = read["book_id"].ToString();
            string campus = read["campus"].ToString();
            string book_number = read["plan_num"].ToString();
            string sql = "update book_demand set state_id=0 where book_id='" + book_id + "' and campus='" + campus + "' and book_number='" + book_number + "' and state_id=1";
            SQLHelper.ExecuteNonQuery(sql);
        }
        string sql3 = "delete from finally_order_information where order_id='" + order_id + "'";//把之前的订书单也删了
        SQLHelper.ExecuteNonQuery(sql3);
        initListData();
    }



    //教材列表中的删除
    protected void gv1Data_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string flag = "";

        string sql0 = "select state_id from finally_order_information where id='" + id + "'";
        SqlDataReader read0 = SQLHelper.ExecuteReader(sql0);
        while (read0.Read())
        {
            flag = read0[0].ToString();
        }
        if (flag == "2")
        {
            WebMessageBox.Show("该订单已提交，不能更改！");
            return;
        }
        string sql1 = "select book_id,campus,plan_num,order_id from finally_order_information where id='" + id + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql1);
        while (read.Read())
        {
            string book_id = read["book_id"].ToString();
            string campus = read["campus"].ToString();
            string book_number = read["plan_num"].ToString();
            string sql = "update book_demand set state_id=0 where "
            +"book_id='" + book_id + "' "
            +"and campus='" + campus + "' "
            +"and book_number='" + book_number + "' "
            +"and state_id=1";
            SQLHelper.ExecuteNonQuery(sql);
            string sql3 = "delete from finally_order_information where id='" + id + "'";
            SQLHelper.ExecuteNonQuery(sql3);
        }
        initOrderData();
    }



    //鼠标经过背景色改变
    protected void Gv1DataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }



    protected void Gv2DataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            switch (e.Row.Cells[7].Text)
            {
                case "1": e.Row.Cells[7].Text = "保存"; break;
                case "2": e.Row.Cells[7].Text = "提交"; break;
                case "3": e.Row.Cells[7].Text = "院审批通过"; break;
                case "4": e.Row.Cells[7].Text = "校审批通过"; break;
            }
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (WebPager2.Pagesize * (Convert.ToInt32((WebPager2.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }


    //教材详细信息
    protected void BookDetail_Click(object sender, EventArgs e)
    {
        string id = (sender as LinkButton).CommandArgument;
        SqlDataReader read = SQLHelper.ExecuteReader("select * from finally_order_information where id =" + id);
        while (read.Read())
        {
            TextBox6.Text = read["order_id"].ToString();
            TextBox7.Text = read["order_date"].ToString();
            TextBox8.Text = read["operate_person"].ToString();
            TextBox10.Text = read["date_year"].ToString();
            TextBox11.Text = read["semester"].ToString();
            TextBox12.Text = read["supply_person"].ToString();
            TextBox13.Text = read["supplier_phone"].ToString();
            TextBox14.Text = read["book_id"].ToString();
            TextBox15.Text = read["book_name"].ToString();
            TextBox16.Text = read["press_name"].ToString();
            TextBox17.Text = read["press_time"].ToString();
            TextBox18.Text = read["book_editor"].ToString();
            TextBox19.Text = read["book_price"].ToString();
            TextBox20.Text = read["plan_num"].ToString();
            TextBox21.Text = read["auto_num"].ToString();
            TextBox22.Text = read["total_num"].ToString();
            TextBox23.Text = read["total_price"].ToString();
            TextBox24.Text = read["campus"].ToString();           
        }
    }

    //查看订单详细信息
    protected void Detail_Click(object sender, EventArgs e)
    {
        SaveOrderID.Text = (sender as LinkButton).CommandArgument;
        initOrderData();
    }
}