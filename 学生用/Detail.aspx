<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Detail" %>
<%--<%@ Register TagPrefix="uc1" TagName="Head" Src="UserControl/Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Foot" Src="UserControl/Foot.ascx" %>
--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <link href="Include/Menu.css" rel="stylesheet" type="text/css"/>
    <link href="Include/style.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form runat="server" name="form1" action ="" id="form2">
        
    </form>
    <%--<uc1:Head id="pageHead" runat="server"/>--%>
    <div id="tabs7">
	    <ul>
		    <li><a href="javascript:history.back()"><span>返回</span></a></li>
	    </ul>
    </div>
    <p class="caption" style="margin-top:45px;">教材订购详细列表:</p>
    <table width="100%" cellspacing=0 cellpadding=0 id="DetailBookList" runat="server">
	    <tr class="tdTitle">
		    <th width="40">序号</th>
		    <th width="70">学号</th>
		    <th width="70">姓名</th>
		    <th width="80">课程编号</th>
		    <th width="300">课程名称</th>
		    <th width="80">任课教师</th>
		    <th width="120">教材信息</th>
		    <th width="70">作者</th>
		    <th width="80">学期</th>
	    </tr>
    </table>
   <%-- <uc1:Foot ID="pageFoot" runat="server" />--%>
</body>
</html>
