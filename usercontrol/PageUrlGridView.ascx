<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageUrlGridView.ascx.cs"   Inherits="usercontrol_PageUrlGridView" %>

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
<div style="width:99%;text-align:center"  Class="paginator">
分页：总数
<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="green"></asp:Label>
条 页次：
<asp:Label ID="lblCurpage" runat="server" Text="" ForeColor="red"></asp:Label> / <asp:Label ID="lblPages" runat="server" Text=""></asp:Label>　
<span id="text" runat="server">
    <asp:HyperLink ID="lnkbtnFirst"  runat="server">首页</asp:HyperLink>
    <asp:HyperLink ID="lnkbtnPrevious" runat="server">上一页</asp:HyperLink>
    <asp:HyperLink ID="lnkbtnNext"  runat="server">下一页</asp:HyperLink>
    <asp:HyperLink ID="lnkbtnLast" runat="server">末页</asp:HyperLink>
</span><span id="pre" runat="server">
    <asp:HyperLink ID="lnkbtnFTen"  runat="server"><<</asp:HyperLink>
    <asp:HyperLink ID="lnkbtnPTen"  runat="server"><</asp:HyperLink>
</span><span id="number" runat="server" style="text-align: center;">
    <asp:HyperLink ID="lnkbtn0" runat="server" ></asp:HyperLink>
    <asp:HyperLink ID="lnkbtn1" runat="server" ></asp:HyperLink>
    <asp:HyperLink ID="lnkbtn2" runat="server" ></asp:HyperLink>
    <asp:HyperLink ID="lnkbtn3" runat="server" ></asp:HyperLink>
    <asp:HyperLink ID="lnkbtn4" runat="server" ></asp:HyperLink>
    <asp:HyperLink ID="lnkbtn5" runat="server" ></asp:HyperLink>
    <asp:HyperLink ID="lnkbtn6" runat="server" ></asp:HyperLink>
    <asp:HyperLink ID="lnkbtn7" runat="server" ></asp:HyperLink>
    <asp:HyperLink ID="lnkbtn8" runat="server" ></asp:HyperLink>
    <asp:HyperLink ID="lnkbtn9" runat="server" ></asp:HyperLink>
</span><span id="next" runat="server">
    <asp:HyperLink ID="lnkbtnNTen"  runat="server">></asp:HyperLink>
    <asp:HyperLink ID="lnkbtnLTen" runat="server">>></asp:HyperLink>
</span>　
<asp:TextBox ID="txtPage" MaxLength="5" runat="server" CssClass="input1" Width="40px"  Text="20" onkeyup="value=value.replace(/[^\d]/g,'');" ></asp:TextBox>
<asp:LinkButton OnClientClick="return validatePage()" ID="lnkbtnGoto" runat="server"   OnClick="lnkbtnGoto_Click">条/每页</asp:LinkButton>
</div>