<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cancel.aspx.cs" Inherits="Cancel" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <link href="Include/Menu.css" rel="stylesheet" type="text/css"/>
    <link href="Include/style.css" rel="stylesheet" type="text/css"/>
</head>

<body>
    <form action="Cancel.aspx" name="form1" method="post" runat ="server" id="form1">
    <p class="caption" style="margin-top:45px;"> 
        <span style="float:right;">
        学期号：<select name="SelectTerm" id="SelectTerm" runat="server">
                    
                </select>&nbsp;<asp:Button ID="SearchBtn" runat="server" OnClick="SearchBtn_Click"
            Text="查 询" />
        </span>
        已退订教材列表: 
    </p>
    <table width="100%" cellspacing=0 cellpadding=0 id="CanceledBookList" runat="server">
	    <tr class="tdTitle">
		    <th width="40">序号</th>
		    <th width="80">课程编号</th>
		    <th width="220">课程名称</th>
		    <th width="80">任课教师</th>
		    <th width="250">教材信息</th>
		    <th width="80">作者</th>
		    <th width="80">出版社</th>
		    <th width="60">参考单价</th>
		    <th width="40">版本</th>
		    <th width="80">退订日期</th>
	    </tr>
    </table>
    </form>
</body>
</html>
