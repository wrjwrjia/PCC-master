<%@ Page Language="C#" AutoEventWireup="true" CodeFile="本科申领.aspx.cs" Inherits="教材选用_t" %>

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
            height:20px;
        }
        </style>
<link href="../css/loading.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../js/loading.js" charset="gb2312"></script>
<script language="javascript" type='text/javascript' src="../js/PopupCalendarDay.js"> </script>
<body style="margin-top:2px;">
    <form id="form1" runat="server">
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31" style="height: 25px">
                    <img src="../images/module.gif" width="20"  alt="" onclick="location.href=location.href" style="cursor:hand" alt="refresh"/></td>
                <td>
                    教材申领>> 本科申领 </td>
                <td width="50%" align="right">
                     </td>
            </tr>
        </table>
            <div style="width:99%;margin-bottom:10px;"> 
                <div style="width:49%; height:240px;float:left;">
                <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
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
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">学期：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            学院：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">领书校区：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="145px"  Height="22px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>明故宫</asp:ListItem>
                            <asp:ListItem>将军路</asp:ListItem>
                            </asp:DropDownList>
                            <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教师工号：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox2" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">教师姓名：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        <a>*</a>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                            需要教材的教师信息</th>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            课程编号：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                        <a>
                            <asp:DropDownList ID="DropCourseId" runat="server" Width="145px"  
                                Height="22px" onselectedindexchanged="DropCourseId_SelectedIndexChanged" 
                                AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            *</a></td>
                        <th  bgcolor="#ffffff" class="style1">课程名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                        <a>
                            <asp:DropDownList ID="DropCourseName" runat="server" Width="145px"  
                                Height="22px" onselectedindexchanged="DropCourseName_SelectedIndexChanged" 
                                AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            *</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教师工号：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TBTeacherId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">教师姓名：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TBTeacherName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="4" >
                    <asp:Button ID="OK" runat="server" Text="确定" CssClass="submit" 
                        style=" cursor:pointer; height: 18px;" onclick="OK_Click" 
                                OnClientClick="return confirm('确定填写信息无误吗? ')"/>
                        </th>
                        </tr>
                        </table>
        </div>
                <div style="width:49%; float:left;height:240px; margin-left:10px; ">
                <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                             该课程已登记的教材书目</th>
                </tr>
                        </table>
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" >
                <div style="width:100%; height:206px; float:left;overflow:scroll; clear:none;">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="97%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" OnRowDataBound="GvDataType_RowDataBound">
                        <RowStyle Height="23px" />
                        <Columns>
                <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                    <EditItemTemplate>
                        <asp:TextBox ID="T21" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L21" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="T22" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L22" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="版次" SortExpression="press_time">
                    <EditItemTemplate>
                        <asp:TextBox ID="T25" runat="server" Text='<%# Bind("press_time") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L25" runat="server" Text='<%# Bind("press_time") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="T24" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L24" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主编" SortExpression="book_editor">
                    <EditItemTemplate>
                        <asp:TextBox ID="T27" runat="server" Text='<%# Bind("book_editor") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L27" runat="server" Text='<%# Bind("book_editor") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="教材类别" SortExpression="book_catagory">
                    <EditItemTemplate>
                        <asp:TextBox ID="T28" runat="server" Text='<%# Bind("book_catagory") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L28" runat="server" Text='<%# Bind("book_catagory") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="定价" SortExpression="book_price">
                    <EditItemTemplate>
                        <asp:TextBox ID="T23" runat="server" Text='<%# Bind("book_price") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L23" runat="server" Text='<%# Bind("book_price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注" SortExpression="addition">
                    <EditItemTemplate>
                        <asp:TextBox ID="T26" runat="server" Text='<%# Bind("addition") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L26" runat="server" Text='<%# Bind("addition") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button9" runat="server"   CssClass="submit" style="cursor:pointer;"
                            Text="确定" Width="40px" CommandArgument='<%# Eval("id") %>' 
                            onclick="Button3_Click" OnClientClick="return confirm('确定选择该教材吗? ')"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center"/>
                    </asp:GridView> 
                    </div>
                          </th>
                </tr>
                </table>
                </div>
            </div>
            <div style="width:98%;float:left;">
            <table cellpadding="1" cellspacing="1" class="tabGg" width="100%">
            <tr>
                <th bgcolor="#FFFFFF" style=" background:#ECF9FC; height:25px">本次申领的教材书目</th>
            </tr>
            <tr>
                <th bgcolor="#ffffff" >
                    <div style="width:100%; height:171px;float:left;overflow:scroll; clear:none ;">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" OnRowDataBound="GvDataType_RowDataBound">
                        <RowStyle Height="23px" />
                        <Columns>
                <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                    <EditItemTemplate>
                        <asp:TextBox ID="T31" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L31" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="T32" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L32" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="定价" SortExpression="book_price">
                    <EditItemTemplate>
                        <asp:TextBox ID="T33" runat="server" Text='<%# Bind("book_price") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L33" runat="server" Text='<%# Bind("book_price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="T34" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L34" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="版次" SortExpression="press_time">
                    <EditItemTemplate>
                        <asp:TextBox ID="T35" runat="server" Text='<%# Bind("press_time") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L35" runat="server" Text='<%# Bind("press_time") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主编" SortExpression="book_editor">
                    <EditItemTemplate>
                        <asp:TextBox ID="T36" runat="server" Text='<%# Bind("book_editor") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L36" runat="server" Text='<%# Bind("book_editor") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button4" runat="server" CommandArgument='<%# Eval("id") %>' 
                            Text="删除" Width="40px" CssClass="submit" style="cursor:pointer;"
                            onclick="Button4_Click" OnClientClick="return confirm('确定删除吗? ')"/>
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
                                 <asp:Button ID="Button7" runat="server" Text="保存" CssClass="submit" style=" cursor:pointer" 
                        onclick="Button7_Click" OnClientClick="return confirm('确定保存这些信息吗? ')"/>
                <asp:Button ID="Button8" runat="server" Text="提交" CssClass="submit" style=" cursor:pointer" 
                        onclick="Button8_Click" OnClientClick="return confirm('确定提交这些信息吗? ')"/>
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
</script>

