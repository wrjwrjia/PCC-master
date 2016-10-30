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
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;

/// <summary>
///SheetName 的摘要说明
/// </summary>
public class SheetName
{
	public SheetName()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
   
    /// <summary>
    /// 获取Excel中的所有Sheet名,并将Excel改名保存在制定的目录下
    /// </summary>
    /// <param name="FilePath"></param>
    public static string  ChangeSheetName(string FilePath, string username,DropDownList drp)
    {
        Microsoft.Office.Interop.Excel.Application App = null;
        object missing = Missing.Value;
        Microsoft.Office.Interop.Excel.Workbooks workBooks = null;
        Microsoft.Office.Interop.Excel.Workbook myExcelbook = null;
        try
        {
            App = new Microsoft.Office.Interop.Excel.Application();
            workBooks = App.Workbooks;
            myExcelbook = workBooks.Open(FilePath, missing, false, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
            //将所有的sheet名中的空格去掉，并将所有的和修改过的sheet名存储在一个数组中
            int sheetnum = App.Sheets.Count;
            string[] sheet_name = new string[sheetnum];
            for (int i = 0; i < sheetnum; i++)
            {
                ((Microsoft.Office.Interop.Excel.Worksheet)App.Sheets[i + 1]).Name = ((Microsoft.Office.Interop.Excel.Worksheet)App.Sheets[i + 1]).Name.Replace(" ", "");
                sheet_name[i] = ((Microsoft.Office.Interop.Excel.Worksheet)App.Sheets[i + 1]).Name;
            }

            App.DisplayAlerts = false;
            App.AlertBeforeOverwriting = false;

            //登陆用户名+日期时间作为文件名,避免文件名冲突
           string  UploadFileName = username + DateTime.Now.ToString("yyyyMMddHHmmss")+".xls";
            SQLHelper.ExecuteNonQuery("update UploadFile set up_file='" + UploadFileName + "' where username='" + username + "'");
           
            string path = "~/UpLoadFiles/" + UploadFileName;
            myExcelbook.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path), missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
            myExcelbook.Close(false, FilePath, true);
            workBooks.Close();

            //确保Excel进程关闭
            App.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcelbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(App);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBooks);

            myExcelbook = null;
            workBooks = null;
            App = null;

            File.Delete(FilePath);


             drp.Items.Clear();
            //将sheet名显示在dropdownlist中
             for (int i = 0; i < sheetnum; i++)
            {
                drp.Items.Add(sheet_name[i]);
            }

             return System.Web.HttpContext.Current.Server.MapPath(path);
        }
        catch
        {
            myExcelbook.Close(false, FilePath, true);
            workBooks.Close();

            //确保Excel进程关闭
            App.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcelbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(App);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBooks);

            myExcelbook = null;
            workBooks = null;
            App = null;
            return "";
        }
    }


    /// <summary>
    /// 获取Excel路径，将Excel对应sheet中的前四行删除，并生成新的Excel保存在服务器端
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    public static string DeleteRowsForMRPExcel(string FilePath,string SheetName)
    {
        Microsoft.Office.Interop.Excel.Application App = null;
        object missing = Missing.Value;
        Microsoft.Office.Interop.Excel.Workbooks workBooks = null;
        Microsoft.Office.Interop.Excel.Workbook myExcelbook = null;
        Microsoft.Office.Interop.Excel.Worksheet Worksheet = null;
        try
        {
            App = new Microsoft.Office.Interop.Excel.Application();
            App.UserControl = true;
            App.DisplayAlerts = false;

            workBooks = App.Workbooks;
            myExcelbook = workBooks.Open(FilePath, missing, false, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            Worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelbook.Worksheets[SheetName];

            int colnum = Worksheet.UsedRange.Cells.Columns.Count;

            Worksheet.get_Range(Worksheet.Cells[1,1],Worksheet.Cells[3,colnum]).Delete(XlDeleteShiftDirection.xlShiftUp);

            App.DisplayAlerts = false;
            App.AlertBeforeOverwriting = false;

            //登陆用户名+日期时间作为文件名,避免文件名冲突
            string username = HttpContext.Current.Request.Cookies["user"].Values["name"];
            string UploadFileName = username + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            SQLHelper.ExecuteNonQuery("update UploadFile set up_file='" + UploadFileName + "' where username='" + username + "'");

            string path = "~/UpLoadFiles/" + UploadFileName;
            myExcelbook.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path), missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
            myExcelbook.Close(false, FilePath, true);
            workBooks.Close();

            //确保Excel进程关闭
            App.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcelbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(App);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBooks);

            myExcelbook = null;
            workBooks = null;
            App = null;

            File.Delete(FilePath);

            return System.Web.HttpContext.Current.Server.MapPath(path);
        }
        catch
        {
            myExcelbook.Close(false, FilePath, true);
            workBooks.Close();

            //确保Excel进程关闭
            App.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcelbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(App);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBooks);

            myExcelbook = null;
            workBooks = null;
            App = null;
            return "";
        }
    }



}
