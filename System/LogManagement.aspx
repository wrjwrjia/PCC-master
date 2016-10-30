<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogManagement.aspx.cs" Inherits="System_LogManagement" %>
<%@ Register Src="../usercontrol/WebPagerByID.ascx" TagName="WebPager" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>User Management</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/changerows.js"></script>
    <style type="text/css">
    .titlediv{float: left; padding: 5px 2px;width:100%;}
    .AddLnk{float:right;background:url(../images/add.gif) no-repeat 2px center;padding-left:20px;margin-right:5px;}
    .LeftTitle{font-size:14px;float:left;color:Green;}
        .style1
        {
            height: 25px;
            width: 467px;
        }
    </style>
</head>
<script language="javascript" type='text/javascript' src="../js/PopupCalendarDay.js"> </script>
<body style="margin-top:2px;">
    <form id="form1" runat="server"> 
        
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31" style="height: 25px">
                    <img src="../images/module.gif" width="20"  alt="" onclick="location.href=location.href" style="cursor:hand" alt="refresh"/></td>
                <td class="style1">
                    System >> History</td>
                <td width="80%" align="center">
                    User Name : <asp:TextBox
                        ID="txtUserName" runat="server" CssClass="input1"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Operation Time : 
                    <asp:TextBox ID="txtOperationTimeStart" runat="server" CssClass="input1" Width="108px"   onclick="getDateString(this,oCalendarChs)"></asp:TextBox>
                       -- 
                    <asp:TextBox ID="txtOperationTimeEnd" runat="server" CssClass="input1" Width="108px"   onclick="getDateString(this,oCalendarChs)"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="submit" OnClick="btnSearch_Click"  />
                    </td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td bgcolor="#9AA29A" height="5px">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="No Data"
            Width="99%" RowStyle-HorizontalAlign="center" OnRowDataBound="gvData_RowDataBound" >
            <RowStyle Height="23px" />
            <Columns>
                 <asp:BoundField HeaderText="NO.">
                  <ItemStyle Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>                            
                <asp:BoundField DataField="usr_name" HeaderText="User Name"/>
                <asp:BoundField DataField="opt" HeaderText="Operation" />           
                <asp:BoundField DataField="opt_date" HeaderText="Operation Time"/>
                <asp:BoundField DataField="detail" HeaderText="Details"/>              
              
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
        </asp:GridView>
        <table width="99%" border="0" cellspacing="0" cellpadding="0">
            <tr>
            <td>  <uc1:WebPager ID="WebPager1" DataId="GvData" PageSize="15"  runat="server" />     </td>
                <td align="right" style="height: 5px">
                    &nbsp;</td>
            </tr>
        </table>     
        <asp:HiddenField ID="hidDeptid" runat="server" />
        <asp:HiddenField ID="HidModuId" runat="server" />
    </form>
    <script type="text/javascript">
    function setPermission(pid)
    {
        openwin("Purview.aspx?id=" + pid + "&t=2",600,600,50);
        return false;
    }
    </script>
</body>
</html>
<script language="javascript"  type="text/javascript"><!--
//senfe("表格名称","奇数行背景","偶数行背景","鼠标经过背景","点击后背景");
 senfe("GvData","#ECF9FC","#FFFFFF","#D6F1F8");
--></script>
