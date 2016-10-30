using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Collections;

/// <summary>
/// ExcelHelper 的摘要说明
/// </summary>
public class ExcelHelper
{
	public ExcelHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    #region Const Variables

    /// <summary>
    /// Excel 版本号
    /// </summary>
    private const string ExcelDefaultVersion = "8.0";

    /// <summary>
    /// 连接字符串模板
    /// </summary>
    private const string ConnectionStringTemplate = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source ={0};Extended Properties=Excel {1}";
    #endregion


    #region Public Methods
    /// <summary>
    /// 创建连接
    /// </summary>
    /// <param name="excelPath">Excel文件绝对路径。</param>
    /// <returns></returns>
    internal static OleDbConnection CreateConnection(string excelPath)
    {
        return CreateConnection(excelPath, ExcelDefaultVersion);
    }

    /// <summary>
    /// 创建连接
    /// </summary>
    /// <param name="excelPath">Excel文件绝对路径。</param>
    /// <param name="excelVersion">Excel版本号。默认为 8.0</param>
    /// <returns></returns>
    internal static OleDbConnection CreateConnection(string excelPath, string excelVersion)
    {
        return new OleDbConnection(GetConnectionString(excelPath, excelVersion));
    }

    //// <summary>
    /// 把DataTable内容导出伟excel并返回客户端
    /// </summary>
    /// <param name="dgData">待导出的DataTable</param>   
    public static void DataTable2Excel(System.Data.DataTable dtData)
    {
        System.Web.UI.WebControls.DataGrid dgExport = null;
        // 当前对话
        System.Web.HttpContext curContext = System.Web.HttpContext.Current;
        // IO用于导出并返回excel文件
        System.IO.StringWriter strWriter = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;
        //设置导出格式
        string style = @"<style> .text {mso-number-format:\@} </script>";
        if (dtData != null)
        {
            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.AddHeader("content-disposition", "attachment;");
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF32;
            curContext.Response.Charset = "";

            // 导出excel文件
            strWriter = new System.IO.StringWriter();
            htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

            // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid
            dgExport = new System.Web.UI.WebControls.DataGrid();
            dgExport.DataSource = dtData.DefaultView;
            dgExport.AllowPaging = false;
            dgExport.DataBind();

            // 返回客户端
            curContext.Response.Write(style);
            dgExport.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();
        }
    }

    /// <summary>
    /// 获取Excel的第一个Sheet的数据。注意，这里的第一个是按Sheet名排列后的第一个Sheet。
    /// <example>
    /// DataTable dt = Query(@"C:\My Documents\1.xls");
    /// </example>
    /// </summary>
    /// <param name="excelPath">Excel文件绝对路径。</param>
    /// <returns></returns>
    public static DataTable Query(string excelPath)
    {
        return Query(excelPath, 0);
    }

    /// <summary>
    /// 获取Excel指定Sheet名称的数据。
    /// <example>
    /// DataTable dt = Query(@"C:\My Documents\1.xls", "sheet1");
    /// </example>
    /// </summary>
    /// <param name="excelPath">Excel文件绝对路径。</param>
    /// <param name="sheetName">Sheet名，允许空格存在。如：sheet1</param>
    /// <returns></returns>
    public static DataTable Query(string excelPath, string sheetName)
    {
        OleDbConnection conn = CreateConnection(excelPath);
        conn.Open();

        DataTable dt = new DataTable();
        dt = QueryBySheetName(conn, sheetName + "$");

        conn.Close();
        return dt;
    }

    /// <summary>
    /// 获取Excel指定Sheet名称的数据。
    /// <example>
    /// DataTable dt = Query(@"C:\My Documents\1.xls", "sheet1$");
    /// DataTable dt = Query(@"C:\My Documents\1.xls", "'My Sheet'$");
    /// </example>
    /// </summary>
    /// <param name="excelPath">Excel文件绝对路径。</param>
    /// <param name="rawSheetName">Sheet名，允许空格存在。如：sheet1$, 'My Sheet'$</param>
    /// <returns></returns>
    public static DataTable QueryEx(string excelPath, string rawSheetName)
    {
        OleDbConnection conn = CreateConnection(excelPath);
        DataTable dt = new DataTable();
        try
        {
            conn.Open();
            dt = QueryBySheetName(conn, rawSheetName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }

        return dt;
    }

    /// <summary>
    /// 获取指定序号的Sheet的数据。序号从0开始。注意，是按Sheet名排列后的第Index个Sheet。
    /// <example>
    /// DataTable dt = Query(@"C:\My Documents\1.xls", 0);
    /// </example>
    /// </summary>
    /// <param name="excelPath">Excel文件绝对路径。</param>
    /// <param name="sheetIndex">Sheet的序号，从0开始。</param>
    /// <returns></returns>
    public static DataTable Query(string excelPath, int sheetIndex)
    {
        OleDbConnection conn = CreateConnection(excelPath);
        conn.Open();

        ArrayList arrSheets = GetSheetNames(conn);
        if (arrSheets.Count <= sheetIndex)
            throw new ArgumentOutOfRangeException();

        string sheetName = arrSheets[sheetIndex].ToString();
        DataTable dt = QueryBySheetName(conn, sheetName);
        conn.Close();
        return dt;
    }

    /// <summary>
    /// 获取Excel的所有的Sheet的名称。
    /// </summary>
    /// <param name="excelPath">Excel文件绝对路径。</param>
    /// <returns></returns>
    public static ArrayList GetSheetNames(string excelPath)
    {
        OleDbConnection conn = CreateConnection(excelPath);
        ArrayList arrSheets = GetSheetNames(conn);
        conn.Close();
        return arrSheets;
    }

    /// <summary>
    /// 将DataTable的内容保存到Excel的一个指定模板的Sheet中。
    /// 指定模板是指指定了的列头。
    /// </summary>
    /// <param name="dt">要保存的数据</param>
    /// <param name="excelPath">Excel文件绝对路径。</param>
    /// <param name="sheetName">Sheet名，允许空格存在。如：sheet1</param>
    public static void DataTableToExcel(DataTable dt, string excelPath, string sheetName)
    {
        OleDbConnection conn = CreateConnection(excelPath);
        conn.Open();
        DataTableToExcel(conn, dt, sheetName + "$");
        conn.Close();
    }

    /// <summary>
    /// 将DataTable的内容保存到Excel的一个指定模板的Sheet中。
    /// 指定模板是指指定了的列头。
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="excelPath">Excel文件绝对路径。</param>
    /// <param name="sheetIndex">Sheet的序号，从0开始。</param>
    public static void DataTableToExcel(DataTable dt, string excelPath, int sheetIndex)
    {
        OleDbConnection conn = CreateConnection(excelPath);
        conn.Open();

        ArrayList arrSheets = GetSheetNames(conn);
        if (arrSheets.Count <= sheetIndex)
            throw new ArgumentOutOfRangeException();

        string sheetName = arrSheets[sheetIndex].ToString();
        //string sheetName = "ceshi";
        DataTableToExcel(conn, dt, sheetName);
        conn.Close();
    }

   
    #endregion

    #region Private Methods

    /// <summary>
    /// 获取连接字符串
    /// </summary>
    /// <param name="excelPath"></param>
    /// <param name="excelVersion"></param>
    /// <returns></returns>
    private static string GetConnectionString(string excelPath, string excelVersion)
    {
        return "Provider=Microsoft.Jet.OleDb.4.0;Data Source= " + excelPath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2;'";
    }

    /// <summary>
    /// 根据Sheet的名获取数据。
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="sheetName"></param>
    /// <returns></returns>
    private static DataTable QueryBySheetName(OleDbConnection conn, string sheetName)
    {
        string cmd = "select * from [" + sheetName + "]";
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd, conn);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        return dt;

    }

    /// <summary>
    /// 获取所有的Sheet名
    /// </summary>
    /// <param name="conn"></param>
    /// <returns></returns>
    private static ArrayList GetSheetNames(OleDbConnection conn)
    {
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        ArrayList arrSheets = new ArrayList();
        foreach (DataRow row in dt.Rows)
        {
            arrSheets.Add(row[2]);
        }
        return arrSheets;
    }

    /// <summary>
    /// 两个DataTable的数据对拷
    /// </summary>
    /// <param name="srcTable"></param>
    /// <param name="destTable"></param>
    private static void CopyDataTable(DataTable srcTable, DataTable destTable)
    {
        foreach (DataRow row in srcTable.Rows)
        {
            DataRow newRow = destTable.NewRow();
            for (int i = 0; i < destTable.Columns.Count; i++)
            {
                newRow[i] = row[i];
            }
            destTable.Rows.Add(newRow);
        }
    }

    /// <summary>
    /// 将DataTable的内容保存到Excel中。
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="dt"></param>
    /// <param name="sheetName"></param>
    private static void DataTableToExcel(OleDbConnection conn, DataTable dt, string sheetName)
    {
        string cmd = "select * from [" + sheetName + "]";
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd, conn);
        OleDbCommandBuilder cmdBuilder = new OleDbCommandBuilder(adapter);
        cmdBuilder.QuotePrefix = "[";
        cmdBuilder.QuoteSuffix = "]";
        DataSet ds = new DataSet();         
        adapter.Fill(ds, "Table1");
        CopyDataTable(dt, ds.Tables[0]);
        adapter.Update(ds, "Table1");  
    }
    
    #endregion
}
