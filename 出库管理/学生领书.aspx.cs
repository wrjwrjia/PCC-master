using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 库存管理_学生领书 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //出版社
            string sql2 = "select press_name from press_message";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            while (read2.Read())
            {
                DropPressName.Items.Add(new ListItem(read2[0].ToString()));
            }

            if (!IsPostBack)
            {
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
    }


    private void initData()
    {
        string bi = TbBookId.Text;
        string bn = TbBookName.Text;
        string pn = DropPressName.SelectedValue;
        string pt = TbPressTime.Text;

        this.WebPager1.SqlField = " id,book_id,book_name,press_time,press_name,book_price,sell_discount,Storage_location,remain_num ";
        this.WebPager1.TableName = " Storage_management_end ";
        this.WebPager1.orderByID = " remain_num asc ";//按照剩余量排序
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = ""
            + " and book_id like '%" + bi + "%'"
            + " and book_name like '%" + bn + "%'"
            + " and press_name like '%" + pn + "%'"
            + " and press_time like '%" + pt + "%'"
            + " and state_id=1";
    }


    //查询
    protected void Button2_Click(object sender, EventArgs e)
    {
        initData();
    }


    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            double price = Convert.ToDouble(e.Row.Cells[6].Text);
            double discount = Convert.ToDouble(e.Row.Cells[7].Text) / 100;
            if (price == 0)
                e.Row.Cells[8].Text = "价格待定";
            else
                e.Row.Cells[8].Text = (price * discount).ToString();
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[1].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }


    //领取
    protected void  GetBook_Click(object sender, EventArgs e)
    {
        //判断教材数量合法与否
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
            {
                //领取前再查一遍库存剩余量
                string sql3 = "select remain_num from Storage_management_end where "
                + "id='" + GridView1.DataKeys[i].Value + "' "//统计这本书的需求册数(用BookID锁定)
                + "and state_id= 1";//代表还可以领
                int total_num=0;
                SqlDataReader reader3 = SQLHelper.ExecuteReader(sql3);
                while (reader3.Read())
                {
                    total_num = Convert.ToInt32(reader3[0].ToString());//计算总量
                    break;
                }

                int StoreNum = total_num;//当前最新剩余量
                TextBox tbgn = (TextBox)GridView1.Rows[i].FindControl("TbGetNum");
                int GetNum = Convert.ToInt32(tbgn.Text);
                if (StoreNum < GetNum || GetNum <= 0)//库存不能大于领取，领取不能为负
                    WebMessageBox.Show("领取教材数量不合法！");
            }
        }

        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
            {
                string id = GridView1.DataKeys[i].Value.ToString();//以id为准
                int gn = Convert.ToInt32((GridView1.Rows[i].FindControl("tbGetNum") as TextBox).Text);


                //读出写进历史中的关于教材的基本信息
                string StuID = "", StuName = "";
                string BookID="",BookName = "",Author = "", Publish = "" ;
                string UnitPrice="",Version = "",InfoDate="",campusName = "";
                string sell_price = "",sell_discount = "",Storage_location = "",get_num="";
                string sql1 = "select * "
                + "from Storage_management_end "
                + "where id='" + id + "' ";
                SqlDataReader read1 = SQLHelper.ExecuteReader(sql1);
                for (; read1.Read(); )
                {
                    BookID = read1["book_id"].ToString();
                    BookName = read1["book_name"].ToString();
                    Author = read1["book_editor"].ToString();
                    Publish = read1["press_name"].ToString();
                    UnitPrice = read1["book_price"].ToString();
                    Version = read1["press_time"].ToString();
                    sell_discount = read1["sell_discount"].ToString();
                    sell_price = (Convert.ToDouble(UnitPrice) * Convert.ToDouble(sell_discount) / 100).ToString();
                    Storage_location = read1["Storage_location"].ToString();
                    break;
                }
                get_num=gn.ToString();
                StuID = TbStuId.Text;
                StuName = TbStuName.Text;
                if (StuID == "" || StuName == "")
                    WebMessageBox.Show("请输入学生信息！");


                //对Storage_management_end处理
                    string sql = "select * "//1.找到这本书库存剩余量最小的库存，默认从库存量最小的地方取书
                    + "from Storage_management_end "
                    + "where "
                    + "id='" + id + "'";
                    SqlDataReader read = SQLHelper.ExecuteReader(sql);
                    while (read.Read())
                    {
                        string remain_num = read["remain_num"].ToString();
                        string out_num = read["out_num"].ToString();
                        //读取记录历史的价格方面的信息
                        if (Convert.ToInt32(remain_num) == gn)//2.判断库存是否足数
                        {
                            string str_out = Convert.ToString(Convert.ToInt32(out_num) + Convert.ToInt32(remain_num));//3.更新此最小库存
                            SQLHelper.ExecuteNonQuery("update Storage_management_end set remain_num=0,out_num='" + str_out + "',state_id=2 where id='" + id + "'");
                        }
                        else
                        {
                            string str_out = Convert.ToString(Convert.ToInt32(out_num) + Convert.ToInt32(gn));//3.更新此最小库存
                            string str_rmn = Convert.ToString(Convert.ToInt32(remain_num) - Convert.ToInt32(gn));//4.更新剩余量
                            SQLHelper.ExecuteNonQuery("update Storage_management_end set remain_num='" + str_rmn + "',out_num='" + str_out + "' where id='" + id + "'");
                        }

                        //记录历史，写入Abook_message_class_history
                        InfoDate = DateTime.Now.ToString("yyyyMMdd");// 2008-9-4时间
                        string sql3 = "insert into Abook_message_stu_history "
                            + "values('" + StuID + "','" + StuName + "','" + BookID + "','" + BookName + "',"
                            + "'" + Author + "','" + Publish + "','" + UnitPrice + "','" + Version + "','" + InfoDate + "','" + campusName + "','" + Storage_location + "',"
                            + "'" + sell_discount + "','" + sell_price + "','" + get_num + "')";
                        SQLHelper.ExecuteNonQuery(sql3);
                    }
                }
            }
        JScript.AjaxAlertAndLocationHref(this.Page, "出售成功", "学生领书.aspx");
    }
}