using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class 教材计划_形成订单 : System.Web.UI.Page
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

            //显示操作人
            string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
            string sql1 = "select * from tbl_UserInformation where user_login= '" + username + "'";
            SqlDataReader read = SQLHelper.ExecuteReader(sql1);
            for (; read.Read(); )
            {
                TextBox3.Text = read["teacher_name"].ToString();
            }
        }
    }

    protected void OK_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" || DropDownList1.SelectedValue == "" || DropDownList2.SelectedValue == "" || DropDownList3.SelectedValue == "" || TextBox8.Text == "")
        {
            WebMessageBox.Show("所有信息不得为空");
            return;
        }

        //上面已经填的信息不能修改
        TextBox1.Enabled = false;
        TextBox2.Enabled = false;
        TextBox3.Enabled = false;
        DropDownList1.Enabled = false;
        DropDownList2.Enabled = false;
        DropDownList3.Enabled = false;
        TextBox8.Enabled = false;

        string sql2 = "select id,book_id,book_name,plan_num,auto_num,total_num,total_price,book_price,press_time,press_name,campus from form_order where order_id='" + TextBox1.Text + "' and supply_person='" + DropDownList3.SelectedValue + "'";
        DataTable dt = SQLHelper.GetDataTable(sql2);
        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "id" };
        GridView1.DataBind();
        SqlDataReader readtp = SQLHelper.ExecuteReader(sql2);
        double totalprice = 0;
        while (readtp.Read())
        {
            totalprice += Convert.ToDouble(readtp["total_price"].ToString());
        }
        TotalPrice.Text = totalprice.ToString();

    }

    //订单中的删除
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            string id = (sender as Button).CommandArgument;

            string sql2 = "select demand_id from form_order where id=" + id;
            SqlDataReader read = SQLHelper.ExecuteReader(sql2);
            while (read.Read())
            {
                string demand_id = read["demand_id"].ToString();
                string sql3 = "update book_demand set state_id='0' where id='" + demand_id + "'";
                SQLHelper.ExecuteNonQuery(sql3);
            }
            string sql1 = "delete from form_order where id=" + id;
            SQLHelper.ExecuteNonQuery(sql1);

            string sql4 = "select id,book_id,book_name,plan_num,auto_num,total_num,total_price,book_price,press_time,press_name,campus from form_order where order_id='" + TextBox1.Text + "' and supply_person='" + DropDownList3.SelectedValue + "'";
            DataTable dt = SQLHelper.GetDataTable(sql4);
            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();

            Search_Click(sender, e);
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }

    //修改机动册数
    protected void Button2_Click(object sender, EventArgs e)
    {
        string id = (sender as Button).CommandArgument;
        TextBoxTemp.Text = id;
        TextBox5.Text = "";
    }

    //修改机动册数确定
    protected void ChangeOK_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox3");
                if (cbox.Checked)
                {
                    string id = GridView1.DataKeys[i].Value.ToString();
                    double auto_per = (Convert.ToDouble(TextBox5.Text)) * 0.01;//输入的内容转换成百分比，小数
                    string plan_num = "", total_num = "", book_price = "", total_price = "";
                    string sql1 = "select book_price,plan_num from form_order where id = " + id;
                    SqlDataReader reader = SQLHelper.ExecuteReader(sql1);
                    while (reader.Read())
                    {
                        book_price = reader["book_price"].ToString();
                        plan_num = reader["plan_num"].ToString();
                    }

                    //只入不舍
                    string auto_num = (Convert.ToInt32((Convert.ToInt32(plan_num)) * auto_per) + 1).ToString();
                    total_num = ((Convert.ToInt32(plan_num)) + (Convert.ToInt32(auto_num))).ToString();
                    total_price = ((Convert.ToDouble(total_num)) * (Convert.ToDouble(book_price))).ToString();
                    string sql = "update form_order set auto_num='" + auto_num + "',total_num='" + total_num + "',total_price='" + total_price + "' where id =" + id;
                    SQLHelper.ExecuteNonQuery(sql);
                }
            }
            string sql2 = "select id,book_id,book_name,plan_num,auto_num,total_num,total_price,book_price,press_time,press_name,campus from form_order where order_id='" + TextBox1.Text + "' and supply_person='" + DropDownList3.SelectedValue + "'";
            DataTable dt = SQLHelper.GetDataTable(sql2);
            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();

            SqlDataReader readtp = SQLHelper.ExecuteReader(sql2);
            double totalprice = 0;
            while (readtp.Read())
            {
                totalprice += Convert.ToDouble(readtp["total_price"].ToString());
            }
            TotalPrice.Text = totalprice.ToString();

            TextBox5.Text = "";
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {

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

    //选择教材模糊查询
    protected void Search_Click(object sender, EventArgs e)
    {
        string book_id;
        string book_name;
        string press_name;
        string press_time;
        book_id = TextBox9.Text;
        book_name = TextBox10.Text;
        press_name = TextBox11.Text;
        press_time = TextBox12.Text;
        string sql = "select id,book_id,book_name,book_editor,book_price,book_number,campus,press_name,press_time from book_demand"
        +" where book_id like '%" + book_id + "%'"
        +" and book_name like '%" + book_name + "%'"
        +" and press_name like '%" + press_name + "%'"
        +"and press_time like '%" + press_time + "%'"
        +"and date_year = '" + DropDownList1.SelectedValue + "'"
        +"and semester= '" + DropDownList2.SelectedValue + "'"
        +"and state_id=0";
        DataTable dt = SQLHelper.GetDataTable(sql);
        GridView2.DataSource = dt;
        GridView2.DataKeyNames = new string[] { "id" };
        GridView2.DataBind();
    }

    //选择教材分批
    protected void Divide_Click(object sender, EventArgs e)
    {
        string id = (sender as Button).CommandArgument;
        string sql = "select id,book_id,book_name,press_name,press_time,book_number from book_demand where id='" + id + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql);
        while (read.Read())
        {
            ISBN.Text = read["book_id"].ToString();
            BookName.Text = read["book_name"].ToString();
            PressName.Text = read["press_name"].ToString();
            PressTime.Text = read["press_time"].ToString();
            Number.Text = read["book_number"].ToString();
        }
        SaveID.Text = id;
    }

    //分批完成
    protected void DivideOK_Click(object sender, EventArgs e)
    {
        try
        {
            string[] num = new string[7];
            //判断各批次和是否与总量符合
            int addnum = 0;
            num[0] = Regex.Replace(FirstNum.Text, @" ", "");
            num[1] = Regex.Replace(SecondNum.Text, @" ", "");
            num[2] = Regex.Replace(ThirdNum.Text, @" ", "");
            num[3] = Regex.Replace(FourthNum.Text, @" ", "");
            num[4] = Regex.Replace(FifthNum.Text, @" ", "");
            num[5] = Regex.Replace(SixthNum.Text, @" ", "");
            num[6] = Regex.Replace(SeventhNum.Text, @" ", "");
            for (int i = 0; i < 7; i++)
            {
                if(num[i]!="")
                    addnum += Convert.ToInt32(num[i]);
            }
            if (addnum != Convert.ToInt32(Number.Text))
                WebMessageBox.Show("各批次累计和与总量不符！");

            //存储分批后的信息
            string date_year = "";
            string semester = "";
            string book_id = "";
            string book_name = "";
            string book_editor = "";
            string book_price = "";
            string campus = "";
            string press_name = "";
            string press_time = "";
            string id = SaveID.Text;
            SqlDataReader reader = SQLHelper.ExecuteReader("select * from book_demand where id='" + id + "'");
            while (reader.Read())
            {
                date_year = reader["date_year"].ToString();
                semester = reader["semester"].ToString();
                book_id = reader["book_id"].ToString();
                book_name = reader["book_name"].ToString();
                book_editor = reader["book_editor"].ToString();
                book_price = reader["book_price"].ToString();
                campus = reader["campus"].ToString();
                press_name = reader["press_name"].ToString();
                press_time = reader["press_time"].ToString();
            }

            //分条写入信息，并将之前的记录删除
            for (int j = 0; j < 7; j++)
            {
                if (num[j] != "")
                {
                    string sql = "insert into book_demand(date_year,semester,book_id,book_name,book_editor,book_price,campus,press_name,press_time,book_number,state_id)";
                    sql += "values('" + date_year + "','" + semester + "','" + book_id + "','" + book_name + "','" + book_editor + "','" + book_price + "','" + campus + "','" + press_name + "','" + press_time + "','" + num[j].ToString() + "',0)";
                    SQLHelper.ExecuteNonQuery(sql);
                }
            }
            SQLHelper.ExecuteNonQuery("delete from book_demand where id=" + id);

            //所有TextBox清空
            ISBN.Text = "";
            BookName.Text = "";
            PressName.Text = "";
            PressTime.Text = "";
            Number.Text = "";
            FirstNum.Text = "";
            SecondNum.Text = "";
            ThirdNum.Text = "";
            FourthNum.Text = "";
            FifthNum.Text = "";
            SixthNum.Text = "";
            SeventhNum.Text = "";

            Search_Click(sender, e);
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }
    
    //保存
    protected void Save_Click(object sender, EventArgs e)
    {
        //得到订单相关信息
        string order_id = "", order_date = "", operate_person = "", date_year = "", semester = "", supply_person = "", supplier_phone = "";
        string book_id = "", book_name = "", book_editor = "", press_time = "", press_name = "", book_price = "", plan_num = "", auto_num = "", total_num = "", total_price = "", campus = "", done_time = DateTime.Now.ToString("yyyyMMdd"); ;
        order_id = TextBox1.Text;
        order_date = TextBox2.Text;
        operate_person = TextBox3.Text;
        date_year = DropDownList1.SelectedValue;
        semester = DropDownList2.SelectedValue;
        supply_person = DropDownList3.SelectedValue;
        supplier_phone = TextBox8.Text;
        string sql1 = "select * from form_order where date_year='" + date_year + "' and semester='" + semester + "' and order_id='" + order_id + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql1);
        while (read.Read())
        {
            book_id = read["book_id"].ToString();
            book_name = read["book_name"].ToString();
            press_time = read["press_time"].ToString();
            press_name = read["press_name"].ToString();
            book_price = read["book_price"].ToString();
            book_editor = read["book_editor"].ToString();
            plan_num = read["plan_num"].ToString();
            auto_num = read["auto_num"].ToString();
            total_num = read["total_num"].ToString();
            total_price = read["total_price"].ToString();
            campus = read["campus"].ToString();


            string sql = "insert into finally_order_information (order_id,order_date,operate_person,date_year,semester,supply_person,supplier_phone,book_id,book_name,book_editor,book_price,press_name,press_time,plan_num,auto_num,total_num,total_price,state_id,campus,done_time)";
            sql += "values('" + order_id + "','" + order_date + "','" + operate_person + "','" + date_year + "','" + semester + "','" + supply_person + "','" + supplier_phone + "','" + book_id + "','" + book_name + "','" + book_editor + "','" + book_price + "','" + press_name + "','" + press_time + "','" + plan_num + "','" + auto_num + "','" + total_num + "','" + total_price + "','1','" + campus + "','" + done_time + "')";
            SQLHelper.ExecuteNonQuery(sql);

            string sql2 = "delete from form_order where order_id='" + order_id + "' and date_year='" + date_year + "' and semester='" + semester + "'";
            SQLHelper.ExecuteNonQuery(sql2);

        }
        JScript.AjaxAlertAndLocationHref(this.Page, "订单保存成功", "查看订单.aspx");
    }



    //选择教材确定
    protected void Choose_Click(object sender, EventArgs e)
    {
        try
        {
            //得到订单相关信息
            string order_id = "", order_date = "", operate_person = "", date_year = "", semester = "", supply_person = "", supplier_phone = "";// book_id = "";
            order_id = TextBox1.Text;
            order_date = TextBox2.Text;
            operate_person = TextBox3.Text;
            date_year = DropDownList1.SelectedValue;
            semester = DropDownList2.SelectedValue;
            supply_person = DropDownList3.SelectedValue;
            supplier_phone = TextBox8.Text;

            //将提供商选中的教材等相关信息  保存到 form_order 表中。。。。。。
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView2.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked)
                {
                    SqlDataReader reader1 = SQLHelper.ExecuteReader("select * from book_demand where id='" + GridView2.DataKeys[i].Value.ToString() + "'");
                    string id = "", book_id = "", book_name = "", book_editor = "", book_price = "", campus = "", press_name = "", press_time = "", plan_num = "";
                    while (reader1.Read())
                    {
                        id = reader1["id"].ToString();
                        book_id = reader1["book_id"].ToString();
                        book_name = reader1["book_name"].ToString();
                        book_editor = reader1["book_editor"].ToString();
                        book_price = reader1["book_price"].ToString();
                        campus = reader1["campus"].ToString();
                        press_name = reader1["press_name"].ToString();
                        press_time = reader1["press_time"].ToString();
                        plan_num = reader1["book_number"].ToString();
                        double total_price = (Convert.ToDouble(book_price)) * (Convert.ToDouble(plan_num));

                        string sql = "insert into form_order (order_id,order_date,operate_person,date_year,semester,supply_person,supplier_phone,book_id,book_name,book_editor,book_price,press_name,press_time,plan_num,auto_num,total_num,total_price,campus,demand_id)";
                        sql += "values('" + order_id + "','" + order_date + "','" + operate_person + "','" + date_year + "','" + semester + "','" + supply_person + "','" + supplier_phone + "','" + book_id + "','" + book_name + "','" + book_editor + "','" + book_price + "','" + press_name + "','" + press_time + "','" + plan_num + "','0','" + plan_num + "','" + total_price + "','" + campus + "','" + id + "')";
                        SQLHelper.ExecuteNonQuery(sql);

                        string sql1 = "update book_demand set state_id='1' where id=" + id;
                        SQLHelper.ExecuteNonQuery(sql1);
                    }
                }
            }

            string sql2 = "select id,book_id,book_name,plan_num,auto_num,total_num,total_price,book_price,press_time,press_name,campus from form_order where order_id='" + TextBox1.Text + "' and supply_person='" + DropDownList3.SelectedValue + "'";
            DataTable dt = SQLHelper.GetDataTable(sql2);
            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();

            SqlDataReader readtp = SQLHelper.ExecuteReader(sql2);
            double totalprice=0;
            while (readtp.Read())
            {
                totalprice += Convert.ToDouble(readtp["total_price"].ToString());
            }
            TotalPrice.Text = totalprice.ToString();
            Search_Click(sender, e);
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }

    //取消
    protected void Cancel_Click(object sender, EventArgs e)
    {
        string order_id = "", date_year = "", semester = "", supply_person = "";
        string book_id = "", campus = "";
        order_id = TextBox1.Text;
        date_year = DropDownList1.SelectedValue;
        semester = DropDownList2.SelectedValue;
        supply_person = DropDownList3.SelectedValue;
        string sql1 = "select book_id,campus from form_order where date_year='" + date_year + "' and semester='" + semester + "' and order_id='" + order_id + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql1);
        while (read.Read())
        {
            book_id = read["book_id"].ToString();
            campus = read["campus"].ToString();

            string sql = "update book_demand set state_id=0 where date_year='" + date_year + "' and semester='" + semester + "' and book_id='" + book_id + "' and campus='" + campus + "'";
            SQLHelper.ExecuteNonQuery(sql);

            string sql2 = "delete from form_order where order_id='" + order_id + "' and date_year='" + date_year + "' and semester='" + semester + "'";
            SQLHelper.ExecuteNonQuery(sql2);

        }
        JScript.AjaxAlertAndLocationHref(this.Page, "已取消订单", "查看订单.aspx");
    }
}