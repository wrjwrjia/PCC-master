<%@ Page Language="C#" AutoEventWireup="true" CodeFile="查看订单.aspx.cs" Inherits="教材计划_查看订单" %>
<% @ Register Src="../usercontrol/WebPagerByIDForList.ascx" TagName="WebPager2" TagPrefix="uc1"%>
<% @ Register Src="../usercontrol/WebPagerByIDForOrder.ascx" TagName="WebPager1" TagPrefix="uc1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title>本科申领</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/changerows.js"></script>
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
            width: 15%;
            height:20px;
        }
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
                    查看订单</td>
                <td width="50%" align="right">
                     </td>
            </tr>
        </table>
            <div style="width:98%;margin-bottom:10px; height: 353px; float:left;"> 
                <div style="width:49%; float:left;">
                <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="2" style=" background:#ECF9FC; height:25px">
                            基本信息
                            </th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            学年：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="145px"  Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            学期：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            订单号：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            供货商：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                    <asp:DropDownList ID="DropSupplyPerson" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="contact_person" 
                        DataValueField="contact_person" Width="145px"  Height="22px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                        <a><asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SQLCONN %>" 
            SelectCommand="SELECT [contact_person] FROM [press_message]">
        </asp:SqlDataSource>
                            </a> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            操作人：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            &nbsp;校区：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                        <a>
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>明故宫</asp:ListItem>
                            <asp:ListItem>将军路</asp:ListItem>
                            </asp:DropDownList>
                            </a>&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1" style=" width:27%">
                            提交时间范围：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                         <asp:TextBox ID="TextBox4" onfocus="setday(this);" runat="server" 
                        CssClass="input1" Width="140px" Height="16px"></asp:TextBox>至<asp:TextBox 
                                ID="TextBox5" onfocus="setday(this);" runat="server" 
                        CssClass="input1" Width="140px" Height="16px"></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1" style=" width:27%">
                            状态：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropState" runat="server" Width="145px"  
                                Height="22px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>已保存</asp:ListItem>
                                <asp:ListItem>已提交</asp:ListItem>
                                <asp:ListItem>院审批已通过</asp:ListItem>
                                <asp:ListItem>校审批已通过</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="2" >
                                 <asp:Button ID="Search" runat="server" Text="查询" CssClass="submit" style=" cursor:pointer" onclick="Search_Click"/>
                        </th>
                        </tr>
                        </table>
        </div>
            <div style="width:49%; float:right;height:300px;">
            <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                             教材基本信息</th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            订单号：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox6" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True"></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">订单日期：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox7" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            操作人：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox8" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">&nbsp;</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            &nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            学年：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox10" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">学期：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox11" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            供货商： 
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox12" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">联系方式：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox13" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            ISBN：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox14" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">教材名称：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox15" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版社：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox16" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">版次：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox17" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            主编：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox18" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">单价：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox19" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            计划册数：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox20" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">机动册数：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox21" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            总册数：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox22" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            &nbsp;&nbsp;&nbsp;</td>
                        <th  bgcolor="#ffffff" class="style1">总价：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox23" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            &nbsp;&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            校区：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox24" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                            &nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <th  bgcolor="#ffffff" class="style1">&nbsp;</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">

                            <asp:TextBox ID="SaveID" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" Visible="False" ></asp:TextBox> 

                            </td>
                        </tr>
                        </table>
            </div>
            </div>
            <div style="width:98%;float:left;">
                <div style="width:49%; float:left;height:270px;">
                <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" style=" background:#ECF9FC; height:25px">
       订单列表</th>
                </tr>
                        </table>
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" >
           <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="没有符合条件的信息"  OnRowDataBound="Gv2DataType_RowDataBound" 
            AllowSorting="True" CssClass="gridview" OnRowDeleting="gv2Data_RowDeleting" 
            Width="100%" RowStyle-HorizontalAlign="center" DataKeyNames="order_id">
            <RowStyle Height="23px" />
            <Columns>
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="order_id" HeaderText="订单号"/>
             <asp:BoundField DataField="order_date" HeaderText="日期"/>
             <asp:BoundField DataField="operate_person" HeaderText="操作人"/>
             <asp:BoundField DataField="supply_person" HeaderText="供应商"/>
             <asp:BoundField DataField="supplier_phone" HeaderText="电话"/>
             <asp:BoundField DataField="campus" HeaderText="校区"/>
             <asp:BoundField DataField="state_id" HeaderText="状态"/>
                             <asp:TemplateField HeaderText="详细">
                    <ItemTemplate>
                        <asp:LinkButton ID="Detail" runat="server" 
                            CommandArgument='<%# Eval("order_id") %>' onclick="Detail_Click">详细</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:CommandField HeaderText="删除" ShowDeleteButton="True" DeleteText="删除" />
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView> 
                             <br />
        <uc1:WebPager2 ID="WebPager2" DataId="GridView2" PageSize="15"  runat="server" />

                          </th>
                </tr>
                </table>

                            <asp:TextBox ID="SaveOrderID" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" Visible="False" ></asp:TextBox> 

                </div>
            <div style="width:49%; height:240px;float:right;">
            <table cellpadding="1" cellspacing="1" class="tabGg" width="100%">
            <tr>
                <th bgcolor="#FFFFFF" style=" background:#ECF9FC; height:25px">此订单中的教材</th>
            </tr>
            <tr>
                <th bgcolor="#ffffff" >
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="没有符合条件的信息"  OnRowDataBound="Gv1DataType_RowDataBound" 
            AllowSorting="True" CssClass="gridview" OnRowDeleting="gv1Data_RowDeleting"
            Width="100%" RowStyle-HorizontalAlign="center" DataKeyNames="id">
            <RowStyle Height="23px" />
            <Columns>
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="book_id" HeaderText="ISBN"/>
             <asp:BoundField DataField="book_name" HeaderText="教材名"/>
             <asp:BoundField DataField="press_name" HeaderText="出版社"/>
             <asp:BoundField DataField="press_time" HeaderText="版次"/>
             <asp:BoundField DataField="book_editor" HeaderText="主编"/>
             <asp:BoundField DataField="book_price" HeaderText="定价"/>
             <asp:BoundField DataField="total_num" HeaderText="册数"/>
             <asp:BoundField DataField="total_price" HeaderText="总价"/>
               <asp:TemplateField HeaderText="详细">
                    <ItemTemplate>
                        <asp:LinkButton ID="Detail" runat="server" 
                            CommandArgument='<%# Eval("id") %>' onclick="BookDetail_Click">详细</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:CommandField HeaderText="删除" ShowDeleteButton="True" 
                     DeleteText="删除" />
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
                    <br />
        <uc1:WebPager1 ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />

                </th>
            </tr>
            </table>
                <br />
            </div>
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
