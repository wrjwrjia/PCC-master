<%@ Page Language="C#" AutoEventWireup="true" CodeFile="班级历史.aspx.cs" Inherits="库存管理_班级历史" %>
<%@  Register Src="../usercontrol/WebPagerByIDForOrder.ascx" TagName="WebPager1" TagPrefix="uc1"%>
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
<script type="text/javascript" src="../js/CalendarYMD.js" ></script>
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
                    库存管理&gt;&gt;班级历史</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th  bgcolor="#FFFFFF" class="style1" >班号：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="ClassName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
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
                <th  bgcolor="#FFFFFF" class="style1" >ISBN：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbBookId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <td bgcolor="#FFFFFF" class="style1" >
                            领取时间：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                         <asp:TextBox ID="TbTimeFrom" onfocus="setday(this);" runat="server" 
                        CssClass="input1" Width="140px" Height="16px"></asp:TextBox>到<asp:TextBox 
                                ID="TbTimeTo" onfocus="setday(this);" runat="server" 
                        CssClass="input1" Width="140px" Height="16px"></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1" >教材名称：</th>
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
                <th  bgcolor="#FFFFFF" class="style1" >版次：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbPressTime" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <td bgcolor="#FFFFFF" class="style1" >
                            &nbsp;</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                        &nbsp;</td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" colspan="4">
                        <asp:Button ID="Search" runat="server" Text="查询" CssClass="submit" onclick="Search_Click" style="cursor: pointer;"/>
                    </th>
            </tr>
            </table>
                </div>
                <div style="width:99%;float:left;height:320px; margin-bottom:10px;">                            
                    <div style="width:100%; height:320px; float:left;">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                             符合条件的信息</th>
                </tr>
                        </table> 
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" >
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="没有符合条件的信息"  OnRowDataBound="GvDataType_RowDataBound" 
            AllowSorting="True" CssClass="gridview" 
            Width="100%" RowStyle-HorizontalAlign="center" DataKeyNames="id">
            <RowStyle Height="23px" />
            <Columns>
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="ClassName" HeaderText="班号"/>
             <asp:BoundField DataField="BookID" HeaderText="ISBN"/>
             <asp:BoundField DataField="BookName" HeaderText="教材名"/>
             <asp:BoundField DataField="Author" HeaderText="主编"/>
             <asp:BoundField DataField="Publish" HeaderText="出版社"/>
             <asp:BoundField DataField="Version" HeaderText="版次"/>
             <asp:BoundField DataField="InfoDate" HeaderText="领取时间"/>
             <asp:BoundField DataField="campusName" HeaderText="校区"/>
             <asp:BoundField DataField="Unit_price" HeaderText="进价"/>
             <asp:BoundField DataField="sell_discount" HeaderText="折扣"/>
             <asp:BoundField DataField="sell_price" HeaderText="单价"/>
            <asp:BoundField DataField="Storage_location" HeaderText="库位"/>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
        <uc1:WebPager1 ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />
                          </th>
                </tr>
                <tr>
                         <td bgcolor="#ffffff" style=" text-align:right">
                         <font color="red">  <a href="班级领书.aspx" class="F_red">继续领取>> </a></font>
                          </td>
                </tr>
                        </table>
                    </div>
                    <br />
             </div>
        <br />
    </div>
    </form>
</body>
</html>
<script language="javascript"  type="text/javascript"><!--
    //senfe("表格名称","奇数行背景","偶数行背景","鼠标经过背景","点击后背景");
    senfe("GridView1", "#ECF9FC", "#FFFFFF", "#D6F1F8");
</script>
