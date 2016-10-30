using System;
using System.Data;
using System.Data .SqlClient ;
using System.Configuration ;
using System.Diagnostics ;
using System.Xml;
using System.IO;

/// <summary>
/// Author	: GuoGuo
/// Data	: 2006-07
/// Description	: database operate class
/// </summary>
public sealed class SqlBase
{
    static string nullSqlExpress = "sql表达式为空，请设置";
    static string Sql_Connection_Err = "数据库连接错误，请检查数据库密码是否正确";

	#region Connect Message
	/// <summary>
	/// <param name="connectionString"></param>
	/// </summary>
	private static string connectionString = null;

	public static string ConnectionString
	{
		get
		{
            if (connectionString == null)
            {
                return ConfigurationManager.ConnectionStrings["SQLCONN"].ToString();
            }
            else
            {
                return SqlBase.connectionString;
            }
		}
		set
        {
            SqlBase.connectionString = value;
        }
	}

	/// <summary>
	/// get the connect object
	/// </summary>
	/// <returns></returns>
	public static SqlConnection GetConnection()
	{
		return new SqlConnection(ConnectionString);
	}

    /// <summary>
    /// 校验数据库连接是否有效
    /// </summary>
    /// <returns></returns>
    public static bool CheckDBConnection()
    {
        SqlConnection conn = GetConnection();
        try
        {
            conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }
	#endregion

	#region SqlDataReader Method
	/// <summary>
	/// use datareader to get the data from database
	/// </summary>
	/// <param name="con">database connection object</param>
	/// <param name="sql">procedure name or sql expression</param>
	/// <returns>DataReader object</returns>
	public static SqlDataReader ExecuteDataReader(string sql,CommandType type)
	{
		if(sql == null || sql == "")
			throw new Exception(nullSqlExpress);
		SqlConnection con = SqlBase.GetConnection();
		try
		{
			con.Open();														
		}
		catch
		{
			throw new Exception(Sql_Connection_Err);
		}
		SqlCommand command = con.CreateCommand();
		command.CommandText = sql;
		command.CommandType = type;

		SqlDataReader read = command.ExecuteReader(CommandBehavior.CloseConnection); 
		return read;
	}

	/// <summary>
	///	use procedure to do some database operation
	/// </summary>
	/// <param name="con">sql connection object</param>
	/// <param name="sql">procedure name or sql expression</param>
	/// <param name="param">parameter array</param>
	/// <returns>DataReader object</returns>
	public static SqlDataReader ExecuteDataReader(string sql,CommandType type,SqlParameter[] param)
	{
		if(sql == null || sql == "")
			throw new Exception(nullSqlExpress);
		SqlConnection con = SqlBase.GetConnection();
		try
		{
			con.Open();														
		}
		catch
		{
			throw new Exception(Sql_Connection_Err);
		}
		SqlCommand command = con.CreateCommand();
		command.CommandText = sql;
		command.CommandType = type;
		foreach(SqlParameter p in param)
		{
			command.Parameters .Add(p);
		}
		SqlDataReader read = command.ExecuteReader(CommandBehavior.CloseConnection);
		return read;
	}

	#endregion

	#region Return Data Object
	/// <summary>
	/// get the data with dataset object
	/// </summary>
	/// <param name="sql">procedure name or sql expression</param>
	/// <returns>DataSet</returns>
	public static DataSet ExecuteDataSet(string sql,CommandType type)
	{
		if(sql == null || sql == "")
			throw new Exception(nullSqlExpress);
		using(SqlConnection con = SqlBase.GetConnection())
		{
			try
			{
				con.Open();														
			}
			catch
			{
				throw new Exception(Sql_Connection_Err);
			}
			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = new SqlCommand();
			da.SelectCommand .CommandType = type;
			da.SelectCommand .CommandText = sql;
			da.SelectCommand .Connection = con;
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}
	}

	/// <summary>
	/// use dataset to help page the data
	/// </summary>
	/// <param name="sql">sql expression</param>
	/// <param name="currentPage"></param>
	/// <param name="pagesize"></param>
	/// <param name="tableName"></param>
	/// <param name="recordNo">the total number of the data collection</param>
	/// <returns></returns>
	public static DataView ExecuteDataView(string sql,int currentPage,int pagesize,string tableName)
	{
		if(sql == null || sql == "")
			throw new Exception(nullSqlExpress);
		using(SqlConnection con = SqlBase.GetConnection())
		{
			try
			{
				con.Open();														
			}
			catch
			{
				throw new Exception(Sql_Connection_Err);
			}
			int start = (currentPage-1)*pagesize;
			SqlDataAdapter da = new SqlDataAdapter(sql,con);
			DataSet ds = new DataSet();
			da.Fill(ds,start,pagesize,tableName);
			return ds.Tables[0].DefaultView;
		}
	}

	/// <summary>
	/// use SqlDataAdapter to fill the DataTable
	/// </summary>
	/// <param name="sql">procedure name or sql expression</param>
	/// <returns>DataTable</returns>
	public static DataTable ExecuteDataTable(string sql,CommandType type)
	{
		if(sql == null || sql == "")
            throw new Exception(nullSqlExpress);
		using(SqlConnection con = SqlBase.GetConnection())
		{
			try
			{
				con.Open();														
			}
			catch
			{
                throw new Exception(Sql_Connection_Err);
			}
			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = new SqlCommand();
			da.SelectCommand .Connection  = con;
			da.SelectCommand .CommandText = sql;
			da.SelectCommand .CommandType = type;
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}
	}

	public static DataTable ExecuteDataTable(string sql,SqlParameter[] param,CommandType type)
	{
		if(sql == null || sql == "")
			throw new Exception(nullSqlExpress);
		using(SqlConnection con = SqlBase.GetConnection())
		{
			try
			{
				con.Open();														
			}
			catch
			{
				throw new Exception(Sql_Connection_Err);
			}
			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = new SqlCommand ();
			da.SelectCommand .Connection  = con;
			da.SelectCommand .CommandText = sql;
			da.SelectCommand .CommandType = type;
			foreach(SqlParameter p in param)
			{
				da.SelectCommand .Parameters .Add(p);
			}
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;
		}
	}
	#endregion

	#region Executscalar Method
	/// <summary>
	/// return a single value of the sql expression effected
	/// </summary>
	/// <param name="sqlString">CommandText</param>
	/// <returns>eturn a single value of the sql expression effected</returns>
	public static void ExecuteScalar(string sqlString,out int returnValue)
	{
		if(sqlString == null || sqlString == "")
			throw new Exception(nullSqlExpress);
		using(SqlConnection con = SqlBase.GetConnection())
		{
			try
			{
				con.Open();														
			}
			catch
			{
				throw new Exception(Sql_Connection_Err);
			}
			SqlCommand command = new SqlCommand (sqlString,con);
			returnValue = (int)command.ExecuteScalar();
		}
	}

	#endregion

	#region ExecutQuery method
	/// <summary>
	/// execute the sql expression according to the sql
	/// </summary>
	/// <param name="con"></param>
	/// <param name="sqlString">procedure name or sql expression</param>
	/// <param name="type" >execute style</param>
	public static int ExecuteQuery(string sqlString,CommandType type)
	{
		using(SqlConnection con = SqlBase.GetConnection())
		{
			if(sqlString == null || sqlString == "")
				throw new Exception(nullSqlExpress);
			try
			{
				con.Open();														
			}
			catch
			{
				throw new Exception(Sql_Connection_Err);
			}
			SqlCommand command = new SqlCommand ();
			command.Connection = con;
			command.CommandText = sqlString;
			command.CommandType = type;
			return (int)command.ExecuteNonQuery();
		}
	}

	/// <summary>
	///  execute the procedure with params
	/// </summary>
	/// <param name="con"></param>
	/// <param name="sqlString">procedure name or sql expression</param>
	/// <param name="param">params array</param>
	/// <param name="type" >execute style</param>
	public static int ExecuteQuery(string sqlString,SqlParameter[] param,CommandType type)
	{
		if(sqlString == null || sqlString == "")
			throw new Exception(nullSqlExpress);
		using(SqlConnection con = SqlBase.GetConnection())
		{
			try
			{
				con.Open();														
			}
			catch
			{
				throw new Exception(Sql_Connection_Err);
			}
			SqlCommand command = con.CreateCommand();
			command.CommandText = sqlString;
			command.CommandType = type;
			foreach(SqlParameter p in param)
			{
				command.Parameters .Add(p);
			}
			return (int)command.ExecuteNonQuery();
		}
	}
	#endregion

	#region Create Param Method
	/// <summary>
	/// Create a SqlParameter and initialize it to the passed in values
	/// </summary>
	/// <param name="paramName">The name of the parameter being created.</param>
	/// <param name="dbType">The type in database this parameter represents.</param>
	/// <param name="size">The size of the parameter type if applicable.</param>
	/// <param name="direction">The direction the parameter is sending data.</param>
	/// <param name="oValue">The value of the parameter. Can Only assign a value if "direction" is Input or InputOutput.</param>
	/// <returns>An initialized SqlParameter</returns>
	public static SqlParameter CreateParam(string paramName, SqlDbType dbType, int size, ParameterDirection direction, object oValue)
	{
		SqlParameter sp;
		
		if (size != 0)
			sp = new SqlParameter(paramName, dbType, size);
		else
			sp = new SqlParameter(paramName, dbType);

		sp.Direction = direction;

		//Only assign a value for Input or InputOutput parameters
		if (oValue != null && (direction == ParameterDirection.Input || direction == ParameterDirection.InputOutput))
			sp.Value = oValue;

		return sp;
	}
	#endregion

    #region 备份数据库
    /// <summary>
    /// 备份数据库
    /// </summary>
    /// <param name="DBName">数据库名称</param>
    /// <param name="newDBName">备份数据库名称</param>
    /// <param name="path">备份路径</param>
    public static bool BackUpDatabase(string DBName, string newDBName, string path)
    {
        Debug.Assert(DBName != "" || DBName != null, "Invalid DBName,please check it and try again");
        Debug.Assert(path != "" || path != null, "Invalid file Path,please check it and try again");
        if (!path.EndsWith("\\"))
        {
            path += "\\";
        }
        using (SqlConnection con = SqlBase.GetConnection())
        {
            con.Open();
            if (System.IO.File.Exists(path + newDBName + ".dat"))
            {
                System.IO.File.Delete(path + newDBName + ".dat");   
            }
            SqlBase.ExecuteQuery("BACKUP DATABASE " + DBName + " TO DISK = '" + path + newDBName + ".dat'", CommandType.Text);
            return true;
        }
    }
    #endregion

    #region 数据库连接属性
    public static string Server
    {
        get {
            string[] array = connectionString.Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].StartsWith("Data Source="))
                {
                    return array[i].Replace("Data Source=", "");
                }
            }
            return "";
        }
    }

    public static string UID
    {
        get
        {
            string[] array = connectionString.Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].StartsWith("User ID="))
                {
                    return array[i].Replace("User ID=", "");
                }
            }
            return "";
        }
    }

    public static string PWD
    {
        get
        {
            string[] array = connectionString.Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].StartsWith("Password="))
                {
                    return array[i].Replace("Password=", "");
                }
            }
            return "";
        }
    }

    public static string DataBase
    {
        get
        {
            string[] array = connectionString.Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].StartsWith("Initial Catalog="))
                {
                    return array[i].Replace("Initial Catalog=", "");
                }
            }
            return "";
        }
    }
    #endregion
}
