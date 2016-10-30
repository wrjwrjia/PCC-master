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

public partial class SaveOrder : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["list"] == null)
        {
            Message.Text = "对不起，你尚未选择要选订的教材，请检查";
        }
        else {
            if (isOpen)
            {
                string[] listResult = Request.Form["list"].ToString().Split(',');
                SqlParameter[] param = new SqlParameter[5];
                for (int i = 0; i < listResult.Length; i++)
                {
                    string id = listResult[i].Trim();
                    string sql1 = "select *from Recommend where ID=" + id;
                    SqlDataReader read = SQLHelper.ExecuteReader(sql1);
                    for (; read.Read(); )
                    {
                        string BookID = read["BookID"].ToString();
                        string BookName = read["BookName"].ToString();
                        string CourseID = read["CourseID"].ToString();
                        string CourseName = read["CourseName"].ToString();
                        string Author = read["Author"].ToString();
                        string Publish = read["Publish"].ToString();
                        string UnitPrice = read["UnitPrice"].ToString();
                        string Version = read["Version"].ToString();
                        string Term = read["Term"].ToString();
                        string InfoDate = DateTime.Now.ToString();
                        try
                        {
                            string sql2 = "insert into Abook_message_stu(StuID,StuName,ClassName,DeptName,BookID,BookName,CourseID,courseName,Author,Publish,UnitPrice,Version,InfoDate,Orderkind,Term,get_id)";
                            sql2 += "values('" + stuID + "','" + stuName + "','" + className + "','" + DeptName + "','" + BookID + "','" + BookName + "','" + CourseID + "','" + CourseName + "','" + Author + "','" + Publish + "','" + UnitPrice + "','" + Version + "','" + InfoDate + "','" + orderKind + "','"+Term+"','0')";
                            SQLHelper.ExecuteNonQuery(sql2);
                            string sql3 = "delete from Abook_message_stu where BookID in (select BookID from Abook_message_stu  group  by  BookID having count(BookID) > 1)and id not in (select min(id) from   Abook_message_stu   group by BookID   having count(BookID )>1)";
                            sql3 += "and StuID=" + stuID;
                            SQLHelper.ExecuteNonQuery(sql3);
                        }
                        catch (Exception ex)
                        {
                            WebMessageBox.Show(ex.Message);
                        }
                    }
                        
                }

                Response.Redirect("Default.aspx");
            }
            else {
                Message.Text = "对不起，教材订购系统已关闭，你不能选订相关教材";
            }
        }
    }
}
