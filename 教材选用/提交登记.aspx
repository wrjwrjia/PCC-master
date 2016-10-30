<%@ Page Language="C#" AutoEventWireup="true" CodeFile="提交登记.aspx.cs" Inherits="教材选用_提交登记" %>
<%@ Register Src="../usercontrol/WebPagerByIDForSearch.ascx" TagName="WebPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>提交登记</title>
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
                <td width="2%" height="22" >
                    <img src="../images/module.gif"  height="22"/></td>
                <td width="55%" >
                    教材选用&gt;&gt;提交登记</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="4" style=" background:#ECF9FC; height:25px">请输入查询条件</th>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">课程编号：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">课程名称：
                        </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox2" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th   bgcolor="#FFFFFF" class="style1">ISBN：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                <th   bgcolor="#FFFFFF" class="style1">教材名称：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox4" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="4" style=" height:25px">
                        <asp:Button ID="Button1" runat="server" Text="查询" CssClass="submit" onclick="Button1_Click" style="cursor: pointer;"/>
                </th>
            </tr>
        </table>
                </div>
                <div style="width:99%;float:left;">                            
                    <div style="width:45%; height:600px;float:left;">
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
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="course_id" HeaderText="课程编号"/>
             <asp:BoundField DataField="course_name" HeaderText="课程名称"/>
             <asp:BoundField DataField="book_id" HeaderText="教材编号"/>
             <asp:BoundField DataField="book_name" HeaderText="教材名称"/>
             <asp:BoundField DataField="state_id" HeaderText="状态"/>
             <asp:BoundField DataField="college_advise" HeaderText="院意见" Visible="false"/>
             <asp:BoundField DataField="university_advise" HeaderText="校意见" Visible="false"/>
             <asp:CommandField HeaderText="详细" ShowDeleteButton="True" 
                     DeleteText="详细" />
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
        <uc1:WebPager ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />

                          </th>
                </tr>
                        </table>
                    </div>
                    <div style="width:52%; height:550px;float:right; ">
                    <table width="100%" cellpadding="1" cellspacing="1" class="tabGg"">
                <tr>
                         <th bgcolor="#ffffff" colspan="5" style=" background:#ECF9FC; height:25px">
                            基本信息
                            </th>
                </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2" rowspan="3">
                           个人信息</th>
                        <th  bgcolor="#ffffff" class="style1">
                            学院：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox5" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">系别：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox6" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教师工号：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox7" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">教师姓名：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox8" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            联系方式：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox9" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">&nbsp;</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            &nbsp;</td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2" rowspan="10">
                            教材信息</th>
                        <th  bgcolor="#ffffff" class="style1">
                            课程编号：
                        </th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox10" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">课程名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox11" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            ISBN：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox12" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">教材名称：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox13" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            教材类别：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                        RepeatDirection="Horizontal" >
                        <asp:ListItem>出版教材</asp:ListItem>
                        <asp:ListItem>自编讲义</asp:ListItem>
                        <asp:ListItem>翻印教材</asp:ListItem>
                    </asp:RadioButtonList>
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1" rowspan="3">
                            教材类型：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="国家级获奖" />
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="国家级立项/规划" />
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="省（部）级获奖" />
                            </td>
                        </tr>
                        <tr>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                        <asp:CheckBox ID="CheckBox4" runat="server" Text="省（部）级立项/规划" />
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="校级出版" />
                        <asp:CheckBox ID="CheckBox6" runat="server" Text="校级讲义" />

                            </td>
                        </tr>
                        <tr>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                        <asp:CheckBox ID="CheckBox7" runat="server" Text="21世纪" />
                        <asp:CheckBox ID="CheckBox8" runat="server" Text="教学指导委员会推荐" />
                        <asp:CheckBox ID="CheckBox9" runat="server" Text="统编" />
                        <asp:CheckBox ID="CheckBox10" runat="server" Text="其他" />
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            主编：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox14" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        <th  bgcolor="#ffffff" class="style1">定价：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox15" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版社：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="145px"  
                                Height="22px">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        <th  bgcolor="#ffffff" class="style1">版次：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg">
                            <asp:TextBox ID="TextBox16" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            出版年月：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                            <asp:TextBox ID="TextBox17" CssClass="input1" runat="server" Width="140px" 
                                Height="16px" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            备注：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                            <asp:TextBox ID="TextBox19" CssClass="input1" runat="server" Width="300px" 
                                Height="16px" ></asp:TextBox> 
                        </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style2" rowspan="2">
                            审批意见</th>
                        <th  bgcolor="#ffffff" class="style1">
                            院审批意见：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                            <asp:TextBox ID="TextBox21" Height="50px" runat="server" Width="300px" 
                                style="BORDER-RIGHT: #BBD0E1 1px solid; BORDER-TOP: #BBD0E1 1px solid; FONT-WEIGHT: normal; FONT-SIZE: 9pt; BACKGROUND: #ffffff; BORDER-LEFT: #BBD0E1 1px solid; COLOR: #000000; LINE-HEIGHT: normal; BORDER-BOTTOM: #BBD0E1 1px solid; FONT-STYLE: normal;FONT-VARIANT: normal" 
                                TextMode="MultiLine" ReadOnly="True"></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" class="style1">
                            校审批意见：</th>
                        <td align="left" bgcolor="#ffffff" class="right_bg" colspan="3">
                            <asp:TextBox ID="TextBox22" Height="50px" runat="server" Width="300px" 
                                style="BORDER-RIGHT: #BBD0E1 1px solid; BORDER-TOP: #BBD0E1 1px solid; FONT-WEIGHT: normal; FONT-SIZE: 9pt; BACKGROUND: #ffffff; BORDER-LEFT: #BBD0E1 1px solid; COLOR: #000000; LINE-HEIGHT: normal; BORDER-BOTTOM: #BBD0E1 1px solid; FONT-STYLE: normal;FONT-VARIANT: normal" 
                                TextMode="MultiLine" ReadOnly="True"></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                        <th  bgcolor="#ffffff" colspan="5" style=" height:25px">
                    <asp:Button ID="Button3" runat="server" Text="提交" CssClass="submit" 
                        style=" cursor:pointer" onclick="Button2_Click" 
                                OnClientClick="return confirm('确定提交吗? ')"/>
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

