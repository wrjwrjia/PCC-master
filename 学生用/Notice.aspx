<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Notice.aspx.cs" Inherits="Notice" %>
<%--<%@ Register TagPrefix="uc1" TagName="Head" Src="UserControl/Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Foot" Src="UserControl/Foot.ascx" %>
--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <link href="Include/Menu.css" rel="stylesheet" type="text/css"/>
    <link href="Include/Style.css" rel="stylesheet" type="text/css"/>
    <style>
        .p1 {font-weight:bold;font-size:18pt;}
        .p2 {font-weight:bold;font-size:14pt;font-family:楷体_gb2312;}
        .p3 {margin:4px;font-size:12pt;font-family:楷体_gb2312;}
    </style>
</head>

<body>
    </div>
    <form id="form1" runat="server">
    <div>
        <p class=p1 align=center>教材订购说明</p>
        <br>
        <p class="p3"><%=regularText.Replace("\n","<br>") %></p>
    </div>
    <p align=center><asp:Button ID="ReadSta" Text="我已经阅读并同意" runat="server" OnClick="ReadSta_Click"/></p>
    </form>
    <%--<uc1:Foot ID="pageFoot" runat="server" />--%>
</body>
</html>
