<%@ Page Language="C#" AutoEventWireup="true" CodeFile="班级退书.aspx.cs" Inherits="库存管理_班级退书" %>
<%@ Register Src="../usercontrol/WebPagerByID.ascx" TagName="WebPager1" TagPrefix="uc1"%>
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
                    库存管理&gt;&gt;班级退书</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <td  bgcolor="#FFFFFF" class="style1">班号：
                        </td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbClassName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <td bgcolor="#FFFFFF" class="style1">
                            版次：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbPressTime" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <td  bgcolor="#FFFFFF" class="style1">ISBN：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbBookId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <td bgcolor="#FFFFFF" class="style1" >
                            出版社：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:DropDownList ID="DropPressName" runat="server" Width="145px"  
                                Height="22px">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <td  bgcolor="#FFFFFF" class="style1" >教材名称：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbBookName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <td bgcolor="#FFFFFF" class="style1" >
                            校区：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                        <a>
                            <asp:DropDownList ID="DropCampus" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>明故宫</asp:ListItem>
                            <asp:ListItem>将军路</asp:ListItem>
                            </asp:DropDownList>
                            </a> 
                    </td>
            </tr>
            <tr>
                <td  bgcolor="#FFFFFF" colspan="4" align="center">
                        <asp:Button ID="Search" runat="server" Text="查询" CssClass="submit" 
                            onclick="Search_Click" style="cursor: pointer; height: 18px;"/>
                    </td>
            </tr>
            </table>
                </div>
                <div style="width:99%;float:left;">                            
                    <div style="width:100%; height:330px;float:left; margin-bottom:15px">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                             班级已领教材</th>
                </tr>
                        </table> 
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" class="style2" >
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="没有符合条件的信息"  OnRowDataBound="GvDataType_RowDataBound" 
            AllowSorting="True" CssClass="gridview" 
            Width="100%" RowStyle-HorizontalAlign="center" DataKeyNames="ID">
            <RowStyle Height="23px" />
            <Columns>
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="ClassName" HeaderText="班号"/>
             <asp:BoundField DataField="BookID" HeaderText="ISBN"/>             
             <asp:BoundField DataField="BookName" HeaderText="教材名"/>
             <asp:BoundField DataField="Author" HeaderText="主编"/>
             <asp:BoundField DataField="Publish" HeaderText="出版社"/>
             <asp:BoundField DataField="Version" HeaderText="版次"/>
             <asp:BoundField DataField="sell_price" HeaderText="单价"/>
             <asp:BoundField DataField="campusName" HeaderText="校区"/>
             <asp:TemplateField HeaderText="总册数">
                    <ItemTemplate>
                        <asp:Label ID="LSum" runat="server" Text='0'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="Return" runat="server" style=" cursor:pointer"
                            Text="退书" CommandArgument='<%# Eval("BookID") %>' 
                            onclick="Return_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
        <uc1:WebPager1 ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />
                          </th>
                </tr>
                        </table>
                    </div>
                    <div style="width:100%; height:300px;float:left;">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="2" style=" background:#ECF9FC; height:25px">
                             该教材的库存信息</th>
                </tr>
                <tr>
                         <td bgcolor="#ffffff" style="background:#ECF9FC; height:25px; width:20%; text-align:right;">
                             退还册数：</td>
                         <td bgcolor="#ffffff" style="height:25px">
                            &nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="ReturnNum" CssClass="input1" runat="server" Width="60px" ></asp:TextBox> 
                             册<asp:TextBox ID="TbSaveId" CssClass="input1" runat="server" Width="140px" Visible="false"></asp:TextBox> 
                             <asp:TextBox ID="TbSaveNum" CssClass="input1" runat="server" Width="140px" 
                                 Visible="false"></asp:TextBox> 
                         </td>
                </tr>
                        </table> 
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" class="style2" >
                <div style="width:100%; height:290px;float:left;overflow:scroll; clear:none ;">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" PageSize="100"  OnRowDataBound="GvDataType_RowDataBound2" >
                        <RowStyle Height="23px" />
                        <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="版次" SortExpression="press_time">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("press_time") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("press_time") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="订单号" SortExpression="order_id">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("order_id") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("order_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库位" SortExpression="Storage_location">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox15" runat="server" 
                                Text='<%# Bind("Storage_location") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label14" runat="server" Text='<%# Bind("Storage_location") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="校区" SortExpression="Campus">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("Campus") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("Campus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="剩余量" SortExpression="remain_num">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("remain_num") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("remain_num") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="id" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="分配册数">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox9" runat="server" CssClass="input1" Width="50px">0</asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    </asp:GridView> 
                    </div>
                          </th>
                </tr>
                <tr>
                         <td bgcolor="#ffffff" >
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="OK" runat="server" Text="确定" CssClass="submit" 
                                 onclick="OK_Click" style="cursor: pointer;"/>
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
