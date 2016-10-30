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
using System.Xml;

public partial class LeftMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Object.Equals(Request.Cookies["user"], null))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                initTree();
            }
        }
    }
    private DataTable GetMenu()
    {
        string sql = "select * from menu where 1=1 ";           
        string  permission = (string)SQLHelper.ExecuteScalar("select permission from purview where roleid = " + Request.Cookies["user"].Values["roleid"]);

        if (!string.IsNullOrEmpty(permission)) //配置了菜单权限，就读取对应的权限
        {
            sql += " and menuid in (" + permission + ") ";
        }
        else                                           //未配置菜单权限，就只显示一级和二级菜单，具体的菜单不显示
        {
            sql += " and menuid in (select menuid from menu where url  is null)";
        }
        sql += " order by menuid";
        DataTable dt = SQLHelper.GetDataTable(sql);           
        return dt;
    }
    private void initTree()
    {
        DataTable menu = GetMenu();
        LblTree.Text += "<script language=\"javascript\" type=\"text/jscript\"> \n";
        LblTree.Text += "ImgDir = \"images/leftimage/\";\n";
        LblTree.Text += "SetFolderIcon(\"desktop.gif\");";
        LblTree.Text += "treeMenuAddItem(0, \"&nbspOperation Menu\");\n";
        LblTree.Text += "SetFolderIcon();\n";
        LblTree.Text += "var MenuColor=top.MenuColor;\n";
        LblTree.Text += "var MenuTextColor=\"#000000\";\n";

        for (int i = 0; i < menu.Rows.Count; i++)
        {
            if (menu.Rows[i]["parentid"].ToString() == "0")
            {
                LblTree.Text += "treeMenuAddItem(1, \"" + menu.Rows[i]["menu"] + "\");\n";

                for (int j = 0; j < menu.Rows.Count; j++)
                {
                    if (menu.Rows[i]["menuid"].ToString() == menu.Rows[j]["parentid"].ToString())
                    {
                        if (menu.Rows[j]["url"].ToString() != "")
                        {
                            LblTree.Text += "treeMenuAddItem(2, \"" + menu.Rows[j]["menu"] + "\", \"" + menu.Rows[j]["Url"] + "?moduid=" + menu.Rows[j]["menuid"] + "\", \"tabWin\",\"l4.gif\");\n";
                        }
                        else
                        {
                            LblTree.Text += "treeMenuAddItem(2, \"" + menu.Rows[j]["menu"] + "\");\n";

                            for (int k = 0; k < menu.Rows.Count; k++)
                            {
                                if (menu.Rows[j]["menuid"].ToString() == menu.Rows[k]["parentid"].ToString())
                                {

                                    LblTree.Text += "treeMenuAddItem(3, \"" + menu.Rows[k]["menu"] + "\", \"" + menu.Rows[k]["Url"] + "?moduid=" + menu.Rows[k]["menuid"] + "\", \"tabWin\",\"l4.gif\");\n";
                                }
                            }

                        }

                    }
                }
            }
        }
        LblTree.Text += "</script>\n";
        LblTree.Text += "<script language=\"JavaScript\" type=\"text/jscript\">\n";
        LblTree.Text += "initializeDocument();\n";
        LblTree.Text += "clickOnNode(0);\n";
        LblTree.Text += "</script>\n";
    }
      
}
