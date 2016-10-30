using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; 
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class 库存管理_班级历史 : System.Web.UI.Page
{
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

            string ClassName = Request.QueryString["ClassName"];
            if (Object.Equals(Request.Cookies["user"], null))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                initData();
                if (ClassName != null)
                    this.WebPager1.WhereClause = " and ClassName like '%" + ClassName + "%' ";
                else
                    this.WebPager1.WhereClause = " and 2<1 ";
            }
        }
    }


    //对于GridView1，即某一订单下的信息
    private void initData()
    {
        string cn = ClassName.Text;
        string bi = TbBookId.Text;
        string bn = TbBookName.Text;
        string vs=TbPressTime.Text;
        string pn=DropPressName.SelectedValue;
        string dt1 = TbTimeFrom.Text;
        string dt2 = TbTimeTo.Text;
        string cp = DropCampus.SelectedValue;

        dt1 = Regex.Replace(dt1, @"-", "");
        dt2 = Regex.Replace(dt2, @"-", "");
        //判断前后日期是否合理
        if (dt1 != "" && dt2 != "" && Convert.ToInt32(dt1) > Convert.ToInt32(dt2))
            WebMessageBox.Show("输入领取时间不合法！");

        this.WebPager1.SqlField = " ID,ClassName,BookID,BookName,Author,number,Publish,Version,campusName,InfoDate,Unit_price,sell_discount,sell_price,Storage_location ";
        this.WebPager1.TableName = " Abook_message_class_history ";
        this.WebPager1.orderByID = " id asc ";
        this.WebPager1.TblID = " id ";
        this.WebPager1.FlagForOrder = "Yes";
        this.WebPager1.BookId = "BookID";
        this.WebPager1.Number = "number";
        this.WebPager1.Price = "total_price";
        this.WebPager1.WhereClause = " "
           + "and ClassName like '%" + cn + "%' "
           + "and BookID like '%" + bi + "%' "
           + "and BookName like '%" + bn + "%' "
           + "and Version like '%" + vs + "%' "
           + "and Publish like '%" + pn + "%' "
           + "and campusName like '%" + cp + "%' ";
        if (dt1 != "")
            this.WebPager1.WhereClause += "and InfoDate>=" + dt1;
        if (dt2 != "")
            this.WebPager1.WhereClause += "and InfoDate<=" + dt2;
    }


    protected void Search_Click(object sender, EventArgs e)
    {
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
            e.Row.Cells[0].Text = (WebPager1.Pagesize * (Convert.ToInt32((WebPager1.FindControl("lblCurpage") as Label).Text) - 1) + e.Row.RowIndex + 1).ToString();
        }
    }
}