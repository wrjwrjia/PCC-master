<%@ Page Language="C#" AutoEventWireup="true" CodeFile="导入excel.aspx.cs" Inherits="TEST_导入excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>excel导入</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/changerows.js"></script>
    <style type="text/css">
        th{
        }
    td a{color:red;}
        .style1
        {
            BACKGROUND-COLOR: #ECF9FC;
            text-align: right;
            padding-left: 3px;
            width: 146px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31" height="22">
                    <img src="../images/module.gif" width="20" height="22" alt=""/></td>
                <td style="width: 310px; text-align: left;">
                    导入excel</td>
                <td width="990" align="right"></td>
            </tr>
        </table>
    <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">选择excel： &nbsp;</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
    <asp:FileUpload ID="FileUpload1" runat="server" Width="285px" />
              </td>
            </tr>
            <tr align="left">
                <th bgcolor="#ffffff" >
                    &nbsp;</th>
                <th bgcolor="#ffffff" >
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="导入" style="cursor:pointer"/>
                </th>
            </tr>
        </table>
    <div>
    
    </div>
    </form>
</body>
</html>
