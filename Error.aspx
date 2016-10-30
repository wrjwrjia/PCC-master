<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Error</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width='100%' align='center' style='font-size: 10pt; font-family:Arial'>
            <tr align='center'>
                <td align="center" colspan="2">
                    <b>Error on page</b>
                </td>
            </tr>
            <tr>
                <td align='right' width="200">
                    <b>stackTrace :</b>
                </td>
                <td align='left'>
                    <asp:Label ID="lblStackTrace" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align='right'>
                    <b>Error message :</b>
                </td>
                <td align='left'>
                    <asp:Label ID="lblMessageError" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align='right'>
                    <b>Source :</b>
                </td>
                <td align='left'>
                    <asp:Label ID="lblSourceError" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align='right'>
                    <b>TargetSite :</b>
                </td>
                <td align='left'>
                    <asp:Label ID="lblTagetSiteError" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>