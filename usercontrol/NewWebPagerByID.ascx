<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewWebPagerByID.ascx.cs" Inherits="NewWebPagerByID" %>
<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>
<script type="text/javascript">
function validatePage()
{
    var txt=document.getElementById('<%=this.ClientID %>_txtPage');
    if(txt.value == "")
    {
        alert("Plase input a number!");
        txt.focus();
        return false;
    }
    return true;
}
function validatePageGo()
{
    var txt=document.getElementById('<%=this.ClientID %>_txtGo');
    if(txt.value == "")
    {
        alert("Plase input a number!");
        txt.focus();
        return false;
    }
    return true;
}
</script>
<div style="width:99%;text-align:left;float:right"  Class="paginator" >


    <asp:LinkButton ID="lnkbtnFirst" CommandName="first" OnClick="LinkButton_Click" runat="server">First</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkbtnPrevious" CommandName="prev" OnClick="LinkButton_Click" runat="server">Previous</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkbtnNext" CommandName="next" OnClick="LinkButton_Click" runat="server">Next</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkbtnLast" CommandName="last" OnClick="LinkButton_Click" runat="server">Last</asp:LinkButton>&nbsp;&nbsp;
    <asp:TextBox ID="txtPage" MaxLength="3" runat="server" CssClass="input1" Width="40px"  Text="15" onkeyup="value=value.replace(/[^\d]/g,'');" ></asp:TextBox>
    <asp:LinkButton OnClientClick="return validatePage()" ID="lnkbtnGoto" runat="server"   OnClick="lnkbtnGoto_Click">Records/PerPage </asp:LinkButton>&nbsp;&nbsp;
    
    Total Records:
    <asp:Label ID="lblTotal" runat="server" Text="" ForeColor="green" Font-Bold="true" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
     Page:
    <asp:Label ID="lblCurpage" runat="server" Text="" ForeColor="red"></asp:Label> / <asp:Label ID="lblPages" runat="server" Text=""></asp:Label>　&nbsp;&nbsp;
    <asp:TextBox ID="txtGo" MaxLength="5" runat="server" CssClass="input1" Width="40px"  Text="1" onkeyup="value=value.replace(/[^\d]/g,'');" ></asp:TextBox>
    <asp:LinkButton OnClientClick="return validatePageGo()" ID="lnbGo" runat="server" OnClick="lnbGo_Click"   >Go</asp:LinkButton>
    <cc1:PopupWin ID="PopupWin1" runat="server" Visible="False" ColorStyle="Blue" />
</div>

