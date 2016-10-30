using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Collections;

/// <summary>
/// PageCommon 的摘要说明
/// </summary>
public class PageCommon
{
	public PageCommon()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//ii
	}
    public static string getFlowNo()
    {
       string yearMonth = DateTime.Now.ToString("yyyyMM");
       if (SQLHelper.ExecuteScalar("select yearmonth from dict_flow_no where id = '1' ").ToString() == yearMonth)
       {
           SQLHelper.ExecuteNonQuery("update dict_flow_no set serial_no = serial_no + 1 where id = '1' ");
           string serial_no = "0000" + SQLHelper.ExecuteScalar("select serial_no from dict_flow_no where id = '1' ");
           serial_no = serial_no.Substring(serial_no.Length-3,3);
           return yearMonth + serial_no;
       }
       else
       {
           SQLHelper.ExecuteNonQuery("update dict_flow_no set yearmonth = '" + yearMonth + "',serial_no = 1 where id = '1' ");
           string serial_no = "0000" + SQLHelper.ExecuteScalar("select serial_no from dict_flow_no where id = '1' ");
           serial_no = serial_no.Substring(serial_no.Length - 3, 3);
           return yearMonth + serial_no;
       } 
    }    
    public static string getPathbymoduleid(string moduleid)
    {        
        string strpath = "";
        DataTable  menu = SQLHelper.GetDataTable("select a.menu ,(select c.menu from menu c where c.menuid=a.parentid and c.menunode=0 and c.parentid<>0) as menu1,(select e.menu from menu e where e.menuid=(select d.parentid from menu d where d.menuid=(select b.menuid from menu b where b.menuid=a.parentid ))) as menu2 from menu a where a.menunode=1 and a.menuid=" + moduleid);
        if (string.IsNullOrEmpty(menu.Rows[0]["menu1"].ToString()) && string.IsNullOrEmpty(menu.Rows[0]["menu2"].ToString()))
        {
            DataTable dt = SQLHelper.GetDataTable("select a.menu ,(select c.menu from menu c where c.menuid=a.parentid ) as menu1 from menu a where a.menunode=1 and a.menuid=" + moduleid);
            strpath = dt.Rows[0]["menu1"] + " >> " + dt.Rows[0]["menu"];
        }
        else
        {
            strpath = menu.Rows[0]["menu2"] + " >> " + menu.Rows[0]["menu1"] + " >> " + menu.Rows[0]["menu"];
        }     
        return strpath;        
    }
    public static bool setPurview(string roleid, string moduleid,  Button btnSearch, Button btnAdd, Button btnDel, Button btnIMP, Button btnEXP, Button btnMail)
    {
        DataTable isVisible = SQLHelper.GetDataTable("select isSearch,isAdd,isDel,isUpdate,isIMP,isEXP,isMail from UserPurview where moduleid = '" + moduleid + "' and roleid = '" + roleid + "' ");
        if (isVisible.Rows.Count > 0)
        {
            btnSearch.Visible = isVisible.Rows[0]["isSearch"].ToString() == "0" ? false : true;
            btnAdd.Visible = isVisible.Rows[0]["isAdd"].ToString() == "0" ? false : true;
            btnDel.Visible = isVisible.Rows[0]["isDel"].ToString() == "0" ? false : true;
            btnIMP.Visible = isVisible.Rows[0]["isIMP"].ToString() == "0" ? false : true;
            btnEXP.Visible = isVisible.Rows[0]["isEXP"].ToString() == "0" ? false : true;
            btnMail.Visible = isVisible.Rows[0]["isMail"].ToString() == "0" ? false : true;
            return isVisible.Rows[0]["isUpdate"].ToString() == "0" ? false : true;
        }
        return true;
    }
    public static void deleteFromGridView(GridView gv, string tableName, EeekSoft.Web.PopupWin PopupWin1)
    {
        int count = 0;
        if (gv.Rows.Count > 0)
        {
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                CheckBox ck = gv.Rows[i].Cells[1].FindControl("chkSelect") as CheckBox;
                Label lb = gv.Rows[i].Cells[2].FindControl("lbID") as Label;
                if (ck.Checked)
                {
                    SQLHelper.ExecuteNonQuery("delete from "+tableName+" where id = "+ lb.Text);
                    count++;
                }
            }
        }
        JScript.ShowMsg(PopupWin1,"Delete "+count.ToString()+" Records!");
        Log.writeLog(System.Web.HttpContext.Current.Request.Cookies["user"].Values["id"], System.Web.HttpContext.Current.Request.Cookies["user"].Values["name"], "Delete " + tableName, "Count: " + count.ToString());        
    }
    public static string getSelectedIDFromGridView(GridView gv)
    {
        string whereClause = "";
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            CheckBox ck = gv.Rows[i].Cells[1].FindControl("chkSelect") as CheckBox;
            Label lb = gv.Rows[i].Cells[2].FindControl("lbID") as Label;
            if (ck.Checked)
            {
                whereClause += lb.Text+",";
            }
        }
        return whereClause == "" ? "" : whereClause.Remove(whereClause.Length - 1, 1);
    }
    public static string getSelectedIMEIFromGridView(GridView gv)
    {
        string whereClause = "";
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            CheckBox ck = gv.Rows[i].Cells[1].FindControl("chkSelect") as CheckBox;
            Label lb = gv.Rows[i].Cells[2].FindControl("lbIMEI") as Label;
            if (ck.Checked)
            {
                whereClause += " '" + lb.Text + "',";
            }
        }
        return whereClause == "" ? "" : whereClause.Remove(whereClause.Length - 1, 1);
    }
    public static string returnFileUploadPath(FileUpload fu)
    {
        if (fu.PostedFile.FileName == "")
        {
            return "";
        }
        string path = "~/UpLoadFiles/" + DateTime.Now.ToString("yyyyMMddhhmmss") + fu.FileName;
        fu.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));
        return System.Web.HttpContext.Current.Server.MapPath(path);
    }

    //从控件中得到文件路径
    public static string UploadXls(FileUpload fu,Page page)
    {
        if (!fu.HasFile)
        {
            JScript.AjaxAlert(page, "Please choose excel file first!");
            return "";
        }

        //string path = "~/UpLoadFiles/" + DateTime.Now.ToString("yyyyMMddhhmmss") + fu.FileName;
        string path = "~/UpLoadFiles/" + fu.FileName;

        string fileextend;
        fileextend = fu.FileName.Substring(fu.FileName.LastIndexOf(".") + 1);
        if (fileextend != "xls")
        {
            JScript.AjaxAlert(page, "Sorry,it is not excel file!");
            return "";
        }
        else
        {
            fu.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));
        }

        return System.Web.HttpContext.Current.Server.MapPath(path);
    }






    public static string fileCopy(string oriPath,string desPath)
    {
        string path = System.Web.HttpContext.Current.Server.MapPath("~/Output/" + desPath);
        if (File.Exists(path))
        {           
           File.Delete(path);          
        }
        File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/Template/"+oriPath),path);
        return path;
    }
    public static void outExcel(string strOutputFile)
    {
        try
        {
            FileInfo file = new FileInfo(strOutputFile);
            if (file.Exists)
            {
                string attachment = "attachment; filename=" + file.Name;               
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", attachment);
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

                System.Web.HttpContext.Current.Response.WriteFile(strOutputFile);
                System.Web.HttpContext.Current.Response.Flush();
                file.Delete();
                System.Web.HttpContext.Current.Response.End();               
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static void outWord(string strOutputFile)
    {
        try
        {
            FileInfo file = new FileInfo(strOutputFile);
            if (file.Exists)
            {
                string attachment = "attachment; filename=" + HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8);
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", attachment);
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", file.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.WriteFile(strOutputFile);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
                file.Delete();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static void outPDF(string strOutputFile)
    {
        try
        {
            FileInfo file = new FileInfo(strOutputFile);
            if (file.Exists)
            {
                string attachment = "attachment; filename=" + HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8);
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", attachment);
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", file.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentType = "application/PDF";
                System.Web.HttpContext.Current.Response.WriteFile(strOutputFile);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
                file.Delete();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static void DDLProductCategory(DropDownList ddlProductCategory, DropDownList ddlCustomer_name, DropDownList ddlSite_name, DropDownList ddlEricssonPN, DropDownList ddlCustomerPN)
    {
        //Customer Name
        if (ddlProductCategory.Text == "All")
        {
            ddlCustomer_name.DataSource = SQLHelper.GetDataTable("select distinct(customer_name) from product_category ");
            ddlCustomer_name.DataTextField = "customer_name";
            ddlCustomer_name.DataBind();
            ddlCustomer_name.Items.Add("All");
            ddlCustomer_name.Text = "All";
        }
        else
        {
            ddlCustomer_name.DataSource = SQLHelper.GetDataTable("select distinct(customer_name) from product_category where category = '" + ddlProductCategory.Text + "' ");
            ddlCustomer_name.DataTextField = "customer_name";
            ddlCustomer_name.DataBind();
            ddlCustomer_name.Items.Add("All");
            ddlCustomer_name.Text = "All";
        }
        //Ericsson PN
        if (ddlCustomer_name.Text == "All" && ddlProductCategory.Text == "All")
        {
            ddlEricssonPN.DataSource = SQLHelper.GetDataTable("select distinct(typeno) from product_category ");
            ddlEricssonPN.DataTextField = "typeno";
            ddlEricssonPN.DataBind();
            ddlEricssonPN.Items.Add("All");
            ddlEricssonPN.Text = "All";
        }
        else if (ddlCustomer_name.Text == "All" && ddlProductCategory.Text != "All")
        {
            ddlEricssonPN.DataSource = SQLHelper.GetDataTable("select distinct(typeno) from product_category where  category = '" + ddlProductCategory.Text + "' ");
            ddlEricssonPN.DataTextField = "typeno";
            ddlEricssonPN.DataBind();
            ddlEricssonPN.Items.Add("All");
            ddlEricssonPN.Text = "All";
        }
        else if (ddlCustomer_name.Text != "All" && ddlProductCategory.Text == "All")
        {
            ddlEricssonPN.DataSource = SQLHelper.GetDataTable("select distinct(typeno) from product_category where  customer_name = '" + ddlCustomer_name.Text + "' ");
            ddlEricssonPN.DataTextField = "typeno";
            ddlEricssonPN.DataBind();
            ddlEricssonPN.Items.Add("All");
            ddlEricssonPN.Text = "All";
        }
        else
        {
            ddlEricssonPN.DataSource = SQLHelper.GetDataTable("select distinct(typeno) from product_category where  category = '" + ddlProductCategory.Text + "' and customer_name = '" + ddlCustomer_name.Text + "' ");
            ddlEricssonPN.DataTextField = "typeno";
            ddlEricssonPN.DataBind();
            ddlEricssonPN.Items.Add("All");
            ddlEricssonPN.Text = "All";
        }    
    }
    public static void DDLCustomerName(DropDownList ddlProductCategory, DropDownList ddlCustomer_name, DropDownList ddlSite_name, DropDownList ddlEricssonPN, DropDownList ddlCustomerPN)
    {
        //Site Name
        ddlSite_name.DataSource = SQLHelper.GetDataTable("select distinct(site_name) from product_category where  customer_name = '" + ddlCustomer_name.Text + "' ");
        ddlSite_name.DataTextField = "site_name";
        ddlSite_name.DataBind();
        ddlSite_name.Items.Add("All");
        ddlSite_name.Text = "All";

        //Ericsson PN
        if (ddlCustomer_name.Text == "All" && ddlProductCategory.Text == "All")
        {
            ddlEricssonPN.DataSource = SQLHelper.GetDataTable("select distinct(typeno) from product_category ");
            ddlEricssonPN.DataTextField = "typeno";
            ddlEricssonPN.DataBind();
            ddlEricssonPN.Items.Add("All");
            ddlEricssonPN.Text = "All";
        }
        else if (ddlCustomer_name.Text == "All" && ddlProductCategory.Text != "All")
        {
            ddlEricssonPN.DataSource = SQLHelper.GetDataTable("select distinct(typeno) from product_category where  category = '" + ddlProductCategory.Text + "' ");
            ddlEricssonPN.DataTextField = "typeno";
            ddlEricssonPN.DataBind();
            ddlEricssonPN.Items.Add("All");
            ddlEricssonPN.Text = "All";     
        }
        else if (ddlCustomer_name.Text != "All" && ddlProductCategory.Text == "All")
        {
            ddlEricssonPN.DataSource = SQLHelper.GetDataTable("select distinct(typeno) from product_category where  customer_name = '" + ddlCustomer_name.Text + "' ");
            ddlEricssonPN.DataTextField = "typeno";
            ddlEricssonPN.DataBind();
            ddlEricssonPN.Items.Add("All");
            ddlEricssonPN.Text = "All";     
        }
        else
        {
            ddlEricssonPN.DataSource = SQLHelper.GetDataTable("select distinct(typeno) from product_category where  category = '" + ddlProductCategory.Text + "' and customer_name = '" + ddlCustomer_name.Text + "' ");
            ddlEricssonPN.DataTextField = "typeno";
            ddlEricssonPN.DataBind();
            ddlEricssonPN.Items.Add("All");
            ddlEricssonPN.Text = "All";
        }
        //Customer PN
        ddlCustomerPN.DataSource = SQLHelper.GetDataTable("select distinct(customer_pn) from product_category where category = '" + ddlProductCategory.Text + "' and customer_name = '" + ddlCustomer_name.Text + "' ");
        ddlCustomerPN.DataTextField = "customer_pn";
        ddlCustomerPN.DataBind();
        ddlCustomerPN.Items.Add("All");
        ddlCustomerPN.Text = "All";
    }
    public static void BindProductCategory(DropDownList ddlProductCategory)
    {
        ddlProductCategory.DataSource = SQLHelper.GetDataTable("select distinct(category) from product_category ");
        ddlProductCategory.DataTextField = "category";
        ddlProductCategory.DataBind();
        ddlProductCategory.Items.Add("All");
        ddlProductCategory.Text = "All";
    }
    public static void BindCustomerName(DropDownList ddlCustomerName)
    {
        ddlCustomerName.DataSource = SQLHelper.GetDataTable("select distinct(customer_name) from product_category ");
        ddlCustomerName.DataTextField = "customer_name";
        ddlCustomerName.DataBind();
        ddlCustomerName.Items.Add("All");
        ddlCustomerName.Text = "All";
    } 
    public static void BindEricssonPN(DropDownList ddlEricssonPN)
    {
        ddlEricssonPN.DataSource = SQLHelper.GetDataTable("select distinct(typeno) from product_category ");
        ddlEricssonPN.DataTextField = "typeno";
        ddlEricssonPN.DataBind();
        ddlEricssonPN.Items.Add("All");
        ddlEricssonPN.Text = "All";
    }
    public static void BindFinalResult(DropDownList ddlFinalResult)
    {
        ddlFinalResult.DataSource = SQLHelper.GetDataTable("select type_item from dict_item where type_name = 'FinalResult' ");
        ddlFinalResult.DataTextField = "type_item";
        ddlFinalResult.DataBind();
        ddlFinalResult.Items.Add("All");
        ddlFinalResult.Text = "All";
    }
    public static void BindFW(DropDownList ddlFW)
    {
        ddlFW.DataSource = SQLHelper.GetDataTable("select type_item from dict_item where type_name = 'FW' ");
        ddlFW.DataTextField = "type_item";
        ddlFW.DataBind();
        ddlFW.Items.Add("All");
        ddlFW.Text = "All";
    }
    public static void BindSecondReject(DropDownList ddlSecondReject)
    {
        ddlSecondReject.DataSource = SQLHelper.GetDataTable("select type_item from dict_item where type_name = 'SecondReject' ");
        ddlSecondReject.DataTextField = "type_item";
        ddlSecondReject.DataBind();
        ddlSecondReject.Items.Add("All");
        ddlSecondReject.Text = "All";
    }
    public static void BindContactPersonCategory(DropDownList ddlCategory)
    {
        ddlCategory.DataSource = SQLHelper.GetDataTable(" select type_item from dict_item where type_name = 'ContactPerson' ");
        ddlCategory.DataTextField = "type_item";
        ddlCategory.DataBind();
        ddlCategory.Items.Add("All");
        ddlCategory.Text = "All";
    }
    public static void BindRMAType(DropDownList ddlRMAType)
    {
        ddlRMAType.DataSource = SQLHelper.GetDataTable("select type_item from dict_item where type_name = 'RMAType'");
        ddlRMAType.DataTextField = "type_item";
        ddlRMAType.DataBind();
        ddlRMAType.Items.Add("All");
        ddlRMAType.Text = "All";
    }
    public static void BindRMAStatus(DropDownList ddlRMAStatus)
    {
        ddlRMAStatus.DataSource = SQLHelper.GetDataTable("select type_item from dict_item where type_name = 'RMAStatus'");
        ddlRMAStatus.DataTextField = "type_item";
        ddlRMAStatus.DataBind();
        ddlRMAStatus.Items.Add("All");
        ddlRMAStatus.Text = "All";
    }
    public static void BindImportDate(DropDownList  ddlImportdate,string tableName)
    {
        ddlImportdate.DataSource = SQLHelper.GetDataTable(" select distinct(convert(varchar(23),importdate,21)) as importdate from "+tableName+" where importdate is not null order by importdate desc ");
        //ddlImportdate.DataTextField = "importdate";      
        ddlImportdate.DataValueField = "importdate";
        ddlImportdate.DataBind();        
    }
    public static bool judgeIMEIExists(string txtIMEI,string tableName)
    {
        if ( SQLHelper.ReturnInteger("select count(*) from " + tableName + " where imei = '" + Common.FormatParameter(txtIMEI) + "' ") == 0)
        {
            return false;
        }
        return true;
    }
    public static bool isResale(string txtIMEI, string tableName)
    {
        if (SQLHelper.ExecuteScalar("select isresale from " + tableName + " where imei = '" + Common.FormatParameter(txtIMEI) + "' ").ToString() == "1")
        {
            return true;
        }
        return false;
    }
    public static bool judgePPIDExists(string  txtPPID,string tableName)
    {
        if ( SQLHelper.ReturnInteger("select count(*) from " + tableName + " where ppid = '" + Common.FormatParameter(txtPPID) + "' ") == 0)
        {
            return false;
        }
        return true;
    }
    public static void ClearPageAllTextBox(ControlCollection objControlCollection)
    { 
       foreach(System.Web.UI.Control objControl in objControlCollection)
       {
           if (objControl.HasControls())
           {
               ClearPageAllTextBox(objControl.Controls);
           }
           else
           {
               if (objControl is System.Web.UI.WebControls.TextBox)
               {
                   ((TextBox)objControl).Text = string.Empty;
               }
               if (objControl is System.Web.UI.WebControls.DropDownList)
               {
                   ((DropDownList)objControl).Text = "All";
               }
           }
       }
    }
    public static string[,] listTranverse(List<List<string>> excelValue,int type)
    {
        if (type == 0)
        {
            //By Week
            string[,] Value = new string[excelValue[0].Count, excelValue.Count];
            for (int i = 0; i < excelValue.Count; i++)
            {
                for (int j = 0; j < excelValue[0].Count; j++)
                {
                    Value[j, i] = excelValue[i][j];
                }
            }
            return Value;
        }
        else
        { 
           //By Customer
            string[,] Value = new string[excelValue.Count, excelValue[0].Count];
            for (int i = 0; i < excelValue.Count; i++)
            {
                for (int j = 0; j < excelValue[0].Count; j++)
                {
                    Value[i, j] = excelValue[i][j];
                }
            }
            return Value;
        }
    }
    public static string[,] dtToArray(DataTable dt)
    {
        string[,] desData = new string[dt.Rows.Count, dt.Columns.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
            for (int j = 0; j < dt.Columns.Count;j++ )
                desData[i,j] = dt.Rows[i][j].ToString();
                return desData;
    }
    public static string[,] dtToArray(DataTable dt,ArrayList ar)
    {
        string[,] desData = new string[dt.Rows.Count + 1, dt.Columns.Count];
        for (int i = 0; i < dt.Rows.Count+1; i++)
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (i == 0)
                {
                    desData[i, j] = ar[j].ToString();
                }
                else
                {
                    desData[i, j] = dt.Rows[i - 1][j].ToString();
                }
            }
        return desData;
    }
    static public bool SendMailTo(string sendFrom,string sendTo,string mailSubject,string mailContent,string userName,string userPWD,string attachmentPath)
    { 
        MailMessage msg = new System.Net.Mail.MailMessage();       
        msg.To.Add(sendTo); //收件人

        //发件人信息
        msg.From = new MailAddress(sendFrom, userName, System.Text.Encoding.UTF8);
        msg.Subject = mailSubject;   //邮件标题
        msg.SubjectEncoding = System.Text.Encoding.UTF8;    //标题编码
        msg.Body = mailContent; //邮件主体
        msg.BodyEncoding = System.Text.Encoding.UTF8;
        msg.IsBodyHtml = true;  //是否HTML
        msg.Priority = MailPriority.High;   //优先级
        msg.Attachments.Add(new Attachment(attachmentPath)); //添加一个附件

        SmtpClient client = new SmtpClient();
        //设置GMail邮箱和密码 
        client.Credentials = new System.Net.NetworkCredential(userName, userPWD);
        client.Port = 587;  
        client.Host = "smtp.gmail.com";  //smtp.eapac.ericsson.se   25 eapac/
        client.EnableSsl = true;
        object userState = msg;
        try
        {
            client.Send(msg);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public static string OpenOutLook(string strBody)
    {
        string emailString = string.Empty;
        StringBuilder sbEmail = new StringBuilder();
        sbEmail.Append("mailto:");
        sbEmail.Append(HttpUtility.UrlEncode(" ", System.Text.Encoding.Default).Replace("+", "%20"));
        sbEmail.Append("?body=");
        string body = strBody;
        sbEmail.Append(HttpUtility.UrlEncode(body, System.Text.Encoding.Default));
        emailString = sbEmail.ToString();
        return emailString;
    }  

    /// <summary>
    /// 补全MRP表中的值
    /// </summary>
    public static void ADDMRP()
    {
        //写MRP值，对于已有的ProductNumber不动，没有的写入0值
        string sql = "select ProductNumber from tbl_Product group by ProductNumber order by ProductNumber ";
        DataTable ProductNumber_dt = SQLHelper.GetDataTable(sql);
        int rownum = ProductNumber_dt.Rows.Count;

        sql = "select date from tbl_mrp group by date order by date";
        DataTable date_dt = SQLHelper.GetDataTable(sql);
        int date_num = date_dt.Rows.Count;

        if (date_num == 0)   //目前MRP中没有数据
        {
            DateTime date = DateTime.Now;
            for (int i = 0; i < rownum; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    SQLHelper.ExecuteNonQuery("insert into tbl_mrp(ProductRange,ProductNumber,date,tvalue,ttype) values('','" + ProductNumber_dt.Rows[i][0].ToString() + "','" + date.AddMonths(j).ToString("yyyy-MM") + "','0','MRP')");
                    SQLHelper.ExecuteNonQuery("insert into tbl_mrp(ProductRange,ProductNumber,date,tvalue,ttype) values('','" + ProductNumber_dt.Rows[i][0].ToString() + "','" + date.AddMonths(j).ToString("yyyy-MM") + "','0','PrvMRP')");
                    SQLHelper.ExecuteNonQuery("insert into tbl_mrp(ProductRange,ProductNumber,date,tvalue,ttype) values('','" + ProductNumber_dt.Rows[i][0].ToString() + "','" + date.AddMonths(j).ToString("yyyy-MM") + "','0','Flexibility')");
                }
            }
        }
        else                       //已经有数据
        {
            for (int i = 0; i < rownum; i++)
            {
                for (int j = 0; j < date_num; j++)
                {
                    if (SQLHelper.ReturnDouble("select count(ProductNumber) from tbl_mrp where ProductNumber = '" + ProductNumber_dt.Rows[i][0].ToString() + "' and Date = '" + date_dt.Rows[j][0].ToString() + "'") == 0)
                    {
                        SQLHelper.ExecuteNonQuery("insert into tbl_mrp(ProductRange,ProductNumber,date,tvalue,ttype) values('','" + ProductNumber_dt.Rows[i][0].ToString() + "','" + date_dt.Rows[j][0].ToString() + "','0','MRP')");
                        SQLHelper.ExecuteNonQuery("insert into tbl_mrp(ProductRange,ProductNumber,date,tvalue,ttype) values('','" + ProductNumber_dt.Rows[i][0].ToString() + "','" + date_dt.Rows[j][0].ToString() + "','0','PrvMRP')");
                        SQLHelper.ExecuteNonQuery("insert into tbl_mrp(ProductRange,ProductNumber,date,tvalue,ttype) values('','" + ProductNumber_dt.Rows[i][0].ToString() + "','" + date_dt.Rows[j][0].ToString() + "','0','Flexibility')");
                    }
                }
            }
        }
    }

}
