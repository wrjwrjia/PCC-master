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
        ѧ�ںţ�<select name="SelectTerm" id="SelectTerm" runat="server">
                    
                </select>&nbsp;<asp:Button ID="SearchBtn" runat="server" OnClick="SearchBtn_Click"
            Text="�� ѯ" />
        </span>
        ���˶��̲��б�: 
    </p>
    <table width="100%" cellspacing=0 cellpadding=0 id="CanceledBookList" runat="server">
	    <tr class="tdTitle">
		    <th width="40">���</th>
		    <th width="80">�γ̱��</th>
		    <th width="220">�γ�����</th>
		    <th width="80">�ον�ʦ</th>
		    <th width="250">�̲���Ϣ</th>
		    <th width="80">����</th>
		    <th width="80">������</th>
		    <th width="60">�ο�����</th>
		    <th width="40">�汾</th>
		    <th width="80">�˶�����</th>
	    </tr>
    </table>
    </form>
</body>
</html>
