<%@ Page Language="C#" AutoEventWireup="true" CodeFile="班级领书.aspx.cs" Inherits="库存管理_班级领书" %>
<%@ Register Src="../usercontrol/WebPagerByID.ascx" TagName="WebPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>允许更改</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/changerows.js"></script>
    <style type="text/css">
        td a{color:red;}
        .style1
        {
            BACKGROUND-COLOR: #ECF9FC;
            text-align: right;
            padding-left: 3px;
            width:20%;
        }
        </style>
<link href="../css/loading.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../js/loading.js" charset="gb2312"></script>
<script language="javascript" type='text/javascript' src="../js/PopupCalendarDay.js"> </script>
    <script type="text/javascript">
        function CheckAll(form) {
            for (var i = 0; i < form.elements.length; i++) {
                var e = form.elements[i];
                if (e.name != 'chkall' && e.type == 'checkbox')
                    e.checked = form.chkall.checked;
            }
        }
</script>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0"  >
            <tr>
                <td width="2%" height="22" >
                    <img src="../images/module.gif"  height="22"/></td>
                <td width="55%" >
                    库存管理&gt;&gt;班级领书</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th  bgcolor="#FFFFFF" class="style1" style=" width:40%">请输入班号：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="Class" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1" style=" width:40%">&nbsp;</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                        <asp:Button ID="Search" runat="server" Text="查询" CssClass="submit" onclick="Search_Click" style="cursor: pointer;"/>
                    </td>
            </tr>
            </table>
                </div>
                <div style="width:99%;float:left;">                            
                    <div style="width:100%; height:330px;float:left; margin-bottom:15px">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" style=" background:#ECF9FC; height:25px">
                             班级未领教材</th>
                </tr>
                <tr>
                         <th bgcolor="#ffffff" class="style2" >
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="没有符合条件的信息"  OnRowDataBound="GvDataType_RowDataBound" 
            AllowSorting="True" CssClass="gridview" 
            Width="100%" RowStyle-HorizontalAlign="center" DataKeyNames="BookID">
            <RowStyle Height="23px" />
            <Columns>
             <asp:TemplateField>
                 <ItemTemplate>
                     <asp:CheckBox ID="CheckBox1"  runat="server" />
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField HeaderText="NO."/>
             <asp:BoundField DataField="ClassName" HeaderText="班号"/>
             <asp:BoundField DataField="BookID" HeaderText="教材编号"/>
             <asp:BoundField DataField="BookName" HeaderText="教材名称"/>
             <asp:BoundField DataField="Publish" HeaderText="出版社"/>
             <asp:BoundField DataField="Version" HeaderText="版次"/>
             <asp:TemplateField HeaderText="id" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lbId" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="需求册数">
                <ItemTemplate>
                    <asp:Label ID="lbNeedNum" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="库存册数">
                <ItemTemplate>
                    <asp:Label ID="lbStoreNum" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="领取册数">
                <ItemTemplate>
                    <asp:TextBox ID="tbGetNum" CssClass="input1" runat="server" Width="50px" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
        <uc1:WebPager ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />

</th>
                </tr>
                <tr>
                         <td bgcolor="#ffffff" class="style2" >
                         <input type="checkbox" name="chkall" onClick="CheckAll(this.form)" value="on"/>
                            <font color="#FF0000">全选</font><asp:Button ID="GetBook" runat="server" Text="领取" CssClass="submit" 
                        style=" cursor:pointer" OnClientClick="return confirm('确定领取这些教材吗? ')" 
                                 onclick="GetBook_Click"/>

</td>
                </tr>
                        </table> 
                    </div>
             </div>
    </div>
    </form>
</body>
</html>
<script language="javascript"  type="text/javascript"><!--
    //senfe("表格名称","奇数行背景","偶数行背景","鼠标经过背景","点击后背景");
    senfe("GridView1", "#ECF9FC", "#FFFFFF", "#D6F1F8");
    senfe("GridView2", "#ECF9FC", "#FFFFFF", "#D6F1F8");
</script>
