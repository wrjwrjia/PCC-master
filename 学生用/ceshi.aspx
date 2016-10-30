<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshi.aspx.cs" Inherits="ceshi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="default.aspx?sid=<%=Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("07010101")).Replace("+", "%2B")%>&cid=<%=Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("01503020")).Replace("+", "%2B")%>">ceshi</a>
        <a href="direct.aspx?sid=07010101">ceshi2</a>
        <a href="direct.aspx?n=<%=Server.UrlEncode("C++程序设计教程(第2版）")%>">ceshi3</a>
    </div>
    </form>
</body>
</html>
