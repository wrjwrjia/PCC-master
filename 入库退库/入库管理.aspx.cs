using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 库存管理_入库管理1 : System.Web.UI.Page
{
    static string ID, book_id, order_id, total;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //加载订单号
            string sql = "select distinct order_id from finally_order_information";
            SqlDataReader read = SQLHelper.ExecuteReader(sql);
            while (read.Read())
                DropOrderId.Items.Add(new ListItem(read[0].ToString()));

            if (Object.Equals(Request.Cookies["user"], null))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                initData();
                this.WebPager1.WhereClause += " and 2<1";
            }
        }
    }


    private void initData()
    {
        //读出订单中符合条件的信息
        this.WebPager1.SqlField = " id,order_id,supply_person,book_id,book_name,press_time,press_name,book_price ";
        this.WebPager1.TableName = " finally_order_information ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = ""
            + " and supply_person like '" + DropSupplyPerson.SelectedValue + "'"
            + " and book_id like '" + TbBookId.Text + "'"
            + " and book_name like '" + TbBookName.Text + "'"
            + " and order_id like '" + DropOrderId.SelectedValue + "'"
            + " and press_name like '" + DropPressName.SelectedValue + "'"
            + " and press_time like '" + DropPressTime.SelectedValue + "'";
        if (CheckBox1.Checked == false)
            this.WebPager1.WhereClause += " and state_id<>5 ";//没有到完货
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
    }


    //编辑
    protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
    }


    //取消
    protected void gvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
    }


    //更新
    protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //First Step: 确定此订单的状态
        //Second Step: 存入到货历史表Storage_management中
        //Third Step: 存入到货量最终表Storage_management_end中
        //Fourth Step: 更改finally_order_information
        GridView1.EditIndex = -1;
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            ID = (sender as Button).CommandArgument;
            string sql = "select order_id,id,order_date,operate_person,date_year,semester,supply_person,book_id,book_name,press_name,book_price,total_num from finally_order_information where id='" + ID + "'";
            DataTable dt = SQLHelper.GetDataTable(sql);
            GridView1.AllowSorting = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //已到货量默认为0
            Arrived_Text.Text = "0";
            SqlDataReader read = SQLHelper.ExecuteReader("select * from finally_order_information where id ='" + ID + "'");
            //记录下书号和订单号，作为一本书的唯一标示
            for (; read.Read(); )
            {
                book_id = read["book_id"].ToString();
                order_id = read["order_id"].ToString();
                BookName_Text.Text = read["book_name"].ToString();
                BookId_Text.Text = book_id;
                Supplier_Text.Text = read["supply_person"].ToString();
                Press_Text.Text = read["press_name"].ToString();
                PressDate_Text.Text = read["press_date"].ToString();//出版时间
                PressTime_Text.Text = read["press_time"].ToString();//版次
                OrderId_Text.Text = order_id;
                Book_Editor.Text = read["book_editor"].ToString();
                Price_Text.Text = read["book_price"].ToString();
                TextBox4.Text = Convert.ToInt32(read["total_num"].ToString()).ToString();
                total = read["total_num"].ToString();
            }
            SqlDataReader read2 = SQLHelper.ExecuteReader("select arrived_amount from Storage_management where book_id ='" + book_id + "'and order_id='" + order_id + "'");
            for (; read2.Read(); )
            {
                if (read2["arrived_amount"].ToString() == null)
                {
                    Arrived_Text.Text = "0";
                    TextBox4.Text = (Convert.ToInt32(total) - Convert.ToInt32(Arrived_Text.Text)).ToString();
                }
                else
                {
                    Arrived_Text.Text = read2["arrived_amount"].ToString();

                    TextBox4.Text = (Convert.ToInt32(total) - Convert.ToInt32(Arrived_Text.Text)).ToString();
                }
            }
            SqlDataReader read3 = SQLHelper.ExecuteReader("select * from Storage_management where book_id= '" + book_id + "' and order_id='" + order_id + "'");
            for (; read3.Read(); )
            {
                if (Convert.ToInt32(read3["total_num"].ToString()) <= Convert.ToInt32(read3["arrived_amount"].ToString()))
                {
                    WebMessageBox.Show("所订教材已全部到货！");
                }
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lb = GridView1.Rows[i].Cells[0].FindControl("Label1") as Label;
                Button bu = GridView1.Rows[i].Cells[0].FindControl("Button2") as Button;
                if (ID == lb.Text)
                {
                    bu.Enabled = false;
                } break;
            }
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        string Order_ID, Order_Date, Operate_Person, Date_Year, Semester;
        string Supply_Person, Supplier_phone, Book_ID, Book_Name;
        string Press_Name, Press_Time, Book_Price, Total_Num, State_ID;
        string Discount, Arrived_Amount, Unarrived_Amount;
        string Arrived_Date, Arrival_Amount, Order_Discount, Storage_ID, book_editor;
        {
            try
            {
                SqlDataReader read2 = SQLHelper.ExecuteReader("select * from finally_order_information where id ='" + ID + "'");
                for (; read2.Read(); )
                {
                    Order_ID = OrderId_Text.Text;
                    Order_Date = read2["order_date"].ToString();
                    Operate_Person = read2["operate_person"].ToString();
                    Date_Year = read2["date_year"].ToString();
                    Semester = read2["semester"].ToString();
                    Supply_Person = Supplier_Text.Text;
                    Supplier_phone = read2["supplier_phone"].ToString();
                    Book_ID = BookId_Text.Text;
                    Book_Name = BookName_Text.Text;
                    Press_Name = Press_Text.Text;
                    Press_Time = PressTime_Text.Text;//版次
                    Book_Price = Price_Text.Text;
                    Total_Num = read2["total_num"].ToString();
                    State_ID = read2["state_id"].ToString();
                    Order_Discount = read2["order_discount"].ToString();
                    book_editor = Book_Editor.Text;
                    Arrived_Date = System.DateTime.Now.ToString("d");
                    Arrived_Amount = ((Convert.ToInt32(Arrived_Text.Text)) + (Convert.ToInt32(TextBox4.Text))).ToString();
                    Discount = TextBox3.Text;
                    Arrival_Amount = TextBox4.Text;
                    Unarrived_Amount = (Convert.ToInt32(Total_Num) - Convert.ToInt32(Arrived_Amount)).ToString();
                    Storage_ID = System.DateTime.Now.ToString("d") + Order_ID;//少加一个批号
                    string state_id = null;//表示订单表中的到货情况：3代表未到货，4代表部分到货，5代表全部到货。
                    //判断到货情况 0代表未到货，1代表部分到货，2代表全部到货。
                    if (Convert.ToInt32(Arrived_Amount) == 0)
                    {
                        State_ID = "0";
                        state_id = "3";
                    }
                    else if (Convert.ToInt32(Arrived_Amount) == Convert.ToInt32(Total_Num))
                    {
                        State_ID = "2";
                        state_id = "5";
                    }
                    else if (Convert.ToInt32(Arrived_Amount) > 0 && Convert.ToInt32(Arrived_Amount) < Convert.ToInt32(Total_Num))
                    {
                        State_ID = "1";
                        state_id = "4";
                    }
                    string sql = "insert into Storage_management(order_id,order_date,operate_person,date_year,semester,supply_person,supplier_phone,book_id,book_name,press_name,press_time,book_price,total_num,state_id,order_discount,sell_discount,arrival_amount,arrived_amount,unarrived_amount,arrived_date,out_num,remain_num,Storage_location,storage_id,book_editor)";
                    sql += "values('" + Order_ID + "','" + Order_Date + "','" + Operate_Person + "','" + Date_Year + "','" + Semester + "','" + Supply_Person + "','" + Supplier_phone + "','" + Book_ID + "','" + Book_Name + "','" + Press_Name + "','" + Press_Time + "','" + Book_Price + "','" + Total_Num + "','" + State_ID + "','" + Order_Discount + "','" + Discount + "','" + Arrival_Amount + "','" + Arrived_Amount + "','" + Unarrived_Amount + "','" + Arrived_Date + "','0','" + Arrived_Amount + "','" + Storage_TextBox.Text + "','" + Storage_ID + "','" + book_editor + "')";
                    string sql5 = "select * from Storage_management_end";
                    int flag = 0;//判断是否与库存相同
                    SqlDataReader read3 = SQLHelper.ExecuteReader(sql5);
                    for (; read3.Read(); )
                    {
                        if (read3["Storage_location"].ToString() == Storage_TextBox.Text && read3["book_id"].ToString() == BookId_Text.Text)
                        {
                            SQLHelper.ExecuteNonQuery("update Storage_management_end set arrived_amount='" + Arrival_Amount + "'where book_id='" + BookId_Text.Text + "' and Storage_location='" + Storage_TextBox.Text + "'");
                            flag = 1;
                        }
                    }
                    string sql2 = "insert into Storage_management_end(order_id,order_date,operate_person,date_year,semester,supply_person,supplier_phone,book_id,book_name,press_name,press_time,book_price,total_num,state_id,order_discount,sell_discount,arrival_amount,arrived_amount,unarrived_amount,arrived_date,out_num,remain_num,Storage_location,storage_id,book_editor)";
                    sql2 += "values('" + Order_ID + "','" + Order_Date + "','" + Operate_Person + "','" + Date_Year + "','" + Semester + "','" + Supply_Person + "','" + Supplier_phone + "','" + Book_ID + "','" + Book_Name + "','" + Press_Name + "','" + Press_Time + "','" + Book_Price + "','" + Total_Num + "','" + State_ID + "','" + Order_Discount + "','" + Discount + "','" + Arrival_Amount + "','" + Arrived_Amount + "','" + Unarrived_Amount + "','" + Arrived_Date + "','0','" + Arrived_Amount + "','" + Storage_TextBox.Text + "','" + Storage_ID + "','" + book_editor + "')";
                    SQLHelper.ExecuteNonQuery(sql);
                    if (flag == 0)
                    {
                        SQLHelper.ExecuteNonQuery(sql2);
                        string sql3 = "delete from Storage_management_end where book_id in (select book_id from Storage_management_end  group  by  book_id having count(book_id) > 1)and arrived_date not in (select max(arrived_date) from   Storage_management_end   group by book_id   having count(book_id )>1)";
                        SQLHelper.ExecuteNonQuery(sql3);
                    }
                    string sql4 = "update finally_order_information set state_id='" + state_id + "'where id ='" + ID + "'";
                    SQLHelper.ExecuteNonQuery(sql4);
                }

                //显示本书的到货历史
                string sql1 = "select id,order_id,supply_person,book_id,book_name,press_name,book_price,order_discount,total_num,arrived_discount,arrival_amount,arrived_amount,unarrived_amount,arrived_date,state_id "
                +"from Storage_management "
                +"where book_id='" + book_id + "'and order_id='" + order_id + "'";
                DataTable dt = SQLHelper.GetDataTable(sql1);
                GridView2.DataSource = dt;
                GridView2.DataBind();
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    Label lb = GridView2.Rows[i].Cells[0].FindControl("Label2") as Label;
                    if (lb.Text == "0")
                    {
                        lb.Text = "未到货";
                    }
                    else if (lb.Text == "1")
                    {
                        lb.Text = "部分到货";
                    }
                    else
                    {
                        lb.Text = "全部到货";
                    }
                }
            }
            catch (Exception ex)
            {
                WebMessageBox.Show(ex.Message);
            }

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.Visible = false;
        GridView1.DataBind();
        GridView2.DataSource = null;
        GridView2.DataBind();
        Arrived_Text.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        GridView1.AllowSorting = true;
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        Button1_Click(sender, e);
    }


    public void GridViewBind()
    {
        SqlConnection sqlCon = new SqlConnection();
        sqlCon.ConnectionString = SQLHelper.connectionString;
        string c = DropSupplyPerson.SelectedValue;
        string c2 = TbBookName.Text;
        string SqlStr = "";
        if (CheckBox1.Checked == true)
            SqlStr = "select order_id,id,order_date,operate_person,date_year,semester,supply_person,book_id,book_name,press_name,book_price,total_num from finally_order_information where book_id like '%" + c + "%' and book_name like '%" + c2 + "%'";
        else
            SqlStr = "select order_id,id,order_date,operate_person,date_year,semester,supply_person,book_id,book_name,press_name,book_price,total_num from finally_order_information where book_id like '%" + c + "%' and book_name like '%" + c2 + "%' and state_id!='5'";
        SqlDataAdapter da = new SqlDataAdapter(SqlStr, sqlCon);
        DataSet ds = new DataSet();
        da.Fill(ds, "finally_order_information");
        DataView dv = ds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        dv.Sort = sort;
        GridView1.DataSource = dv;
        GridView1.DataBind();

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sPage = e.SortExpression;
        if (ViewState["SortOrder"].ToString() == sPage)
        {
            if (ViewState["OrderDire"].ToString() == "Desc")
                ViewState["OrderDire"] = "ASC";
            else
                ViewState["OrderDire"] = "Desc";
        }
        else
        {
            ViewState["SortOrder"] = e.SortExpression;
        }
        GridViewBind();
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