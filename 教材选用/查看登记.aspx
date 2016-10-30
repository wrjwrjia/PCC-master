<%@ Page Language="C#" AutoEventWireup="true" CodeFile="查看登记.aspx.cs" Inherits="教材选用_查看所有登记" %>
<%@ Register Src="../usercontrol/WebPagerByIDForSearch.ascx" TagName="WebPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看登记</title>
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
<script type="text/javascript" src="../js/ExcelPrint.js" ></script>
<script language="javascript" type='text/javascript' src="../js/PopupCalendarDay.js"> </script>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0"  >
            <tr>
                <td width="2%" height="22" >
                    <img src="../images/module.gif"  height="22"/></td>
                <td width="55%" >
                    教材选用&gt;&gt;查看登记</td>
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
                            <asp:TextBox ID="TextBox4" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
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
                            <asp:TextBox ID="TextBox2" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">课程名称：
                        </th>
                <th align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox5" CssClass="input1" runat="server" Width="140px"></asp:TextBox> 
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
                            <asp:TextBox ID="TextBox3" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                            </td>
                <th   bgcolor="#FFFFFF" class="style1">教材名称：</th>
                <th align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TextBox1" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
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
             </div>
    </div>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="没有符合条件的信息"  OnRowDataBound="GvDataType_RowDataBound" 
            AllowSorting="True" CssClass="gridview" 
            Width="99%" RowStyle-HorizontalAlign="center" DataKeyNames="id">
            <RowStyle Height="23px" />
            <Columns>
             <asp:BoundField HeaderText="NO." />
             <asp:BoundField DataField="teacher_college" HeaderText="学院"/>
             <asp:BoundField DataField="course_id" HeaderText="课程编号"/>
             <asp:BoundField DataField="course_name" HeaderText="课程名称"/>
             <asp:BoundField DataField="book_id" HeaderText="教材编号"/>
             <asp:BoundField DataField="book_name" HeaderText="教材名称"/>
             <asp:BoundField DataField="book_catagory" HeaderText="类型"/>
             <asp:BoundField DataField="book_price" HeaderText="定价"/>
             <asp:BoundField DataField="press_name" HeaderText="出版社"/>
             <asp:BoundField DataField="press_time" HeaderText="版次"/>
             <asp:BoundField DataField="state_id" HeaderText="状态"/>
             <asp:BoundField DataField="college_advise" HeaderText="院意见"/>
             <asp:BoundField DataField="university_advise" HeaderText="校意见"/>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
        <uc1:WebPager ID="WebPager1" DataId="GridView1" PageSize="15"  runat="server" />
    </form>
    <input type="submit" name="print" value="打印" onclick="MakeExcel()" />
</body>  
  <script type="text/javascript">
        function setPermission(pid) {
            openwin("Purview.aspx?id=" + pid + "&t=2", 600, 600, 50);
            return false;
        }
    </script>
</html>

<script language="javascript"  type="text/javascript"><!--
    //senfe("表格名称","奇数行背景","偶数行背景","鼠标经过背景","点击后背景");
    senfe("GridView1", "#ECF9FC", "#FFFFFF", "#D6F1F8");
</script>

