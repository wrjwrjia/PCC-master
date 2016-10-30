<%@ Page Language="C#" AutoEventWireup="true" CodeFile="其他申领.aspx.cs" Inherits="教材申领_其他申领" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title>其他申领</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/changerows.js"></script>
    <style type="text/css">
        td a{color:red;}
        .style2
        {
            BACKGROUND-COLOR: #ECF9FC;
            text-align: right;
            padding-left: 3px;
            }
</style>
<link href="../css/loading.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../js/loading.js" charset="gb2312"></script>
<script type="text/javascript" src="../js/CalendarYM.js" ></script>
<script language="javascript" type='text/javascript' src="../js/PopupCalendarDay.js"> </script>
<body style="margin-top:2px;">
    <form id="form1" runat="server">
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31" style="height: 25px">
                    <img src="../images/module.gif" width="20"  alt="" onclick="location.href=location.href" style="cursor:hand" alt="refresh"/></td>
                <td>
                    教材申领>> 其他申领 </td>
                <td width="50%" align="right">
                     </td>
            </tr>
        </table>
        <div style="width:99%; margin-bottom:10px; height:310px"> 
                <div style="width:49%; height:240px;float:left; margin-bottom:10px;">
                <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="2" style=" background:#ECF9FC; height:25px">
                             基本信息</th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            学年：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="145px"  Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            学期：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            学院：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            校区： </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>明故宫</asp:ListItem>
                            <asp:ListItem>将军路</asp:ListItem>
                            </asp:DropDownList>
                            <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            教师编号：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox2" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            教师姓名：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            课程编号：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox4" CssClass="input1" runat="server" Width="140px" 
                                ontextchanged="TextBox4_TextChanged" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            课程名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox5" CssClass="input1" runat="server" Width="140px" 
                                ontextchanged="TextBox5_TextChanged" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  align="center" bgcolor="#ffffff" class="right_bg"colspan="2" >
                    <asp:Button ID="OK" runat="server" Text="确定" CssClass="submit" 
                        style=" cursor:pointer; height: 18px;" onclick="OK_Click" 
                                OnClientClick="return confirm('确定填写信息无误吗? ')"/>
                            </th>
                        </tr>
                        </table>
        </div>
                <div style="width:49%; float:left;height:270px;margin-left:10px; ">
                <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="2" style=" background:#ECF9FC; height:25px">
                            添加教材
                            </th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            ISBN：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox7" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            教材名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox8" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            出版社：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropPressName" runat="server" Width="145px"  
                                Height="22px">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            版次： </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                        <a>
                            <asp:DropDownList ID="PressTime" runat="server" Width="145px"  
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
                        <th  bgcolor="#ffffff" class="style2">
                            主编：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox13" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            定价：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox12" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2">
                            出版年月：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                         <asp:TextBox ID="TBPressDate" onfocus="setday(this);" runat="server" 
                        CssClass="input1" Width="140px" Height="16px"></asp:TextBox><a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="2" style=" height:28px">
                            <asp:Button ID="Button1" runat="server" Text="添加" CssClass="submit" 
                                style="cursor:pointer;" onclick="Button1_Click"/>
                        </th>
                        </tr>
                        </table>
                </div>
            </div>
            <div style="width:98%;float:left;">
            <table cellpadding="1" cellspacing="1" class="tabGg" width="100%">
            <tr>
                <th bgcolor="#FFFFFF" style=" background:#ECF9FC; height:25px">已申领的教材书目</th>
            </tr>
            <tr>
                <th bgcolor="#ffffff" >
                    <div style="width:100%; height:171px;float:left;overflow:scroll; clear:none ;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" OnRowDataBound="GvDataType_RowDataBound" Height="20px" 
                            PageSize="100">
                        <RowStyle Height="23px" />
                        <Columns>
                <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                    <ItemTemplate>
                        <asp:Label ID="L11" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T11" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                    <ItemTemplate>
                        <asp:Label ID="L12" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T12" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="版次" SortExpression="press_time">
                    <ItemTemplate>
                        <asp:Label ID="L16" runat="server" Text='<%# Bind("press_time") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T16" runat="server" Text='<%# Bind("press_time") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                    <ItemTemplate>
                        <asp:Label ID="L15" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T15" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主编" SortExpression="book_editor">
                    <ItemTemplate>
                        <asp:Label ID="L14" runat="server" Text='<%# Bind("book_editor") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T14" runat="server" Text='<%# Bind("book_editor") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版日期" SortExpression="press_date">
                    <ItemTemplate>
                        <asp:Label ID="L17" runat="server" Text='<%# Bind("press_date") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T17" runat="server" Text='<%# Bind("press_date") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="定价" SortExpression="book_price">
                    <ItemTemplate>
                        <asp:Label ID="L13" runat="server" Text='<%# Bind("book_price") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T13" runat="server" Text='<%# Bind("book_price") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button4" runat="server" CssClass=" submit" style="cursor:pointer;"
                            onclick="Button4_Click" Text="删除" 
                            CommandArgument='<%# Eval("id") %>' OnClientClick="return confirm('确定删除吗? ')"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    </asp:GridView> 
                    </div>
                </th>
            </tr>
            <tr>
                <th bgcolor="#ffffff" style=" height:25px">
                <asp:Button ID="Button2" runat="server" Text="保存" CssClass="submit" style="cursor:pointer;" 
                        onclick="Button2_Click" OnClientClick="return confirm('确定保存这些信息吗? ')"/>
                <asp:Button ID="Button3" runat="server" Text="提交" CssClass="submit" style="cursor:pointer;"
                        onclick="Button3_Click" OnClientClick="return confirm('确定提交这些信息吗? ')"/>
                    </th>
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
--></script>
