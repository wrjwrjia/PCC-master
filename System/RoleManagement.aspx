<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleManagement.aspx.cs" Inherits="System_RoleManagement" %>
<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>

<html>
<head id="Head1" runat="server">
<title>角色管理</title>
<script type="text/javascript" src="../js/changerows.js"></script>
<link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
function CheckAll(form) {
    for (var i=0;i<form.elements.length;i++) {
	    var e = form.elements[i];
	    if (e.name != 'chkall' && e.type=='checkbox')
		    e.checked = form.chkall.checked;
    }
}
</script>
<link href="../css/loading.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../js/loading.js" charset="gb2312"></script>
<body>
    <form id="form1" runat="server">
    <table width="99%" border="0" cellpadding="0" cellspacing="0">
  <tr> 
    <td width="31" style="height: 25px"><img src="../images/module.gif" width="20" height="22" onclick="location.href=location.href" style="cursor:hand" alt="refresh"></td>
    <td style="width: 232px; height: 25px;">
        System >> RoleManagement</td>
    <td width="990" align="right" style="height: 25px">Role Name :
        <asp:TextBox
            ID="TextBox1" runat="server" CssClass="input1"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="submit" CausesValidation="False" OnClick="btnSearch_Click"  /></td>
  </tr>
</table>
<table width="99%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td bgcolor="#9AA29A" height="5px"></td>
  </tr>
</table>

        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" Width="99%" CssClass="gridview" RowStyle-HorizontalAlign="center" EmptyDataText="No Data" OnRowDataBound="GvDataType_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="role_na" HeaderText="Role Name" />
                <asp:BoundField DataField="ProNum" HeaderText="User Qty" />
                <asp:TemplateField HeaderText="Operation">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnEdit" CssClass="lnkbtn" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="lnkbtnEdit_Click">Edit</asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnSet" CssClass="lnkbtn" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="lnkbtnSet_Click">Change</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select"><ItemTemplate>
                    <asp:CheckBox ID="chkDelete"  runat="server" />
                </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
        <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30px" align="right"><input type="checkbox" name="chkall" onClick="CheckAll(this.form)" value="on"><font color="#FF0000">Select All</font>&nbsp;
            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="submit" CausesValidation="False" OnClick="btnAdd_Click"  />&nbsp;<asp:Button  ID="btnDel" runat="server" Text="Delete" CssClass="submit" OnClientClick="return confirm('Confirm to delete?')" OnClick="btnDel_Click1" />&nbsp;
          <cc1:PopupWin ID="PopupWin1" runat="server" Visible="False" ColorStyle="Blue" style="left: 6px; top: 64px" />
        </td>
      </tr>
      </table>
        <p>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>&nbsp;
        </p>
    </form>
</body>
</html>
<script language="javascript"><!--
//senfe("表格名称","奇数行背景","偶数行背景","鼠标经过背景","点击后背景");
 senfe("gvData","#ECF9FC","#FFFFFF","#D6F1F8");
--></script>
