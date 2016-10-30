using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class 教材选用_提交登记 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //出版社DropDownList设置
            string sql0 = "select press_name from press_message";
            SqlDataReader read0 = SQLHelper.ExecuteReader(sql0);
            while (read0.Read())
            {
                DropDownList1.Items.Add(new ListItem(read0[0].ToString()));
            }

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
        string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
        string sql1 = "select * from tbl_UserInformation where user_login= '" + username + "'";
        SqlDataReader read1 = SQLHelper.ExecuteReader(sql1);
        string ti = "";
        for (; read1.Read(); )
        {
            ti = read1["teacher_id"].ToString();
        }

        this.WebPager1.SqlField = " id,course_id,course_name,book_id,book_name,state_id,college_advise,university_advise ";
        this.WebPager1.TableName = " Rbook_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
            + "and teacher_id like '%" + ti + "%' "
            + "and state_id=1 "
            + "and course_id like '%" + TextBox1.Text + "%' "
            + "and course_name like '%" + TextBox2.Text + "%' "
            + "and book_id like '%" + TextBox3.Text + "%' "
            + "and book_name like '%" + TextBox4.Text + "%' ";
    }

    //查询
    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
    }

    //详细
    protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string state_id = "";
        string book_catagory = "";
        string book_award = "";
        string asi = "";

        SqlDataReader read = SQLHelper.ExecuteReader("select * from Rbook_message where id =" + GridView1.DataKeys[e.RowIndex].Value.ToString());
        for (; read.Read(); )
        {
            TextBox5.Text = read["teacher_college"].ToString();
            TextBox6.Text = read["teacher_major"].ToString();
            TextBox7.Text = read["teacher_id"].ToString();
            TextBox8.Text = read["teacher_name"].ToString();
            TextBox9.Text = read["phone_number"].ToString();
            TextBox10.Text = read["course_id"].ToString();
            TextBox11.Text = read["course_name"].ToString();
            TextBox12.Text = read["book_id"].ToString();
            TextBox13.Text = read["book_name"].ToString();
            book_catagory = read["book_catagory"].ToString();
            TextBox14.Text = read["book_editor"].ToString();
            TextBox15.Text = read["book_price"].ToString();
            DropDownList1.SelectedValue = read["press_name"].ToString();
            TextBox16.Text = read["press_time"].ToString();
            TextBox17.Text = read["press_date"].ToString();
            book_award = read["book_award"].ToString();
            state_id = read["state_id"].ToString();
            asi = read["add_state_id"].ToString();
            TextBox19.Text = read["addition"].ToString();
            TextBox21.Text = read["college_advise"].ToString();
            TextBox22.Text = read["university_advise"].ToString();
        }

        //book_catagory
        if (book_catagory == "出版教材")
        {
            RadioButtonList1.SelectedIndex = 0;
        }
        else if (book_catagory == "自编讲义")
        {
            RadioButtonList1.SelectedIndex = 1;
        }
        else RadioButtonList1.SelectedIndex = 2;

        //book_award
        string[] ba = book_award.Split(' ');
        for (int i = 0; i < ba.Length; i++)
        {
            if (ba[i] == "国家级获奖")
                CheckBox1.Checked = true;
            if (ba[i] == "国家级立项/规划")
                CheckBox2.Checked = true;
            if (ba[i] == "省（部）级获奖")
                CheckBox3.Checked = true;
            if (ba[i] == "省（部）级立项/规划")
                CheckBox4.Checked = true;
            if (ba[i] == "校级出版")
                CheckBox5.Checked = true;
            if (ba[i] == "校级讲义")
                CheckBox6.Checked = true;
            if (ba[i] == "21世纪")
                CheckBox7.Checked = true;
            if (ba[i] == "学校指导委员会推荐")
                CheckBox8.Checked = true;
            if (ba[i] == "统编")
                CheckBox9.Checked = true;
            if (ba[i] == "其他")
                CheckBox10.Checked = true;
        }
        SaveID.Text = GridView1.DataKeys[e.RowIndex].Value.ToString();
    }
    
    //提交
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBox5.Text == "" || TextBox6.Text == "" || TextBox7.Text == "" || TextBox8.Text == "" || TextBox9.Text == "" || TextBox10.Text == "" || TextBox11.Text == "" || TextBox13.Text == "" || TextBox14.Text == "" )
            {
                if (RadioButtonList1.SelectedIndex != 1)
                {
                    WebMessageBox.Show("除定价外，其他的信息必填，不能为空！");
                    return;
                }
                else if (TextBox12.Text == "" || TextBox15.Text == "" || DropDownList1.SelectedValue == "")
                {
                    WebMessageBox.Show("请输入必要信息!");
                }
            }
            
            if (TextBox10.Text == " ")
                TextBox10.Text = "价格未定";

            string book_catagory;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                book_catagory = "出版教材";
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                book_catagory = "自编讲义";
            }
            else
            {
                book_catagory = "翻印教材";
            }

            string book_award = "";
            if (CheckBox1.Checked)
                book_award += "国家级获奖  ";
            if (CheckBox2.Checked)
                book_award += "国家级立项/规划  ";
            if (CheckBox3.Checked)
                book_award += "省（部）级获奖  ";
            if (CheckBox4.Checked)
                book_award += "省（部）级立项/规划  ";
            if (CheckBox5.Checked)
                book_award += "校级出版  ";
            if (CheckBox6.Checked)
                book_award += "校级讲义  ";
            if (CheckBox7.Checked)
                book_award += "21世纪  ";
            if (CheckBox8.Checked)
                book_award += "学校指导委员会推荐  ";
            if (CheckBox9.Checked)
                book_award += "统编  ";
            if (CheckBox10.Checked)
                book_award += "其他  ";
            string sql = "update Rbook_message set teacher_college='" + TextBox5.Text + "',teacher_major='" + TextBox6.Text + "',"
                                +"teacher_id='"+TextBox7.Text+"',teacher_name='" + TextBox8.Text + "',phone_number='" + TextBox9.Text + "',"
                                +"course_id='" + TextBox10.Text + "',course_name='" + TextBox11.Text + "',"
                                +"book_id='" + TextBox12.Text + "',book_name='" + TextBox13.Text + "',"
                                +"book_catagory='" + book_catagory + "',book_editor='" + TextBox14.Text + "',"
                                +"book_price='" + TextBox15.Text + "',press_name='" + DropDownList1.SelectedValue + "',press_time='" + TextBox16.Text + "',"
                                +"book_award='" + book_award + "',press_date='"+TextBox17.Text+"',"
                                +"addition='"+TextBox19.Text+"',state_id=2 where id =" + SaveID.Text;
            SQLHelper.ExecuteNonQuery(sql);

            //所有数据都清空
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
            TextBox13.Text = "";
            TextBox14.Text = "";
            TextBox15.Text = "";
            TextBox16.Text = "";
            TextBox17.Text = "";
            TextBox19.Text = "";
            TextBox21.Text = "";
            TextBox22.Text = "";
            RadioButtonList1.SelectedIndex = 0;
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            CheckBox6.Checked = false;
            CheckBox7.Checked = false;
            CheckBox8.Checked = false;
            CheckBox9.Checked = false;
            CheckBox10.Checked = false;
            DropDownList1.SelectedValue = "";

            initData();
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }
    //鼠标经过背景色改变
    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            if (e.Row.Cells[5].Text == "1" )
            {
                e.Row.Cells[5].Text = "保存";
            }
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }
}