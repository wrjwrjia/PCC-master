<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoOrder.aspx.cs" Inherits="DoOrder" %>
<%--<%@ Register TagPrefix="uc1" TagName="Head" Src="UserControl/Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Foot" Src="UserControl/Foot.ascx" %>--%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <link href="Include/Menu.css" rel="stylesheet" type="text/css"/>
    <link href="Include/style.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <%--<uc1:Head id="pageHead" runat="server"/>--%>
    <div id="tabs7">
	    <ul>
		    <li><a href="javascript:history.back()"><span>返回</span></a></li>
	    </ul>
    </div>
    <form action="SaveOrder.aspx" method="post" name="form1">
    <p class="caption">
        <asp:Label ID="Title" runat="server" Text="Label"></asp:Label>&nbsp;</p>
    <table width="100%" cellspacing=0 cellpadding=0 id="RecommendBookList" runat="server">
	    <tr class="tdTitle">
		    <th width="40">选定</th>
		    <th width="60">教材号</th>
		    <th width="200">教材名称</th>
		    <th width="70">作者</th>
		    <th width="100">出版社</th>
		    <th width="80">出版日期</th>
		    <th width="40">版本</th>
		    <th width="80">参考单价</th>
		    <th width="80">任课教师</th>
		    <th width="100">备注</th>
	    </tr>
    </table>
    </form>
    <%--<uc1:Foot ID="pageFoot" runat="server"/>--%>
</body>
</html>
