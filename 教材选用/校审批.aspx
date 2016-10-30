<%@ Page Language="C#" AutoEventWireup="true" CodeFile="校审批.aspx.cs" Inherits="教材选用_校审批" %>
<%@ Register Src="../usercontrol/WebPagerByIDForSearch.ascx" TagName="WebPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>院审批</title>
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
        .style2
        {
            BACKGROUND-COLOR: #ECF9FC;
            text-align: right;
            padding-left: 3px;
            width:3%;
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
                    教材选用&gt;&gt;校审批</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="8" style=" background:#ECF9FC; height:25px">请输入查询条件</th>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">学院：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="145px"  Height="22px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                <td  bgcolor="#FFFFFF" class="style1">
                            教师工号：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <td bgcolor="#FFFFFF" class="style1">
                            课程编号：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">ISBN：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TBISBN" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">&nbsp;</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            &nbsp;</td>
                <td  bgcolor="#FFFFFF" class="style1">
                            教师姓名：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox4" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </td>
                <td bgcolor="#FFFFFF" class="style1">
                            课程名称：</td>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox2" CssClass="input1" runat="server" 
                        Width="140px" Height="16px" ></asp:TextBox> 
                            </td>
                <th   bgcolor="#FFFFFF" class="style1">&nbsp;教材名称</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TBBookName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
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
                    <div style="width:64%; height:500px;float:left;">
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
            AllowSorting="True" CssClass="gridview" OnRowDeleting="gvData_RowDeleting"
            Width="100%" RowStyle-HorizontalAlign="center" DataKeyNames="id">
            <RowStyle Height="23px" />
            <Columns>
             <asp:TemplateField>
                 <ItemTemplate>
                     <asp:CheckBox ID="CheckBox1"  runat="server" />
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="teacher_college" HeaderText="学院"/>
             <asp:BoundField DataField="teacher_name" HeaderText="教师姓名"/>
             <asp:BoundField DataField="course_id" HeaderText="课程编号"/>
             <asp:BoundField DataField="course_name" HeaderText="课程名称"/>
             <asp:BoundField DataField="book_id" HeaderText="教材编号"/>
             <asp:BoundField DataField="book_name" HeaderText="教材名称"/>
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
                    <div style="width:34%; height:643px; float:right; ">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="3" style=" background:#ECF9FC; height:25px">
                            基本信息
                            </th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2" rowspan="2">
                            学年学期</th>
                        <th  bgcolor="#ffffff" class="style1">
                            学年：&nbsp;&nbsp;&nbsp;</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TBYear" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            学期：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TBSemester" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2" rowspan="5">
                            教师信息</th>
                        <th  bgcolor="#ffffff" class="style1">
                            学院：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox5" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            系别：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                        <asp:TextBox ID="TextBox6" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教师工号：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox7" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教师姓名：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox8" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            联系方式：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox9" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2" rowspan="2">
                            课程信息</th>
                        <th  bgcolor="#ffffff" class="style1">
                            课程编号：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox10" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            课程名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox11" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2" rowspan="10">
                            教材信息</th>
                        <th  bgcolor="#ffffff" class="style1">
                            ISBN：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox12" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教材名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox13" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教材类别：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox14" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                           出版年月：</td>
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox22" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教材类型：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                         <asp:TextBox ID="TextBox15" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                             主编：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox16" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                             定价：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox17" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版社：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox18" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            版次：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox19" CssClass="input1" runat="server" Width="140px" 
                                ReadOnly="True" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            备注：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox20" Height="50px" runat="server" Width="140px" 
                                style="BORDER-RIGHT: #BBD0E1 1px solid; BORDER-TOP: #BBD0E1 1px solid; FONT-WEIGHT: normal; FONT-SIZE: 9pt; BACKGROUND: #ffffff; BORDER-LEFT: #BBD0E1 1px solid; COLOR: #000000; LINE-HEIGHT: normal; BORDER-BOTTOM: #BBD0E1 1px solid; FONT-STYLE: normal;FONT-VARIANT: normal" 
                                TextMode="MultiLine" ReadOnly="True"></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2" rowspan="2">
                            审批意见</th>
                        <th  bgcolor="#ffffff" class="style1">
                            院审批意见：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox21" Height="50px" runat="server" Width="140px" 
                                style="BORDER-RIGHT: #BBD0E1 1px solid; BORDER-TOP: #BBD0E1 1px solid; FONT-WEIGHT: normal; FONT-SIZE: 9pt; BACKGROUND: #ffffff; BORDER-LEFT: #BBD0E1 1px solid; COLOR: #000000; LINE-HEIGHT: normal; BORDER-BOTTOM: #BBD0E1 1px solid; FONT-STYLE: normal;FONT-VARIANT: normal" 
                                TextMode="MultiLine" ReadOnly="True"></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            校审批意见：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox23" Height="50px" runat="server" Width="140px" 
                                style="BORDER-RIGHT: #BBD0E1 1px solid; BORDER-TOP: #BBD0E1 1px solid; FONT-WEIGHT: normal; FONT-SIZE: 9pt; BACKGROUND: #ffffff; BORDER-LEFT: #BBD0E1 1px solid; COLOR: #000000; LINE-HEIGHT: normal; BORDER-BOTTOM: #BBD0E1 1px solid; FONT-STYLE: normal;FONT-VARIANT: normal" 
                                TextMode="MultiLine"></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="3" style=" height:25px">
                        <asp:Button ID="Button4" runat="server" Text="同意" CssClass="submit" 
                        style=" cursor:pointer; height: 18px;" onclick="Button4_Click" 
                        OnClientClick="return confirm('确定同意吗? ')"/>
                    <asp:Button ID="Button5" runat="server" Text="不同意" CssClass="submit" 
                        style=" cursor:pointer" onclick="Button5_Click" 
                        OnClientClick="return confirm('确定不同意吗? ')"/>
                            <asp:Label ID="SaveID" runat="server" Text="Label" Visible="False"></asp:Label>
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

