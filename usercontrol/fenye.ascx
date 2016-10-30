<%@ Control Language="C#" AutoEventWireup="true" CodeFile="fenye.ascx.cs"
    Inherits="usercontrol_fenye" %>

<style type="text/css">
a{margin:0px 5px;color:blue;}
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
    else
    {
        if(isNaN(txt.value))
        {
            alert("请输入数字");
            return false;
        }
        else
        {
            if(txt.value.indexOf('.')!=-1)
            {
                alert("页数必须是整数");
                return false;
            }
        }
    }
    return true;
}
</script>
<div style="width:99%;text-align:center;padding:5px;">
记录总数:
<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="green"></asp:Label>
条 页次:
<asp:Label ID="lblCurpage" runat="server" Text="" ForeColor="red"></asp:Label>
/
<asp:Label ID="lblPages" runat="server" Text=""></asp:Label>　
<span id="text" runat="server">
    <asp:LinkButton ID="lnkbtnFirst" CommandName="first" OnClick="LinkButton_Click" runat="server">首页</asp:LinkButton>
    <asp:LinkButton ID="lnkbtnPrevious" CommandName="prev" OnClick="LinkButton_Click"
        runat="server">上一页</asp:LinkButton>
    <asp:LinkButton ID="lnkbtnNext" CommandName="next" OnClick="LinkButton_Click" runat="server">下一页</asp:LinkButton>
    <asp:LinkButton ID="lnkbtnLast" CommandName="last" OnClick="LinkButton_Click" runat="server">末页</asp:LinkButton>
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
<asp:TextBox ID="txtPage" MaxLength="5" Style="border: solid 1px gray; width: 40px;"
    runat="server"></asp:TextBox>
<asp:LinkButton OnClientClick="return validatePage()" ID="lnkbtnGoto" runat="server"
    OnClick="lnkbtnGoto_Click">跳转</asp:LinkButton>
</div>