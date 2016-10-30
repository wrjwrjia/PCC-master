<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="bg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title><%=System.Configuration.ConfigurationSettings.AppSettings["Title"].ToString()%></title>
    <style type="text/css">
.Button
{
	border-right: #4d4a46 1px solid;
	padding-right: 1px;
	border-top: #4d4a46 1px solid;
	padding-left: 1px;
	font-size: 12px;
	padding-bottom: 1px;
	border-left: #4d4a46 1px solid;
	color: #000000;
	padding-top: 1px;
	border-bottom: #4d4a46 1px solid;
	font-family: Arial, Helvetica, sans-serif;
	width: 80px;
}
.TextBox
{
	border-right: #A7A6AA 1px solid;
	border-top: #A7A6AA 1px solid;
	font: 12px Arial, Helvetica, sans-serif;
	border-left: #A7A6AA 1px solid;
	color: black;
	border-bottom: #A7A6AA 1px solid;
	background-color: #ffffff;
}
a,a:visited {
	color:#666666;
}
        .style1
        {
            width: 25%;
        }
    </style>
<script type ="text/javascript" language ="javascript">
    function tomail()
    {
        window.location.href ="mailto:yali.chen@ericsson.com";
    }
</script>
</head>
<body bgcolor="#EEEEEE">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" valign="middle">
                <table width="508" height="277" border="0" cellpadding="0" cellspacing="0" background="images/bg.jpg" style="margin-top:150px" >
                    <tr>
                        <td valign="bottom" style="font-family: Arial"  >
                                        <form id="form1" runat="server">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="90" class="style1">
                                     
                                        &nbsp;
                                        </td>
                                    <td width="64%" valign="top">
                                            <table border="0" cellspacing="2" cellpadding="2">
                                                <tr>
                                                    <td style="font-size: 12px">
                                                        User Name:</td>
                                                    <td width="108px" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px">
                                                        <asp:TextBox ID="txtUserName" runat="server" Width="128px"></asp:TextBox></td>
                                                    <td>&nbsp;                                                        </td>
                                                    <td rowspan="2">
                                                        <asp:ImageButton ID="lbtLogin" runat="server" ImageUrl="images/login04.gif"
                                                             TabIndex="2" OnClick="lbtLogin_Click" />                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 12px">
                                                        Password :</td>
                                                    <td width="108px">
                                                        <asp:TextBox ID="txtPWD" runat="server" Width="128px" TextMode="Password"></asp:TextBox></td>
                                                    <td>&nbsp;                                                        </td>
                                                </tr>
                                            </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="40" class="style1">&nbsp;
                                        </td>
                                    <td>
                                        </td>
                                </tr>
                            </table>
                                        </form>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>