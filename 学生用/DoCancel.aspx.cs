using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class DoCancel : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string  id = Request.QueryString["ID"];
        if (id == null || id.ToString() == "")
        {
            Message.Text = "参数丢失";
        }
        else {
            if (isOpen)
            {
                SqlDataReader read = SQLHelper.ExecuteReader("select *from Abook_message_stu where id="+id);
                for(;read.Read();)
                {
                    string StuID=read["StuID"].ToString();
                    string StuName=read["StuName"].ToString();
                    string ClassName=read["ClassName"].ToString();
                    string DeptName=read["DeptName"].ToString();
                    string BookName=read["BookName"].ToString();
                    string BookID=read["BookID"].ToString();
                    string CourseName=read["CourseName"].ToString();
                    string CourseID=read["CourseID"].ToString();
                    string Author=read["Author"].ToString();
                    string Publish=read["Publish"].ToString();
                    string UnitPrice=read["UnitPrice"].ToString();
                    string Version=read["Version"].ToString();
                    string TeacID=read["TeacID"].ToString();
                    string TeacName = read["TeacName"].ToString();
                    string Term=read["Term"].ToString();
                    string InfoDate=DateTime.Now.ToString();
                    string sql1 = "insert into CancelInfo(StuID,StuName,ClassName,DeptName,BookID,BookName,CourseID,CourseName,Author,Publish,UnitPrice,Version,TeacID,TeacName,Term,InfoDate) values('" + StuID + "','" + StuName + "','" + ClassName + "','" + DeptName + "','" + BookID + "','" + BookName + "','" + CourseID + "','" + CourseName + "','" + Author + "','" + Publish + "','" + UnitPrice + "','" + Version + "','" + TeacID + "','"+TeacName+"','" + Term + "','" + InfoDate + "')";
                    SQLHelper.ExecuteNonQuery(sql1);
                }
                string sql2 = "delete from Abook_message_stu where id=" + id;
                SQLHelper.ExecuteNonQuery(sql2);
                Response.Redirect("Default.aspx");
            }
            else
            {
                Message.Text = "对不起，教材订购系统已关闭，你不能退订相关教材";
            }
        }
    }
}
