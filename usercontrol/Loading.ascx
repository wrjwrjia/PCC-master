<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Loading.ascx.cs" Inherits="usercontrol_Loading" %>

<div id='doing' style='Z-INDEX: 12000; LEFT: 0px; WIDTH: 100%; CURSOR: wait; POSITION: absolute; TOP: 0px; HEIGHT: 100%'>
<table width='100%' height='100%' id="Table1">
<tr align='center' valign='middle'>
<td >
<table  id="Table2" class="loading">
<tr align='center' valign='middle'>
<td>Loading...</td>
</tr>
</table>
</td>
</tr>
</table>
</div>
<script language="javascript">
function ShowWaiting()
{
document.getElementById('doing').style.visibility = 'visible';
}
function CloseWaiting()
{
document.getElementById('doing').style.visibility = 'hidden';
}
function MyOnload()
{
document.getElementById('doing').style.visibility = 'hidden';
}
if (window.onload == null)
{
window.onload = MyOnload;
}
</script>







