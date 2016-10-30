<%@ Page Language="C#" AutoEventWireup="true" CodeFile="校审批.aspx.cs" Inherits="教材申领_校审批" %>
<%@Register Src="../usercontrol/WebPagerByIDForSearch.ascx" TagName="WebPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>校审批</title>
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
    <style type="text/css">
        td a{color:red;}
        .style1
        {
            BACKGROUND-COLOR: #ECF9FC;
            text-align: right;
            padding-left: 3px;
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
                <td width="2%" height="22" 
            bgcolor="#ffffff">
                    <img src="../images/module.gif"  height="22"/></td>
                <td width="55%" >
                    教材申领&gt;&gt;校审批</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="6" style=" background:#ECF9FC; height:25px">请输入查询条件</th>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">学院：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="145px"  
                                Height="22px" onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                                AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                <td bgcolor="#FFFFFF" class="style1">
                            申请教师工号：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">ISBN：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TBISBN" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">&nbsp;课程编号：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:DropDownList ID="DropCourseId" runat="server" Width="145px"  
                                Height="22px" onselectedindexchanged="DropCourseId_SelectedIndexChanged" 
                                AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                <td bgcolor="#FFFFFF" class="style1">
                            申请教师姓名：
                        </td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox2" CssClass="input1" runat="server" 
                        Width="140px" Height="16px" ></asp:TextBox> 
                            </td>
                <th   bgcolor="#FFFFFF" class="style1">教材名称：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox5" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">课程名称：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:DropDownList ID="DropCourseName" runat="server" Width="145px"  
                                Height="22px" onselectedindexchanged="DropCourseName_SelectedIndexChanged" 
                                AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                <td bgcolor="#FFFFFF" class="style1">
                            需求教师工号：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TBTeacherId" CssClass="input1" runat="server" 
                        Width="140px" Height="16px" ></asp:TextBox> 
                            </td>
                <th   bgcolor="#FFFFFF" class="style1">领书校区：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:DropDownList ID="DropCampus" runat="server" Width="145px"  
                                Height="22px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>明故宫</asp:ListItem>
                                <asp:ListItem>将军路</asp:ListItem>
                            </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">&nbsp;</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            &nbsp;</td>
                <td bgcolor="#FFFFFF" class="style1">
                            需求教师姓名：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TBTeacherName" CssClass="input1" runat="server" 
                        Width="140px" Height="16px" ></asp:TextBox> 
                            </td>
                <th   bgcolor="#FFFFFF" class="style1">&nbsp;</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            &nbsp;</td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="6" style=" height:25px">
                        <asp:Button ID="Button1" runat="server" Text="查询" CssClass="submit" 
                                onclick="Button1_Click" style="cursor: pointer; height: 18px;"/>
                </th>
            </tr>
        </table>
                </div>
            <div style="width:99%;float:left;">                            
                    <div style="width:49%; height:300px;float:left;">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                             符合条件的信息</th>
                </tr>
                        </table> 
                        <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" >
                             <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                 CssClass="gridview" EmptyDataText="没有符合条件的信息" GridLines="None" 
                                 OnRowDataBound="GvDataType_RowDataBound" PageSize="100" 
                                 RowStyle-HorizontalAlign="center" Width="120%">
                                 <RowStyle Height="23px" />
                                 <Columns>
                                     <asp:TemplateField>
                                         <ItemTemplate>
                                             <asp:CheckBox ID="CheckBox1" runat="server" />
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="学院" SortExpression="teacher_college">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("teacher_college") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label3" runat="server" Text='<%# Bind("teacher_college") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="教师工号" SortExpression="teacher_id">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("teacher_id") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" runat="server" Text='<%# Bind("teacher_id") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="教师姓名" SortExpression="teacher_name">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("teacher_name") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label2" runat="server" Text='<%# Bind("teacher_name") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="课程编号" SortExpression="course_id">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("course_id") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label4" runat="server" Text='<%# Bind("course_id") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="课程名称" SortExpression="course_name">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("course_name") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label5" runat="server" Text='<%# Bind("course_name") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="ISBN" SortExpression="book_id">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("book_id") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label6" runat="server" Text='<%# Bind("book_id") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label7" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="详细信息">
                                         <ItemTemplate>
                                             <asp:LinkButton ID="LinkButton1" runat="server" 
                                                 CommandArgument='<%# Eval("id") %>' onclick="LinkButton1_Click">详细</asp:LinkButton>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                 <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                             </asp:GridView>--%>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="没有符合条件的信息"  OnRowDataBound="GvDataType_RowDataBound" 
            AllowSorting="True" CssClass="gridview" OnRowDeleting="gvData_RowDeleting"
            Width="100%" RowStyle-HorizontalAlign="center" DataKeyNames="id">
            <RowStyle Height="23px" />
            <Columns>
            <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="teacher_college" HeaderText="学院"/>
             <asp:BoundField DataField="apply_teacher_name" HeaderText="申请人"/>
             <asp:BoundField DataField="teacher_name" HeaderText="需求人"/>
             <asp:BoundField DataField="course_name" HeaderText="课程名称"/>
             <asp:BoundField DataField="book_name" HeaderText="教材名称"/>
             <asp:BoundField DataField="Campus" HeaderText="校区"/>
             <asp:CommandField HeaderText="详细" ShowDeleteButton="True" 
                     DeleteText="详细" />
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
        <uc1:WebPager ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />

                          </th>
                </tr>
                <tr>
                         <td bgcolor="#ffffff" style=" height:25px">
                         <input type="checkbox" name="chkall" onClick="CheckAll(this.form)" value="on"/>
                            <font color="#FF0000">全选&nbsp; </font><asp:Button ID="Button2" runat="server" Text="同意" CssClass="submit" 
                        style=" cursor:pointer" onclick="Button2_Click" OnClientClick="return confirm('确定同意吗? ')"/>
                    <asp:Button ID="Button3" runat="server" Text="不同意" CssClass="submit" 
                        style=" cursor:pointer" onclick="Button3_Click" OnClientClick="return confirm('确定不同意吗? ')"/>
                          </td>
                </tr>
                        </table>
                    </div>
                    <div style="width:49%; height:300px;float:right; ">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="4" style=" background:#ECF9FC; height:25px">
                            基本信息
                            </th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            学院：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox8" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        <th  bgcolor="#ffffff" class="style1"></th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教师工号：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox9" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        <th  bgcolor="#ffffff" class="style1">教师姓名：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox10" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            课程编号：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox11" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        <th  bgcolor="#ffffff" class="style1">课程名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox12" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            ISBN：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox13" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        <th  bgcolor="#ffffff" class="style1">教材名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox14" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版社：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox15" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        <th  bgcolor="#ffffff" class="style1">版次：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox16" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版年份：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox17" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        <th  bgcolor="#ffffff" class="style1">主编：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox18" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            备注：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox19" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        <th  bgcolor="#ffffff" class="style1"></th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:Label ID="SaveID" runat="server" Text="Label" Visible="False"></asp:Label>
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            校审批意见：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                            <asp:TextBox ID="TextBox20" Height="50px" runat="server" Width="335px" 
                                style="BORDER-RIGHT: #BBD0E1 1px solid; BORDER-TOP: #BBD0E1 1px solid; FONT-WEIGHT: normal; FONT-SIZE: 9pt; BACKGROUND: #ffffff; BORDER-LEFT: #BBD0E1 1px solid; COLOR: #000000; LINE-HEIGHT: normal; BORDER-BOTTOM: #BBD0E1 1px solid; FONT-STYLE: normal;FONT-VARIANT: normal" 
                                TextMode="MultiLine"></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="4" style=" height:25px">
                        <asp:Button ID="Button4" runat="server" Text="同意" CssClass="submit" 
                        style=" cursor:pointer" onclick="Button4_Click" 
                        OnClientClick="return confirm('确定同意吗? ')"/>
                    <asp:Button ID="Button5" runat="server" Text="不同意" CssClass="submit" 
                        style=" cursor:pointer" onclick="Button5_Click" 
                        OnClientClick="return confirm('确定不同意吗? ')"/>
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
