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

/// <summary>
///DeleteFile 的摘要说明
/// </summary>
public class DeleteFile
{
	public DeleteFile()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}



    /// <summary>
    /// 判断一个文件是否正在使用函数
    /// </summary>
    /// <param name="fileName">将要判断文件的文件名</param>
    /// <returns> bool</returns>
    public static bool IsInUse(string fileName)
    {
        bool inUse = true;
        if (File.Exists(fileName))
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                inUse = false;
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return inUse;           //true表示正在使用,false没有使用
        }
        else
        {
            return false;           //文件不存在则一定没有被使用
        }
    }


    /// <summary>
    /// 删除特定目录下的文件
    /// </summary>
    public static void DeleteOverdueFile(string path)
    {
        //获取系统当前时间
        DateTime timenow = System.DateTime.Now;
        TimeSpan timespan;

        string[] FileCollection = System.IO.Directory.GetFiles(path);

        for (int i = 0; i < FileCollection.Length; i++)
        {
            DateTime createtime = File.GetCreationTime(FileCollection[i]);
            timespan = timenow - createtime;

            //删除App_Code文件夹中的过期文件(一天之前的文件)
            if (timespan.TotalDays > 1)
            {
                if (!IsInUse(FileCollection[i]))
                {
                    File.Delete(FileCollection[i]);
                }
            }
        }
    }

}
