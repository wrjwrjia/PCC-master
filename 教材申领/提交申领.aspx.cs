using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class 教材申领_提交申领 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //加载出版社名称
            string sql2 = "select press_name from press_message";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            while (read2.Read())
            {
                DropPressName.Items.Add(new ListItem(read2[0].ToString()));
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
        //这个老师只能看到自己保存的信息
        string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
        string sql = "select teacher_id from tbl_UserInformation where user_login= '" + username + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql);
        string ati = "";
        for (; read.Read(); )
        {
            ati = read[0].ToString();
        }

        string ti = TextBox1.Text;
        string tn = TextBox2.Text;
        string ci = TextBox3.Text;
        string cn = TextBox4.Text;
        string bi = TextBox5.Text;
        string bn = TextBox6.Text;
        this.WebPager1.SqlField = " id,teacher_id,teacher_name,course_id,course_name,book_id,book_name,state_id ";
        this.WebPager1.TableName = " book_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
            + "and teacher_id like '%" + ti + "%' "
            + "and apply_teacher_id like '%" + ati + "%' "
            + "and teacher_name like '%" + tn + "%' "
            + "and course_id like '%" + ci + "%' "
            + "and course_name like '%" + cn + "%' "
            + "and book_id like'%" + bi + "%' "
            + "and book_name like'%" + bn + "%' "
            + "and state_id=1";
    }


    //查询
    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
        ////对状态一栏的设置
        //for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        //{
        //    Label lb1 = this.GridView1.Rows[i].Cells[0].FindControl("L15") as Label;
        //    switch (lb1.Text)
        //    {
        //        case "1": lb1.Text = "保存"; break;
        //        case "2": lb1.Text = "提交"; break;
        //        case "4": lb1.Text = "审批通过"; break;
        //    }
        //}
        ////对校审批一栏的设置
        //for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        //{
        //    Label lb1 = this.GridView1.Rows[i].Cells[0].FindControl("L15") as Label;
        //    Label lb2 = this.GridView1.Rows[i].Cells[0].FindControl("L16") as Label;
        //    if (lb1.Text == "保存")
        //    {
        //        if (lb2.Text == "")
        //        {
        //            lb2.Text = "未审批";
        //        }
        //        else
        //        {
        //            GridView1.Rows[i].Cells[5].ForeColor = System.Drawing.Color.Red;
        //        }
        //    }
        //    if (lb1.Text == "提交")
        //        lb2.Text = "未审批";
        //    else if (lb1.Text == "审批通过" && lb2.Text == "")
        //        lb2.Text = "同意";
    }

    //进入
    protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SaveID.Text = id;
        SqlDataReader read = SQLHelper.ExecuteReader("select * from book_message where id =" + id);
        for (; read.Read(); )
        {
            TextBox9.Text = read["teacher_id"].ToString();
            TextBox10.Text = read["teacher_name"].ToString();
            TextBox11.Text = read["course_id"].ToString();
            TextBox12.Text = read["course_name"].ToString();
            TextBox13.Text = read["book_id"].ToString();
            TextBox14.Text = read["book_name"].ToString();
            DropPressName.SelectedValue = read["press_name"].ToString();
            DropPressTime.SelectedValue = read["press_time"].ToString();
            TBPressDate.Text = read["press_date"].ToString();
            TextBox18.Text = read["book_editor"].ToString();
            TextBox19.Text = read["addition"].ToString();
            TBUniversityAdvise.Text = read["university_advise"].ToString();
        }
    }

    //提交
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (TextBox9.Text != "" && TextBox10.Text != "" && TextBox11.Text != "" 
            && TextBox12.Text != "" && TextBox14.Text != "" && DropPressName.SelectedValue != "" 
            && DropPressTime.SelectedValue != "" && TBPressDate.Text != "" && TextBox18.Text != "")
        {
            string ti = TextBox9.Text;
            string tn = TextBox10.Text;
            string ci = TextBox11.Text;
            string cn = TextBox12.Text;
            string bi = TextBox13.Text;
            string bn = TextBox14.Text;
            string pn = DropPressName.SelectedValue;
            string pt = DropPressTime.SelectedValue;
            string pd = TBPressDate.Text;
            string be = TextBox18.Text;
            string ad = TextBox19.Text;
            string id = SaveID.Text;
            string sql = "update book_message set course_id='" + ci + "',"
            +"course_name='" + cn + "',book_id='" + bi + "',book_name='" + bn + "',press_name='" + pn + "',"
            + "press_time='" + pt + "',press_date='" + pd + "',book_editor='" + be + "',state_id=2 where id =" + id;
            SQLHelper.ExecuteNonQuery(sql);

            //清空表格
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
            TextBox13.Text = "";
            TextBox14.Text = "";
            DropPressName.SelectedValue = "";
            DropPressTime.SelectedValue = "";
            TBPressDate.Text = "";
            TextBox18.Text = "";
            TextBox19.Text = "";

            //刷新未提交的表单
            initData();
        }
        else
        {
            WebMessageBox.Show("请输入必要信息！");
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
            ////设置状态栏颜色及内容
            //if (e.Row.Cells[5].Text == "1")
            //{
            //    e.Row.Cells[5].Text = "保存";
            //}
            //for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            //{
            //    Label lb1 = this.GridView1.Rows[i].Cells[0].FindControl("L15") as Label;
            //    switch (lb1.Text)
            //    {
            //        case "1": lb1.Text = "保存"; break;
            //        case "2": lb1.Text = "提交"; break;
            //        case "4": lb1.Text = "审批通过"; break;
            //    }
            //}
            ////对校审批一栏的设置
            //for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            //{
            //    Label lb1 = this.GridView1.Rows[i].Cells[0].FindControl("L15") as Label;
            //    Label lb2 = this.GridView1.Rows[i].Cells[0].FindControl("L16") as Label;
            //    if (lb1.Text == "保存")
            //    {
            //        if (lb2.Text == "")
            //        {
            //            lb2.Text = "未审批";
            //        }
            //        else
            //        {
            //            GridView1.Rows[i].Cells[5].ForeColor = System.Drawing.Color.Red;
            //        }
            //    }
            //    if (lb1.Text == "提交")
            //        lb2.Text = "未审批";
            //    else if (lb1.Text == "审批通过" && lb2.Text == "")
            //        lb2.Text = "同意";
            //}
        
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[1].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }

    //提交
    protected void Assign_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
            {
                string sql = "update book_message set state_id=2 where id=" + GridView1.DataKeys[i].Value;
                SQLHelper.ExecuteNonQuery(sql);
            }
        }
        initData();
    }
}