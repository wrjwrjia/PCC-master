<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageNavigator.ascx.cs"
    Inherits="usercontrol_PageNavigator" %>

<style type="text/css">
a{margin:0px 5px;color:blue;}
.paginator { font: 11px Arial, Helvetica, sans-serif;padding:10px 20px 10px 0; margin: 0px;}
.paginator a {padding: 1px 3px; border: solid 1px #ddd; background: #fff; text-decoration: none;margin-right:1px}
.paginator a:visited {padding: 1px 3px; border: solid 1px #ddd; background: #fff; text-decoration: none;}
.paginator input {border: solid 1px #ddd; font-size: 12px;height:17px;}
.paginator a:hover {color: #fff; background: #ffa501;border-color:#ffa501;text-decoration: none;}
</style>
<script type="text/javascript">
function validatePage()
{
    var txt=document.getElementById('<%=this.ClientID %>_txtPage');
    if(txt.value == "")
    {
        alert("请输入页数");
        txt.focus();
        return false;
    }
    return true;
}
</script>
<div style="width:99%;text-align:center;padding:5px;" class="paginator">
Total Records :
<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="green"></asp:Label>
  Page :
<asp:Label ID="lblCurpage" runat="server" Text="" ForeColor="red"></asp:Label> / <asp:Label ID="lblPages" runat="server" Text=""></asp:Label>　
<span id="text" runat="server">
    <asp:LinkButton ID="lnkbtnFirst" CommandName="first" OnClick="LinkButton_Click" runat="server">First</asp:LinkButton>
    <asp:LinkButton ID="lnkbtnPrevious" CommandName="prev" OnClick="LinkButton_Click" runat="server">Previous</asp:LinkButton>
    <asp:LinkButton ID="lnkbtnNext" CommandName="next" OnClick="LinkButton_Click" runat="server">Next</asp:LinkButton>
    <asp:LinkButton ID="lnkbtnLast" CommandName="last" OnClick="LinkButton_Click" runat="server">Last</asp:LinkButton>
</span><span id="pre" runat="server">
    <asp:LinkButton ID="lnkbtnFTen" CommandArgument="ft" OnClick="lnkbtnTen_Click" runat="server"><<</asp:LinkButton>
    <asp:LinkButton ID="lnkbtnPTen" CommandArgument="pt" OnClick="lnkbtnTen_Click" runat="server"><</asp:LinkButton>
</span><span id="number" runat="server" style="text-align: center;">
    <asp:LinkButton ID="lnkbtn0" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtn1" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtn2" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtn3" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtn4" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtn5" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtn6" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtn7" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtn8" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtn9" runat="server" OnClick="lnkbtn_Click"></asp:LinkButton>
</span><span id="next" runat="server">
    <asp:LinkButton ID="lnkbtnNTen" CommandArgument="nt" OnClick="lnkbtnTen_Click" runat="server">></asp:LinkButton>
    <asp:LinkButton ID="lnkbtnLTen" CommandArgument="lt" OnClick="lnkbtnTen_Click" runat="server">>></asp:LinkButton>
</span>　
<asp:TextBox ID="txtPage" MaxLength="5" runat="server" CssClass="input1" Text="20" Width="40px" onkeyup="value=value.replace(/[^\d]/g,'');"></asp:TextBox>
<asp:LinkButton OnClientClick="return validatePage()" ID="lnkbtnGoto" runat="server"
    OnClick="lnkbtnGoto_Click" Width="108px">Records /PerPage</asp:LinkButton>
</div>