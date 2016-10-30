<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order" %>
<%--<%@ Register TagPrefix="uc1" TagName="Head" Src="UserControl/Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Foot" Src="UserControl/Foot.ascx" %>--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <link href="Include/Menu.css" rel="stylesheet" type="text/css"/>
    <link href="Include/style.css" rel="stylesheet" type="text/css"/>
</head>

<body>
    <form action="Order.aspx" name="form1" method="post" runat ="server" id="form1">
   <%-- <uc1:Head id="pageHead" runat="server"/>--%>
    <%--<div id="tabs7">
	    <ul>
		    <li><a href="Notice.aspx"><span>教材订购通知</span></a></li>
		    <li><a href="Default.aspx"><span>教材订购</span></a></li>
		    <li><a href="Cancel.aspx"><span>教材退订查询</span></a></li>
		    <li id="current"><a href="Order.aspx"><span>教材订购查询</span></a></li>
		    <li><a href="Stat.aspx"><span>班级订购统计</span></a></li>
		    <li><a href="MoreOrder.aspx"><span>选订其它教材</span></a></li>
		    <li style="float:right;"><span style="color:Red;font-weight:bold;cursor:hand;" onclick="location.href='loginout.aspx'">安全退出</span></li>
	    </ul>
    </div>--%>
    <p class="caption" style="margin-top:45px;">
        <span style="float:right;">
        学期号：<select name="SelectTerm" id="SelectTerm" runat="server">
                    
                </select>&nbsp;<asp:Button ID="SearchBtn" runat="server" OnClick="SearchBtn_Click"
            Text="查 询" />
            <asp:Button ID="MoreOrderBtn" runat="server" 
            Text="选定其它课程教材" OnClick="MoreOrderBtn_Click" />
        </span>
        已选订教材列表:  
    </p>
    <table width="100%" cellspacing=0 cellpadding=0 id="OrderedBookList" runat="server">
	    <tr class="tdTitle">
		    <th width="40">序号</th>
		    <th width="80">课程编号</th>
		    <th width="200">课程名称</th>
		    <th width="70">任课教师</th>
		    <th width="150">教材信息</th>
		    <th width="80">作者</th>
		    <th width="80">出版社</th>
		    <th width="60">参考单价</th>
		    <th width="40">版本</th>
		    <th width="80">订购日期</th>
		    <th width="70">订购类型</th>
	    </tr>
    </table>
  <%--  <uc1:Foot ID="pageFoot" runat="server" />--%>
    </form>
</body>
</html>
