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
		    <li><a href="Notice.aspx"><span>�̲Ķ���֪ͨ</span></a></li>
		    <li><a href="Default.aspx"><span>�̲Ķ���</span></a></li>
		    <li><a href="Cancel.aspx"><span>�̲��˶���ѯ</span></a></li>
		    <li id="current"><a href="Order.aspx"><span>�̲Ķ�����ѯ</span></a></li>
		    <li><a href="Stat.aspx"><span>�༶����ͳ��</span></a></li>
		    <li><a href="MoreOrder.aspx"><span>ѡ�������̲�</span></a></li>
		    <li style="float:right;"><span style="color:Red;font-weight:bold;cursor:hand;" onclick="location.href='loginout.aspx'">��ȫ�˳�</span></li>
	    </ul>
    </div>--%>
    <p class="caption" style="margin-top:45px;">
        <span style="float:right;">
        ѧ�ںţ�<select name="SelectTerm" id="SelectTerm" runat="server">
                    
                </select>&nbsp;<asp:Button ID="SearchBtn" runat="server" OnClick="SearchBtn_Click"
            Text="�� ѯ" />
            <asp:Button ID="MoreOrderBtn" runat="server" 
            Text="ѡ�������γ̲̽�" OnClick="MoreOrderBtn_Click" />
        </span>
        ��ѡ���̲��б�:  
    </p>
    <table width="100%" cellspacing=0 cellpadding=0 id="OrderedBookList" runat="server">
	    <tr class="tdTitle">
		    <th width="40">���</th>
		    <th width="80">�γ̱��</th>
		    <th width="200">�γ�����</th>
		    <th width="70">�ον�ʦ</th>
		    <th width="150">�̲���Ϣ</th>
		    <th width="80">����</th>
		    <th width="80">������</th>
		    <th width="60">�ο�����</th>
		    <th width="40">�汾</th>
		    <th width="80">��������</th>
		    <th width="70">��������</th>
	    </tr>
    </table>
  <%--  <uc1:Foot ID="pageFoot" runat="server" />--%>
    </form>
</body>
</html>
