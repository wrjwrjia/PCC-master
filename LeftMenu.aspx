<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeftMenu.aspx.cs" Inherits="LeftMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>NULL</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <link rel="stylesheet" href="css/ssy_left.css" type="text/css" />

    <script language="JavaScript" src="js/tree.js"></script>

</head>
<body bgcolor="#ffffff" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"
    style="overflow-x: hidden; background-repeat: repeat-y; background-position: right top;"
    background="images/leftimage/leftbg.gif">
    <table width="179" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top">
                <table width="50%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                </table>
                <table width="93%" border="0" align="right" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height: 12px">
                        
                            <asp:Label ID="LblTree" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <!----------------------------------MENU END---------------------------------->
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
   
</body>
</html>
