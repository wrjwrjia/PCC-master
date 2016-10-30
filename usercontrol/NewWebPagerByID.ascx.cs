using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class NewWebPagerByID : System.Web.UI.UserControl
{
    int total;//总记录数
    int curpage;//当前页数  
    int curpageSize;//每页记录数
    //int pagesize;//每页记录数   
    int totalpage;//总页数
    public enum Display { Text, Num };//显示方式
    Display showtype = Display.Text;
    string dataid;//数据绑定控件id
    //string url;//来源地址
    string connectionstring;//连接字符串
    ArrayList arrPara = null;//参数数组

    public int Pagesize
    {
        set { ViewState["pagesize"] = value; }
        get { return (int)ViewState["pagesize"]; }
    }


    public int hebing
    {
        set { ViewState["hebing"] = value; }
        get { return (int)ViewState["hebing"]; }
    }


    public string WhereClause
    {
        set { ViewState["whereClause"] = value; }
        get { return ViewState["whereClause"].ToString(); }
    }
    public string SqlField
    {
        set { ViewState["sqlField"] = value; }
        get { return ViewState["sqlField"].ToString(); }
    }

    //自己添加的
    public string TblID
    {
        set { ViewState["TblID"] = value; }
        get { return ViewState["TblID"].ToString(); }
    }

    public int col
    {
        set { ViewState["col"] = value; }
        get { return (int)ViewState["col"]; }
    }

    public string SQL
    {
        set { ViewState["SQL"] = value; }
        get { return ViewState["SQL"].ToString(); }
    }

    public int displaycol
    {
        set { ViewState["displaycol"] = value; }
        get { return (int)ViewState["displaycol"]; }
    }


    public int rowcount
    {
        set { ViewState["rowcount"] = value; }
        get { return (int)ViewState["rowcount"]; }
    }

     public string value
    {
        set { ViewState["value"] = value; }
        get { return ViewState["value"].ToString(); }
    }

    public string groupby
    {
        set { ViewState["groupby"] = value; }
        get { return ViewState["groupby"].ToString(); }
    }

    public string datetable
    {
        set { ViewState["datetable"] = value; }
        get { return ViewState["datetable"].ToString(); }
    }

    public string HiddenFiled
    {
        set { ViewState["HiddenFiled"] = value; }
        get { return ViewState["HiddenFiled"].ToString(); }
    }




    public string DataId
    {
        get { return dataid; }
        set { dataid = value; }
    }
    public string orderByID
    {
        set { ViewState["id"] = value; }
        get { return ViewState["id"].ToString(); }
    }
    public string TableName
    {
        set { ViewState["tableName"] = value; }
        get { return ViewState["tableName"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //No Data hide webpager
        //if (total == 0)
        //{
        //    this.Visible = false;
        //}
        //else
        //{
        //    this.Visible = true;
        //}

        this.PopupWin1.Visible = false;

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        string sql = "declare @sql varchar(8000) set @sql = 'select " + ViewState["sqlField"] + "' select @sql = @sql + ' , max(case Date when ''' + Date + ''' then " + ViewState["value"] + " else NULL end) [' + Date + ']' from (select top  " + ((int)ViewState["displaycol"] + 12).ToString() + " Date from " + ViewState["datetable"] + " where Date not in (select top " + ViewState["displaycol"].ToString() + " Date from  " + ViewState["datetable"] + " group by Date order by Date) group by Date order by Date) as a set @sql = @sql + ' from " + ViewState["tableName"] + " where 2 > 1 " + ViewState["whereClause"] + " group by  " + ViewState["groupby"] + " order by " + ViewState["id"] + "' exec(@sql) ";  
        total = SQLHelper.GetDataTable(sql).Rows.Count; 

        totalpage = total / Pagesize;
        if (total % Pagesize != 0)
        {
            totalpage++;
        }
        if (lblCurpage.Text == "")
        {
            curpage = 1;
            lblCurpage.Text = "1";
        }
        else
        {
            curpage = Convert.ToInt32(lblCurpage.Text);
        }
        Bind(GenerateDataTable(lblCurpage.Text));

        lblTotal.Text = total.ToString();//记录总数
        lblPages.Text = totalpage.ToString();//总页数

        if (Convert.ToInt32(lblCurpage.Text) > Convert.ToInt32(this.lblPages.Text))
        {
            curpage = 1;
            lblCurpage.Text = "1";
            Bind(GenerateDataTable(lblCurpage.Text));
        }

    }
    protected void LinkButton_Click(object sender, EventArgs e)
    {
        switch ((sender as LinkButton).CommandName)
        {
            case "first":
                lblCurpage.Text = "1";
                Bind(GenerateDataTable(lblCurpage.Text));
                break;
            case "prev":
                lblCurpage.Text = Convert.ToInt32(lblCurpage.Text) > 1 ? (Convert.ToInt32(lblCurpage.Text) - 1).ToString() : "1";
                Bind(GenerateDataTable(lblCurpage.Text));
                break;
            case "next":
                if (lblPages.Text != "0")
                {
                    lblCurpage.Text = Convert.ToInt32(lblCurpage.Text) < Convert.ToInt32(lblPages.Text) ? (Convert.ToInt32(lblCurpage.Text) + 1).ToString() : lblPages.Text;
                    Bind(GenerateDataTable(lblCurpage.Text));
                }
                break;
            case "last":
                if (lblPages.Text != "0")
                {
                    lblCurpage.Text = lblPages.Text;
                    Bind(GenerateDataTable(lblCurpage.Text));
                }
                break;
            default:
                break;
        }
    }
    protected void lnkbtnGoto_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtPage.Text) <= 0)
        {
            JScript.ShowMsg(PopupWin1, " Page number illegal!");
            return;
        }
        Pagesize = Convert.ToInt32(txtPage.Text);
        lblCurpage.Text = "";
    }
    private void Bind(DataTable dt)
    {
        object obj = this.Page.FindControl(dataid);
        //(obj as GridView).AllowPaging = true;
        //(obj as GridView).PagerSettings.Visible = false;
        //(obj as GridView).PageSize = pagesize;
        //(obj as GridView).PageIndex = pagenum - 1;
        (obj as GridView).DataSource = dt;
        (obj as GridView).DataBind();

        if (ViewState["hebing"] != null )
        {
            for (int i = 1; i < (int)ViewState["hebing"]; i++)
            {
                GroupRows(obj, i);
            }
        }

    }
    private DataTable GenerateDataTable(string currentPage)
    {
        string sql = "";
        DataTable dt = new DataTable();
        if (currentPage == "1")
        {
            sql = "declare @sql varchar(8000) set @sql = 'select top " + Pagesize + " " + ViewState["sqlField"] + "' select @sql = @sql + ' , max(case Date when ''' + Date + ''' then " + ViewState["value"] + " else NULL end) [' + Date + ']' from (select top  " + ((int)ViewState["displaycol"] + 12).ToString() + " Date from " + ViewState["datetable"] + " where Date not in (select top " + ViewState["displaycol"].ToString() + " Date from  " + ViewState["datetable"] + " group by Date order by Date)  group by  Date order by Date ) as a set @sql = @sql + ' from " + ViewState["tableName"] + " where 2 > 1 " + ViewState["whereClause"] + " group by  " + ViewState["groupby"] + " order by " + ViewState["id"] + "' exec(@sql) ";          
            dt = SQLHelper.GetDataTable(sql);
            
        }
        else
        {
            string CaculateSize = (Pagesize * (Convert.ToInt32(currentPage) - 1)).ToString();
            sql = "declare @sql varchar(8000) set @sql = 'select top " + (Convert.ToInt16(CaculateSize) + Convert.ToInt16(Pagesize)).ToString() + " " + ViewState["sqlField"] + "' select @sql = @sql + ' , max(case Date when ''' + Date + ''' then " + ViewState["value"] + " else NULL end) [' + Date + ']' from (select top  " + ((int)ViewState["displaycol"] + 12).ToString() + " Date from " + ViewState["datetable"] + " where Date not in (select top " + ViewState["displaycol"].ToString() + " Date from  " + ViewState["datetable"] + " group by Date order by Date) group by  Date order by Date) as a set @sql = @sql + ' from " + ViewState["tableName"] + " where 2 > 1 " + ViewState["whereClause"] + " group by  " + ViewState["groupby"] + " order by " + ViewState["id"] + "' exec(@sql) ";
            DataTable demo = SQLHelper.GetDataTable(sql);

            foreach (DataColumn dc in demo.Columns)
            {
                DataColumn newdc = new DataColumn();
                newdc.ColumnName = dc.ColumnName;
                newdc.DataType = dc.DataType;
                dt.Columns.Add(newdc);
            }

            int lastnum = Convert.ToInt16(CaculateSize) + Convert.ToInt16(Pagesize);
            if (Convert.ToInt16(CaculateSize) + Convert.ToInt16(Pagesize)>total)
            {
                lastnum = total;
            }

            for (int drvIndex = Convert.ToInt16(CaculateSize); drvIndex < lastnum; drvIndex++)
            {
                dt.ImportRow(demo.Rows[drvIndex]);
            }

        }

        //存在排序辅助列，将其删除
        if (ViewState["HiddenFiled"] != null && ViewState["HiddenFiled"].ToString() != string.Empty)
        {
            char[] separator = { ',' };
            String[] splitStrings = new String[100];
            splitStrings = ViewState["HiddenFiled"].ToString().Split(separator);
            foreach (string s in splitStrings)
            {
                dt.Columns.Remove(s);
            }
        }


        return dt;
    }
    protected void lnbGo_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtGo.Text) <= 0 || Convert.ToInt32(txtGo.Text) > Convert.ToInt32(lblPages.Text))
        {
            JScript.ShowMsg(PopupWin1, " Page number illegal!");
            return;
        }
        lblCurpage.Text = txtGo.Text;
        Bind(GenerateDataTable(lblCurpage.Text));
    }

    //   <summary>   
    ///   合并GridView列中相同的行   
    ///   </summary>   
    ///   <param   name="GridView1">GridView对象</param>   
    ///   <param   name="cellNum">需要合并的列</param>   
    public static void GroupRows(object obj,int cellNum)
    {
        int i = 0, rowSpanNum = 1;
        while (i < (obj as GridView).Rows.Count - 1)
        {
            GridViewRow gvr = (obj as GridView).Rows[i];
            for (++i; i < (obj as GridView).Rows.Count; i++)
            {
                GridViewRow gvrNext = (obj as GridView).Rows[i];
                if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                {
                    gvrNext.Cells[cellNum].Visible = false;
                    rowSpanNum++;
                }
                else
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    rowSpanNum = 1;
                    break;
                }
                if (i == (obj as GridView).Rows.Count - 1)
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                }
            }
        }
    }


}
