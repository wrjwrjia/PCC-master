<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stat.aspx.cs" Inherits="Stat" %>
<%--<%@ Register TagPrefix="uc1" TagName="Head" Src="UserControl/Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Foot" Src="UserControl/Foot.ascx" %>--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <link href="Include/Menu.css" rel="stylesheet" type="text/css">
    <link href="Include/style.css" rel="stylesheet" type="text/css">
</head>

<body>
    <form id="form1" runat = "server">
   <%-- <uc1:Head id="pageHead" runat="server"/>--%>
   <%-- <div id="tabs7">
	    <ul>
		    <li><a href="Notice.aspx"><span>�̲Ķ���֪ͨ</span></a></li>
		    <li><a href="Default.aspx"><span>�̲Ķ���</span></a></li>
		    <li><a href="Cancel.aspx"><span>�̲��˶���ѯ</span></a></li>
		    <li><a href="Order.aspx"><span>�̲Ķ�����ѯ</span></a></li>
		    <li id="current"><a href="Stat.aspx"><span>�༶����ͳ��</span></a></li>
		    <li><a href="MoreOrder.aspx"><span>ѡ�������̲�</span></a></li>
		    <li style="float:right;"><span style="color:Red;font-weight:bold;cursor:hand;" onclick="location.href='loginout.aspx'">��ȫ�˳�</span></li>
	    </ul>
    </div>--%>
    <p class="caption" style="margin-top:45px;">
        <span style="float:right;">
        ѧ�ںţ�<select name="SelectTerm" id="SelectTerm" runat="server">  
                </select>&nbsp;
        �������ͣ�<select name="OrderType" id="OrderType" runat="server"/> <asp:Button ID="SearchBtn" runat="server" OnClick="SearchBtn_Click"
            Text="�� ѯ" />
        </span>
        <%=className%>-�̲Ķ���ͳ���б�:
    </p>

    <table width="100%" cellspacing=0 cellpadding=0 id="StatedBookList" runat="server">
	    <tr class="tdTitle">
		    <th width="40">���</th>
		    <th width="80">�γ̱��</th>
		    <th width="300">�γ�����</th>
		    <th width="80">�ον�ʦ</th>
		    <th width="140">�̲�����</th>
		    <th width="80">����</th>
		    <th width="80">������</th>
		     <th width="80">��������</th>
		    <th width="80">��������</th>
	    </tr>
    </table>
   <%-- <uc1:Foot ID="pageFoot" runat="server" />--%>
    </form>
</body>
</html>
