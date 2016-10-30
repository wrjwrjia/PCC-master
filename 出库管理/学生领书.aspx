<%@ Page Language="C#" AutoEventWireup="true" CodeFile="学生领书.aspx.cs" Inherits="库存管理_学生领书" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register src="../usercontrol/WebPagerByID.ascx" tagname="WebPager1" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学生领书</title>
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
            }
        .auto-style2
        {
            BACKGROUND-COLOR: #ECF9FC;
            text-align: right;
            padding-left: 3px;
            width: 20%;
            height: 27px;
        }
        .auto-style3
        {
            padding: 5px;
            background-color: #FFFFFF;
            height: 27px;
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
                    库存管理&gt;&gt;学生领书</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px; height:120px;">
      <div style="width:34%;float:left; ">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="100%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="2" style=" background:#ECF9FC; height:25px">请输入学生信息</th>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">学号：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbStuId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">姓名：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbStuName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="2" style=" height:25px">
                        &nbsp;</th>
            </tr>
        </table>
      </div>
      <div style="width:59%; height:100px; float:left; margin-left:10px; margin-bottom=10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="100%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="4" style=" background:#ECF9FC; height:25px">请输入查询条件</th>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">ISBN：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbBookId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">出版社：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:DropDownList ID="DropPressName" runat="server" Width="145px"  Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="auto-style2">教材名称：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="auto-style3" >
                            <asp:TextBox ID="TbBookName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="auto-style2">版次：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="auto-style3">
                            <asp:TextBox ID="TbPressTime" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="4" style=" height:25px">
                        <asp:Button ID="Button2" runat="server" Text="查询" CssClass="submit" onclick="Button2_Click" style="cursor: pointer;" Height="17px"/>
                </th>
            </tr>
        </table>
      <div>
                </div>
    </div>
    </div>
                <div style="width:99%;float:left;">                            
                    <div style="width:100%; height:300px;float:left;">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" style=" background:#ECF9FC; height:25px">
                             符合条件的信息</th>
                </tr>
                        </table> 
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" >
                           <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                 AutoGenerateColumns="False" CssClass="gridview" DataKeyNames="id" 
                                 EmptyDataText="没有符合条件的信息" OnRowDataBound="GvDataType_RowDataBound" 
                                 RowStyle-HorizontalAlign="center" Width="100%" >
                                 <RowStyle Height="23px" />
                                 <Columns>
                                      <asp:TemplateField>
                                         <ItemTemplate>
                                             <asp:CheckBox ID="CheckBox1"  runat="server" />
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:BoundField HeaderText="NO." />
                                     <asp:BoundField DataField="book_id" HeaderText="ISBN" />
                                     <asp:BoundField DataField="book_name" HeaderText="教材名称" />
                                     <asp:BoundField DataField="press_time" HeaderText="版次" />
                                     <asp:BoundField DataField="press_name" HeaderText="出版社" />
                                     <asp:BoundField DataField="book_price" HeaderText="定价" />
                                     <asp:BoundField DataField="sell_discount" HeaderText="折扣" />
                                      <asp:TemplateField HeaderText="售价">
                                         <ItemTemplate>
                                             <asp:Label ID="LbPrice" runat="server"></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="Storage_location" HeaderText="库位" />
                                     <asp:BoundField DataField="remain_num" HeaderText="库余量" />
                                     <asp:TemplateField HeaderText="领取册数">
                                         <ItemTemplate>
                                             <asp:TextBox ID="TbGetNum" CssClass="input1" runat="server" Width="60px" >0</asp:TextBox>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                 <RowStyle HorizontalAlign="Center" />
                                 <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                             </asp:GridView>
                             <uc1:WebPager1 ID="WebPager1" runat="server" DataId="GridView1" PageSize="15" />
                             
                          </th>
                </tr>
                        </table>
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                      <td align="left" bgcolor="#FFFFFF" class="auto-style1">
                         <input type="checkbox" name="chkall" onClick="CheckAll(this.form)" value="on"/>
                            <font color="#FF0000">全选</font><asp:Button ID="GetBook" runat="server" Text="出售" CssClass="submit" 
                        style=" cursor:pointer" OnClientClick="return confirm('确定出售这些教材吗? ')" 
                                 onclick="GetBook_Click"/>

                      </td>
                </tr>
                        </table> 
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


