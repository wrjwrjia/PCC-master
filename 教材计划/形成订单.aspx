<%@ Page Language="C#" AutoEventWireup="true" CodeFile="形成订单.aspx.cs" Inherits="教材计划_形成订单" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title>形成订单</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/changerows.js"></script>
    <script type="text/javascript">
        function CheckAll(form) {
            for (var i = 0; i < form.elements.length; i++) {
                var e = form.elements[i];
                if (e.name != 'chkall' && e.type == 'checkbox')
                    e.checked = form.chkall.checked;
            }
        }
    </script>
        <script type="text/javascript">
            function CheckAll1(form) {
                for (var i = 0; i < form.elements.length; i++) {
                    var e = form.elements[i];
                    if (e.name != 'chkall1' && e.type == 'checkbox')
                        e.checked = form.chkall1.checked;
                }
            }
    </script>
    <style type="text/css">
    td a{color:red;}
    .titlediv{float: left; padding: 5px 2px;width:100%;}
    .AddLnk{float:right;no-repeat:2px center;padding-left:20px;margin-right:5px;}
    .LeftTitle{font-size:14px;float:left;color:Green;}
    .style1
        {
            BACKGROUND-COLOR: #ECF9FC;
            text-align: right;
            padding-left: 3px;
            height:20px;
        }
        .RadPicker{vertical-align:middle}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle;width:160px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2012.3.1016.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center}
        div.RadPicker table.rcSingle .rcInputCell{padding-right:0}.RadPicker table.rcTable .rcInputCell{padding:0 4px 0 0}
        </style>
<link href="../css/loading.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../js/loading.js" charset="gb2312"></script>
<script type="text/javascript" src="../js/CalendarYMD.js" ></script>
<script language="javascript" type='text/javascript' src="../js/PopupCalendarDay.js"> </script>
<body style="margin-top:2px;">
    <form id="form1" runat="server">
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31" style="height: 25px">
                    <img src="../images/module.gif" width="20"  alt="" onclick="location.href=location.href" style="cursor:hand" alt="refresh"/></td>
                <td>
                    教材计划&gt;&gt; 
                    形成订单</td>
                <td width="50%" align="right">
                     </td>
            </tr>
        </table>
            <div style="width:99%;margin-bottom:1px;"> 
                <div style="width:99%; float:left;">
                <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                            基本信息
                            </th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            订单号：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">订单日期：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                         <asp:TextBox ID="TextBox2" onfocus="setday(this);" runat="server" 
                        CssClass="input1" Width="140px" Height="16px"></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            操作人：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">&nbsp;</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            &nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            学年：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                        <a>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="145px"  Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            *</a></td>
                        <th  bgcolor="#ffffff" class="style1">学期：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                        <a>
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                            *</a>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            供应商：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                    <asp:DropDownList ID="DropDownList3" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="contact_person" 
                        DataValueField="contact_person" Width="145px"  Height="22px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                        <a>*<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SQLCONN %>" 
            SelectCommand="SELECT [contact_person] FROM [press_message]">
        </asp:SqlDataSource>
                            </a></td>
                        <th  bgcolor="#ffffff" class="style1">联系方式：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox8" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="4" >
                                 <asp:Button ID="OK" runat="server" Text="确定" CssClass="submit" style=" cursor:pointer; height: 18px;" 
                        onclick="OK_Click" OnClientClick="return confirm('确定信息无误吗? ')"/>
                            </th>
                        </tr>
                        </table>
        </div>
                
            </div>
            <div style="width:98%;float:left; margin-bottom:10px">
            <table cellpadding="1" cellspacing="1" class="tabGg" width="100%">
            <tr>
                <th bgcolor="#FFFFFF" style=" background:#ECF9FC; height:25px" colspan="2">此订单下的书目</th>
            </tr>
            <tr>
                <th bgcolor="#ffffff" colspan="2" >
                    <div style="width:100%; height:171px;float:left;overflow:scroll; clear:none ;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" OnRowDataBound="GvDataType_RowDataBound">
                        <RowStyle Height="23px" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox3" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="版次" SortExpression="press_time">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("press_time") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("press_time") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价" SortExpression="book_price">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("book_price") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("book_price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划数量" SortExpression="plan_num">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("plan_num") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("plan_num") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="机动数量" SortExpression="auto_num">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("auto_num") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("auto_num") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="总数量" SortExpression="total_num">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("total_num") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("total_num") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="总金额" SortExpression="total_price">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("total_price") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("total_price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="校区" SortExpression="campus">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("campus") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("campus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="Delete" runat="server" CommandArgument='<%# Eval("id") %>'  Text="删除" CssClass="submit" style=" cursor:pointer" 
                                                     onclick="Delete_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    </asp:GridView> 
                    </div>
                </th>
            </tr>
            <tr>
                <td bgcolor="#ffffff" colspan="2" >
                        <input type="checkbox" name="chkall1" onClick="CheckAll1(this.form)" 
                        value="on"/>
                            <font color="#FF0000">全选</font><asp:Button ID="Button9" 
                        runat="server" Text="修改机动册数" CssClass="submit" Width="90px"
                        style=" cursor:pointer" onclick="Button2_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            请输入机动百分比：<asp:TextBox ID="TextBox5" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            %&nbsp;&nbsp;
                                 <asp:TextBox ID="TextBoxTemp" runat="server" Visible="False"></asp:TextBox>
                                 <asp:Button ID="ChangeOK" runat="server" Text="确定" CssClass="submit" 
                        onclick="ChangeOK_Click" OnClientClick="return confirm('确定修改机动册数吗? ')"/>
                    </td>
            </tr>
            <tr>
                        <th  bgcolor="#ffffff" class="style1" style=" width:20%">
                            总金额：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TotalPrice" CssClass="input1" runat="server" Width="80px" ></asp:TextBox> 
                            元</td>
                        </tr>
            <tr>
                <th bgcolor="#ffffff" style=" height:25px" colspan="2">
                                 <asp:Button ID="Save" runat="server" Text="保存" CssClass="submit" style=" cursor:pointer; "
                        onclick="Save_Click" OnClientClick="return confirm('确定保存这些信息吗? ')"/>
                <asp:Button ID="Cancel" runat="server" Text="取消" CssClass="submit" style=" cursor:pointer; "
                        onclick="Cancel_Click" OnClientClick="return confirm('确定取消这些信息吗? ')"/>
                    </th>
            </tr>
        </table>
                </div>
                <div style="width:68%; height:463px; float:left; margin-right:1%">
                <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                             选择教材
                                          </th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            ISBN：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox9" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">教材名称：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox10" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版社：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox11" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">版次：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox12" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="4">
                            <asp:Button ID="Search" runat="server" Text="模糊查询" CssClass="submit" style=" cursor:pointer" 
                        onclick="Search_Click" Width="70px"/></th>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="4" >
        <div style="width:100%; height:318px;float:left;overflow:scroll; clear:none ;">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                CssClass="gridview" EmptyDataText="没有符合条件的教材信息" Width="97%" RowStyle-HorizontalAlign="center" 
                GridLines="None" OnRowDataBound="GvDataType_RowDataBound" PageSize="1000">
                <RowStyle Height="23px" />
                <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label19" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="版次" SortExpression="press_time">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("press_time") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label20" runat="server" Text='<%# Bind("press_time") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label21" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="主编" SortExpression="book_editor">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("book_editor") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label22" runat="server" Text='<%# Bind("book_editor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单价" SortExpression="book_price">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox36" runat="server" Text='<%# Bind("book_price") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label23" runat="server" Text='<%# Bind("book_price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="计划册数" SortExpression="book_number">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox37" runat="server" Text='<%# Bind("book_number") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label24" runat="server" Text='<%# Bind("book_number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="校区" SortExpression="campus">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox38" runat="server" Text='<%# Bind("campus") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label25" runat="server" Text='<%# Bind("campus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="分批处理">
                                                <ItemTemplate>
                                                    <asp:Button ID="Divide" runat="server" CommandArgument='<%# Eval("id") %>'  Text="分批" CssClass="submit" style=" cursor:pointer" 
                                                     onclick="Divide_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
            </asp:GridView> 
        </div>
                        </th>
                        </tr>
                        <tr>
                        <td  bgcolor="#ffffff" colspan="4" >
                            <input type="checkbox" name="chkall" onClick="CheckAll(this.form)" value="on"/>
                            <font color="#FF0000">全选</font><asp:Button ID="Choose" runat="server" Text="确定" CssClass="submit" 
                        style=" cursor:pointer" onclick="Choose_Click" OnClientClick="return confirm('确定选择吗? ')"/>
                            </td>
                        </tr>
                        </table>
        </div>
        <div style="width:29%; height:400px; float:left;">
                <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="2" style=" background:#ECF9FC; height:25px">
                             分批
                                          </th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            ISBN：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="ISBN" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            &nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教材名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="BookName" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版社：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="PressName" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True"></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            版次：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="PressTime" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            总量：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="Number" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            本</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            第一批数量：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="FirstNum" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            本</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            第二批数量：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="SecondNum" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            本</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            第三批数量：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="ThirdNum" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            本</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            第四批数量：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="FourthNum" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            本</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            第五批数量：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="FifthNum" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            本</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            第六批数量：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="SixthNum" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            本</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            第七批数量：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="SeventhNum" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            本</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="2">
                            <asp:TextBox ID="SaveID" runat="server" Visible="False"></asp:TextBox>
                            <asp:Button ID="DivideOK" runat="server" Text="确定" CssClass="submit" style=" cursor:pointer; height: 18px;" 
                        onclick="DivideOK_Click" Width="55px"/></th>
                        </tr>
                        </table>
        </div>
        </form>
</body>
</html>
<script language="javascript"  type="text/javascript"><!--
    //senfe("表格名称","奇数行背景","偶数行背景","鼠标经过背景","点击后背景");
    senfe("GridView1", "#ECF9FC", "#FFFFFF", "#D6F1F8");
    senfe("GridView2", "#ECF9FC", "#FFFFFF", "#D6F1F8");
    senfe("GridView3", "#ECF9FC", "#FFFFFF", "#D6F1F8");
</script>
