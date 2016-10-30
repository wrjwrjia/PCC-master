<%@ Page Language="C#" AutoEventWireup="true" CodeFile="入库管理.aspx.cs" Inherits="库存管理_入库管理1" %>

<%@ Register Src="../usercontrol/WebPagerByID.ascx" TagName="WebPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>入库管理</title>
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
<script type="text/javascript" src="../js/CalendarYMD.js" ></script>
<script language="javascript" type='text/javascript' src="../js/PopupCalendarDay.js"> </script>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0"  >
            <tr>
                <td width="2%" height="22" >
                    <img src="../images/module.gif"  height="22"/></td>
                <td width="55%" >
                    库存管理&gt;&gt;入库管理</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="4" style=" background:#ECF9FC; height:25px">请输入查询条件</th>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">供货商： </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                    <asp:DropDownList ID="DropSupplyPerson" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="contact_person" 
                        DataValueField="contact_person" Width="145px"  Height="22px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                        <a><asp:SqlDataSource ID="SDSSupplyPerson" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SQLCONN %>" 
            SelectCommand="SELECT [contact_person] FROM [press_message]">
        </asp:SqlDataSource>
                            </a> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">ISBN：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TbBookId" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                        </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">订单号：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                    <asp:DropDownList ID="DropOrderId" runat="server" Width="145px"  Height="22px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">教材名称：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TbBookName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                     <asp:CheckBox ID="CheckBox1" runat="server" Text="显示已全部到货" ToolTip="默认屏蔽全部到货" AutoPostBack="True" />
                        </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">订单时间：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            从<asp:TextBox ID="TbFromTime" onfocus="setday(this);" runat="server" 
                        CssClass="input1" Width="140px" Height="16px"></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">出版社：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                    <asp:DropDownList ID="DropPressName" runat="server" 
                        DataSourceID="SDSPressName" DataTextField="press_name" 
                        DataValueField="press_name" Width="145px"  Height="22px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                        <a><asp:SqlDataSource ID="SDSPressName" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SQLCONN %>" 
            SelectCommand="SELECT [press_name] FROM [press_message]">
        </asp:SqlDataSource>
                            </a> 
                    </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">&nbsp;</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            至<asp:TextBox ID="TbToTime" onfocus="setday(this);" runat="server" 
                        CssClass="input1" Width="140px" Height="16px"></asp:TextBox>
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">版次：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">                           
                        <a>
                            <asp:DropDownList ID="DropPressTime" runat="server" Width="145px"  
                                Height="22px">
                            <asp:ListItem></asp:ListItem>
                                <asp:ListItem>最新版</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                            </a></td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="4" style=" height:25px">
                        <asp:Button ID="Button1" runat="server" Text="查询" CssClass="submit" onclick="Button1_Click" style="cursor: pointer;"/>
                </th>
            </tr>
        </table>
                </div>
                <div style="width:99%;float:left; margin-bottom:10px; height: 205px;">   
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" style=" background:#ECF9FC; height:25px">
                             符合条件的信息</th>
                </tr>
                <tr>
                         <th bgcolor="#ffffff"  colspan="4" style=" height:25px">
                                                 <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" PageSize="100"  OnRowDataBound="GvDataType_RowDataBound" AllowSorting="True" onsorting="GridView1_Sorting" >
                        <RowStyle Height="23px" />
                        <Columns>
                <asp:TemplateField HeaderText="订单号" SortExpression="order_id">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("order_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("order_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" 
                    Visible="False">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="订单日期" SortExpression="order_date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("order_date") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("order_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作人" SortExpression="operate_person">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("operate_person") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("operate_person") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学年" SortExpression="date_year">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("date_year") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("date_year") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学期" SortExpression="semester">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("semester") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("semester") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="供货商" SortExpression="supply_person">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("supply_person") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("supply_person") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="书号" SortExpression="book_id">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="书名" SortExpression="book_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版商" SortExpression="press_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="book_price" HeaderText="定价" 
                    SortExpression="book_price" />
                <asp:TemplateField HeaderText="总量" SortExpression="total_num">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("total_num") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("total_num") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="到货情况">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CommandArgument='<%# Eval("id") %>' 
                             Text="详细"  onclick="Button2_Click" 
                           CssClass="submit" style="cursor: pointer;"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    </asp:GridView>--%>  

                           <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                 AutoGenerateColumns="False" CssClass="gridview" DataKeyNames="id" 
                                 EmptyDataText="没有符合条件的信息" OnRowDataBound="GvDataType_RowDataBound" 
                                 RowStyle-HorizontalAlign="center" Width="100%" 
                                 OnRowEditing="gvData_RowEditing"
                                 OnRowUpdating="gvData_RowUpdating" 
                                 OnRowCancelingEdit="gvData_RowCancelingEdit">
                                 <RowStyle Height="23px" />
                                 <Columns>
                                     <asp:BoundField HeaderText="NO." />
                                     <asp:BoundField DataField="order_id" HeaderText="订单号" ReadOnly="true"/>
                                     <asp:BoundField DataField="supply_person" HeaderText="供货商" ReadOnly="true" />
                                     <asp:BoundField DataField="book_id" HeaderText="ISBN"/>
                                     <asp:BoundField DataField="book_name" HeaderText="教材名称" />
                                     <asp:BoundField DataField="press_time" HeaderText="版次" />
                                     <asp:BoundField DataField="press_name" HeaderText="出版社" />
                                     <asp:BoundField DataField="book_price" HeaderText="定价" />
                                     <asp:BoundField HeaderText="折扣" />
                                     <asp:BoundField HeaderText="库位" />
                                     <asp:BoundField HeaderText="到货量" />
                                     <asp:CommandField HeaderText="到货" ShowEditButton="True" CancelText="取消" 
                                      EditText="到货" UpdateText="确定" />
                                 </Columns>
                                 <RowStyle HorizontalAlign="Center" />
                                 <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                             </asp:GridView>
                             <uc1:WebPager ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />
                         </th>
                </tr>
                        </table>
             </div>
        <div style="margin-bottom:10px;">
            <div style=" width:42%;height:300px;float:left">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="4" style=" background:#ECF9FC; height:25px">输入信息</th>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">书名： </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="BookName_Text" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">ISBN：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="BookId_Text" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">主编：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="Book_Editor" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">&nbsp;</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            &nbsp;</td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">供货商：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="Supplier_Text" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">出版社：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="Press_Text" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">版次：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="PressTime_Text" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">出版时间：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="PressDate_Text" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">定价：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="Price_Text" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">订单号：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="OrderId_Text" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">已到货数量：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="Arrived_Text" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">库位：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="Storage_TextBox" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">出售折扣：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                    <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Italic="True" 
                        Text="%"></asp:Label>
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">本次到货量：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox4" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="4" style=" height:25px">
                        <asp:Button ID="Button3" runat="server" Text="确定" CssClass="submit" onclick="Button3_Click" style="cursor: pointer;"/>
                </th>
            </tr>
        </table>
                </div>
             <div style="width:56%;height:300px;float:left; margin-left:10px;">                            
                    <div style="width:100%;">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                             该书库存信息</th>
                </tr>
                        </table> 
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" >
                <div style="width:100%;clear:none ;">
                    <div style="width:100%; height:264px;float:left;overflow:scroll; clear:none ;">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="100%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" PageSize="100"  OnRowDataBound="GvDataType_RowDataBound" onsorting="GridView1_Sorting" >
                        <RowStyle Height="23px" />
                         <Columns>
                <asp:TemplateField HeaderText="id" SortExpression="id" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="订单号" SortExpression="order_id">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("order_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("order_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="申请人" SortExpression="supply_person">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("supply_person") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("supply_person") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="书名" SortExpression="book_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="定价" SortExpression="book_price">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("book_price") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("book_price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="订货折扣" SortExpression="order_discount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("order_discount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("order_discount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总量" SortExpression="total_num">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("total_num") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("total_num") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="折扣" SortExpression="arrived_discount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" 
                            Text='<%# Bind("arrived_discount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("arrived_discount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="到货量" SortExpression="arrival_amount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("arrival_amount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("state_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="已到货量" SortExpression="arrived_amount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("arrived_amount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("arrived_amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="未到货量" SortExpression="unarrived_amount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" 
                            Text='<%# Bind("unarrived_amount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("unarrived_amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="到货日期" SortExpression="arrived_date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("arrived_date") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("arrived_date") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("arrived_date") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="到货情况" SortExpression="state_id">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("order_discount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    </asp:GridView> 
                    </div>
                          </th>
                </tr>
                            <tr>
                               <th bgcolor="#ffffff"  colspan="4" style=" height:25px">
                                    <asp:Button ID="Button4" runat="server" Text="返回" CssClass="submit" onclick="Button4_Click" style="cursor: pointer;"/>
                               </th>
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
