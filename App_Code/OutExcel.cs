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
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///OutExcel 的摘要说明
/*             
 * 
 */
/// </summary>
public class OutExcel
{
	public OutExcel()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="head"></param>
    /// <param name="absFileName"></param>
    public static void ExportToExcelForTwo(System.Data.DataTable dt, string[] head, string absFileName)
    {
        //设置多少行为一个Sheet
        int RowsToDivideSheet = 65535;
        //计算Sheet数
        int sheetCount = (dt.Rows.Count - 1) / RowsToDivideSheet + 1;
        GC.Collect();
        Application excel = null;
        _Workbook xBk = null;
        _Worksheet xSt = null;
        try
        {
            excel = new ApplicationClass();
            xBk = excel.Workbooks.Add(true);

            //excel.Visible = true;


            //循环中要使用的变量
            int dvRowStart;
            int dvRowEnd;
            //对全部Sheet进行操作
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                //计算起始行
                dvRowStart = sheetIndex * RowsToDivideSheet;
                dvRowEnd = dvRowStart + RowsToDivideSheet - 1;
                if (dvRowEnd > dt.Rows.Count - 1)
                {
                    dvRowEnd = dt.Rows.Count - 1;
                }

                //创建一个Sheet
                if (null == xSt)
                {
                    xSt = (_Worksheet)xBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                }
                else
                {
                    xSt = (_Worksheet)xBk.Worksheets.Add(Type.Missing, xSt, 1, Type.Missing);
                }
                //设置SheetName
                xSt.Name = "Excel";
                if (sheetCount > 1)
                {
                    xSt.Name += ((int)(sheetIndex + 1)).ToString();
                }

                //题头导出
                int rowCount = head.Length;
                int colCount = 1;
                object[,] dataArray = new object[colCount, rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray[j, i] = head[i];
                    }
                }
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).NumberFormatLocal = "@"; //设置单元格格式为文本
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Value2 = dataArray;
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Size = 10;


                //数据导出
                System.Data.DataTable SheetTable = new System.Data.DataTable();
                foreach (DataColumn dc in dt.Columns)
                {
                    DataColumn newdc = new DataColumn();
                    newdc.ColumnName = dc.ColumnName;
                    newdc.DataType = dc.DataType;
                    SheetTable.Columns.Add(newdc);
                }

                for (int drvIndex = dvRowStart; drvIndex <= dvRowEnd; drvIndex++)
                {
                    SheetTable.ImportRow(dt.Rows[drvIndex]);
                }
                //保存数据
                int MRProw=-1;//记忆MRP所在行号
                rowCount = SheetTable.Rows.Count;
                colCount = SheetTable.Columns.Count;
                object[,] dataArray1 = new object[rowCount, colCount];
                excel.DisplayAlerts = false;
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray1[i, j] = SheetTable.Rows[i][j];
                    }
                    if (SheetTable.Rows[i][1].ToString().Length == 0)
                    {
                        dataArray1[i, 1] = SheetTable.Rows[i][2];
                        SheetTable.Rows[i][1] = SheetTable.Rows[i][2];
                        xSt.get_Range(xSt.Cells[i + 2, 2], xSt.Cells[i + 2, 3]).Merge(Type.Missing);
                    }
                }
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray1;  
                int start = 0;
                int start1 = 0;
                string strstart = SheetTable.Rows[start][0].ToString();
                string strstart1 = SheetTable.Rows[start1][1].ToString();
                double num1, num2;
                for (int i = 0; i < rowCount; i++)
                { 
                    if (strstart.Trim().ToUpper() != SheetTable.Rows[i][0].ToString().Trim().ToUpper())
                    {
                        xSt.get_Range(xSt.Cells[start + 2, 1], xSt.Cells[i+1, 1]).Merge(Type.Missing);
                        start = i;
                        strstart = SheetTable.Rows[start][0].ToString();
                    }

                    if (SheetTable.Rows[i][1].ToString().CompareTo("MRP") == 0)
                    {
                        MRProw = i;
                    }
                    if (SheetTable.Rows[i][1].ToString().Contains("Promised Capacity"))
                    {
                        for (int j=3; j < colCount; j++)
                        {
                            if (Convert.ToDouble(SheetTable.Rows[MRProw][j].ToString()) > Convert.ToDouble(SheetTable.Rows[i][j].ToString()))
                            {
                                xSt.get_Range(xSt.Cells[i + 2, j + 1], xSt.Cells[i + 2, j + 1]).Interior.ColorIndex = 3;
                            }
                        }
                    }
                    if (SheetTable.Rows[i][1].ToString().Contains("Bottleneck Capacity"))
                    {

                        for (int j = 3; j < colCount; j++)
                        {
                            if (Convert.ToDouble(SheetTable.Rows[MRProw][j].ToString()) > Convert.ToDouble(SheetTable.Rows[i][j].ToString()))
                            {
                                xSt.get_Range(xSt.Cells[i + 2, j + 1], xSt.Cells[i + 2, j + 1]).Interior.ColorIndex = 3;
                            }
                        }
                    }
                    if (strstart1.Trim().ToUpper() != SheetTable.Rows[i][1].ToString().Trim().ToUpper())
                    {
                        xSt.get_Range(xSt.Cells[start1 + 2, 2], xSt.Cells[i + 1, 2]).Merge(Type.Missing);
                        start1 = i;
                        strstart1 = SheetTable.Rows[start1][1].ToString();
                    }
                }
                if (start != rowCount)
                {
                    xSt.get_Range(xSt.Cells[start + 2, 1], xSt.Cells[rowCount + 1, 1]).Merge(Type.Missing);
                }
                if (start1 != rowCount)
                {
                    xSt.get_Range(xSt.Cells[start1 + 2, 2], xSt.Cells[rowCount + 1, 2]).Merge(Type.Missing);
                }
                excel.DisplayAlerts = true;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).NumberFormatLocal = "0.00_ "; //保留小数位数为2;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Size = 10;

                //单元格边框
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).Borders.LineStyle = 1;

                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置居中对齐 
               xSt.Columns.AutoFit();      //自适应宽度
            }
            //删除Sheet1
            excel.DisplayAlerts = false; //注意一定要加上这句
            ((Microsoft.Office.Interop.Excel.Worksheet)xBk.Worksheets["Sheet1"]).Delete();
            excel.DisplayAlerts = true;//注意一定要加上这句


            object objOpt = System.Reflection.Missing.Value;
            excel.Visible = false;

            xBk.SaveCopyAs(absFileName);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
        catch (Exception e)
        {
            //throw (e);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="head"></param>
    /// <param name="absFileName"></param>
    public static void ExportToExcelForOne(System.Data.DataTable dt, string[] head, string absFileName)
    {
        //设置多少行为一个Sheet
        int RowsToDivideSheet = 65535;
        //计算Sheet数
        int sheetCount = (dt.Rows.Count - 1) / RowsToDivideSheet + 1;
        GC.Collect();
        Application excel = null;
        _Workbook xBk = null;
        _Worksheet xSt = null;
        try
        {
            excel = new ApplicationClass();
            xBk = excel.Workbooks.Add(true);

            //excel.Visible = true;


            //循环中要使用的变量
            int dvRowStart;
            int dvRowEnd;
            //对全部Sheet进行操作
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                //计算起始行
                dvRowStart = sheetIndex * RowsToDivideSheet;
                dvRowEnd = dvRowStart + RowsToDivideSheet - 1;
                if (dvRowEnd > dt.Rows.Count - 1)
                {
                    dvRowEnd = dt.Rows.Count - 1;
                }

                //创建一个Sheet
                if (null == xSt)
                {
                    xSt = (_Worksheet)xBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                }
                else
                {
                    xSt = (_Worksheet)xBk.Worksheets.Add(Type.Missing, xSt, 1, Type.Missing);
                }
                //设置SheetName
                xSt.Name = "Excel";
                if (sheetCount > 1)
                {
                    xSt.Name += ((int)(sheetIndex + 1)).ToString();
                }

                //题头导出
                int rowCount = head.Length;
                int colCount = 1;
                object[,] dataArray = new object[colCount, rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray[j, i] = head[i];
                    }
                }
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).NumberFormatLocal = "@"; //设置单元格格式为文本
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Value2 = dataArray;
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Size = 10;


                //数据导出
                System.Data.DataTable SheetTable = new System.Data.DataTable();
                foreach (DataColumn dc in dt.Columns)
                {
                    DataColumn newdc = new DataColumn();
                    newdc.ColumnName = dc.ColumnName;
                    newdc.DataType = dc.DataType;
                    SheetTable.Columns.Add(newdc);
                }

                for (int drvIndex = dvRowStart; drvIndex <= dvRowEnd; drvIndex++)
                {
                    SheetTable.ImportRow(dt.Rows[drvIndex]);
                }
                //保存数据
                rowCount = SheetTable.Rows.Count;
                colCount = SheetTable.Columns.Count;
                object[,] dataArray1 = new object[rowCount, colCount];
                int start=0;
                string strstart = SheetTable.Rows[start][0].ToString();
                excel.DisplayAlerts = false;
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray1[i, j] = SheetTable.Rows[i][j];
                    }
                    if (strstart.CompareTo(SheetTable.Rows[i][0].ToString()) != 0)
                    {
                        xSt.get_Range(xSt.Cells[start + 2, 1], xSt.Cells[i+1, 1]).Merge(Type.Missing);
                        start = i;
                        strstart = SheetTable.Rows[start][0].ToString();
                    }
                }
                if (start != rowCount)
                {
                    excel.DisplayAlerts = false;
                }
                xSt.get_Range(xSt.Cells[start + 2, 1], xSt.Cells[rowCount+1, 1]).Merge(Type.Missing);
                excel.DisplayAlerts = true;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).NumberFormatLocal = "0.00_ "; //保留小数位数为2;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray1;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Size = 10;

                //单元格边框
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).Borders.LineStyle = 1;

                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置居中对齐   
                xSt.Columns.AutoFit();      //自适应宽度
            }
            //删除Sheet1
            excel.DisplayAlerts = false; //注意一定要加上这句
            ((Microsoft.Office.Interop.Excel.Worksheet)xBk.Worksheets["Sheet1"]).Delete();
            excel.DisplayAlerts = true;//注意一定要加上这句
            
            object objOpt = System.Reflection.Missing.Value;
            excel.Visible = false;

            xBk.SaveCopyAs(absFileName);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
        catch (Exception e)
        {
            //throw (e);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="head"></param>
    /// <param name="absFileName"></param>
    public static void ExportToExcelForOneNode(System.Data.DataTable dt, string[] head, string absFileName)
    {
        //设置多少行为一个Sheet
        int RowsToDivideSheet = 65535;
        //计算Sheet数
        int sheetCount = (dt.Rows.Count - 1) / RowsToDivideSheet + 1;
        GC.Collect();
        Application excel = null;
        _Workbook xBk = null;
        _Worksheet xSt = null;
        try
        {
            excel = new ApplicationClass();
            xBk = excel.Workbooks.Add(true);

            //excel.Visible = true;


            //循环中要使用的变量
            int dvRowStart;
            int dvRowEnd;
            //对全部Sheet进行操作
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                //计算起始行
                dvRowStart = sheetIndex * RowsToDivideSheet;
                dvRowEnd = dvRowStart + RowsToDivideSheet - 1;
                if (dvRowEnd > dt.Rows.Count - 1)
                {
                    dvRowEnd = dt.Rows.Count - 1;
                }

                //创建一个Sheet
                if (null == xSt)
                {
                    xSt = (_Worksheet)xBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                }
                else
                {
                    xSt = (_Worksheet)xBk.Worksheets.Add(Type.Missing, xSt, 1, Type.Missing);
                }
                //设置SheetName
                xSt.Name = "Excel";
                if (sheetCount > 1)
                {
                    xSt.Name += ((int)(sheetIndex + 1)).ToString();
                }

                //题头导出
                int rowCount = head.Length;
                int colCount = 1;
                object[,] dataArray = new object[colCount, rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray[j, i] = head[i];
                    }
                }
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).NumberFormatLocal = "@"; //设置单元格格式为文本
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Value2 = dataArray;
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Size = 10;


                //数据导出
                System.Data.DataTable SheetTable = new System.Data.DataTable();
                foreach (DataColumn dc in dt.Columns)
                {
                    DataColumn newdc = new DataColumn();
                    newdc.ColumnName = dc.ColumnName;
                    newdc.DataType = dc.DataType;
                    SheetTable.Columns.Add(newdc);
                }

                for (int drvIndex = dvRowStart; drvIndex <= dvRowEnd; drvIndex++)
                {
                    SheetTable.ImportRow(dt.Rows[drvIndex]);
                }

                //保存数据
                int MRProw = -1;//记忆MRP所在行号
                rowCount = SheetTable.Rows.Count;
                colCount = SheetTable.Columns.Count;
                object[,] dataArray1 = new object[rowCount, colCount];
                int start = 0;
                string strstart = SheetTable.Rows[start][0].ToString();
                excel.DisplayAlerts = false;
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray1[i, j] = SheetTable.Rows[i][j];
                    }
                    if (strstart.CompareTo(SheetTable.Rows[i][0].ToString()) != 0)
                    {
                        xSt.get_Range(xSt.Cells[start + 2, 1], xSt.Cells[i + 1, 1]).Merge(Type.Missing);
                        start = i;
                        strstart = SheetTable.Rows[start][0].ToString();
                    }

                    if (SheetTable.Rows[i][1].ToString().CompareTo("MRP") == 0)
                    {
                        MRProw = i;
                    }
                    if (SheetTable.Rows[i][1].ToString().Contains("-shift"))
                    {
                        for (int j = 2; j < colCount; j++)
                        {
                            if (Convert.ToDouble(SheetTable.Rows[MRProw][j].ToString()) > Convert.ToDouble(SheetTable.Rows[i][j].ToString()))
                            {
                                xSt.get_Range(xSt.Cells[i + 2, j + 1], xSt.Cells[i + 2, j + 1]).Interior.ColorIndex = 3;
                            }
                        }
                    }
                }
                if (start != rowCount)
                {
                    xSt.get_Range(xSt.Cells[start + 2, 1], xSt.Cells[rowCount + 1, 1]).Merge(Type.Missing);
                }
                excel.DisplayAlerts = true;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).NumberFormatLocal = "0.00_ "; //保留小数位数为2;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray1;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Size = 10;

                //单元格边框
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).Borders.LineStyle = 1;

                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置居中对齐   
                xSt.Columns.AutoFit();      //自适应宽度

            }
            //删除Sheet1
            excel.DisplayAlerts = false; //注意一定要加上这句
            ((Microsoft.Office.Interop.Excel.Worksheet)xBk.Worksheets["Sheet1"]).Delete();
            excel.DisplayAlerts = true;//注意一定要加上这句


            object objOpt = System.Reflection.Missing.Value;
            excel.Visible = false;

            xBk.SaveCopyAs(absFileName);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
        catch (Exception e)
        {
            //throw (e);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
    }



    /*<summary> 
  * 将DataTable中的数据导出到Excel中，并在服务器端AppData文件夹中生成xls文件
  * </summary>
  * <param name="dt">要导出数据的DataTable</param>
  * <param name="head">题头数据</param>
  * <param name="absFileName">文件的绝对路径</param>
  * <returns></returns>
  */
    public static void ExportToExcel(System.Data.DataTable dt, string[] head, string absFileName)
    {
        //设置多少行为一个Sheet
        int RowsToDivideSheet = 65535;
        //计算Sheet数
        int sheetCount = (dt.Rows.Count - 1) / RowsToDivideSheet + 1;
        GC.Collect();
        Application excel = null;
        _Workbook xBk = null;
        _Worksheet xSt = null;
        try
        {
            excel = new ApplicationClass();
            xBk = excel.Workbooks.Add(true);

            //excel.Visible = true;


            //循环中要使用的变量
            int dvRowStart;
            int dvRowEnd;
            //对全部Sheet进行操作
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                //计算起始行
                dvRowStart = sheetIndex * RowsToDivideSheet;
                dvRowEnd = dvRowStart + RowsToDivideSheet - 1;
                if (dvRowEnd > dt.Rows.Count - 1)
                {
                    dvRowEnd = dt.Rows.Count - 1;
                }

                //创建一个Sheet
                if (null == xSt)
                {
                    xSt = (_Worksheet)xBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                }
                else
                {
                    xSt = (_Worksheet)xBk.Worksheets.Add(Type.Missing, xSt, 1, Type.Missing);
                }
                //设置SheetName
                xSt.Name = "Excel";
                if (sheetCount > 1)
                {
                    xSt.Name += ((int)(sheetIndex + 1)).ToString();
                }

                //题头导出
                int rowCount = head.Length;
                int colCount = 1;
                object[,] dataArray = new object[colCount, rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray[j, i] = head[i];
                    }
                }
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).NumberFormatLocal = "@"; //设置单元格格式为文本
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Value2 = dataArray;
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Size = 10;
                

                //数据导出
                System.Data.DataTable SheetTable = new System.Data.DataTable();
                foreach (DataColumn dc in dt.Columns)
                {
                    DataColumn newdc = new DataColumn();
                    newdc.ColumnName = dc.ColumnName;
                    newdc.DataType = dc.DataType;
                    SheetTable.Columns.Add(newdc);
                }

                for (int drvIndex = dvRowStart; drvIndex <= dvRowEnd; drvIndex++)
                {
                    SheetTable.ImportRow(dt.Rows[drvIndex]);
                }
                //保存数据
                rowCount = SheetTable.Rows.Count;
                colCount = SheetTable.Columns.Count;
                object[,] dataArray1 = new object[rowCount, colCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray1[i, j] = SheetTable.Rows[i][j];
                    }
                }
              //注释掉下句可以解决无法读取时间的格式的问题
              //xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).NumberFormatLocal = "0"; //保留小数位数为2;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray1;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Size = 10;

                //单元格边框
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).Borders.LineStyle = 1;

                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).HorizontalAlignment   =   Excel.XlVAlign.xlVAlignCenter;//设置居中对齐   
                xSt.Columns.AutoFit();      //自适应宽度
            }
            //删除Sheet1
            excel.DisplayAlerts = false; //注意一定要加上这句
            ((Microsoft.Office.Interop.Excel.Worksheet)xBk.Worksheets["Sheet1"]).Delete();
            excel.DisplayAlerts = true;//注意一定要加上这句

            object objOpt = System.Reflection.Missing.Value;
            excel.Visible = false;

            xBk.SaveCopyAs(absFileName);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
        catch (Exception e)
        {
            //throw (e);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
    }


    /*<summary> 
* 将DataTable中的数据导出到Excel中，并在服务器端AppData·文件夹中生成xls文件
* </summary>
* <param name="dt">要导出数据的DataTable</param>
* <param name="head">题头数据</param>
* <param name="absFileName">文件的绝对路径</param>
* <returns></returns>
*/
    public static void ExportToExcelDiagram(System.Data.DataTable dt, string[] head, string absFileName)
    {
        //设置多少行为一个Sheet
        int RowsToDivideSheet = 65535;
        //计算Sheet数
        int sheetCount = (dt.Rows.Count - 1) / RowsToDivideSheet + 1;
        GC.Collect();
        string excelTemplateDPath; //EXCEL模板默认服务器物理存放路径 
        string tempEFilePath = "\\TempFiles"; //EXCEL临时文件保存服务器物理存放路径 
        string tempEFileXPath = "/TempFiles"; //EXCEL临时文件保存服务器虚拟存放路径 
        Excel.Application excel = null;
        Excel._Workbook xBk=null ;
        Excel._Worksheet xSt=null ;
        Excel.Workbooks workbooks; //工作簿集合  
        Excel.Sheets sheets; //SHEET页集合 

        #region 
        //读取配置文件中路线模板路径及名称并验证是否存在 //获取配置文件中路线模板路径及名称
        excelTemplateDPath = System.Web.HttpContext.Current.Server.MapPath("~/moban");
        tempEFileXPath = "~/moban" + tempEFileXPath;
        tempEFilePath = excelTemplateDPath + tempEFilePath;
        excelTemplateDPath += "\\TaskTemplet.xls";

        //验证EXCEL临时文件夹是否存在 
        if (!File.Exists(tempEFilePath))
        { Directory.CreateDirectory(tempEFilePath); }
        #endregion 


        try
        {
            #region 
            //启动excel进程并加载模板
            //启动EXCEL进程 
            excel = new Excel.Application(); 
            excel.Visible = false;
            excel.UserControl = true;
            excel.DisplayAlerts = false;
            //加载读取模板 
            workbooks = excel.Workbooks;
            xBk = workbooks.Add(excelTemplateDPath);
            sheets = xBk.Worksheets;
            xSt = (Excel._Worksheet)sheets.get_Item(1);
            #endregion 



            //循环中要使用的变量
            int dvRowStart;
            int dvRowEnd;
            //对全部Sheet进行操作
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                //计算起始行
                dvRowStart = sheetIndex * RowsToDivideSheet;
                dvRowEnd = dvRowStart + RowsToDivideSheet - 1;
                if (dvRowEnd > dt.Rows.Count - 1)
                {
                    dvRowEnd = dt.Rows.Count - 1;
                }

                //创建一个Sheet
                if (null == xSt)
                {
                    xSt = (Excel._Worksheet)xBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                }
                else
                {
                    xSt = (Excel._Worksheet)xBk.Worksheets.Add(Type.Missing, xSt, 1, Type.Missing);
                }
                //设置SheetName
                xSt.Name = "Excel";
                if (sheetCount > 1)
                {
                    xSt.Name += ((int)(sheetIndex + 1)).ToString();
                }

                //题头导出
                int rowCount = head.Length;
                int colCount = 1;
                object[,] dataArray = new object[colCount, rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray[j, i] = head[i];
                    }
                }
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).NumberFormatLocal = "@"; //设置单元格格式为文本
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Value2 = dataArray;
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Size = 10;


                //数据导出
                System.Data.DataTable SheetTable = new System.Data.DataTable();
                foreach (DataColumn dc in dt.Columns)
                {
                    DataColumn newdc = new DataColumn();
                    newdc.ColumnName = dc.ColumnName;
                    newdc.DataType = dc.DataType;
                    SheetTable.Columns.Add(newdc);
                }

                for (int drvIndex = dvRowStart; drvIndex <= dvRowEnd; drvIndex++)
                {
                    SheetTable.ImportRow(dt.Rows[drvIndex]);
                }
                //保存数据
                rowCount = SheetTable.Rows.Count;
                colCount = SheetTable.Columns.Count;
                object[,] dataArray1 = new object[rowCount, colCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray1[i, j] = SheetTable.Rows[i][j];
                    }

                }
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).NumberFormatLocal = "0.00_ "; //保留小数位数为2;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray1;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Size = 10;

                //单元格边框
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).Borders.LineStyle = 1;

                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置居中对齐   
                xSt.Columns.AutoFit();      //自适应宽度

                //设置图标的标题
                string Charttitle = dt.Rows[0][0].ToString();

            }
            //删除Sheet1
            excel.DisplayAlerts = false; //注意一定要加上这句
            ((Microsoft.Office.Interop.Excel.Worksheet)xBk.Worksheets["Sheet1"]).Delete();
            excel.DisplayAlerts = true;//注意一定要加上这句

            object objOpt = System.Reflection.Missing.Value;
            excel.Visible = false;

            xBk.SaveCopyAs(absFileName);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
        catch (Exception e)
        {
            //throw (e);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
    }

    /*<summary> 
* 将DataTable中的数据导出到Excel中，并在服务器端AppData文件夹中生成xls文件
* </summary>
* <param name="dt">要导出数据的DataTable</param>
* <param name="head">题头数据</param>
* <param name="absFileName">文件的绝对路径</param>
* <returns></returns>
*/
    public static void ExportToExcelDiagramfor9(System.Data.DataTable dt, string[] head, string absFileName)
    {
        //设置多少行为一个Sheet
        int RowsToDivideSheet = 65535;
        //计算Sheet数
        int sheetCount = (dt.Rows.Count - 1) / RowsToDivideSheet + 1;
        GC.Collect();
        string excelTemplateDPath; //EXCEL模板默认服务器物理存放路径 
        string tempEFilePath = "\\TempFiles"; //EXCEL临时文件保存服务器物理存放路径 
        string tempEFileXPath = "/TempFiles"; //EXCEL临时文件保存服务器虚拟存放路径 
        Excel.Application excel = null;
        Excel._Workbook xBk = null;
        Excel._Worksheet xSt = null;
        Excel.Workbooks workbooks; //工作簿集合  
        Excel.Sheets sheets; //SHEET页集合 

        #region
        //读取配置文件中路线模板路径及名称并验证是否存在 //获取配置文件中路线模板路径及名称
        excelTemplateDPath = System.Web.HttpContext.Current.Server.MapPath("~/moban");
        tempEFileXPath = "~/moban" + tempEFileXPath;
        tempEFilePath = excelTemplateDPath + tempEFilePath;
        excelTemplateDPath += "\\TaskTemplet9.xls";

        //验证EXCEL临时文件夹是否存在 
        if (!File.Exists(tempEFilePath))
        { Directory.CreateDirectory(tempEFilePath); }
        #endregion


        try
        {
            #region
            //启动excel进程并加载模板
            //启动EXCEL进程 
            excel = new Excel.Application();
            excel.Visible = false;
            excel.UserControl = true;
            excel.DisplayAlerts = false;
            //加载读取模板 
            workbooks = excel.Workbooks;
            xBk = workbooks.Add(excelTemplateDPath);
            sheets = xBk.Worksheets;
            xSt = (Excel._Worksheet)sheets.get_Item(1);
            #endregion



            //循环中要使用的变量
            int dvRowStart;
            int dvRowEnd;
            //对全部Sheet进行操作
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                //计算起始行
                dvRowStart = sheetIndex * RowsToDivideSheet;
                dvRowEnd = dvRowStart + RowsToDivideSheet - 1;
                if (dvRowEnd > dt.Rows.Count - 1)
                {
                    dvRowEnd = dt.Rows.Count - 1;
                }

                //创建一个Sheet
                if (null == xSt)
                {
                    xSt = (Excel._Worksheet)xBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                }
                else
                {
                    xSt = (Excel._Worksheet)xBk.Worksheets.Add(Type.Missing, xSt, 1, Type.Missing);
                }
                //设置SheetName
                xSt.Name = "Excel";
                if (sheetCount > 1)
                {
                    xSt.Name += ((int)(sheetIndex + 1)).ToString();
                }

                //题头导出
                int rowCount = head.Length;
                int colCount = 1;
                object[,] dataArray = new object[colCount, rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray[j, i] = head[i];
                    }
                }
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).NumberFormatLocal = "@"; //设置单元格格式为文本
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Value2 = dataArray;
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[colCount, rowCount]).Font.Size = 10;


                //数据导出
                System.Data.DataTable SheetTable = new System.Data.DataTable();
                foreach (DataColumn dc in dt.Columns)
                {
                    DataColumn newdc = new DataColumn();
                    newdc.ColumnName = dc.ColumnName;
                    newdc.DataType = dc.DataType;
                    SheetTable.Columns.Add(newdc);
                }

                for (int drvIndex = dvRowStart; drvIndex <= dvRowEnd; drvIndex++)
                {
                    SheetTable.ImportRow(dt.Rows[drvIndex]);
                }
                //保存数据
                rowCount = SheetTable.Rows.Count;
                colCount = SheetTable.Columns.Count;
                object[,] dataArray1 = new object[rowCount, colCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray1[i, j] = SheetTable.Rows[i][j];
                    }

                }
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).NumberFormatLocal = "0.00_ "; //保留小数位数为2;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray1;
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Name = "Arial";
                xSt.get_Range(xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]).Font.Size = 10;

                //单元格边框
                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).Borders.LineStyle = 1;

                xSt.get_Range(xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置居中对齐   
                xSt.Columns.AutoFit();      //自适应宽度

                //设置图标的标题
                string Charttitle = dt.Rows[0][0].ToString();
             
            }
            //删除Sheet1
            excel.DisplayAlerts = false; //注意一定要加上这句
            ((Microsoft.Office.Interop.Excel.Worksheet)xBk.Worksheets["Sheet1"]).Delete();
            excel.DisplayAlerts = true;//注意一定要加上这句

            object objOpt = System.Reflection.Missing.Value;
            excel.Visible = false;

            xBk.SaveCopyAs(absFileName);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
        catch (Exception e)
        {
            //throw (e);
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
        }
    }
    public static void CreateExcelFileForDataTable(System.Data.DataTable table, string strFullFilePath, string title)
{
    //文件存在时先删除文件后再进行下一步操作
    FileInfo file = new FileInfo(strFullFilePath);
    if(file.Exists)
    {
      file.Delete();
    }
    int rowIndex=3;      //开始写入数据的单元格行
    int colIndex=0;      //开始写入数据的单元格列
    System.Reflection.Missing miss = System.Reflection.Missing.Value; 
    Excel.ApplicationClass mExcel = new Excel.ApplicationClass(); 
    mExcel.Visible = false; 
    Excel.Workbooks mBooks = (Excel.Workbooks)mExcel.Workbooks; 
    Excel.Workbook mBook = (Excel.Workbook)(mBooks.Add(miss)); 
    Excel.Worksheet mSheet = (Excel.Worksheet)mBook.ActiveSheet; 
    Excel.Range er = mSheet.get_Range((object)"A1",System.Reflection.Missing.Value); //向Excel文件中写入标题文本
    er.Value2 = title;
    try
    {
         foreach(DataColumn col in table.Columns)    //将所得到的表的列名,赋值给单元格
         {
             colIndex++; 
              mSheet.Cells[3,colIndex]=col.ColumnName;    
         }
         foreach(DataRow row in table.Rows)     //同样方法处理数据
         {
      rowIndex++;
      colIndex=0;
      foreach(DataColumn col in table.Columns)
      {
           colIndex++;
           if(colIndex==2)
           {
                mSheet.Cells[rowIndex,colIndex]="'"+row[col.ColumnName].ToString();//第二行数据为银行帐号信息转换为字符防止首位0丢失
           }
           else
           {
                mSheet.Cells[rowIndex,colIndex]=row[col.ColumnName].ToString();
           }
          }
     }
     //保存工作已写入数据的工作表
 mBook.SaveAs(strFullFilePath, miss, miss, miss, miss,miss, Excel.XlSaveAsAccessMode.xlNoChange, miss,miss,miss, miss, miss); 
    // return true;
   }
    catch(Exception ee)
    {
         throw new Exception(ee.Message);
    }
    finally    //finally中的代码主要用来释放内存和中止进程()
    {
         mBook.Close(false, miss, miss); 
         mBooks.Close(); 
         mExcel.Quit(); 
         System.Runtime.InteropServices.Marshal.ReleaseComObject(er); 
         System.Runtime.InteropServices.Marshal.ReleaseComObject(mSheet); 
         System.Runtime.InteropServices.Marshal.ReleaseComObject(mBook); 
         System.Runtime.InteropServices.Marshal.ReleaseComObject(mBooks); 
         System.Runtime.InteropServices.Marshal.ReleaseComObject(mExcel); 
         GC.Collect(); 
    }
    //return false;
}

}
