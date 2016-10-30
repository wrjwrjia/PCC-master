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

public partial class usercontrol_WebPagerByID3 : System.Web.UI.UserControl
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

    //string tableName;
    //string sqlField;   
    //string whereClause;
    //string orderByField;

    public int Pagesize
    {
        //get { return pagesize; }
        //set { pagesize = value; }
        set { ViewState["pagesize"] = value; }
        get { return (int)ViewState["pagesize"]; }
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

    public int displaycol
    {
        set { ViewState["displaycol"] = value; }
        get { return (int)ViewState["displaycol"]; }
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
        total = SQLHelper.GetDataTable(" select ProductNumber from " + ViewState["tableName"] + " where 2 > 1 " + ViewState["whereClause"]).Rows.Count;
        Pagesize = total>0?total:1;
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
        CheckBox cb;
        Control ctl;
        ctl = Page.Form.FindControl("CheckBox4");
        

        object obj = this.Page.FindControl(dataid);
        //(obj as GridView).AllowPaging = true;
        //(obj as GridView).PagerSettings.Visible = false;
        //(obj as GridView).PageSize = pagesize;
        //(obj as GridView).PageIndex = pagenum - 1;
        (obj as GridView).DataSource = dt;
        (obj as GridView).DataBind();

        if (ctl is CheckBox)
        {
            cb = ctl as CheckBox;
            for (int i = 0; i <= (obj as GridView).Rows.Count - 1; i++)
            {
                CheckBox cbox1 = (CheckBox)(obj as GridView).Rows[i].FindControl("CheckBox1");
                CheckBox cbox2 = (CheckBox)(obj as GridView).Rows[i].FindControl("CheckBox2");
                CheckBox cbox3 = (CheckBox)(obj as GridView).Rows[i].FindControl("CheckBox3");
                if (cb.Checked == true)
                {
                    cbox1.Checked = true;
                    cbox2.Checked = true;
                    cbox3.Checked = true;
                }
                else
                {
                    cbox1.Checked = false;
                    cbox2.Checked = false;
                    cbox3.Checked = false;
                }
            }
            switch (total % 3)
            {
                case 1:
                    {
                    CheckBox cbox2 = (CheckBox)(obj as GridView).Rows[(obj as GridView).Rows.Count - 1].FindControl("CheckBox2");
                    cbox2.Visible = false;
                    CheckBox cbox3 = (CheckBox)(obj as GridView).Rows[(obj as GridView).Rows.Count - 1].FindControl("CheckBox3");
                    cbox3.Visible = false;
                    break;
                        }
                case 2:
                    {
                        CheckBox cbox3 = (CheckBox)(obj as GridView).Rows[(obj as GridView).Rows.Count - 1].FindControl("CheckBox3");
                        cbox3.Visible = false;
                        break;
                    }
            }
       
        }
   

    }
    private DataTable GenerateDataTable(string currentPage)
    {
        string sql = "";
        DataTable dt = new DataTable();
        DataTable dtdemo = new DataTable();
        //if (currentPage == "1")
        //{
            sql = "select top " + Pagesize + " " + ViewState["sqlField"] + " from " + ViewState["tableName"] + " where 2 > 1 " + ViewState["whereClause"] + " order by " + ViewState["id"];
            dtdemo = SQLHelper.GetDataTable(sql);
            dt.Columns.Add("ProductNumber1", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductNumber2", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductNumber3", System.Type.GetType("System.String"));
            int row_num = dtdemo.Rows.Count;
            int i;
            DataRow dr;
            for ( i = (Convert.ToInt32(currentPage)-1)*Pagesize; i < row_num-3; i+=3)
            {
                 dr = dt.NewRow();
                dr["productNumber1"] = dtdemo.Rows[i][0].ToString();
                dr["productNumber2"] = dtdemo.Rows[i+1][0].ToString();
                dr["productNumber3"] = dtdemo.Rows[i+2][0].ToString();
                dt.Rows.Add(dr);
            }
            if (row_num > 0)
            {
                dr = dt.NewRow();
                if (i < row_num)
                    dr["ProductNumber1"] = dtdemo.Rows[i++][0].ToString();
                if (i < row_num)
                    dr["ProductNumber2"] = dtdemo.Rows[i++][0].ToString();
                if (i < row_num)
                    dr["ProductNumber3"] = dtdemo.Rows[i][0].ToString();
                dt.Rows.Add(dr);
            }
        //}
        //else
        //{
        //    string CaculateSize = (Pagesize * (Convert.ToInt32(currentPage) - 1)).ToString();
        //    sql = "select top " + Pagesize + " " + ViewState["sqlField"] + " from " + ViewState["tableName"] + " where 2 > 1 " + ViewState["whereClause"] + @" and "+ViewState["TblID"]+" not in"
        //      + " (select top " + CaculateSize + ViewState["TblID"]+ @"  from " + ViewState["tableName"] + "  where 2 > 1 " + ViewState["whereClause"] + @" order by " + ViewState["id"] + ")  order by " + ViewState["id"] + " ";
        //    dt = SQLHelper.GetDataTable(sql);
        //    //string CaculateSize = (Pagesize * (Convert.ToInt32(currentPage) - 1)).ToString();
        //    //sql = "select top " + Pagesize + " " + ViewState["sqlField"] + " from " + ViewState["tableName"] + " where 2 > 1 " + ViewState["whereClause"] + @" and not exists"
        //    //  + " (select top " + CaculateSize + " id " + @"  from " + ViewState["tableName"] + " as T  where 2 > 1 and T.id = " + ViewState["tableName"] + ".id " + ViewState["whereClause"] + @" order by " + ViewState["id"] + ")  order by " + ViewState["id"] + " ";
        //    //dt = SQLHelper.GetDataTable(sql);
        //}
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
}
