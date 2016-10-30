using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 教材申领_校审批 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //显示学院
            string sql = "select xsm from CODE_XSB";
            SqlDataReader read = SQLHelper.ExecuteReader(sql);
            while (read.Read())
            {
                DropDownList1.Items.Add(new ListItem(read[0].ToString()));
            }

            string coursesql = "select course_id,course_name from tbl_CourseInformation";
            SqlDataReader courseread = SQLHelper.ExecuteReader(coursesql);
            while (courseread.Read())
            {
                DropCourseId.Items.Add(new ListItem(courseread["course_id"].ToString()));
                DropCourseName.Items.Add(new ListItem(courseread["course_name"].ToString()));
            }

            this.WebPager1.SqlField = " id,teacher_college,apply_teacher_name,teacher_name,course_name,book_name,Campus ";
            this.WebPager1.TableName = " book_message ";
            this.WebPager1.orderByID = " id asc ";
            this.WebPager1.TblID = " id ";
            this.WebPager1.WhereClause = " and state_id=2 ";
        }
    }

    private void initData()
    {
        string tc = DropDownList1.SelectedValue;
        string ati = TextBox1.Text;
        string atn = TextBox2.Text;
        string ci = DropCourseId.SelectedValue;
        string cn = DropCourseName.SelectedValue;
        string ti = TBTeacherId.Text;
        string tn = TBTeacherName.Text;
        string bi = TBISBN.Text;
        string bn = TextBox5.Text;
        string cp = DropCampus.SelectedValue;

        this.WebPager1.SqlField = " id,teacher_college,apply_teacher_name,teacher_name,course_name,book_name,Campus ";
        this.WebPager1.TableName = " book_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
        + "and state_id=2 "
        + "and teacher_college like '%" + tc + "%' "
        + "and teacher_id like '%" + ti + "%' "
        + "and teacher_name like '%" + tn + "%' "
        + "and apply_teacher_id like '%" + ati + "%' "
        + "and apply_teacher_name like '%" + atn + "%' "
        + "and Campus like '%" + cp + "%' "
        + "and course_id like '%" + ci + "%' "
        + "and course_name like '%" + cn + "%' "
        + "and book_id like '%" + bi + "%' "
        + "and book_name like '%" + bn + "%'";
    }

    //查询
    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
    }
    
    //全部同意
    protected void Button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
            {
                SQLHelper.ExecuteNonQuery("update book_message set university_advise='同意',state_id=4, state=0 where id=" + GridView1.DataKeys[i].Value);
            }
        }
        initData();
    }

    //全部不同意
    protected void Button3_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
            {
                SQLHelper.ExecuteNonQuery("update book_message set university_advise='不同意',state_id=1 where id=" + GridView1.DataKeys[i].Value);
            }
        }
        initData();
    }

    //进入
    protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();;
        SaveID.Text = id;
        SqlDataReader read = SQLHelper.ExecuteReader("select * from book_message where id =" + id);
        for (; read.Read(); )
        {
            TextBox8.Text = read["teacher_college"].ToString();
            TextBox9.Text = read["teacher_id"].ToString();
            TextBox10.Text = read["teacher_name"].ToString();
            TextBox11.Text = read["course_id"].ToString();
            TextBox12.Text = read["course_name"].ToString();
            TextBox13.Text = read["book_id"].ToString();
            TextBox14.Text = read["book_name"].ToString();
            TextBox15.Text = read["press_name"].ToString();
            TextBox16.Text = read["press_time"].ToString();
            TextBox17.Text = read["press_date"].ToString();
            TextBox18.Text = read["book_editor"].ToString();
            TextBox19.Text = read["addition"].ToString();
            TextBox20.Text = read["university_advise"].ToString();
        }
    }

    //同意
    protected void Button4_Click(object sender, EventArgs e)
    {
        string ua;
        string id = SaveID.Text;
        if (TextBox20.Text == "")
        {
            ua = "同意";
        }
        else
        {
            ua = TextBox20.Text;
        }
        SQLHelper.ExecuteNonQuery("update book_message set university_advise='" + ua + "',state_id=4,state=0 where id=" + id);
        
        //清空表格
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
        TextBox18.Text = "";
        TextBox19.Text = "";
        TextBox20.Text = "";
        SaveID.Text = "";

        initData();
    }

    //不同意
    protected void Button5_Click(object sender, EventArgs e)
    {
        string id = SaveID.Text;
        if (TextBox10.Text == "")
        {
            WebMessageBox.Show("审批意见不能为空！");
            return;
        }
        SQLHelper.ExecuteNonQuery("update book_message set university_advise='" + TextBox20.Text + "',state_id=1 where id=" + id);

        //清空表格
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
        TextBox18.Text = "";
        TextBox19.Text = "";
        TextBox20.Text = "";
        SaveID.Text = "";

        initData();
    }

    //鼠标经过背景色改变
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

    //学院变了，课程编号和名称也要跟着变
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropCourseId.Items.Clear();
        DropCourseName.Items.Clear();
        DropCourseId.Items.Add("");
        DropCourseName.Items.Add("");
        string sql = "select course_id,course_name from tbl_CourseInformation where college= '" + DropDownList1.SelectedValue + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql);
        for (; read.Read(); )
        {
            DropCourseId.Items.Add(new ListItem(read[0].ToString()));
            DropCourseName.Items.Add(new ListItem(read[1].ToString()));
        }
    }

    //课程编号变，课程名称和学院就变
    protected void DropCourseId_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select college,course_name from tbl_CourseInformation where course_id= '" + DropCourseId.SelectedValue + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql);
        while (read.Read())
        {
            DropDownList1.SelectedValue=read[0].ToString();
            DropCourseName.SelectedValue = read[1].ToString();
        }
    }

    protected void DropCourseName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select college,course_id from tbl_CourseInformation where course_name= '" + DropCourseName.SelectedValue + "'";
        SqlDataReader read = SQLHelper.ExecuteReader(sql);
        while (read.Read())
        {
            DropDownList1.SelectedValue = read[0].ToString();
            DropCourseId.SelectedValue = read[1].ToString();
        }
    }
}