<%@ Page Language="C#" AutoEventWireup="true" CodeFile="提交订单.aspx.cs" Inherits="教材计划_提交订单" %>
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
                    教材计划&gt;&gt;提交订单</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="6" style=" background:#ECF9FC; height:25px">请输入查询条件</th>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">学年：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="145px"  Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                <td bgcolor="#FFFFFF" class="style1">
                            订单号： 
                        </td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">操作人：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">学期：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                    </td>
                <td bgcolor="#FFFFFF" class="style1">
                            供货商：
                        </td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <a>
                    <asp:DropDownList ID="DropSupplier" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="contact_person" 
                        DataValueField="contact_person" Width="145px"  Height="22px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SQLCONN %>" 
            SelectCommand="SELECT [contact_person] FROM [press_message]">
        </asp:SqlDataSource>
                            </a> 
                            </td>
                <th   bgcolor="#FFFFFF" class="style1">校区：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                        <a>
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>明故宫</asp:ListItem>
                            <asp:ListItem>将军路</asp:ListItem>
                            </asp:DropDownList>
                            </a> 
                            </td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="6" style=" height:25px">
                        <asp:Button ID="Search" runat="server" Text="查询" CssClass="submit" onclick="Search_Click" style="cursor: pointer;"/>
                </th>
            </tr>
        </table>
                </div>
                <div style="width:99%;float:left;">                            
                    <div style="width:100%; height:300px;float:left;">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                             符合条件的信息</th>
                </tr>
                        </table> 
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" >
                             <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" PageSize="100"  OnRowDataBound="GvDataType_RowDataBound" AllowSorting="True">
                        <RowStyle Height="23px" />
                        <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="订单号" SortExpression="order_id">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("order_id") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("order_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="订单日期" SortExpression="order_date">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("order_date") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("order_date") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作人" SortExpression="operate_person">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("operate_person") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("operate_person") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学年" SortExpression="date_year">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("date_year") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("date_year") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学期" SortExpression="semester">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("semester") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("semester") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="供货商" SortExpression="supply_person">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("supply_person") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("supply_person") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="校区" SortExpression="campus">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("campus") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("campus") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    </asp:GridView>--%>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="没有符合条件的信息"  OnRowDataBound="GvDataType_RowDataBound" 
            AllowSorting="True" CssClass="gridview" 
            Width="100%" RowStyle-HorizontalAlign="center" DataKeyNames="order_id">
            <RowStyle Height="23px" />
            <Columns>
            <asp:TemplateField>
                 <ItemTemplate>
                     <asp:CheckBox ID="CheckBox1"  runat="server" />
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="date_year" HeaderText="学年"/>
             <asp:BoundField DataField="semester" HeaderText="学期"/>
             <asp:BoundField DataField="order_id" HeaderText="订单号"/>
             <asp:BoundField DataField="order_date" HeaderText="订单日期"/>
             <asp:BoundField DataField="operate_person" HeaderText="操作人"/>
             <asp:BoundField DataField="supply_person" HeaderText="供应人"/>
             <asp:BoundField DataField="campus" HeaderText="校区"/>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
        <uc1:WebPager ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />
</th>
                </tr>
                <tr>
                         <td bgcolor="#ffffff" >
                          <input type="checkbox" name="chkall" onClick="CheckAll(this.form)" value="on"/>
                            <font color="#FF0000">全选&nbsp; </font><asp:Button ID="Submit" runat="server" Text="提交" CssClass="submit" 
                        style=" cursor:pointer" onclick="Submit_Click" OnClientClick="return confirm('确定提交吗? ')"/>
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
</script>
