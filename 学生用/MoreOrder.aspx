<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoreOrder.aspx.cs" Inherits="MoreOrder" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <link href="Include/Menu.css" rel="stylesheet" type="text/css"/>
    <link href="Include/style.css" rel="stylesheet" type="text/css"/>
</head>

<body>
    <form name="form1" runat ="server" id="form1" style="margin:0px;">
    <p class="caption" style="margin-top:45px;">
        <span style="float:right;">
        学期号：<select name="SelectTerm" id="SelectTerm" runat="server"></select>&nbsp;
            课程编号：<asp:TextBox ID="CourseID" runat="server"></asp:TextBox>
            <asp:Button ID="SearchBtn" runat="server" Text="查 询" OnClick="SearchBtn_Click" />
        </span>
        <asp:Label ID="Title" runat="server" Text="选订其它教材"></asp:Label></p>
    </form>
    <form name="form2" action="SaveOrder.aspx" method = "post" style="margin:0px;">
    <table width="100%" cellspacing="0" cellpadding="0" id="MoreBookList" runat="server">
	    <tr class="tdTitle">
		    <th width="40">选择</th>
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
    </form>
</body>
</html>
