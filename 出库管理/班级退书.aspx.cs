using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class 库存管理_班级退书 : System.Web.UI.Page
{
    static string ClassName, BookID = "", BookName, Author, Publish, campusName, Unit_price, Sell_discount, Sell_price, Version;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //加载出版社名
            string sql2 = "select press_name from press_message";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            while (read2.Read())
            {
                DropPressName.Items.Add(new ListItem(read2[0].ToString()));
            }
            initData();
            this.WebPager1.WhereClause += " and 2<1";
        }
    }


    private void initData()
    {
        string bi = TbBookId.Text;
        string bn = TbBookName.Text;
        string pt = TbPressTime.Text;
        string pn = DropPressName.SelectedValue;
        string cp = DropCampus.SelectedValue;

        this.WebPager1.SqlField = " ID,BookID,BookName,Author,Publish,ClassName,Version,campusName,sell_price ";
        this.WebPager1.TableName = " Abook_message_stu ";
        this.WebPager1.orderByID = " ID asc ";
        this.WebPager1.TblID = " ID ";
        this.WebPager1.WhereClause = " "
          + "and ID in (select max(ID) from Abook_message_stu group by BookID)"//只显示不同BookID的数据，重复的合并
          + "and ClassName = '" + TbClassName.Text + "' "
          + "and BookID like '%" + bi + "%'"
          + "and BookName like '%" + bn + "%'"
          + "and Version like '%" + pt + "%'"
          + "and Publish like '%" + pn + "%'"
          + "and campusName like '%" + cp + "%'"
          + "and get_id=1";
    }



    protected void Search_Click(object sender, EventArgs e)
    {
        string cn = TbClassName.Text;
        if (cn == "")
        {
            WebMessageBox.Show("班号不能为空！");
            return;
        }
        initData();
    }



    //退书
    protected void Return_Click(object sender, EventArgs e)
    {
        try
        {
            //从Abook_message_stu中读数据，用于以后的历史记录            
            ClassName = TbClassName.Text;
            BookID = (sender as LinkButton).CommandArgument;
            string sql1 = "select BookName,Author,Publish,Version,campusName "
            +"from Abook_message_stu  where "
            +"BookID='" + BookID + "' "
            +"and ClassName='" + ClassName + "' "
            +"and get_id=1";
            SqlDataReader read1 = SQLHelper.ExecuteReader(sql1);
            while (read1.Read())
            {
                BookName = read1["BookName"].ToString();
                Author = read1["Author"].ToString();
                Publish = read1["Publish"].ToString();
                campusName = read1["campusName"].ToString();
                Version = read1["Version"].ToString();
                break;
            }

            //显示GridView2
            string sql2 = "select id,book_id,book_name,press_name,press_time,"
            +"order_id,Storage_location,Campus,remain_num "
            +"from Storage_management_end "
            +"where book_id='" + BookID + "'"
            +"and state_id= 1";
            SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
            GridView2.DataSource = SQLHelper.GetDataTable(sql2);
            GridView2.DataKeyNames = new string[] { "id" };//设置主键
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }
    }

    protected void OK_Click(object sender, EventArgs e)
    {
        try
        {
            //判断输入合法性
            string sql1 = "select count(*) from Abook_message_stu where BookID='" + BookID + "'and ClassName='" + ClassName + "'and get_id=1";//统计符合这条信息的条数
            string need_num = "";
            SqlDataReader reader1 = SQLHelper.ExecuteReader(sql1);
            while (reader1.Read())
            {
                need_num = reader1[0].ToString();//该班级共领了多少书
            }
            string cancel_num = ReturnNum.Text;
            int gcancel_num = 0;
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView2.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked)
                {
                    TextBox gcn = GridView2.Rows[i].FindControl("TextBox9") as TextBox;//退订册数
                    gcancel_num += Convert.ToInt32(gcn.Text);//要退订的册数
                }
            }
            if (Convert.ToInt32(need_num) < Convert.ToInt32(cancel_num) || gcancel_num != Convert.ToInt32(cancel_num))
            {
                gcancel_num = 0;
                WebMessageBox.Show("退订册数不合法！");
            }


            //得到价格,用于历史记录
            string sql5 = "select book_price,sell_discount from Storage_management_end where book_id='" + BookID + "'";
            SqlDataReader read5 = SQLHelper.ExecuteReader(sql5);
            while (read5.Read())
            {
                Unit_price = read5["book_price"].ToString();
                Sell_discount = read5["sell_discount"].ToString();
                Sell_price = (Convert.ToDouble(Unit_price) * Convert.ToDouble(Sell_discount)*0.01).ToString();
                break;
            }


            //退订操作

            //改变Storage_management_end里的状态(out_num和remain_num)
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView2.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked)
                {
                    Label id = GridView2.Rows[i].FindControl("Label2") as Label;
                    TextBox cn = GridView2.Rows[i].FindControl("TextBox9") as TextBox;//退订册数
                    string sql2 = "select remain_num,out_num from Storage_management_end where id='" + id.Text + "'";
                    SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
                    while (read2.Read())
                    {
                        string str_brn = read2["remain_num"].ToString();
                        int in_rn = Convert.ToInt32(str_brn);
                        in_rn += Convert.ToInt32(cn.Text);
                        string str_frn = Convert.ToString(in_rn);//最终剩余量
                        string str_bon = read2["out_num"].ToString();
                        int in_on = Convert.ToInt32(str_bon);
                        in_on -= Convert.ToInt32(cn.Text);
                        string str_fon = Convert.ToString(in_on);//最终已发量
                        SQLHelper.ExecuteNonQuery("update Storage_management_end set remain_num='" + str_frn + "',out_num='" + str_fon + "' where id=" + id.Text);
                    }
                }
            }

            //在Abook_message_stu中更改相应个数的状态
            //Label bi = GridView2.Rows[0].FindControl("Label9") as Label;
            //BookID = bi.Text;
            string sql3 = "select stuID from Abook_message_stu where BookID='" + BookID + "' and ClassName='" + ClassName + "' and get_id=1";
            SqlDataReader read3 = SQLHelper.ExecuteReader(sql3);
            for (int k = 0; read3.Read() && k < Convert.ToInt32(ReturnNum.Text); k++)
            {
                string stuID = read3[0].ToString();
                SQLHelper.ExecuteNonQuery("update Abook_message_stu set get_id=0 where stuID=" + stuID + " and BookID='" + BookID + "' and get_id=1");
            }

            //记录历史，写入Abook_message_class_history
            string time = DateTime.Now.ToString("yyyy-MM-dd");// 2008-9-4时间
            string number = "-" + ReturnNum.Text;
            string TotalPrice = (Convert.ToDouble(number) * Convert.ToDouble(Unit_price) * Convert.ToDouble(Sell_discount) * 0.01).ToString();
            string sql4 = "insert into Abook_message_class_history(ClassName,BookID,BookName,number,Author,Publish,Version,campusName,InfoDate,Unit_price,sell_discount,sell_price,total_price)";
            sql4 += "values('" + ClassName + "','" + BookID + "','" + BookName + "','" + number + "','" + Author + "','" + Publish + "','" + Version + "','" + campusName + "','" + time + "','" + Unit_price + "','" + Sell_discount + "','" + Sell_price + "','"+TotalPrice+"')";
            SQLHelper.ExecuteNonQuery(sql4);

            JScript.AjaxAlertAndLocationHref(this.Page, "退书成功", "班级退书.aspx?ClassName=" + TbClassName.Text);
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

            //对于Gridview中总册数的设置
            string bi = e.Row.Cells[2].Text;//ISBN
            string cn= e.Row.Cells[1].Text;//班级名称
            string sql1 = "select count(*) from Abook_message_stu where BookID='" + bi + "' and ClassName='" + cn + "'and get_id=1";//统计符合这条信息的条数
            SqlDataReader reader1 = SQLHelper.ExecuteReader(sql1);
            while (reader1.Read())
            {
                e.Row.Cells[9].Text = reader1[0].ToString();
            }
        }
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }

    protected void GvDataType_RowDataBound2(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#ADEAEA'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
        }
    }
}