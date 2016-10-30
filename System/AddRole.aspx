<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRole.aspx.cs" Inherits="System_AddRole" %>

<html>
<head id="Head1" runat="server">
    <title>角色管理</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    th{width:100px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
   
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31" height="22">
                    <img src="../images/module.gif" width="20" height="22"></td>
                <td style="width: 310px; text-align: left;">
                    &nbsp;<asp:Label ID="lblPath" runat="server"></asp:Label></td>
                <td width="990" align="right">               
                    [<font color="red"> <a href="RoleManagement.aspx" class="F_red">
                    <font color="red">Return </font></a></font>]</td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td bgcolor="#9AA29A" height="5px">
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="1" class="tabGg" width="99%" >
            <tr>
                <th bgcolor="#FFFFFF" class="r_bg">
                    Role Name:</th>
                <td bgcolor="#FFFFFF" class="right_bg" >
                    <asp:TextBox ID="txtRole" runat="server" CssClass="input1"></asp:TextBox>
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Width="402px"></asp:Label>
                    </td>
            </tr>
            <tr>
                <th bgcolor="#ffffff" class="r_bg">
                </th>
                <td bgcolor="#ffffff" class="right_bg">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="submit" OnClick="btnSave_Click" />
                    <asp:HiddenField ID="hidID" runat="server" />
                </td>
            </tr>
        </table>
        <p>&nbsp;
            
        </p>
    </form>
</body>
</html>