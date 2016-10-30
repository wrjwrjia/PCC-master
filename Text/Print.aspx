﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Text_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>

<head>
<script language="javascript" type="text/javascript">


</script>
<script language="javascript" type="text/javascript">
function MakeExcel(){
var i,j;
    try {
      var xls    = new ActiveXObject ( "Excel.Application" );
     }
    catch(e) {
         alert( "要打印该表，您必须安装Excel电子表格软件，同时浏览器须使用“ActiveX 控件”，您的浏览器须允许执行控件。 请点击【帮助】了解浏览器设置方法！");
              return "";
     }

            xls.visible =true;  //设置excel为可见

    var xlBook = xls.Workbooks.Add;
    var xlsheet = xlBook.Worksheets(1);
    <!--合并-->
      xlsheet.Range(xlsheet.Cells(1,1),xlsheet.Cells(1,7)).mergecells=true;
      xlsheet.Range(xlsheet.Cells(1,1),xlsheet.Cells(1,7)).value="发卡记录";
     //  xlsheet.Range(xlsheet.Cells(1,1),xlsheet.Cells(1,6)).Interior.ColorIndex=5;//设置底色为蓝色 
                //   xlsheet.Range(xlsheet.Cells(1,1),xlsheet.Cells(1,6)).Font.ColorIndex=4;//设置字体色         
   // xlsheet.Rows(1). Interior .ColorIndex = 5 ;//设置底色为蓝色  设置背景色 Rows(1).Font.ColorIndex=4  

    <!--设置行高-->
    xlsheet.Rows(1).RowHeight = 25;
    <!--设置字体 ws.Range(ws.Cells(i0+1,j0), ws.Cells(i0+1,j1)).Font.Size = 13 -->
    xlsheet.Rows(1).Font.Size=14;
    <!--设置字体 设置选定区的字体  xlsheet.Range(xlsheet.Cells(i0,j0), ws.Cells(i0,j0)).Font.Name = "黑体" -->
    xlsheet.Rows(1).Font.Name="黑体";
    <!--设置列宽 xlsheet.Columns(2)=14;-->

    xlsheet.Columns("A:D").ColumnWidth =18;
     <!--设置显示字符而不是数字-->
    xlsheet.Columns(2).NumberFormatLocal="@";
    xlsheet.Columns(7).NumberFormatLocal="@";


     //设置单元格内容自动换行 range.WrapText  =  true  ;
     //设置单元格内容水平对齐方式 range.HorizontalAlignment  =  Excel.XlHAlign.xlHAlignCenter;//设置单元格内容竖直堆砌方式
      //range.VerticalAlignment=Excel.XlVAlign.xlVAlignCenter
    //range.WrapText  =  true;  xlsheet.Rows(3).WrapText=true  自动换行
   
    //设置标题栏

     xlsheet.Cells(2,1).Value="卡号";
     xlsheet.Cells(2,2).Value="密码";
     xlsheet.Cells(2,3).Value="计费方式";
     xlsheet.Cells(2,4).Value="有效天数";
     xlsheet.Cells(2,5).Value="金额";
     xlsheet.Cells(2,6).Value="所属服务项目";
       xlsheet.Cells(2,7).Value="发卡时间";

     var oTable=document.all['fors:data'];
     var rowNum=oTable.rows.length;
     for(i=2;i<=rowNum;i++){
     for (j=1;j<=7;j++){
//html table类容写到excel

       xlsheet.Cells(i+1,j).Value=oTable.rows(i-1).cells(j-1).innerHTML;
            }


    }
    <!--   xlsheet.Range(xls.Cells(i+4,2),xls.Cells(rowNum,4)).Merge; -->
    // xlsheet.Range(xlsheet.Cells(i, 4), xlsheet.Cells(i-1, 6)).BorderAround , 4
     // for(mn=1,mn<=6;mn++) .     xlsheet.Range(xlsheet.Cells(1, mn), xlsheet.Cells(i1, j)).Columns.AutoFit;
      xlsheet.Columns.AutoFit;
                 xlsheet.Range( xlsheet.Cells(1,1),xlsheet.Cells(rowNum+1,7)).HorizontalAlignment =-4108;//居中
                   xlsheet.Range( xlsheet.Cells(1,1),xlsheet.Cells(1,7)).VerticalAlignment =-4108;
                 xlsheet.Range( xlsheet.Cells(2,1),xlsheet.Cells(rowNum+1,7)).Font.Size=10;

      xlsheet.Range( xlsheet.Cells(2,1),xlsheet.Cells(rowNum+1,7)).Borders(3).Weight = 2; //设置左边距
       xlsheet.Range( xlsheet.Cells(2,1),xlsheet.Cells(rowNum+1,7)).Borders(4).Weight = 2;//设置右边距
             xlsheet.Range( xlsheet.Cells(2,1),xlsheet.Cells(rowNum+1,7)).Borders(1).Weight = 2;//设置顶边距
       xlsheet.Range( xlsheet.Cells(2,1),xlsheet.Cells(rowNum+1,7)).Borders(2).Weight = 2;//设置底边距

 

       
        xls.UserControl = true;  //很重要,不能省略,不然会出问题 意思是excel交由用户控制
       xls=null;
       xlBook=null;
       xlsheet=null;

}

 


</script>  <link href="css/styles3.css" rel="stylesheet" type="text/css"/>
<title>ziyuanweihu</title>
</head>
<body>
  <form id="fors" method="post" action="/WebModule/admins/card/showcard.faces" enctype="application/x-www-form-urlencoded">

    
      
      
      
    <table id="fors:top" border="0" cellpadding="0" cellspacing="0" width="100%">
<tbody>
<tr>
<td class="left"><img src="images/jiao1.gif" alt="" /></td>
<td class="topMiddle"></td>
<td class="right"><img src="images/jiao2.gif" alt="" /></td>
</tr>
</tbody>
</table>            
            
  
      
      
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
<tbody>
<tr>
<td class="middleLeft"></td>
<td class="btstyle"><table id="fors:sort" border="0" cellpadding="0" cellspacing="0" style="valign:center" width="100%">
<tbody>
<tr>
<td class="btstyle"><input type="button" name="fors:_id7" value="&#29983;&#25104;excel&#25991;&#20214;" onclick="MakeExcel()" /><input type="submit" name="fors:_id8" value="&#36820;&#22238;" /></td>
</tr>
</tbody>
</table>
<table id="fors:data" border="1" cellpadding="0" cellspacing="1" width="100%">
<thead>
<tr>
<th scope="col"><span id="fors:data:headerText1">&#21345;&#21495;</span></th>
<th scope="col"><span id="fors:data:headerText2">&#23494;&#30721;</span></th>
<th scope="col"><span id="fors:data:headerText3">&#35745;&#36153;&#26041;&#24335;</span></th>
<th scope="col"><span id="fors:data:headerText4">&#26377;&#25928;&#22825;&#25968;</span></th>
<th scope="col">&#37329;&#39069;</th>
<th scope="col"><span id="fors:data:headerText6">&#25152;&#23646;&#26381;&#21153;&#39033;&#30446;</span></th>
<th scope="col"><span id="fors:data:headerText7">&#21457;&#21345;&#26102;&#38388;</span></th>
</tr>
</thead>
<tbody>
<tr>
<td>h000010010</td>
<td>543860</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
<tr>
<td>h000010011</td>
<td>683352</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
<tr>
<td>h000010012</td>
<td>433215</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
<tr>
<td>h000010013</td>
<td>393899</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
<tr>
<td>h000010014</td>
<td>031736</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
<tr>
<td>h000010015</td>
<td>188600</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
<tr>
<td>h000010016</td>
<td>363407</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
<tr>
<td>h000010017</td>
<td>175315</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
<tr>
<td>h000010018</td>
<td>354437</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
<tr>
<td>h000010019</td>
<td>234750</td>
<td>&#35745;&#28857;</td>
<td></td>
<td>2.0</td>
<td>&#27979;&#35797;&#39033;&#30446;</td>
<td>2006-06-23 10:14:40.843</td>
</tr>
</tbody>
</table>
</td>
<td class="middleRight"></td>
</tr>
</tbody>
</table>

    <table id="fors:bottom" border="0" cellpadding="0" cellspacing="0" width="100%">
      <tbody>
        <tr>
          <td class="left">
            <img src="images/jiao3.gif" alt=""/>
          </td>
          <td class="bottomMiddle">          </td>
          <td class="right">
            <img src="images/jiao4.gif" alt=""/>
          </td>
        </tr>
      </tbody>
    </table>
 <input type="hidden" name="fors" value="fors" /></form>
</body>

</html>


