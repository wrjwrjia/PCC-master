<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="System_AddUser" %>

<html>
<head id="Head1" runat="server">
    <title>用户编辑</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    th{width:100px;}
    td a{color:red;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31" height="22">
                    <img src="../images/module.gif" width="20" height="22" alt=""/></td>
                <td style="width: 310px; text-align: left;">
                    &nbsp;<asp:Label ID="lblPath" runat="server"></asp:Label></td>
                <td width="990" align="right">
                    [<font color="red">  <a href="UserManagement.aspx" class="F_red"><font color="red">Return </font></a></font>]</td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td bgcolor="#9AA29A" height="5px">
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th   bgcolor="#FFFFFF" class="r_bg">
                    User Name :&nbsp;</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                    <asp:TextBox ID="txtUserName" CssClass="input1" runat="server" Width="155px"></asp:TextBox> <a>*</a>
              </td>
            </tr>
          
   <!--
            <tr>
                <th  bgcolor="#FFFFFF" class="r_bg">
                    Password :</th>
                <td align="left" bgcolor="#FFFFFF" class="right_bg">
                   
                    <asp:TextBox ID="txtPwd" CssClass="input1" runat="server" Width="130px" 
                        TextMode="Password"></asp:TextBox> <span style="color: #ff0000">
                        *</span></td>
            </tr>
     -->                 
            <tr>
                <th  bgcolor="#FFFFFF" class="r_bg">Role Name :&nbsp;</th>
                <td align="left" bgcolor="#FFFFFF" class="right_bg">
                    <asp:DropDownList ID="ddlRole" runat="server" Width="155px">
                    </asp:DropDownList><a></a></td>
            </tr>
          
            <tr>
                <th bgcolor="#ffffff" class="r_bg">
                </th>
                <td align="left" bgcolor="#ffffff" class="right_bg">
            <asp:Button ID="btnAdd" runat="server" Text=" Save " CssClass="submit" OnClick="btnAdd_Click" /></td>
            </tr>
        </table>
        <div style="text-align:center;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <asp:HiddenField ID="hidDeptid" runat="server" />
        <asp:HiddenField ID="Hidpwd" runat="server" />
        <asp:HiddenField ID="hidModuid" runat="server" />
    </form>
</body>
</html>

