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
using System.Collections.Generic;

/// <summary>
/// SQLHelper 的摘要说明
/// </summary>
public class SQLHelper
{
	public SQLHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["SQLCONN"].ConnectionString;  
        /// <summary>
        /// 返回Command对象的方法
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parames">参数列表</param>
        /// <returns>SqlCommand对象</returns>
        private static SqlCommand GetCommand(string sql, params SqlParameter[] parames)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, connection);
            //加入参数列表
            if (parames != null)
                cmd.Parameters.AddRange(parames); 
            //返回对象
            return cmd;
        }
        public static DataView GridViewBind(String SqlStr,String tablename,GridView GridViewName)
        {
            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = "Data Source=dell-PC\\ZHANGXINYUAN;Initial Catalog=BOOK_DB;User ID=sa;Password=123;";
            //string SqlStr = "select date_year,semester,order_id,book_id,book_name,press_name,book_price,arrived_discount,arrived_date,arrival_amount from Storage_management where book_id like '%" + book_id + "%' and order_id like '%" + order_id + "%'";
            SqlDataAdapter da = new SqlDataAdapter(SqlStr, sqlCon);
            DataSet ds = new DataSet();
            da.Fill(ds, tablename);
            DataView dv = ds.Tables[0].DefaultView;
            //string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
            //dv.Sort = sort;
            GridViewName.DataSource = dv;
            GridViewName.DataBind();
            return dv; 
        }
        /// <summary>
        /// 返回读取器的方法
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parames">参数类表</param>
        /// <returns>SqlDataReader读取器</returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] parames)
        {
            SqlCommand cmd = GetCommand(sql, parames);
            SqlDataReader reader = null;
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }
        /// <summary>
        /// 增,删,改的方法
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parames">参数类表</param>
        /// <returns>int数字</returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parames)
        {
            SqlCommand cmd = GetCommand(sql, parames);
            int result = 0;
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 无参数增,删,改的方法
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>      
        /// <returns>int数字</returns>
        public static int ExecuteNonQuery(string sql)
        {
            SqlCommand cmd = GetCommand(sql, null);
            int result = 0;
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
            }
           catch (Exception ex)
            {
                throw ex;
            }
           finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 用于执行事务方法
        /// </summary>
        /// <param name="trans"></param> 
        /// <param name="sql"></param>
        /// <param name="parames"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlTransaction trans,SqlConnection con, string sql, params SqlParameter[] parames)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = trans;
            cmd.Connection = con;
            cmd.CommandText = sql;
            if (parames != null)
                cmd.Parameters.AddRange(parames); 
            int result = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return result;

        }
        /// <summary>
        /// 返回一行一列的方法
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parames">参数类表</param>
        /// <returns>object对象</returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] parames)
        {
            SqlCommand cmd = GetCommand(sql, parames);
            object result;
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                result = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 返回数字的方法
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parames">参数类表</param>
        /// <returns>object对象</returns>
        public static int ReturnInteger(string sql)
        {
            SqlCommand cmd = GetCommand(sql, null);
            object result;
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(result.ToString()); ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
        }
        /// <summary>
        /// 返回dataset对象的方法
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="tableName">待填充的datatable表</param>
        /// <param name="parames">参数列表</param>
        /// <returns>DataSet对象</returns>
        public static DataSet GetDataSet(string sql, string tableName, params SqlParameter[] parames)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dapter = new SqlDataAdapter();
            dapter.SelectCommand = GetCommand(sql, parames);
            try
            {
                dapter.Fill(ds, tableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        /// <summary>
        /// 返回DataTable对象的方法
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="tableName">待填充的datatable表</param>
        /// <param name="parames">参数列表</param>
        /// <returns>DataTable对象</returns>
        public static DataTable GetDataTable(string sql, string tableName, params SqlParameter[] parames)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dapter = new SqlDataAdapter();
            dapter.SelectCommand = GetCommand(sql, parames);
            try
            {
                dapter.Fill(ds, tableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dapter = new SqlDataAdapter();
            dapter.SelectCommand = GetCommand(sql, null);
            try
            {
                dapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }
   

    /// <summary> 
    /// 将DataTable中数据批量数据库表中
    /// <param name="dt">源数据集</param>  
    /// <param name="ExcelTitle">Excel数据题头</param>  
    /// <param name="DBcolumn">要插入数据的表的对应字段</param>   
    /// <param name="DestinationTableName">写入数据的数据库表名</param>  
    /// </summary>  
    public static void SqlBulkCopyData(System.Data.DataTable dt, string[] DataTableTitle, string[] DBcolumn, string DestinationTableName)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLCONN"].ToString());
        //初始化连接字符串  
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        SqlBulkCopy bcp = new SqlBulkCopy(conn);
        //指定目标数据库表名
        bcp.DestinationTableName = DestinationTableName;
        //指定源列和目标列之间的对应关系
        for (int i = 0; i < DataTableTitle.Length; i++)
        {
            bcp.ColumnMappings.Add(DataTableTitle[i], DBcolumn[i]);
        }
        //写入数据库表 
        bcp.WriteToServer(dt);
        bcp.Close();
        conn.Close();
    }


    /// <summary>
    /// 返回数字的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="parames">参数类表</param>
    /// <returns>object对象</returns>
    public static double ReturnDouble(string sql)
    {
        SqlCommand cmd = GetCommand(sql, null);
        object result;
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value|| result.ToString()==string.Empty)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(result.ToString()); ;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();
        }
    }

}
