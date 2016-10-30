<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemInfo.aspx.cs" Inherits="SystemInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
<HEAD>
<TITLE></TITLE>
<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=UTF-8">
<link href="css/admincss.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
-->
</style>
<script type="text/javascript" src="js/loading.js" ></script>
</HEAD>
<BODY >
<table width="99%" height="62" border="0" align="center" cellpadding="3" cellspacing="1" bgcolor="#9AA29A" >
	<tr class="back">
		<td colspan="2" bgcolor="C6E7FB" class="systemTr">&gt;&gt;Welcome To ENC Capacity</td>
	</tr>
		<tr class="back">
		<td height="28" bgcolor="#FFFFFF"><strong>Hello,
            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>  </strong>
			Today is:
			<script language="JavaScript" type="text/JavaScript" >var day="";
                var month="";
                var ampm="";
                var ampmhour="";
                var myweekday="";
                var year="";
                mydate=new Date();
                myweekday=mydate.getDay();
                mymonth=mydate.getMonth()+1;
                myday= mydate.getDate();
                myyear= mydate.getYear();
                year=(myyear > 200) ? myyear : 1900 + myyear;
                if(myweekday == 0)
                weekday=" Sunday ";
                else if(myweekday == 1)
                weekday=" Monday ";
                else if(myweekday == 2)
                weekday=" Tuesday ";
                else if(myweekday == 3)
                weekday=" Wednesday ";
                else if(myweekday == 4)
                weekday=" Thursday ";
                else if(myweekday == 5)
                weekday=" Friday ";
                else if(myweekday == 6)
                weekday=" Saturday ";
                document.write(year+"-"+mymonth+"-"+myday+" "+weekday);
	    </script>		</td>
	    </tr>
    	
    </table>

</BODY>
</HTML>