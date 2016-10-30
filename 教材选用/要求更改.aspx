<%@ Page Language="C#" AutoEventWireup="true" CodeFile="要求更改.aspx.cs" Inherits="教材选用_允许更改" %>
<%@ Register Src="../usercontrol/WebPagerByID.ascx" TagName="WebPager" TagPrefix="uc1" %>
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
<script language="javascript" type='text/javascript' src="../js/PopupCalendarDay.js"> </script>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0"  >
            <tr>
                <td width="2%" height="22" >
                    <img src="../images/module.gif"  height="22"/></td>
                <td width="55%" >
                    教材选用&gt;&gt;允许更改</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="8" style=" background:#ECF9FC; height:25px">请输入查询条件</th>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">学年：</th>
                <th align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:DropDownList ID="DropDataYear" runat="server" Width="145px"  
                                Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </th>
                <td  bgcolor="#FFFFFF" class="style1" >
                            学院：
                            </td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="145px"  Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">课程编号：</th>
                <th align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TBCourseId" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                    </th>
                <td  bgcolor="#FFFFFF" class="style1">
                            状态：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
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
                <th  bgcolor="#FFFFFF" class="style1">学期：</th>
                <th align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:DropDownList ID="DropSemester" runat="server" Width="145px"  
                                Height="22px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                    </th>
                <td  bgcolor="#FFFFFF" class="style1" >
                            教师工号：
                            </td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TBTeacherId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">课程名称：
                        </th>
                <th align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TBCourseName" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </th>
                <td  bgcolor="#FFFFFF" class="style1">
                            学院意见：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:DropDownList ID="DropCollegeAdv" runat="server" Width="145px"  
                                Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">&nbsp;</th>
                <th align="left" bgcolor="#FFFFFF" class="right_bg">
                            &nbsp;</th>
                <td bgcolor="#FFFFFF" class="style1" >
                            教师姓名：
                        </td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TBTeacherName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                <th   bgcolor="#FFFFFF" class="style1">教材名称：</th>
                <th align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TBBookName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </th>
                <td bgcolor="#FFFFFF" class="style1">
                            学校意见：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:DropDownList ID="DropUniversityAdv" runat="server" Width="145px"  
                                Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="8" style=" height:25px">
                        <asp:Button ID="Button1" runat="server" Text="查询" CssClass="submit" 
                            onclick="Button1_Click" style="cursor: pointer; "/>
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
                    <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview" EmptyDataText="没有符合条件的信息" Width="99%" RowStyle-HorizontalAlign="center" 
                        GridLines="None" PageSize="100"  OnRowDataBound="GvDataType_RowDataBound" AllowSorting="True" onsorting="GridView1_Sorting">
                        <RowStyle Height="23px" />
                        <Columns>
                        <asp:TemplateField HeaderText="学院" SortExpression="teacher_college">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("teacher_college") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("teacher_college") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="教师编号" SortExpression="teacher_id">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("teacher_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("teacher_id") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="教师姓名" SortExpression="teacher_name">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("teacher_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("teacher_name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程编号" SortExpression="course_id">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("course_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("course_id") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程名称" SortExpression="course_name">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("course_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("course_name") %>'></asp:TextBox>
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
            <asp:TemplateField HeaderText="教材名称" SortExpression="book_name">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("book_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("book_name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="出版社" SortExpression="press_name">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("press_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("press_name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="更改">
                <ItemTemplate>
                    <asp:Button ID="Button2" runat="server" CommandArgument='<%# Eval("id") %>' 
                       Text="允许" CssClass="submit" style=" cursor:pointer"
                        onclick="Button2_Click"  OnClientClick="return confirm('确定允许教师更改此书的信息吗? ')" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    </asp:GridView>--%> 
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="没有符合条件的信息"  OnRowDataBound="GvDataType_RowDataBound" 
            AllowSorting="True" CssClass="gridview" OnRowEditing="gvData_RowEditing" 
            OnRowUpdating="gvData_RowUpdating" 
            OnRowCancelingEdit="gvData_RowCancelingEdit"
            Width="99%" RowStyle-HorizontalAlign="center" DataKeyNames="id">
            <RowStyle Height="23px" />
            <Columns>
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="course_id" HeaderText="课程编号" ReadOnly=true/>
             <asp:BoundField DataField="course_name" HeaderText="课程名称" ReadOnly=true/>
             <asp:BoundField DataField="book_id" HeaderText="教材编号" ReadOnly=true/>
             <asp:BoundField DataField="book_name" HeaderText="教材名称" ReadOnly=true/>
             <asp:TemplateField HeaderText="更改原因">
                     <EditItemTemplate>
                          <asp:TextBox ID="TBReason" CssClass="input1" runat="server" Width="140px"/>
                      </EditItemTemplate>
                      <ItemTemplate>
                        <asp:Label ID="LbReason" runat="server" Height="20px"  Width="56px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:CommandField HeaderText="要求更改" ShowEditButton="True" CancelText="取消" 
                    EditText="确定" UpdateText="确定">
               </asp:CommandField>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
                             <br />
        <uc1:WebPager ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />
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
