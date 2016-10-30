<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%--<%@ Register TagPrefix="uc1" TagName="Head" Src="UserControl/Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Foot" Src="UserControl/Foot.ascx" %>--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <link href="Include/Menu.css" rel="stylesheet" type="text/css"/>
    <link href="Include/style.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form runat="server" name="form1" action ="" id="form1">
        
    </form>
   <%-- <uc1:Head id="pageHead" runat="server"/>--%>
   <%-- <div id="tabs7">
	    <ul>
		    <li><a href="Notice.aspx"><span>教材订购通知</span></a></li>
		    <li id="current"><a href="Default.aspx"><span>教材订购</span></a></li>
		    <li><a href="Cancel.aspx"><span>教材退订查询</span></a></li>
		    <li><a href="Order.aspx"><span>教材订购查询</span></a></li>
		    <li><a href="Stat.aspx"><span>班级订购统计</span></a></li>
		    <li><a href="MoreOrder.aspx"><span>选订其它教材</span></a></li>
		    <li style="float:right;"><span style="color:Red;font-weight:bold;cursor:hand;" onclick="location.href='loginout.aspx'">安全退出</span></li>
	    </ul>
    </div>--%>
    <div style="margin-top:20px;">
	    <ul>教材在线订购<hr>
		    <li>学号：<asp:Label ID="StuID" runat="server" Text="Label"></asp:Label>
                姓名：<asp:Label ID="StuName" runat="server" Text="Label"></asp:Label>
                班级：<asp:Label ID="ClassName" runat="server" Text="Label"></asp:Label>
                年级：<asp:Label ID="StuGrade" runat="server" Text="Label"></asp:Label>
            <li>教材订购系统开放状态:<b><asp:Label ID="OpenSta" runat="server" Text="Label"></asp:Label></b>
		    <li>系统开放时间：<b><asp:Label ID="StartDate" runat="server" Text="Label"></asp:Label>-----<asp:Label
                ID="EndDate" runat="server" Text="Label"></asp:Label>
                系统当前时间：<asp:Label ID="CurDate" runat="server" Text="Label"></asp:Label></b>
            <li>当前教材订购年级：<b><asp:Label ID="LimitGrade" runat="server" Text="Label"></asp:Label></b>
	    </ul>
    </div>
    <hr>
    <p class="caption">已选订教材列表:</p>
    <table width="100%" cellspacing=0 cellpadding=0 id="SelectedBookList" runat="server">
	    <tr class="tdTitle">
		    <th width="40">序号</th>
		    <th width="80">课程编号</th>
		    <th width="200">课程名称</th>
		    <th width="80">任课教师</th>
		    <th width="200">教材信息</th>
		    <th width="80">作者</th>
		    <th width="80">课程类型</th>
		    <th width="80">退订</th>
	    </tr>
    </table>
    <br>
    <p class="caption">已选课课程列表:</p>
    <table width="100%" cellspacing=0 cellpadding=0 id="SelectedCourseList" runat="server">
	    <tr class="tdTitle">
		    <th width=40>序号</th>
		    <th width="80">课程编号</th>
		    <th width="600">课程名称</th>
		    <th width="80">任课教师</th>
		    <th width="80">课程类型</th>
		    <th width="80">订购教材</th>
	    </tr>
    </table>
  <%--  <uc1:Foot ID="pageFoot" runat="server" />--%>
    <%
        if (!isOpen)
        { 
    %>
        <script language = "javascript">
            alert("教材订购系统已关闭");
        </script>
    <%        
        }
    %>
</body>
</html>
