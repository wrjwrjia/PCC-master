<%@ Page Language="C#" AutoEventWireup="true" CodeFile="教师领书.aspx.cs" Inherits="库存管理_教师领书" %>
<%@  Register Src="../usercontrol/WebPagerByID.ascx" TagName="WebPager1" TagPrefix="uc1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>入库历史</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/changerows.js"></script>
    <style type="text/css">
        td a{color:red;}
        .style1
        {
            BACKGROUND-COLOR: #ECF9FC;
            text-align: right;
            padding-left: 3px;
            }
        .RadPicker{vertical-align:middle}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle;width:160px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2012.3.1016.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center}
        .auto-style1
        {
            padding: 5px;
            background-color: #FFFFFF;
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
                    <img src="../images/module.gif" height="22" /></td>
                <td width="55%" >
                    库存管理&gt;&gt;教师领书</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" style=" background:#ECF9FC; height:25px"colspan="4">请输入查询条件</th>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">教师工号：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbTeacherId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <td  bgcolor="#FFFFFF" class="style1">
                            ISBN：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbBookId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">教师姓名：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbTeacherName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <td  bgcolor="#FFFFFF" class="style1">
                            教材名称：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbBookName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">学院：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:DropDownList ID="DropCollege" runat="server" Width="145px"  Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                <td  bgcolor="#FFFFFF" class="style1">
                            出版社：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:DropDownList ID="DropPressName" runat="server" Width="145px"  Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <td  bgcolor="#FFFFFF" colspan="4" style=" text-align:center; height:25px;">
                        <asp:Button ID="Button1" runat="server" Text="查询" CssClass="submit" onclick="Button1_Click" style="cursor: pointer;" Height="17px"/>
                    </td>
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
                           <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                 AutoGenerateColumns="False" CssClass="gridview" DataKeyNames="id" 
                                 EmptyDataText="没有符合条件的信息" OnRowDataBound="GvDataType_RowDataBound" 
                                 RowStyle-HorizontalAlign="center" Width="100%" >
                                 <RowStyle Height="23px" />
                                 <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox3" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField HeaderText="NO." />
                                     <asp:BoundField DataField="teacher_id" HeaderText="教师工号" />
                                     <asp:BoundField DataField="teacher_name" HeaderText="教师姓名" />
                                     <asp:BoundField DataField="teacher_college" HeaderText="学院" />
                                     <asp:BoundField DataField="book_id" HeaderText="ISBN" />
                                     <asp:BoundField DataField="book_name" HeaderText="教材名" />
                                     <asp:BoundField DataField="book_editor" HeaderText="主编" />
                                     <asp:BoundField DataField="Press_name" HeaderText="出版社" />
                                     <asp:TemplateField HeaderText="库存册数">
                                        <ItemTemplate>
                                            <asp:Label ID="lbStoreNum" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 </Columns>
                                 <RowStyle HorizontalAlign="Center" />
                                 <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                             </asp:GridView>
                             <uc1:WebPager1 ID="WebPager1" runat="server" DataId="GridView1" PageSize="15" />
                             
                     <table width="100%" cellpadding="1" cellspacing="1" >
                <tr>
                      <td align="left" class="auto-style1">
                         <input type="checkbox" name="chkall" onClick="CheckAll(this.form)" value="on"/>
                            <font color="#FF0000">全选</font>
                           <asp:Button ID="Button2" runat="server" Text="领取" onclick="Button2_Click" 
                              CssClass="submit" style="cursor: pointer; " Height="17px"/>
                      </td>
                </tr>
                        </table> 
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
