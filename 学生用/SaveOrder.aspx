<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveOrder.aspx.cs" Inherits="SaveOrder" %>
<%--<%@ Register TagPrefix="uc1" TagName="Head" Src="UserControl/Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Foot" Src="UserControl/Foot.ascx" %>--%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <link href="Include/Menu.css" rel="stylesheet" type="text/css">
    <link href="Include/style.css" rel="stylesheet" type="text/css">
</head>
<body>
    <%--<uc1:Head id="pageHead" runat="server"/>--%>
    <div id="tabs7">
	    <ul>
		    <li><a href="javascript:history.back()"><span>返回</span></a></li>
	    </ul>
    </div>
    <p class=caption>
        &nbsp;</p>
    <p class="caption">
        信息提示：</p>
    <hr>
    <asp:Label ID="Message" runat="server" Text="Label"></asp:Label>
</body>
</html>
