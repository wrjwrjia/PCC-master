using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class 库存管理_教师领书 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //学院
            string sql = "select xsm from CODE_XSB";
            SqlDataReader read = SQLHelper.ExecuteReader(sql);
            while (read.Read())
            {
                DropCollege.Items.Add(new ListItem(read[0].ToString()));
            }

            //出版社
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
                this.WebPager1.WhereClause = " and 2<1 ";
            }
        }
    }

    private void initData()
    {
        string ti = TbTeacherId.Text;
        string tn = TbTeacherName.Text;
        string tc = DropCollege.SelectedValue;
        string bi = TbBookId.Text;
        string bn = TbBookName.Text;
        string pn = DropPressName.SelectedValue;

        this.WebPager1.SqlField = " id,teacher_id,teacher_name,teacher_college,book_id,book_name,book_editor,press_name ";
        this.WebPager1.TableName = " Abook_message ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = " "
            + "and teacher_id like '%" + ti + "%' "
            + "and teacher_name like '%" + tn + "%' "
            + "and teacher_college like '%" + tc + "%' "
            + "and book_id like '%" + bi + "%' "
            + "and book_name like '%" + bn + "%' "
            + "and press_name like '%" + pn + "%' "
            + "and get_id = 0";//仅显示为领取的书
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        initData();
    }


    //领取
    protected void Button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox3");
            if (cbox.Checked)
            {
                //领取前再查一遍库存剩余量
                string sql3 = "select remain_num from Storage_management_end where "
                + "book_id='" + GridView1.Rows[i].Cells[5].Text.ToString() + "' "//统计这本书的需求册数(用BookID锁定)
                + "and state_id= 1";//代表还可以领
                int total_num = 0;
                SqlDataReader reader3 = SQLHelper.ExecuteReader(sql3);
                while (reader3.Read())
                {
                    total_num += Convert.ToInt32(reader3[0].ToString());//计算总量
                }

                int StoreNum = total_num;//当前最新剩余量
                if (total_num <= 0)//库存不能大于领取，但是领取可以大于需求册数
                    WebMessageBox.Show("" + GridView1.Rows[i].Cells[5].Text.ToString() + "《" + GridView1.Rows[i].Cells[6].Text.ToString() + "》库存不足！");
            }
        }

        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox2 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox3");
            if (cbox2.Checked)
            {
                //库存的改变
                string bi = GridView1.Rows[i].Cells[5].Text.ToString();
                string sql2 = "select remain_num,out_num,id from Storage_management_end where "
                    + "remain_num = (select min(remain_num)from Storage_management_end where remain_num <> 0 and book_id='" + bi + "')"
                    + "and book_id='" + bi + "'";
                SqlDataReader read1 = SQLHelper.ExecuteReader(sql2);
                string r = "";
                string o = "";
                string id = "";
                while (read1.Read())
                {
                    r = read1["remain_num"].ToString();
                    o = read1["out_num"].ToString();
                    id = read1["id"].ToString();
                }
                if (Convert.ToDouble(r) > 0)
                {
                    string R = (Convert.ToDouble(r) - 1).ToString();//R为剩余教材数量
                    string O = (Convert.ToDouble(o) + 1).ToString();//O为出库教材数量
                    SQLHelper.ExecuteNonQuery("update Storage_management_end set remain_num='" + R + "',out_num='" + O + "'  where id=" + id);
                    if(R=="0")
                        SQLHelper.ExecuteNonQuery("update Storage_management_end set state_id=2 where id=" + id);
                }
                else
                {
                    WebMessageBox.Show("名称为" + bi + "的书库存为零！");
                }

                //记录历史到Abook_message_history
                SQLHelper.ExecuteNonQuery("update Abook_message set get_id=1 where id=" + GridView1.DataKeys[i].Value);
                string num = "select * from Abook_message where teacher_id= '" + GridView1.Rows[i].Cells[2].Text.ToString() + "' and book_id='" + bi + "'";
                SqlDataReader read = SQLHelper.ExecuteReader(num);
                while (read.Read())
                {
                    string year = read["year"].ToString();
                    string semester = read["semester"].ToString();
                    string course_id = read["course_id"].ToString();
                    string course_name = read["course_name"].ToString();
                    string teacher_id = read["teacher_id"].ToString();
                    string teacher_name = read["teacher_name"].ToString();
                    string teacher_college = read["teacher_college"].ToString();
                    string teacher_major = read["teacher_major"].ToString();
                    string book_id = read["book_id"].ToString();
                    string book_name = read["book_name"].ToString();
                    string book_catagory = read["book_catagory"].ToString();
                    string book_editor = read["book_editor"].ToString();
                    string book_price = read["book_price"].ToString();
                    string press_name = read["press_name"].ToString();
                    string press_time = read["press_time"].ToString();
                    string press_date = read["press_date"].ToString();
                    string ct = DateTime.Now.ToString("yyyyMMdd");// 2008-9-4时间                       
                    string sql1 = "insert into Abook_message_history"
                        + "(year,semester,course_id,course_name,"
                        + "teacher_id,teacher_name,teacher_college,teacher_major,"
                        + "book_id,book_name,book_catagory,book_editor,book_price,"
                        + "press_name,press_time,press_date,get_time)"
                        + "values('" + year + "','" + semester + "','" + course_id + "','" + course_name + "',"
                        + "'" + teacher_id + "','" + teacher_name + "','" + teacher_college + "','" + teacher_major + "',"
                        + "'" + book_id + "','" + book_name + "','" + book_catagory + "','" + book_editor + "','" + book_price + "',"
                        + "'" + press_name + "','" + press_time + "','" + press_date + "','" + ct + "')";
                    SQLHelper.ExecuteNonQuery(sql1);
                }
            }
            JScript.AjaxAlertAndLocationHref(this.Page, "领取成功", "教师领书.aspx");
        }
    }


    //鼠标经过背景色改变
    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");

            //对于Grideview中库存册数的设置
            string bi = e.Row.Cells[5].Text;//按照book_id来找
            string sql3 = "select remain_num from Storage_management_end where "
                + "book_id='" + bi + "' "//统计这本书的需求册数(用BookID锁定)
                + "and state_id= 1";//代表还可以领
            int total_num = 0;
            SqlDataReader reader3 = SQLHelper.ExecuteReader(sql3);
            while (reader3.Read())
            {
                total_num += Convert.ToInt32(reader3[0].ToString());//计算总量
            }
            e.Row.Cells[9].Text = total_num.ToString();
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[1].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }
}