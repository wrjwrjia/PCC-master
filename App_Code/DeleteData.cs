using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


/// <summary>
///DeleteData 的摘要说明
///给管理员全删数据的功能
/// </summary>
public class DeleteData
{
	public DeleteData()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    //初始化删除按钮信息
    public static void init(Button btn)
    {
        string roleid = HttpContext.Current.Request.Cookies["user"].Values["roleid"];
        if (roleid == "1")
        {
            btn.Visible = true;
            btn.Attributes.Add("onclick ", "return confirm('Confirm to delete all data?'); ");
        }
    }

    /// <summary>
    /// 全部删除数据
    /// </summary>
    /// <param name="datatablename">要删除的数据表名</param>
    public static void DaleteAllData(string datatablename,string tabledescription)
    {
        string sql = "delete from " + datatablename ;
        SQLHelper.ExecuteNonQuery(sql);

        //记录日志
        Log.writeLog(System.Web.HttpContext.Current.Request.Cookies["user"].Values["id"], System.Web.HttpContext.Current.Request.Cookies["user"].Values["name"], "Delete Data", "Delete All data of " + tabledescription);
    }
}
