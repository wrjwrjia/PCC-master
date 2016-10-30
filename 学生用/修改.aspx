<%@ Page Language="C#" AutoEventWireup="true" CodeFile="修改.aspx.cs" Inherits="实验" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/BookStyle.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style3
        {
            height: 25px;
            width: 168px;
        }
        .style4
        {
            height: 25px;
            }
        .style6
        {
            height: 25px;
            width: 108px;
        }
        .style7
        {
            height: 25px;
            width: 64px;
        }
        .style10
        {
            height: 25px;
            width: 160px;
        }
        .style11
        {
            height: 25px;
            width: 166px;
        }
        .style12
        {
        }
        .style13
        {
            height: 25px;
            width: 72px;
        }
        .auto-style2
        {
            width: 72px;
        }
        .auto-style3
        {
            height: 25px;
            width: 161px;
        }
        .auto-style4
        {
            height: 25px;
            width: 116px;
        }
        .auto-style6
        {
            height: 25px;
            }
        .auto-style7
        {
            height: 25px;
            width: 82px;
        }
        .auto-style8
        {
            height: 25px;
            width: 115px;
        }
        </style>
        <script>

            function ConfirmReturn() {

                return confirm('确定要修改吗？');

            }

</script>

</head>
<body >
    <form id="form1" runat="server">
    <div>
        <table width="90%" bordercolor="black" align="center" border="1">
            <tr>
                <td class="style4" align="center" colspan="6">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td class="auto-style7" 
                    style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: medium">
                    &nbsp;</td>
                <td class="auto-style8" 
                    style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: medium">
                    开始时间</td>
                <td class="auto-style3">
                    <%--<telerik:RadDateInput ID="Start_Time" Runat="server" DisplayDateFormat="yyyy'-'MM'-'dd'T'HH':'mm':'ss">
                    </telerik:RadDateInput>--%>
                    <telerik:RadDateInput ID="Start_Time" Runat="server" DateFormat="yyyy/M/d H:mm:ss" DisplayDateFormat="yyyy'-'MM'-'dd'T'HH':'mm':'ss">
                    </telerik:RadDateInput>
                </td>
                <td class="auto-style2">
                    </td>
                <td class="style6">
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </td>
                <td class="auto-style4">
                    </td>
            </tr>
             <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style3">
                    <asp:Label ID="Label1" runat="server" Text="yyy-mmm-ddd  hh:mm:ss" BackColor="White"></asp:Label>
                 </td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="style6">
                    </td>
                <td class="auto-style4">
                </td>
            </tr>
             <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style8">
                    截止时间</td>
                <td class="auto-style3">
                    <%-- <telerik:RadDateInput ID="End_Time" Runat="server" DisplayDateFormat="yyyy'-'MM'-'dd'T'HH':'mm':'ss">
                    </telerik:RadDateInput>--%>
                    <telerik:RadDateInput ID="End_Time" Runat="server" DateFormat="yyyy/M/d H:mm:ss" DisplayDateFormat="yyyy'-'MM'-'dd'T'HH':'mm':'ss">
                    </telerik:RadDateInput>
                 </td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="style6">
                    <asp:CheckBox ID="CheckBox2" runat="server" />
                 </td>
                <td class="auto-style4">
                    &nbsp;</td>
            </tr>
             <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style8">
                </td>
                <td class="auto-style3">
                    <asp:Label ID="Label2" runat="server" Text="yyy-mmm-ddd  hh:mm:ss"></asp:Label>
                </td>
                <td class="auto-style2">
                    限定年级</td>
                <td class="style6">
                </td>
                <td class="auto-style4">
                </td>
            </tr>
             <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style8">
                    类型</td>
                <td class="auto-style3">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="157px">
                    </asp:DropDownList>
                 </td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="style6">
                    <asp:CheckBox ID="CheckBox3" runat="server" />
                 </td>
                <td class="auto-style4">
                    </td>
            </tr>
             <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style8">
                </td>
                <td class="auto-style3">
                    </td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="style6">
                    </td>
                <td class="auto-style4">
                    </td>
            </tr>
             <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style8">
                    学期</td>
                <td class="auto-style3">
                    <asp:DropDownList ID="DropDownList2" runat="server" Height="20px" Width="156px">
                    </asp:DropDownList>
                 </td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="style6">
                    <asp:CheckBox ID="CheckBox4" runat="server" />
                 </td>
                <td class="auto-style4">
                    &nbsp;</td>
            </tr>
             <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="style6">
                    &nbsp;</td>
                <td class="auto-style4">
                    </td>
            </tr>
             <tr>
                <td class="auto-style6" align="center" colspan="6">
                    提示用户信息</td>
            </tr>
             <tr>
                <td class="style4" align="center" colspan="6">
                    <asp:TextBox ID="TextBox3" runat="server" Height="160px" TextMode="MultiLine" 
                        Width="522px" CssClass="style3"></asp:TextBox>
                 </td>
            </tr>
             <tr>
                <td class="style4" align="center" colspan="6">
                    <asp:Button ID="Button1" runat="server" Font-Size="Medium" Height="25px" CommandArgument='<%# Eval("book_id") %>'  OnClientClick="return ConfirmReturn()" 
                        onclick="Button1_Click" Text="确定修改" Width="80px" />
                 </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

     
