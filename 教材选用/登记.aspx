<%@ Page Language="C#" AutoEventWireup="true" CodeFile="登记.aspx.cs" Inherits="教材选用_登记" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title>登记</title>
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
        .style2
        {
            height: 25px;
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
                    教材选用>> 登记</td>
                <td width="50%" align="right">
                     </td>
            </tr>
        </table>
            <div style="width:99%;margin-bottom:10px; height:350px"> 
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
                        <th  bgcolor="#ffffff" class="style1">系别：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                        <asp:TextBox ID="TextBox2" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教师工号：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">教师姓名：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox4" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        <a>*</a>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            联系方式：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                           <asp:TextBox ID="TextBox5" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">&nbsp;</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            &nbsp;</td>
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
                            *</a>&nbsp;&nbsp;</td>
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
                <div style="width:49%; float:left;height:349px; margin-left:10px; ">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                            添加/修改教材</th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            ISBN：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox8" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">教材名称：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox9" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教材类别：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>出版教材</asp:ListItem>
                                <asp:ListItem>自编讲义</asp:ListItem>
                                <asp:ListItem>翻印教材</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            主编：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox10" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">定价：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox11" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                        <a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版社：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="145px"  
                                Height="22px">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        <a>*</a></td>
                        <th  bgcolor="#ffffff" class="style1">版次：</th>
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
                            *</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版年月：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3"> 
                         <asp:TextBox ID="TextBox13" onfocus="setday(this);" runat="server" 
                        CssClass="input1" Width="140px" Height="16px"></asp:TextBox><a>*</a></td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            备注：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                            <asp:RadioButtonList ID="Addition" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>一般</asp:ListItem>
                                <asp:ListItem>特殊适用班级</asp:ListItem>
                                <asp:ListItem>新生</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff"  style="BACKGROUND-COLOR: #ECF9FC; text-align: right; padding-left: 3px;width: 15%; height:40px" 
                                rowspan="3">
                            教材类型：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3" style=" height:10px; text-align: left; padding-left: 3px;">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="国家级获奖" />
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="国家级立项/规划" />
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="省（部）级获奖" />
                            </td>
                        </tr>
                        <tr>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3" style=" height:10px; text-align: left; padding-left: 3px;">
                        <asp:CheckBox ID="CheckBox4" runat="server" Text="省（部）级立项/规划" />
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="校级出版" />
                        <asp:CheckBox ID="CheckBox6" runat="server" Text="校级讲义" />

                            </td>
                        </tr>
                        <tr>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3" style=" height:20px; text-align: left; padding-left: 3px;">                        
                        <asp:CheckBox ID="CheckBox7" runat="server" Text="21世纪" />
                        <asp:CheckBox ID="CheckBox8" runat="server" Text="教学指导委员会推荐" />
                        <asp:CheckBox ID="CheckBox9" runat="server" Text="统编" />
                        <asp:CheckBox ID="CheckBox10" runat="server" Text="其他" />
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="4" class="style2">
                    <asp:Button ID="Button2" runat="server" Text="添加" CssClass="submit" 
                        style=" cursor:pointer; height: 18px;" onclick="Button2_Click" 
                                OnClientClick="return confirm('确定添加吗? ')"/>
                            <asp:Label ID="SaveID" runat="server" Text="0" Visible="False"></asp:Label>
                            </th>
                        </tr>
                        </table>
                </div>
            &nbsp;<a>*</a></div>
            <div style="width:98%;float:left; height:210px; margin-bottom:10px">
            <table cellpadding="1" cellspacing="1" class="tabGg" width="100%">
            <tr>
                <th bgcolor="#FFFFFF" style=" background:#ECF9FC; height:25px">该课程已登记的教材</th>
            </tr>
            <tr>
                <th bgcolor="#ffffff" >
                    <div style="width:100%; height:175px;float:left;overflow:scroll; clear:none ;">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" OnRowDataBound="GvDataType_RowDataBound" >
                        <RowStyle Height="23px" />
                        <Columns>
                <asp:TemplateField HeaderText="学年" SortExpression="date_year">
                    <ItemTemplate>
                        <asp:Label ID="L31" runat="server" Text='<%# Bind("date_year") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T31" runat="server" Text='<%# Bind("date_year") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学期" SortExpression="semester">
                    <ItemTemplate>
                        <asp:Label ID="L32" runat="server" Text='<%# Bind("semester") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T32" runat="server" Text='<%# Bind("semester") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                    <ItemTemplate>
                        <asp:Label ID="L33" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T33" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                    <ItemTemplate>
                        <asp:Label ID="L34" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T34" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="版次" SortExpression="press_time">
                    <ItemTemplate>
                        <asp:Label ID="L35" runat="server" Text='<%# Bind("press_time") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T35" runat="server" Text='<%# Bind("press_time") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="教材类型" SortExpression="book_catagory">
                    <ItemTemplate>
                        <asp:Label ID="L36" runat="server" Text='<%# Bind("book_catagory") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T36" runat="server" Text='<%# Bind("book_catagory") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                    <ItemTemplate>
                        <asp:Label ID="L37" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T37" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主编" SortExpression="book_editor">
                    <ItemTemplate>
                        <asp:Label ID="L38" runat="server" Text='<%# Bind("book_editor") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T38" runat="server" Text='<%# Bind("book_editor") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版日期" SortExpression="press_date">
                    <ItemTemplate>
                        <asp:Label ID="L39" runat="server" Text='<%# Bind("press_date") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T39" runat="server" Text='<%# Bind("press_date") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="定价" SortExpression="book_price">
                    <ItemTemplate>
                        <asp:Label ID="L310" runat="server" Text='<%# Bind("book_price") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T310" runat="server" Text='<%# Bind("book_price") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CssClass="submit" 
                            Text="修改" onclick="Button3_Click" style=" cursor:pointer;"
                            CommandArgument='<%# Eval("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button4" runat="server" CssClass="submit" style=" cursor:pointer;"
                            Text="删除" onclick="Button4_Click"  OnClientClick="return confirm('确定删除吗？')" 
                            CommandArgument='<%# Eval("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    </asp:GridView> 
                    </div>
                </th>
            </tr>
            </table>
                </div>
                <div style="width:98%;float:left; height:210px">
            <table cellpadding="1" cellspacing="1" class="tabGg" width="100%">
            <tr>
                <th bgcolor="#FFFFFF" style=" background:#ECF9FC; height:25px">该课程过去用过的教材</th>
            </tr>
            <tr>
                <th bgcolor="#ffffff" >
                    <div style="width:100%; height:175px;float:left;overflow:scroll; clear:none ;">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" OnRowDataBound="GvDataType_RowDataBound">
                        <RowStyle Height="23px" />
                        <Columns>
                <asp:TemplateField HeaderText="学年" SortExpression="date_year">
                    <ItemTemplate>
                        <asp:Label ID="Label21" runat="server" Text='<%# Bind("date_year") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T21" runat="server" Text='<%# Bind("date_year") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学期" SortExpression="semester">
                    <ItemTemplate>
                        <asp:Label ID="Label22" runat="server" Text='<%# Bind("semester") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T22" runat="server" Text='<%# Bind("semester") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                    <ItemTemplate>
                        <asp:Label ID="L23" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T23" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                    <ItemTemplate>
                        <asp:Label ID="L24" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T24" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="版次" SortExpression="press_time">
                    <ItemTemplate>
                        <asp:Label ID="L25" runat="server" Text='<%# Bind("press_time") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T25" runat="server" Text='<%# Bind("press_time") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="教材类型" SortExpression="book_catagory">
                    <ItemTemplate>
                        <asp:Label ID="L26" runat="server" Text='<%# Bind("book_catagory") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T26" runat="server" Text='<%# Bind("book_catagory") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                    <ItemTemplate>
                        <asp:Label ID="L27" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T27" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主编" SortExpression="book_editor">
                    <ItemTemplate>
                        <asp:Label ID="L28" runat="server" Text='<%# Bind("book_editor") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T28" runat="server" Text='<%# Bind("book_editor") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出版日期" SortExpression="press_date">
                    <ItemTemplate>
                        <asp:Label ID="L29" runat="server" Text='<%# Bind("press_date") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T29" runat="server" Text='<%# Bind("press_date") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="定价" SortExpression="book_price">
                    <ItemTemplate>
                        <asp:Label ID="L210" runat="server" Text='<%# Bind("book_price") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="T210" runat="server" Text='<%# Bind("book_price") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="SelectHistory" runat="server" CssClass="submit" 
                            Text="选择" onclick="SelectHistory_Click" style=" cursor:pointer;"
                            CommandArgument='<%# Eval("id") %>' />
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
                                 <asp:Button ID="Button5" runat="server" Text="保存" CssClass="submit" 
                        onclick="Button5_Click" OnClientClick="return confirm('确定保存这些信息吗? ')"/>
                <asp:Button ID="Button6" runat="server" Text="提交" CssClass="submit" 
                        onclick="Button6_Click" OnClientClick="return confirm('确定提交这些信息吗? ')"/>
                    </th>
            </tr>
        </table>
                </div>
        </form>
</body>
</html>
<script language="javascript"  type="text/javascript"><!--
    //senfe("表格名称","奇数行背景","偶数行背景","鼠标经过背景","点击后背景");
    senfe("GridView2", "#ECF9FC", "#FFFFFF", "#D6F1F8");
    senfe("GridView3", "#ECF9FC", "#FFFFFF", "#D6F1F8");
</script>

