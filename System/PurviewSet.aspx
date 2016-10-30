<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurviewSet.aspx.cs" Inherits="System_PurviewSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <title>System Management >> RoleManagement</title>
    <script type="text/javascript">
  function CheckAll(form) {
    for (var i=0;i<form.elements.length;i++) {
	    var e = form.elements[i];
	    if (e.name != 'chkall' && e.type=='checkbox' )
		    e.checked = form.chkall.checked;
    }
}
function selectAll(oCheckbox)
{

   for(i=1;i<document.all.GvData.rows.length;i++)
   {           

      GvData.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;        
   }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    
     <table  width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31" height="22">
                    <img src="../images/module.gif" width="20" alt="" height="22"/></td>
                <td style="width: 335px">
                    System >> RoleManagement >> Authority</td>
                <td width="490" align="right">

                    &nbsp;</td>
                <td width="500" align="right">
                    
                 <div style="width:56%;float:left" align="right">
                     &nbsp;
                    <input type="checkbox" name="chkall" onClick="CheckAll(this.form)" value="on"><font color="#FF0000">Select All</font>
                     &nbsp; &nbsp; &nbsp; 
                    <asp:Button ID="Button1" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="submit" />
                </div>
                    
                    [<font color="red"> <a href="RoleManagement.aspx" class="F_red">
                        <font color="red"> Return </font></a></font> ]
                </td>
            </tr>
      </table>
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td bgcolor="#9AA29A" height="5px">
                </td>
            </tr>
      </table> 
        <asp:GridView ID="GvData" runat="server" Width="99%" AutoGenerateColumns="False" OnRowDataBound="GvData_RowDataBound" CssClass="gridview">
        <PagerSettings Visible="False" />
            <Columns>
                <asp:BoundField DataField="menuid" HeaderText="ID" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField  DataField="menu" HeaderText="Menu Name">

                <HeaderStyle HorizontalAlign="Center" Width="450px"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Authority">               
                <ItemTemplate>
                    <asp:CheckBox ID="cbQry"  runat="server" Text="Search" Visible="False"/>
                    <asp:CheckBox ID="cbAdd"  runat="server" Text="Add"  Visible="False"/>
                    <asp:CheckBox ID="cbUpdate" runat="server" Text="Edit"  Visible="False"/>
                    <asp:CheckBox ID="cbDelete" runat="server" Text="Delete"  Visible="False"/>
                    <asp:CheckBox ID="cbIMP" runat="server" Text="Import"  Visible="False"/>
                    <asp:CheckBox ID="cbEXP" runat="server" Text="Export"  Visible="False"/>
                    <asp:CheckBox ID="cbMail" runat="server" Text="Mail"  Visible="False"/>
                    <asp:CheckBox ID="cbMenu" runat="server"  Text="Selected"/>
                </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>              
            </Columns>
        </asp:GridView>
       
        <asp:HiddenField ID="hidId" runat="server" />
        <asp:HiddenField ID="hidModuId" runat="server" />
        <asp:HiddenField ID="hidDel" runat="server" />
        <asp:HiddenField ID="hidFlag" runat="server" />
    </form>
</body>
</html>