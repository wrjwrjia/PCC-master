<%@ Page Language="C#" AutoEventWireup="true" CodeFile="查询学生.aspx.cs" Inherits="库存管理_查询学生" %>
<%@  Register Src="../usercontrol/WebPagerByID.ascx" TagName="WebPager1" TagPrefix="uc1"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查询教师</title>
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
        .RadPicker{vertical-align:middle}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle;width:160px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2012.3.1016.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center}
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
                    库存管理&gt;&gt;查询学生</td>
                 <td align="right"></td>
            </tr>
      </table>
      <div style="margin-bottom:10px;">
      <table cellpadding="1" cellspacing="1" class="tabGg" width="99%">
            <tr>
                <th bgcolor="#FFFFFF" colspan="4" style=" background:#ECF9FC; height:25px">请输入查询条件</th>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">学生编号： </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbStuId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">ISBN： </th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TbBookId" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="style1">班级编号：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg" >
                            <asp:TextBox ID="TbClassName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
                <th   bgcolor="#FFFFFF" class="style1">教材名称：</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">
                            <asp:TextBox ID="TbBookName" CssClass="input1" runat="server" Width="140px" ></asp:TextBox> 
                    </td>
            </tr>
            <tr>
                <th bgcolor="#ffffff"  colspan="4" style=" height:25px">
                        <asp:Button ID="Button1" runat="server" Text="查询" CssClass="submit" onclick="Button1_Click" style="cursor: pointer;" Height="17px"/>
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
                             <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                 AutoGenerateColumns="False" CssClass="gridview" DataKeyNames="id" 
                                 EmptyDataText="没有符合条件的信息" OnRowDataBound="GvDataType_RowDataBound" 
                                 RowStyle-HorizontalAlign="center" Width="100%" >
                                 <RowStyle Height="23px" />
                                 <Columns>
                                     <asp:BoundField HeaderText="NO." />
                                     <asp:BoundField DataField="StuID" HeaderText="学号" />
                                     <asp:BoundField DataField="StuName" HeaderText="学生姓名" />
                                     <asp:BoundField DataField="ClassName" HeaderText="班号" />
                                     <asp:BoundField DataField="BookID" HeaderText="ISBN" />
                                     <asp:BoundField DataField="BookName" HeaderText="教材名" />
                                     <asp:BoundField DataField="Author" HeaderText="主编" />
                                     <asp:BoundField DataField="Publish" HeaderText="出版社" />
                                     <asp:BoundField DataField="get_id" HeaderText="状态" />
                                 </Columns>
                                 <RowStyle HorizontalAlign="Center" />
                                 <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                             </asp:GridView>
                             <uc1:WebPager1 ID="WebPager1" runat="server" DataId="GridView1" PageSize="15" />
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
 <script language="javascript" type="text/javascript">
     function preview() {
         bdhtml = window.document.body.innerHTML;
         sprnstr = "<!--startprint-->";
         eprnstr = "<!--endprint-->";
         prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
         prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
         // window.document.body.innerHTML = prnhtml;
         window.print();
     }
</script>

