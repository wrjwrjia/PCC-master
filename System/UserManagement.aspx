<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManagement.aspx.cs" Inherits="System_UserManagement" %>
<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>
<html>
<head id="Head1" runat="server">
    <title>User Management</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/changerows.js"></script>
    <style type="text/css">
    .titlediv{float: left; padding: 5px 2px;width:100%;}
    .AddLnk{float:right;background:url(../images/add.gif) no-repeat 2px center;padding-left:20px;margin-right:5px;}
    .LeftTitle{font-size:14px;float:left;color:Green;}
    </style>
</head>
<body style="margin-top:2px;">
    <form id="form1" runat="server">
        
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31" style="height: 25px">
                    <img src="../images/module.gif" width="20"  onclick="location.href=location.href" style="cursor:hand" alt="refresh" /></td>
                <td style="width: 242px; height: 25px;">
                    System >> UserManagement</td>
                <td width="80%" align="right">
                    User Name : <asp:TextBox
                        ID="txtUserName" runat="server" CssClass="input1"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="submit" OnClick="btnSearch_Click"  />
                   
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="submit" CausesValidation="False"
                        OnClick="btnAdd_Click" /></td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td bgcolor="#9AA29A" height="5px">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" 
            CssClass="gridview" EmptyDataText="No Data"
            Width="99%" RowStyle-HorizontalAlign="center" 
            onrowdatabound="gvData_RowDataBound" >
            <RowStyle Height="23px" />
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
             
                <asp:BoundField DataField="usr_login" HeaderText="User Name" />           
                <asp:BoundField DataField="role_na" HeaderText="Role Name"/>
              
                <asp:TemplateField HeaderText="Operation">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnEdit" CssClass="lnkbtn" runat="server" OnClick="lnkbtnEdit_Click" CommandArgument='<%#Eval("id") %>'>Edit</asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnDel" CssClass="lnkbtn" runat="server" OnClientClick="return confirm('Confirm to delete?')" OnClick="lnkbtnDel_Click" CommandArgument='<%#Eval("id") %>' >Delete</asp:LinkButton>
                     
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
        <table width="99%" border="0" cellspacing="0" cellpadding="0">
            <tr>
            <td> &nbsp;</td>
                <td align="right" style="height: 5px">
                    &nbsp;</td>
            </tr>
        </table>     
        <cc1:PopupWin ID="PopupWin1" runat="server" Visible="False" ColorStyle="Blue" style="left: 6px; top: 64px" />
        <asp:HiddenField ID="hidDeptid" runat="server" />
        <asp:HiddenField ID="HidModuId" runat="server" />
    </form>
    <script type="text/javascript">
    function setPermission(pid)
    {
        openwin("Purview.aspx?id=" + pid + "&t=2",600,600,50);
        return false;
    }
    </script>
</body>
</html>
<script language="javascript"  type="text/javascript"><!--
//senfe("表格名称","奇数行背景","偶数行背景","鼠标经过背景","点击后背景");
 senfe("GvData","#ECF9FC","#FFFFFF","#D6F1F8");
--></script>
