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

     xlsheet.Cells(2,1).Value="学院";
     xlsheet.Cells(2,2).Value="课程编号";

     var oTable=document.all['fors:data'];
     var rowNum=oTable.rows.length;
     for(i=2;i<=rowNum;i++){
     for (j=1;j<=2;j++){
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