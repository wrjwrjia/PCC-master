using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class 库存管理_班级领书 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        this.WebPager1.SqlField = " id,ClassName,BookID,BookName,Publish,Version ";
        this.WebPager1.TableName = " Abook_message_stu ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.WhereClause = ""
            + " and id in (select max(id) from Abook_message_stu group by BookID)"//只显示不同BookID的数据，重复的合并
            + " and ClassName = '" + Class.Text + "'"
            + " and get_id=0 ";
    }


    //查询（精确查询，只能看该班的信息)
    //读Abook_message_stu，其中记录该班级的书籍需求，仅显示未领的书）
    protected void Search_Click(object sender, EventArgs e)
    {
        if (Class.Text == "")
            WebMessageBox.Show("请输入班号！");
        initData();
    }



    //鼠标经过背景色改变,GridView的内容设置
    protected void GvDataType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");

            //对于Gridview中需求册数的设置
            string sql1 = "select BookID,Publish,Version from Abook_message_stu where "
            +"id='" + (e.Row.Cells[6].FindControl("lbId") as Label).Text.ToString() + "' "
            +"and get_id=0";//查找不同的书籍
            SqlDataReader reader1 = SQLHelper.ExecuteReader(sql1);
            string bi = "", pu = "", vs = "";
            while (reader1.Read())
            {
                bi = reader1["BookID"].ToString();//得到这本书的BookID
                pu = reader1["Publish"].ToString();
                vs = reader1["Version"].ToString();
                string sql2 = "select count(*) from Abook_message_stu where "
                + "BookID='" + bi + "'"
                + "and ClassName='" + Class.Text + "' "
                + "and get_id=0 "
                + "and Publish='" + pu + "' " 
                + "and Version='" + vs + "'";//统计这本书的需求册数(用BookID和ClassName锁定)
                SqlDataReader reader2 = SQLHelper.ExecuteReader(sql2);
                while (reader2.Read())
                {
                    e.Row.Cells[8].Text = reader2[0].ToString();
                }
            }

            //对于Grideview中库存册数的设置
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

            //对GridView中领取数量TextBox内容的设置
            int nn=Convert.ToInt32(e.Row.Cells[8].Text);
            int sn=Convert.ToInt32(e.Row.Cells[9].Text);
            int tbc=(nn<sn?nn:sn);
            (e.Row.Cells[10].FindControl("tbGetNum") as TextBox).Text = Convert.ToString(tbc);
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[1].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }



    //领取Button
    protected void GetBook_Click(object sender, EventArgs e)
    {
        //判断教材数量合法与否
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
            {
                //领取前再查一遍库存剩余量
                string sql3 = "select remain_num from Storage_management_end where "
                + "book_id='" + GridView1.Rows[i].Cells[3].Text.ToString() + "' "//统计这本书的需求册数(用BookID锁定)
                + "and state_id= 1";//代表还可以领
                int total_num = 0;
                SqlDataReader reader3 = SQLHelper.ExecuteReader(sql3);
                while (reader3.Read())
                {
                    total_num += Convert.ToInt32(reader3[0].ToString());//计算总量
                }

                int StoreNum = total_num;//当前最新剩余量
                TextBox tbgn = (TextBox)GridView1.Rows[i].FindControl("tbGetNum");
                int GetNum = Convert.ToInt32(tbgn.Text);
                if (StoreNum < GetNum || GetNum<=0)//库存不能大于领取，但是领取可以大于需求册数
                    WebMessageBox.Show("领取教材数量不合法！");
            }
        }
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked)
            {
                string bi = GridView1.DataKeys[i].Value.ToString();//以book_id为准
                int gn = Convert.ToInt32((GridView1.Rows[i].FindControl("tbGetNum") as TextBox).Text);
                int agn = gn;

                //读出写进历史中的关于教材的基本信息
                string BookName="", Author="", Publish="", Version="", campusName="",Sell_discount="",Unit_price="",Sell_price="",Storage_location="";
                string ClassName = Class.Text;
                string BookID = bi;
                string sql1 = "select BookName,Author,Publish,Version,campusName "
                +"from Abook_message_stu  "
                +"where BookID='" + BookID + "' "
                +"and ClassName=" + ClassName;
                SqlDataReader read1 = SQLHelper.ExecuteReader(sql1);
                for (; read1.Read(); )
                {
                    BookName = read1["BookName"].ToString();
                    Author = read1["Author"].ToString();
                    Publish = read1["Publish"].ToString();
                    Version = read1["Version"].ToString();
                    campusName = read1["campusName"].ToString();
                    break;
                }

                //对Storage_management_end处理
                while (gn != 0)
                {
                    string sql_min = "select * "//1.找到这本书库存剩余量最小的库存，默认从库存量最小的地方取书
                    + "from Storage_management_end "
                    + "where "
                    + "remain_num = (select min(remain_num)from Storage_management_end where remain_num <> 0 and book_id='" + BookID + "')"
                    + "and book_id='" + BookID + "'";
                    SqlDataReader read_min = SQLHelper.ExecuteReader(sql_min);
                    while (read_min.Read() && gn>0)
                    {
                        int beforegn = gn;
                        string id = read_min["id"].ToString();
                        string remain_num = read_min["remain_num"].ToString();
                        string out_num = read_min["out_num"].ToString();
                        //读取记录历史的价格方面的信息
                        Sell_discount = read_min["sell_discount"].ToString();//出售折扣
                        Unit_price = read_min["book_price"].ToString();//单价
                        Sell_price=(Convert.ToDouble(Sell_discount) * Convert.ToDouble(Unit_price) * 0.01).ToString();//实际售价
                        Storage_location = read_min["Storage_location"].ToString();
                        if (Convert.ToInt32(remain_num) <= gn)//2.判断库存是否足数
                        {
                            string str_out = Convert.ToString(Convert.ToInt32(out_num) + Convert.ToInt32(remain_num));//3.更新此最小库存
                            SQLHelper.ExecuteNonQuery("update Storage_management_end set remain_num=0,out_num='" + str_out + "',state_id=2 where id='" + id + "'");
                            gn -= Convert.ToInt32(remain_num);//4.更新需求量
                        }
                        else
                        {
                            string str_out = Convert.ToString(Convert.ToInt32(out_num) + Convert.ToInt32(gn));//3.更新此最小库存
                            string str_rmn = Convert.ToString(Convert.ToInt32(remain_num) - Convert.ToInt32(gn));//4.更新剩余量
                            SQLHelper.ExecuteNonQuery("update Storage_management_end set remain_num='" + str_rmn + "',out_num='" + str_out + "' where id='" + id + "'");
                            gn = 0;//5.更新需求量
                        }

                        //记录历史，写入Abook_message_class_history
                        string time = DateTime.Now.ToString("yyyyMMdd");// 2008-9-4时间
                        string TotalNum = (Convert.ToDouble(beforegn - gn) * 0.01 * Convert.ToDouble(Unit_price) * Convert.ToDouble(Sell_discount)).ToString();
                        string sql3 = "insert into Abook_message_class_history"
                            +"(ClassName,BookID,BookName,number,Author,Publish,Version,campusName,Storage_location,InfoDate,Unit_price,sell_discount,sell_price,total_price)"
                            + "values('" + ClassName + "','" + BookID + "','" + BookName + "','" + Convert.ToString(beforegn - gn) + "',"
                            +"'" + Author + "','" + Publish + "','" + Version + "','" + campusName + "','" + Storage_location + "',"
                            +"'" + time + "','" + Unit_price + "','" + Sell_discount + "','" + Sell_price + "','" + TotalNum + "')";
                        SQLHelper.ExecuteNonQuery(sql3);
                    }
                }

                //对Abook_message_stu处理
                string sql2 = "select stuID from Abook_message_stu where BookID='" + bi + "' and ClassName='" + Class.Text + "'";
                SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
                for (int k = 0; read2.Read() && k < agn; k++)
                {
                    string stuID = read2[0].ToString();
                    SQLHelper.ExecuteNonQuery("update Abook_message_stu set get_id=1 where stuID='" + stuID + "' and BookID='"+bi+"'" );
                }
            }
        }
        JScript.AjaxAlertAndLocationHref(this.Page, "领取成功", "班级历史.aspx?ClassName=" + Class.Text);
    }
}