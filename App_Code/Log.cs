using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Log 的摘要说明
/// </summary>
/// 历史记录
public class Log
{
	public Log()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //修改用户
    public static void writeLog(string userID,string userName,string operation,string optDetails)
    {
        SqlParameter[] parames = {  
                new SqlParameter("@usr_id",Common.FormatParameter(userID)), 
                new SqlParameter("@usr_name",Common.FormatParameter(userName)),
                new SqlParameter("@opt",Common.FormatParameter(operation)),
                new SqlParameter("@opt_date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@detail",Common.FormatParameter(optDetails))                 
            };
        SQLHelper.ExecuteNonQuery(@"insert into tbl_log(usr_id,usr_name,opt,opt_date,detail) values(@usr_id,@usr_name,@opt,@opt_date,@detail)", parames);       
    }

    //修该book_message
    public static void writeLogbh(string id)
    {
        SqlParameter[] parames = {  
                new SqlParameter("@usr_id",Common.FormatParameter(id)), 
                new SqlParameter("@opt_date",DateTime.Now.ToString("yyyy-MM-dd")),  
            };
        string sql = "select * from book_message where id='" + id + "'";
        SqlDataReader read=SQLHelper.ExecuteReader(sql);
        //string 
        //while (read.Read())
        //{
        //}
        SQLHelper.ExecuteNonQuery(@"insert into book_message_history(usr_id,usr_name,opt,opt_date,detail) values(@usr_id,@usr_name,@opt,@opt_date,@detail)", parames);
    }
}
