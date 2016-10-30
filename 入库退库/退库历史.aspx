<%@ Page Language="C#" AutoEventWireup="true" CodeFile="退库历史.aspx.cs" Inherits="库存管理_退库历史" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退库历史</title>
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
        .RadPicker{vertical-align:middle}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle;width:160px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2012.3.1016.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center}
        .auto-style1
        {
            padding: 5px;
            background-color: #FFFFFF;
            width: 135px;
        }
        </style>
<link href="../css/loading.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../js/loading.js" charset="gb2312"></script>
<script language="javascript" type='text/javascript' src="../js/PopupCalendarDay.js"> </script>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0"  >
            <tr>
                <td width="2%" height="22" >
                    <img src="../images/module.gif"  height="22"/></td>
                <td width="55%" >
                    库存管理&gt;&gt;退库历史</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="4" style=" background:#ECF9FC; height:25px">请输入查询条件<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
                </th>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">供货商：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">起止时间</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            &nbsp;</td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">书名：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox2" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">开始时间：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
            <telerik:RadDatePicker ID="Start_Time" Runat="server" Culture="zh-CN" HiddenInputTitleAttibute="Visually hidden input created for functionality purposes." WrapperTableSummary="Table holding date picker control for selection of dates." AutoPostBack="True" Height="16px">
                 <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                  <DateInput DisplayDateFormat="yyyy/M/d" DateFormat="yyyy/M/d" LabelWidth="40%" autopostback="True"></DateInput>

                  <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
            </telerik:RadDatePicker>
                            </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">出版社：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="Press_Name" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">截止时间：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
            <telerik:RadDatePicker ID="End_Time" Runat="server" Culture="zh-CN" HiddenInputTitleAttibute="Visually hidden input created for functionality purposes." WrapperTableSummary="Table holding date picker control for selection of dates." AutoPostBack="True" Height="16px">
                 <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                  <DateInput DisplayDateFormat="yyyy/M/d" DateFormat="yyyy/M/d" LabelWidth="40%" autopostback="True"></DateInput>

                  <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
            </telerik:RadDatePicker>
                            </td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="4" style=" height:25px">
                        <asp:Button ID="Button1" runat="server" Text="查询" CssClass="submit" onclick="Button1_Click" style="cursor: pointer;" Height="17px"/>
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
                <div style="width:100%; height:290px;float:left;overflow:scroll; clear:none ;">
                    <!--startprint-->
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" PageSize="100"  OnRowDataBound="GvDataType_RowDataBound" onsorting="GridView1_Sorting" AllowSorting="True">
                        <RowStyle Height="23px" />
                        <Columns>
             <asp:TemplateField HeaderText="学年" SortExpression="date_year">
                 <ItemTemplate>
                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("date_year") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("date_year") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="学期" SortExpression="semester">
                 <ItemTemplate>
                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("semester") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("semester") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="供应商" SortExpression="supply_person">
                 <ItemTemplate>
                     <asp:Label ID="Label4" runat="server" Text='<%# Bind("supply_person") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("supply_person") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="供应商电话" SortExpression="supplier_phone">
                 <ItemTemplate>
                     <asp:Label ID="Label5" runat="server" Text='<%# Bind("supplier_phone") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("supplier_phone") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="订单号" SortExpression="order_id">
                 <ItemTemplate>
                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("order_id") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("order_id") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                 <ItemTemplate>
                     <asp:Label ID="Label6" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="定价" SortExpression="book_price">
                 <ItemTemplate>
                     <asp:Label ID="Label14" runat="server" Text='<%# Bind("book_price") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("book_price") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="书名" SortExpression="book_name">
                 <ItemTemplate>
                     <asp:Label ID="Label7" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="订单折扣" SortExpression="order_discount">
                 <ItemTemplate>
                     <asp:Label ID="Label9" runat="server" Text='<%# Bind("order_discount") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("order_discount") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="出版商" SortExpression="press_name">
                 <ItemTemplate>
                     <asp:Label ID="Label8" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="库位" SortExpression="Storage_location">
                 <ItemTemplate>
                     <asp:Label ID="Label10" runat="server" Text='<%# Bind("Storage_location") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox10" runat="server" 
                         Text='<%# Bind("Storage_location") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="库余" SortExpression="remain_num">
                 <ItemTemplate>
                     <asp:Label ID="Label11" runat="server" Text='<%# Bind("remain_num") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("remain_num") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="退货量" SortExpression="return_num">
                 <ItemTemplate>
                     <asp:Label ID="Label13" runat="server" Text='<%# Bind("return_num") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("return_num") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="退货日期" SortExpression="return_date">
                 <ItemTemplate>
                     <asp:Label ID="Label12" runat="server" Text='<%# Bind("return_date") %>'></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("return_date") %>'></asp:TextBox>
                 </EditItemTemplate>
             </asp:TemplateField>
         </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    </asp:GridView> 
                    <!--endprint-->
                    </div>
                          </th>
                </tr>
                        </table>
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                      <td align="left" bgcolor="#FFFFFF" class="auto-style1">
                          <asp:Button ID="Button2" runat="server" Text="导出Excel" CssClass="submit" onclick="Button2_Click" style="cursor: pointer;" Height="17px"/>
                      </td>
                      <td align="left" bgcolor="#FFFFFF" class="right_bg">
                          <asp:Button ID="Button3" runat="server" Text="打印" CssClass="submit"  OnClientClick="preview()" style="cursor: pointer;" Height="17px"/>
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
 <script language="javascript" type="text/javascript">
     function preview() {
         bdhtml = window.document.body.innerHTML;
         sprnstr = "<!--startprint-->";
         eprnstr = "<!--endprint-->";
         prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
         prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
         // window.document.body.innerHTML = prnhtml;
         window.print();
     }
</script>


