using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class 实验 : System.Web.UI.Page
{
    static int OrderKind;
    string Time = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Time = System.DateTime.Now.Year.ToString();
        string Time1 = (Int32.Parse(Time) + 1).ToString();
        string Term1 = Time + "-" + Time1 + "-1";
        string Term2 = Time + "-" + Time1 + "-2";
        ListItem item0 = new ListItem("", "0");
        ListItem item1 = new ListItem(Term1, "1");
        ListItem item2 = new ListItem(Term2, "2");
        ListItem item6 = new ListItem("", "6");
        ListItem item3 = new ListItem("非公选课教材订购类型", "3");
        ListItem item4 = new ListItem("公选课教材订购类型", "4");
        ListItem item5 = new ListItem("补订教材订购类型", "5");
        CheckBox1.Text = Time + "级";
        CheckBox2.Text = (Int32.Parse(Time) - 1).ToString() + "级";
        CheckBox3.Text = (Int32.Parse(Time) - 2).ToString() + "级";
        CheckBox4.Text = (Int32.Parse(Time) - 3).ToString() + "级";
        DropDownList1.Items.Add(item6);
        DropDownList1.Items.Add(item3);
        DropDownList1.Items.Add(item4);
        DropDownList1.Items.Add(item5);
        DropDownList2.Items.Add(item0);
        DropDownList2.Items.Add(item1);
        DropDownList2.Items.Add(item2);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string sql = "delete from Param";
            SQLHelper.ExecuteNonQuery(sql);
            string StartDate="";
            StartDate = Start_Time.SelectedDate.Value.ToString();
            string EndDate = "";
            EndDate=End_Time.SelectedDate.Value.ToString();
            if (DropDownList1.SelectedItem.Value == "3")
            {
                OrderKind = 0;
            }
            else if (DropDownList1.SelectedItem.Value == "4")
            {
                OrderKind = 1;
            }
            else if (DropDownList1.SelectedItem.Value == "5")
            {
                OrderKind = 2;
            }
            else
            {
                WebMessageBox.Show("类型为必选");
            }
            string Term = DropDownList2.SelectedItem.Text;
            string LimitGrade = "";
            if (CheckBox1.Checked)
                LimitGrade += Time + ",";
            if (CheckBox2.Checked)
                LimitGrade += (Int32.Parse(Time) - 1).ToString() + ",";
            if (CheckBox3.Checked)
                LimitGrade += (Int32.Parse(Time) - 2).ToString() + ",";
            if (CheckBox4.Checked)
                LimitGrade += (Int32.Parse(Time) - 3).ToString();
            string RegularText = TextBox3.Text;
            string sql1 = "insert into Param (StartDate,EndDate,OrderKind,Term,RegularText,LimitGrade)values('" + StartDate + "','" + EndDate + "'," + OrderKind.ToString() + ",'" + Term + "','" + RegularText + "','" + LimitGrade + "')";
            SQLHelper.ExecuteNonQuery(sql1);
            WebMessageBox.Show("修改成功");
        }
        catch (Exception ex)
        {
            WebMessageBox.Show(ex.Message);
        }

    }
    protected bool ValidateTime(string ShiJian)
    {
        bool Flage = true;
        try
        {
            DateTime dt = Convert.ToDateTime(ShiJian);
        }
        catch (System.Exception ex)
        {
            Flage = false;
        }
        return Flage;
    }
}