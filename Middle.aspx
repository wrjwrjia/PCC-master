<html>
<head>
<title>middle</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<SCRIPT ID=clientEventHandlersJS LANGUAGE=javascript>
<!--
function Submit_onclick() {
	parent.myFrame.cols="210,23,*";
}
function click1(){
	Image2.style.display="";
	Image1.style.display="none";
}
function click2(){
	Image2.style.display="none";
	Image1.style.display="";
}

function Submit2_onclick() {
	parent.myFrame.cols="0,23,*";
}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
//-->
</SCRIPT>
</head>

<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0">
<table width="18" border="0" cellspacing="0" cellpadding="0" height="100%">
  <tr> 
    <td background="images/midbg.gif"><a href="javascript:Submit2_onclick()"><img src="images/left.gif" name="Image1" border="0" id="Image1" onClick="click1();" onMouseOver="MM_swapImage('Image1','','images/left.gif',1)" onMouseOut="MM_swapImgRestore()"></a><br> 
      <a href="javascript:Submit_onclick()"><img src="images/right.gif" name="Image2" border="0" id="Image2" style="display:none" onClick="click2();" onMouseOver="MM_swapImage('Image2','','images/right.gif',1)" onMouseOut="MM_swapImgRestore()"></a>    </td>
  </tr>
</table>
</body>
</html>