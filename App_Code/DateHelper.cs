using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// DateHelper 的摘要说明
/// </summary>
public class DateHelper
{
	public DateHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary> 
    /// 得到本月的第一天日期 
    /// </summary> 
    /// <returns>DateTime</returns> 
    public static DateTime GetFirstDayOfMonth()
    {
        return GetFirstDayOfMonth(DateTime.Now);
    }
    /// <summary> 
    /// 得到本月的最有一天的日期 
    /// </summary> 
    /// <returns>DateTime</returns> 
    public static DateTime GetLastDayOfMonth()
    {
        return GetLastDayOfMonth(DateTime.Now);
    }
    /// <summary> 
    /// 得到一个月的第一天 
    /// </summary> 
    /// <param name="someday">这个月的随便一天</param> 
    /// <returns>DateTime</returns> 
    public static DateTime GetFirstDayOfMonth(DateTime someday)
    {
        int totalDays = DateTime.DaysInMonth(someday.Year, someday.Month);
        DateTime result;
        int ts = 1 - someday.Day;
        result = someday.AddDays(ts);
        return result;
    }
    /// <summary> 
    /// 得到一个月的最后一天 
    /// </summary> 
    /// <param name="someday">这个月的随便一天</param> 
    /// <returns>DateTime</returns> 
    public static DateTime GetLastDayOfMonth(DateTime someday)
    {
        int totalDays = DateTime.DaysInMonth(someday.Year, someday.Month);
        DateTime result;
        int ts = totalDays - someday.Day;
        result = someday.AddDays(ts);
        return result;
    }
    public static void GetMonthArrayFromDate(DateTime dtStart, DateTime dtEnd, ref string[] strResult, ref DateTime[] dtArray, ref DateTime[] dtMonthStart, ref DateTime[] dtMonthEnd)
    { 
        if (dtStart > dtEnd)
            return;
        string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //the DateTime Begin
        System.DateTime dt1 = dtStart;
        //the DateTime End
        System.DateTime dt2 = dtEnd;
        //the Month Count of the Interval
        int nMonthCount = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month + 1);
        //the string array to return
        strResult = new string[nMonthCount];
        dtArray = new DateTime[nMonthCount];
        dtMonthStart = new DateTime[nMonthCount];
        dtMonthEnd = new DateTime[nMonthCount];
        //assign the value of the string array
        System.DateTime dt = dt1;
        for (int i = 0; i < nMonthCount; i++)
        {
            dt = dt1.AddMonths(i);
            dtArray[i] = new DateTime(dt.Year, dt.Month, 1);
            strResult[i] = month[dt.Month - 1] + " " + dt.Year.ToString();
            dtMonthStart[i] = GetFirstDayOfMonth(dtArray[i]);
            dtMonthEnd[i] = GetLastDayOfMonth(dtArray[i]);
        }
        dtMonthStart[0] = dtStart;
        dtMonthEnd[nMonthCount - 1] = dtEnd;
    }
    public static void GetWeekArrayFromDate(DateTime dtStart, DateTime dtEnd, ref string[] strResult, ref DateTime[] dtWeekStart, ref DateTime[] dtWeekEnd)
    {
        if (dtStart > dtEnd)
            return;
        int weekCount = 0;
        ArrayList dtTemp = new ArrayList();
        if (dtStart.DayOfWeek.ToString() != "Sunday")
        {
            weekCount++;
            dtTemp.Add(dtStart);
        }
        for (DateTime dt = dtStart; dt <= dtEnd; dt = dt.AddDays(1))
        {
            if (dt.DayOfWeek.ToString() == "Sunday")
            {
                weekCount++;
                dtTemp.Add(dt);
            }
        }
        dtWeekStart = new DateTime[weekCount];
        dtWeekEnd = new DateTime[weekCount];
        strResult = new string[weekCount];
        for (int i = 0; i < weekCount; i++)
        {
            dtWeekStart[i] = Convert.ToDateTime(dtTemp[i]);
            if (dtStart.DayOfWeek.ToString() != "Sunday" && i == 0)
            {
                dtWeekEnd[i] = dtTemp.Count == 1 ? dtEnd : Convert.ToDateTime(dtTemp[i + 1]).AddDays(-1);
            }
            else
            {
                dtWeekEnd[i] = dtWeekStart[i].AddDays(6) > dtEnd ? dtEnd : dtWeekStart[i].AddDays(6);
            }
            string weekNumber = DateHelper.DateToWeek(dtWeekStart[i]).ToString().Length == 1 ? "0" + DateHelper.DateToWeek(dtWeekStart[i]).ToString() : DateHelper.DateToWeek(dtWeekStart[i]).ToString();
            strResult[i] = "W" + dtWeekStart[i].Year.ToString().Remove(0, 2) + weekNumber;
        }
    }
    /// <summary>
    /// 返回时间dt所在的周，周日为一周的第一天
    /// </summary>
    /// <param name="dt">DateTime类型</param>
    /// <returns>返回时间dt所在的周</returns>
    public static int DateToWeek(System.DateTime dt)
    {
        //今天星期几
        int weeknow = Convert.ToInt32(dt.DayOfWeek);
        //今日与周日的天数差
        int daydiff = 7 - weeknow;
        //周日是本年第几天
        int days = dt.AddDays(daydiff).DayOfYear;
        //
        int weeks = days / 7;
        //
        if (days % 7 != 0)
        {
            weeks++;
        }
        //最后一周如果误计算为第一周
        if (weeks == 1 && dt.DayOfYear > 50)
        {
            return (DateToWeek(dt.AddDays(-7)) + 1);
        }
        return (weeks);
    }
    public static double hoursDateDiff(System.DateTime startDate, System.DateTime endDate)
    {
        double diff = 0;
        System.TimeSpan TS = new System.TimeSpan(endDate.Ticks - startDate.Ticks);        
        diff = Convert.ToDouble(TS.TotalHours);             
        return diff;
    }
}
