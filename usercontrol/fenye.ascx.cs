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

public partial class usercontrol_fenye : System.Web.UI.UserControl
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
    string hfuserId;//id
    string hfdata;//time
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
    public string HfuserId
    {
        get { return hfuserId; }
        set { hfuserId = value; }
    }
    public string Hfdata
    {
        get { return hfdata; }
        set { hfdata = value; }
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
        txtPage.Text = "";

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

    //private void Bind(DataTable dt, int pagenum)
    //{
    //    object obj = this.Page.FindControl(dataid);

    //    (obj as GridView).AllowPaging = true;
    //    (obj as GridView).PagerSettings.Visible = false;
    //    (obj as GridView).PageSize = pagesize;
    //    (obj as GridView).PageIndex = pagenum - 1;
    //    (obj as GridView).DataSource = dt;
    //    (obj as GridView).DataBind();
    //    if (DataId == "GvDataType")
    //    {
    //        flag("ComoditiesType","TypeId",5);
    //    }
    //    if (DataId == "GvDataBand")
    //    {
    //        flag("Brand","BrandId",6);
    //    }
    //    if (DataId == "GvDataName")
    //    {
    //        flag("ComoditiesName","ComoditiesNameId",7);
    //    }
    //    if (DataId == "GvDataSp")
    //    {
    //        flag("Specifications","SpecificationsId",9);
    //    }
    //    if (DataId == "GvDataUnits")
    //    {
    //        flag("Units", "UnitsId",5);
    //    }
    //    if (DataId == "GvDataWT")
    //    {
    //        flag("CS_WorkTitle", "WTId",5);
    //    }
    //    if (DataId == "GvDataStaff")
    //    {
    //        flag("CS_Staff", "StaffId",11);
    //    }
    //    if (DataId == "GvDataJs")
    //    {
    //        jszt();
    //        pdkz();
    //    }
    //    if (DataId == "GvDataVW")
    //    {
    //        biaoji();
    //    }
    //}

    private void GetData(int pagenum)
    {
        DataTable dt = new DataTable();

        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionstring].ToString());

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

       // Bind(dt, pagenum);
    }

    protected void lnkbtnGoto_Click(object sender, EventArgs e)
    {
        int nowpage = Convert.ToInt32(txtPage.Text);
        int nowpages = Convert.ToInt32(lblPages.Text);

        if (nowpage > nowpages)
            nowpage = nowpages;
        else if (nowpage < 1)
            nowpage = 1;

        lblCurpage.Text = nowpage.ToString();
        if (number.Visible == true)
        {
            lnkbtn0.Text = (nowpage / 10 * 10 + 1).ToString();
        }
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
    //private void flag(string dataB,string dataZ,int cell)
    //{
    //    object obj = this.Page.FindControl(dataid);
    //    //ZSql zsql = new ZSql();
        
    //    if ((obj as GridView).Rows.Count > 0)
    //    {
    //        for (int i = 0; i < (obj as GridView).Rows.Count; i++)
    //        {
    //            string TypeId = (obj as GridView).Rows[i].Cells[0].Text;
    //            zsql.Open("select * from " + dataB + " where "+dataZ+"='" + TypeId + "' order by "+dataZ+" desc");
    //            if (zsql.m_table.Rows[0]["AuditState"].ToString() == "1")
    //            {
    //                ((obj as GridView).Rows[i].Cells[cell].FindControl("lblFlag") as Label).Text = "已通过";
    //                ((obj as GridView).Rows[i].Cells[cell].FindControl("lblFlag") as Label).ForeColor = System.Drawing.Color.FromName("#ff0000");

    //            }
    //            if (zsql.m_table.Rows[0]["AuditState"].ToString() == "0")
    //            {
    //                ((obj as GridView).Rows[i].Cells[cell].FindControl("lblFlag") as Label).Text = "未通过";
    //                ((obj as GridView).Rows[i].Cells[cell].FindControl("lblFlag") as Label).ForeColor = System.Drawing.Color.FromName("#ff0000");
    //            }
    //            if (zsql.m_table.Rows[0]["AuditState"].ToString() == "")
    //            {
    //                ((obj as GridView).Rows[i].Cells[cell].FindControl("lblFlag") as Label).Text = "未审核";
    //                ((obj as GridView).Rows[i].Cells[cell].FindControl("lblFlag") as Label).ForeColor = System.Drawing.Color.FromName("#ff0000");
    //            }
    //        }
    //    }
    //}
    //private void jszt()
    //{
    //    object obj = this.Page.FindControl(dataid);
    //    ZSql jt = new ZSql();
    //    if ((obj as GridView).Rows.Count > 0)
    //    {
    //        for (int i = 0; i < (obj as GridView).Rows.Count; i++)
    //        {
    //            string StaffName = (obj as GridView).Rows[i].Cells[1].Text.ToString();
    //            string yeData = (obj as GridView).Rows[i].Cells[8].Text.ToString();
    //            jt.Open("select * from CS_Wages where WStaffName='" + StaffName + "' and WageDate='" + yeData + "'");
    //            if (jt.m_table.Rows.Count > 0)
    //            {
    //                if (jt.m_table.Rows[0]["AuditState"].ToString() == "1")
    //                {
    //                    ((obj as GridView).Rows[i].Cells[9].FindControl("lnkbtnClearing") as LinkButton).Enabled = false;
    //                    ((obj as GridView).Rows[i].Cells[9].FindControl("lnkbtnClearing") as LinkButton).Text = "已结算";
    //                    //GvDataJs.Rows[i].BackColor = System.Drawing.Color.Yellow;
    //                    //GvDataJs.Rows[i].Attributes.Add("onmouseover", "{this.style.backgroundColor='#Efefef'}");
    //                    ((obj as GridView).Rows[i].Cells[9].FindControl("lnkbtnLook") as LinkButton).Enabled = true;
    //                }





    //            }
    //        }
    //    }
    //}
    //private void pdkz()
    //{
    //    object obj = this.Page.FindControl(dataid);
    //    if ((obj as GridView).Rows.Count > 0)
    //    {
    //        for (int i = 0; i < (obj as GridView).Rows.Count; i++)
    //        {
    //            if ((obj as GridView).Rows[i].Cells[0].Text == "&nbsp;")
    //            {
    //                (obj as GridView).Rows[i].Cells[0].Text = "暂无业绩";
    //            }
    //            if ((obj as GridView).Rows[i].Cells[6].Text == "&nbsp;")
    //            {
    //                (obj as GridView).Rows[i].Cells[6].Text = "无";
    //            }
    //            if ((obj as GridView).Rows[i].Cells[7].Text == "&nbsp;")
    //            {
    //                string a = (obj as GridView).Rows[i].Cells[3].Text;
    //                (obj as GridView).Rows[i].Cells[7].Text = a;
    //            }
    //            if ((obj as GridView).Rows[i].Cells[8].Text == "&nbsp;")
    //            {
    //                (obj as GridView).Rows[i].Cells[8].Text = "无";
    //            }
    //        }
    //    }

    //}
    //private void biaoji()
    //{
    //    object obj = this.Page.FindControl(dataid);
    //    ZSql jt = new ZSql();
    //    if ((obj as GridView).Rows.Count > 0)
    //    {
    //        for (int i = 0; i < (obj as GridView).Rows.Count; i++)
    //        {
    //            string StaffName = (obj as GridView).Rows[i].Cells[1].Text.ToString();
    //            string yeData = (obj as GridView).Rows[i].Cells[10].Text.ToString();


    //            if (hfuserId == StaffName && hfdata == yeData)
    //            {

    //                (obj as GridView).Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ECF9FC");

    //            }




    //        }
    //    }
    //}

}
