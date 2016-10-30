//PageUrlGridView V1.1  GridView通用URL分页源码及Demo
//作者:喻涛林           时间:2009-02-24

//PageUrlGridView V1.1 更新内容：
//1.修正了当页面存在多个分页控件时，js只验证第一个控件的BUG
//2.修正了当记录总数为0时，状态页总是显示为1/0的BUG
//3.优化了页码呈现部分的逻辑代码
//4.优化了“首页”、“末页”、“前翻”、“后翻”的显示方式
//5.增加了如果总记录数为0，则不显示分页控件的功能。
//6.增加了支持多个查询字符串的功能（增加Url属性，详见www.chinaspc.com）。

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
using System.Data.Common;
using System.Data.SqlClient;

public partial class usercontrol_PageUrlGridView : System.Web.UI.UserControl
{
    int total;//总记录数
    int curpage;//当前页数
    int curpageSize;//每页记录数
    int pagesize;//每页记录数
    int totalpage;//总页数
    public enum Display { Text, Num };//显示方式
    Display showtype = Display.Text;
    string dataid;//数据绑定控件id
    //string url;//来源地址
    string connectionstring;//连接字符串
    ArrayList arrPara = null;//参数数组

    //是否存储过程
    public bool IsProcedure
    {
        set
        {
            ViewState["flag"] = value;
        }
    }

    public Display ShowType
    {
        set { showtype = value; }
    }

    public string DataId
    {
        get { return dataid; }
        set { dataid = value; }
    }


    public int PageSize
    {
        get { return pagesize; }
        set { pagesize = value; }
    }

    public int Total
    {
        get { return total; }
        set { total = value; }
    }

    public string Url
    {
        set { ViewState["Url"] = value; }
        get { return ViewState["Url"].ToString(); }
    }

    //查询SQL语句
    public string Query
    {
        set { ViewState["query"] = value; }
        get { return ViewState["query"].ToString(); }
    }

    public int CurPage
    {
        get { return Convert.ToInt16(this.lblCurpage.Text); }
    }

    public string ConnectionString
    {
        get { return connectionstring; }
        set { connectionstring = value; }
    }

    public SqlParameter[] Param
    {
        set
        {
            arrPara = new ArrayList();
            foreach (SqlParameter para in value)
            {
                Hashtable hb = new Hashtable();
                hb.Add("name", para.ParameterName);
                hb.Add("value", para.Value);
                arrPara.Add(hb);
            }
            ViewState["para"] = arrPara;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Visible = true;
        if (showtype == Display.Num)
        {
            text.Visible = false;
            number.Visible = true;
            pre.Visible = true;
            next.Visible = true;
        }
        else
        {
            text.Visible = true;
            number.Visible = false;
            pre.Visible = false;
            next.Visible = false;
        }
    }

    public void SetCurPage(int num)
    {
        lblCurpage.Text = num.ToString();

        if (showtype == Display.Num)
            lnkbtn0.Text = num.ToString();
    }



    protected void Page_PreRender(object sender, EventArgs e)
    {
        string Url = ViewState["Url"].ToString();

        if (String.IsNullOrEmpty(Request.QueryString["page"]))
            curpage = 1;
        else
            curpage = Convert.ToInt32(Request.QueryString["page"]);

        //每页显示条数
        if (String.IsNullOrEmpty(Request.QueryString["pageSize"]))
            curpageSize = pagesize;
        else
            curpageSize = Convert.ToInt32(Request.QueryString["pageSize"]);
        
        if (!IsPostBack)
        {
            if (String.IsNullOrEmpty(Request.QueryString["pageSize"]))
                txtPage.Text = pagesize.ToString();
            else
                txtPage.Text = Request.QueryString["pageSize"];
        }

        if (ViewState["query"] == null || ViewState["query"].ToString() == string.Empty)
        {
            this.Visible = false;
            return;
        }

        GetData(curpage, curpageSize);

        if (total == 0)
        {
            this.Visible = false;
            return;
        }
        else
        {
            this.Visible = true;
        }

        if (text.Visible == true)
        {
            if (curpage == 1 && totalpage > 1)
            {
                ChangeState(false, true);
            }
            else if (curpage == totalpage && totalpage > 1)
            {
                ChangeState(true, false);
            }
            else if (totalpage == 1)
            {
                ChangeState(false, false);
            }
            else
            {
                ChangeState(true, true);
            }
        }
        else
        {
            int startpage;
            int endpage;
            startpage = Convert.ToInt16(curpage);
            endpage = startpage + 9;

            if (endpage > totalpage)
                endpage = totalpage;

            for (int i = 0; i <= 9; i++)//初使化分页链接
            {
                HyperLink lnkbtn = (this.FindControl("lnkbtn" + i) as HyperLink);
                if (i + startpage <= endpage)
                {
                    lnkbtn.NavigateUrl = Url + "&page=" + (i + startpage).ToString() + "&pageSize=" + txtPage.Text;
                    lnkbtn.Text = (i + startpage).ToString();
                    if (curpage == i + startpage)
                    {
                        lnkbtn.Enabled = false;
                    }
                    else
                    {
                        lnkbtn.Enabled = true;
                    }

                    lnkbtn.Visible = true;
                }
                else
                {
                    lnkbtn.Visible = false;
                }
            }

            if (lnkbtn0.Text == "1")
                pre.Visible = false;
            else
                pre.Visible = true;

            if (lnkbtn9.Visible == false || Convert.ToInt16(lnkbtn9.Text) == totalpage)
                next.Visible = false;
            else
                next.Visible = true;
        }
        lblCurpage.Text = curpage.ToString();//页次
        lblTotal.Text = total.ToString();//记录总数
        lblPages.Text = totalpage.ToString();//总页数

        //Num 分页 下十页,最后一页,上十页,最前一页
        int PageNum;
        if (Convert.ToInt16(lblCurpage.Text) - 10 < 1) { PageNum = 1; } else { PageNum = Convert.ToInt16(lblCurpage.Text) - 10; }
        lnkbtnFTen.NavigateUrl = Url + "&page=" + PageNum.ToString() + "&pageSize=" + txtPage.Text;
        if (Convert.ToInt16(lblCurpage.Text) - 1 < 1) { PageNum = 1; } else { PageNum = Convert.ToInt16(lblCurpage.Text) - 1; }
        lnkbtnPTen.NavigateUrl = Url + "&page=" + PageNum.ToString() + "&pageSize=" + txtPage.Text;
        if (Convert.ToInt16(lblCurpage.Text) + 1 > total) { PageNum = total; } else { PageNum = Convert.ToInt16(lblCurpage.Text) +1; }
        lnkbtnNTen.NavigateUrl = Url + "&page=" + PageNum.ToString() + "&pageSize=" + txtPage.Text;
        if (Convert.ToInt16(lblCurpage.Text) + 10 > total) { PageNum = total; } else { PageNum = Convert.ToInt16(lblCurpage.Text) + 10; }
        lnkbtnLTen.NavigateUrl = Url + "&page=" + PageNum.ToString() + "&pageSize=" + txtPage.Text;

        //Text 分页  首页 上一页 下一页 末页
        lnkbtnFirst.NavigateUrl = Url + "&page=1" + "&pageSize=" + txtPage.Text;
        lnkbtnPrevious.NavigateUrl = Url + "&page=" + (curpage - 1).ToString() + "&pageSize=" + txtPage.Text;
        lnkbtnNext.NavigateUrl = Url + "&page=" + (curpage + 1).ToString() + "&pageSize=" + txtPage.Text;
        lnkbtnLast.NavigateUrl = Url + "&page=" + totalpage.ToString() + "&pageSize=" + txtPage.Text;
    }

    //数据在邦定
    private void Bind(DataTable dt, int pagenum, int pageSize)
    {
        object obj = this.Page.FindControl(dataid);

        (obj as GridView).AllowPaging = true;
        (obj as GridView).PagerSettings.Visible = false;
        (obj as GridView).PageSize = pageSize;
        (obj as GridView).PageIndex = pagenum - 1;
        (obj as GridView).DataSource = dt;
        (obj as GridView).DataBind();
    }

    private void GetData(int pagenum,int pageSize)
    {
        DataTable dt = new DataTable();

        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLCONN"].ConnectionString);

        SqlCommand sqlCmd = new SqlCommand();

        sqlCmd.Connection = sqlCon;

        if (ViewState["flag"] != null && (bool)ViewState["flag"] == true)
        {
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if (ViewState["para"] != null)
            {
                foreach (object arrary in (ArrayList)ViewState["para"])
                {
                    Hashtable htb = (Hashtable)arrary;
                    SqlParameter sqlPara = new SqlParameter(htb["name"].ToString(), htb["value"]);
                    sqlCmd.Parameters.Add(sqlPara);
                }
            }
        }

        sqlCmd.CommandText = ViewState["query"].ToString();
        sqlCmd.Connection.Open();
        dt.Load(sqlCmd.ExecuteReader());
        sqlCmd.Connection.Close();

        total = dt.Rows.Count;
        curpage = pagenum;
        if (total % pageSize == 0)
            totalpage = total / pageSize;
        else
            totalpage = total / pageSize + 1;

        Bind(dt, pagenum, pageSize);
    }

    protected void lnkbtnGoto_Click(object sender, EventArgs e)
    {
        PageSize = Convert.ToInt32(txtPage.Text);
    }

    private void ChangeState(bool b1, bool b2)
    {
        lnkbtnFirst.Enabled = b1;
        lnkbtnPrevious.Enabled = b1;
        lnkbtnNext.Enabled = b2;
        lnkbtnLast.Enabled = b2;
    }

    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        string pagenum = (sender as LinkButton).Text;
        lblCurpage.Text = pagenum;
    }

    
}
