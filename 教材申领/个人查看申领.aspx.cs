using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 教材申领_个人查看申领 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string dy = Request.QueryString["dy"];
            string ti = Request.QueryString["ti"];
            string se = Request.QueryString["se"];
            string ci = Request.QueryString["ci"];

            //加载出版社名称
            string sql2 = "select press_name from press_message";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            while (read2.Read())
            {
                DropPressName.Items.Add(new ListItem(read2[0].ToString()));
            }

            TextBox2.Text = ti;
            TextBox4.Text = ci;
            this.WebPager1.SqlField = " id,apply_teacher_name,teacher_name,course_id,course_name,book_id,book_name,press_name,state_id ";
            this.WebPager1.TableName = " book_message ";
            this.WebPager1.orderByID = " id asc ";
            this.WebPager1.TblID = " id ";
            if (dy != null || ti != null || se != null || ci != null )
            {
                this.WebPager1.WhereClause = " "
                + "and date_year like '%" + dy + "%' "
                + "and teacher_id like '%" + ti + "%' "
                + "and semester like '%" + se + "%' "
                + "and course_id like '%" + ci + "%' ";
            }
        }
    }

    private void initData()
    {
        string ti = TextBox2.Text;
        string tn = TextBox3.Text;
        string ci = TextBox4.Text;
        string cn = TextBox5.Text;
        string bi = TBISBN.Text;
        string bn = TBBookName.Text;
        string pn = DropPressName.SelectedValue;
        string si = DropStateId.SelectedValue;
        switch (si)
        {
            case "已保存": si = "1"; break;
            case "已提交": si = "2"; break;
            case "校审批已通过": si = "3"; break;
        }

        this.WebPager1.SqlField = " id,apply_teacher_name,teacher_name,course_id,course_name,book_id,book_name,press_name,state_id ";
        this.WebPager1.TableName = " book_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
            + "and teacher_id like '%" + ti + "%' "
            + "and teacher_name like '%" + tn + "%' "
            + "and course_id like '%" + ci + "%' "
            + "and course_name like '%" + cn + "%'"
            + "and book_id like '%" + bi + "%' "
            + "and book_name like '%" + bn + "%' "
            + "and press_name like '%" + pn + "%' "
            + "and state_id <>5" //过去一个学期的state_id=5
            + "and state_id like '%" + si + "%' ";
    }

    //查看
    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
    }

    //进入
    protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            SqlDataReader read = SQLHelper.ExecuteReader("select * from book_message where id =" + id);
            for (; read.Read(); )
            {
                TextBox6.Text = read["teacher_college"].ToString();
                TextBox7.Text = read["teacher_id"].ToString();
                TextBox8.Text = read["teacher_name"].ToString();
                TextBox9.Text = read["course_id"].ToString();
                TextBox10.Text = read["course_name"].ToString();
                TextBox11.Text = read["book_id"].ToString();
                TextBox12.Text = read["book_name"].ToString();
                TextBox13.Text = read["press_name"].ToString();
                TextBox14.Text = read["press_time"].ToString();
                TextBox15.Text = read["press_date"].ToString();
                TextBox17.Text = read["book_editor"].ToString();
                string uab = read["university_advise"].ToString();
                string ua = "";
                string si = read["state_id"].ToString();
                if (uab == "" && si == "4")
                    ua = "同意";
                else
                    ua = uab;
                TextBox16.Text = ua;
            }
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
            switch (e.Row.Cells[8].Text)
            {
                case "1": e.Row.Cells[8].Text = "保存"; break;
                case "2": e.Row.Cells[8].Text = "提交"; break;
                case "4": e.Row.Cells[8].Text = "校通过"; break;
            }
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    } 

}