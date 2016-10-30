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

public partial class usercontrol_PageNavigator : System.Web.UI.UserControl
{
    int total;//总记录数
    int curpage;//当前页数
    int pagesize;//每页记录数
    int totalpage;//总页数
    public enum Display { Text, Num };//显示方式
    Display showtype = Display.Text;
    string dataid;//数据绑定控件id
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

    public void SetCurPage(int num)//5%1%a%s%p%x
    {
        lblCurpage.Text = num.ToString();

        if (showtype == Display.Num)
            lnkbtn0.Text = num.ToString();
    }

    protected void LinkButton_Click(object sender, EventArgs e)
    {
        switch ((sender as LinkButton).CommandName)
        {
            case "first":
                lblCurpage.Text = "1";
                break;
            case "prev":
                lblCurpage.Text = Convert.ToInt32(lblCurpage.Text) > 1 ? (Convert.ToInt32(lblCurpage.Text) - 1).ToString() : "1";
                break;
            case "next":
                lblCurpage.Text = Convert.ToInt32(lblCurpage.Text) < Convert.ToInt32(lblPages.Text) ? (Convert.ToInt32(lblCurpage.Text) + 1).ToString() : lblPages.Text;
                break;
            case "last":
                lblCurpage.Text = lblPages.Text;
                break;
            default:
                break;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (lblCurpage.Text == "")
            curpage = 1;
        else
            curpage = Convert.ToInt32(lblCurpage.Text);

        if (ViewState["query"] == null || ViewState["query"].ToString() == string.Empty)
        {
            this.Visible = false;
            return;
        }

        GetData(curpage);

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
            if (lnkbtn0.Text == "")
            {
                startpage = 1;
                endpage = startpage + 9;
            }
            else
            {
                startpage = Convert.ToInt16(lnkbtn0.Text);
                endpage = startpage + 9;
            }

            if (endpage > totalpage)
                endpage = totalpage;

            for (int i = 0; i <= 9; i++)
            {
                LinkButton lnkbtn = (this.FindControl("lnkbtn" + i) as LinkButton);
                if (i + startpage <= endpage)
                {
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

        lblCurpage.Text = curpage.ToString();
        lblTotal.Text = total.ToString();
        lblPages.Text = totalpage.ToString();
    }

    private void Bind(DataTable dt, int pagenum)
    {
        object obj = this.Page.FindControl(dataid);

        (obj as GridView).AllowPaging = true;
        (obj as GridView).PagerSettings.Visible = false;
        (obj as GridView).PageSize = pagesize;
        (obj as GridView).PageIndex = pagenum - 1;
        (obj as GridView).DataSource = dt;
        (obj as GridView).DataBind();
    }

    private void GetData(int pagenum)
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
        if (total % pagesize == 0)
            totalpage = total / pagesize;
        else
            totalpage = total / pagesize + 1;

        Bind(dt, pagenum);
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

    protected void lnkbtnTen_Click(object sender, EventArgs e)
    {
        string ten = (sender as LinkButton).CommandArgument;
        int pagenum = 1;

        if (ten == "ft")
        {
            pagenum = 1;
        }
        else if (ten == "pt")
        {
            pagenum = Convert.ToInt16(lnkbtn0.Text) - 10;
        }
        else if (ten == "nt")
        {
            pagenum = Convert.ToInt16(lnkbtn0.Text) + 10;
        }
        else if (ten == "lt")
        {
            pagenum = Convert.ToInt16(lblPages.Text) / 10 * 10 + 1;
        }

        lnkbtn0.Text = pagenum.ToString();
        lblCurpage.Text = pagenum.ToString();
    }
}
