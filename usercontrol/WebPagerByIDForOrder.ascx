<%@ Control Language="C#"  AutoEventWireup="true" CodeFile="WebPagerByIDForOrder.ascx.cs" Inherits="usercontrol_WebPagerByIDForOrder" %>
<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>
<script type="text/javascript">
    function validatePage() {
        var txt = document.getElementById('<%=this.ClientID %>_txtPage');
        if (txt.value == "") {
            alert("请输入一个数字!");
            txt.focus();
            return false;
        }
        return true;
    }
    function validatePageGo() {
        var txt = document.getElementById('<%=this.ClientID %>_txtGo');
        if (txt.value == "") {
            alert("请输入一个数字!");
            txt.focus();
            return false;
        }
        return true;
    }
</script>
<div style="width:99%;text-align:left;float:right" Class="paginator" >


    <asp:LinkButton ID="lnkbtnFirst" CommandName="first" OnClick="LinkButton_Click" runat="server">第一页</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkbtnPrevious" CommandName="prev" OnClick="LinkButton_Click" runat="server">前一页</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkbtnNext" CommandName="next" OnClick="LinkButton_Click" runat="server">下一页</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkbtnLast" CommandName="last" OnClick="LinkButton_Click" runat="server">最后一页</asp:LinkButton>&nbsp;&nbsp;
    <asp:TextBox ID="txtPage" MaxLength="3" runat="server" CssClass="input1" Width="40px"  Text="15" onkeyup="value=value.replace(/[^\d]/g,'');" ></asp:TextBox>
    <asp:LinkButton OnClientClick="return validatePage()" ID="lnkbtnGoto" runat="server"   OnClick="lnkbtnGoto_Click">条记录/页</asp:LinkButton>&nbsp;&nbsp;
    
    总记录:
    <asp:Label ID="lblTotal" runat="server" Text="" ForeColor="green" Font-Bold="true" Font-Size="Small"></asp:Label>&nbsp; &nbsp; &nbsp; 
    页数:
    <asp:Label ID="lblCurpage" runat="server" Text="" ForeColor="red"></asp:Label> / <asp:Label ID="lblPages" runat="server" Text=""></asp:Label>　&nbsp;&nbsp;
    <asp:TextBox ID="txtGo" MaxLength="5" runat="server" CssClass="input1" Width="40px"  Text="1" onkeyup="value=value.replace(/[^\d]/g,'');" ></asp:TextBox>
    <asp:LinkButton OnClientClick="return validatePageGo()" ID="lnbGo" runat="server" OnClick="lnbGo_Click">确定</asp:LinkButton>&nbsp; &nbsp; &nbsp; 
    <cc1:PopupWin ID="PopupWin1" runat="server" Visible="False" ColorStyle="Blue" />
&nbsp; 种类数:
    <asp:Label ID="lblSpecies" runat="server" Text="" ForeColor="green" 
        Font-Bold="true" Font-Size="Small"></asp:Label>&nbsp;总册数:
    <asp:Label ID="lblNumber" runat="server" Text="" ForeColor="green" 
        Font-Bold="true" Font-Size="Small"></asp:Label>&nbsp;总金额:
    <asp:Label ID="lblPrice" runat="server" Text="" ForeColor="green" 
        Font-Bold="true" Font-Size="Small"></asp:Label>&nbsp;</div>
