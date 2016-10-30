/*==============================
作者：喻涛林
功能：主要完成前台数据验证
开发时间：2002.10
修改日期：2004.10
参数说明：frm:为待检查的对象,str:为提示语句,len:为求的长度
修改者：
修改日期：
说明：本代码为自由代码，用户可以自由使用，但请保留作者文件头部说明
　　　在使用过程中如出现什么问题请及时与作者联系。
　　　联系方式：电子邮件：405121224@qq.com QQ:405121224
================================*/
//检查数据是否为空
function checnull(frm,str)
{
  re=/^\s{0,}$/
  if(re.test(frm.value))
	{
       alert(str)
	  frm.focus()
	  return false
	}else
	{
      return true
	}
}
//检查数据是否全为数字
function checnumber(frm,str)
{
  if(isNaN(frm.value) && frm.value!="")
	  {
         alert(str);
		 frm.select();
		 return false;
	  }else
	  {
         return true;
	  }
}
//检查数据是否全为整数
function checint(frm,str)
{
  re = /^-{0,1}\d+$/
  if(!re.test(frm.value) && frm.value!="")
	  {
         alert(str);
		 frm.select();
		 return false;
	  }else
	  {
         return true;
	  }
}


//判断是否是钱的形式
function isMoney(obj,errMsg){
 strRef = "1234567890.";
 for (i=0;i<obj.value.length;i++) {
  tempChar= obj.value.substring(i,i+1);
  if (strRef.indexOf(tempChar,0)==-1) {
   if (errMsg == null || errMsg =="")
    alert("数据不符合要求,请检查 \n格式为1234.56(如有小数，最多输入两位)!");
   else
    alert(errMsg);   
   if(obj.type=="text") 
    obj.focus(); 
   return false; 
  }else{
   tempLen=obj.value.indexOf(".");
   if(tempLen!=-1){
    strLen=obj.value.substring(tempLen+1,obj.value.length);
    if(strLen.length>2){
     if (errMsg == null || errMsg =="")
      alert("数据不符合要求,请检查 \n格式为1234.56(如有小数，最多输入两位)!");
     else
      alert(errMsg);   
     if(obj.type=="text") 
     obj.focus(); 
     return false; 
    }
   }
  }
 }
 return true;
}
//检查数据是否全为正整数
function checplusint(frm,str)
{
  re = /^\d+$/
  if(!re.test(frm.value) && frm.value!="")
	  {
         alert(str);
		 frm.select();
		 return false;
	  }else
	  {
         return true;
	  }
}

//检查电话号码

function chectelphone(frm,str)
{
  re = /^\d{3}-{0,1}\d{7,8}$/
  if(!re.test(frm.value) && frm.value!="")
	  {
         alert(str);
		 frm.select();
		 return false;
	  }else
	  {
         return true;
	  }
}
 //检查手机号码

function checmobilephone(frm,str)
{
  re=/^0{0,1}1[0-9]{10}$/
  re1=/^106\d{11,12}$/
  if(!(re.test(frm.value)||re1.test(frm.value)) && frm.value!="")
	{
      alert(str)
	  frm.select()
	  return false;
	}else
	{
      return true;
	}
}


function checemail(frm,str)
{
   re=/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/
   if(!re.test(frm.value) && frm.value!="")
	{
      alert(str)
      frm.select()
	  return false
	}else
	{
      return true
	}
}



//检查邮编

function checpost(frm,str)
{
  re=/^\d{6}$/
  if(!re.test(frm.value))
	{
      alert(str)
	  frm.select()
	  return false
  	}else
    {
      return true
	}
}
//文件名检查,mode的格式应为：html|htm|word|xls等

function checfilename(frm,str,mode)
{
  if(frm.value!="")
	{
	  re="/^"+mode+"$/"
	  var checstr=frm.value;
	  var checarray=checstr.split(".");//分离文件名，取文件后缀名
	  var checmode=mode.split("|");//取检验对象
	  var checbool=false;
	  for(var i=0;i<checmode.length;i++)
		{
            if(checarray[checarray.length-1].toUpperCase()==checmode[i].toUpperCase())
			{
              checbool=true
			}
		}
	  if(checbool==false)
		{
           alert(str);
		   frm.select();
		   return false
		}
	  else
	    {
           return true;
		}
	}
  else
	{
      return true
	}
}
//检查字符串长度

function checlen(frm,str,len)
{
  if(frm.value!=""&&frm.value.length>len)
  {
      alert(str)
      frm.select()
      return false
  }else
  {
      return true
  }
}
//检查密码是否一致

function checpass(frm,str,frm1)
{
  if(frm.value!=frm1.value)
  {
      alert(str)
      //frm.value=frm1.value=""
      frm1.select()
      return false
  }else
  {
      return true
  }
}
//检查是否为日期
function checdate(frm,str)
{
    if(frm.value=="") return true
    var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
    result = frm.value.match(reg);
    if(result == null)
    {
      alert(str);
      return false
    }
    var d = new Date(result[1],result[3]-1,result[4])

    if(result[1]*1+result[2]+result[3]*1+result[2]+result[4]*1!=d.getFullYear()+result[2]+(d.getMonth()+1)+result[2]+d.getDate())
    {
      alert(str);
      return false
    }
    return true
}
//只能为数字和英文
function checkCustomer(frm,str){
	var strSource ="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	var ch;
	var i;
	var temp;
	if(frm.value.length<6 ||frm.value.length>30){//字符长度在6----30之间		
		alert(str)
		frm.select()
		return false;
	}
	for (i=0;i<=(frm.value.length-1);i++)
	{

		ch = frm.value.charAt(i);
		temp = strSource.indexOf(ch);
		if (temp==-1) 
		{
		  alert(str)
		  frm.select()
		  return false;
		}
	}
	if (strSource.indexOf(ch)==-1)
	{
      alert(str)
	  frm.select()
	  return false
	}
	else
	{
	  return true;
	} 
}

//表单自动验证函数(text,password,select)
function checknull(form)
{
	var INPUT;//保存现在访问的表单元件
	var check_i=0;
	for(check_i=0;check_i<form.elements.length;check_i++)
	{
		INPUT=form.elements[check_i];
		if(INPUT.type!="submit" &&
		   INPUT.type!="button" &&
		   INPUT.type!="reset" &&
		   INPUT.type!="hidden" &&
		   INPUT.type!="radio" &&
		   INPUT.type!="checkbox")
		{
			if (INPUT.check_null && !checnull(INPUT,INPUT.check_null)) return false;//是否为空
			if (INPUT.check_number && !checnumber(INPUT,INPUT.check_number)) return false;//是否为数字
			if (INPUT.check_int && !checint(INPUT,INPUT.check_int)) return false;//是否为整数
			if (INPUT.check_plusint && !checplusint(INPUT,INPUT.check_plusint)) return false;//是否为正整数
			if (INPUT.check_telphone && !chectelphone(INPUT,INPUT.check_telphone)) return false;//是否为电话号码
			if (INPUT.check_mobilephone && !checmobilephone(INPUT,INPUT.check_mobilephone)) return false;//是否为手机号码
			if (INPUT.check_email && !checemail(INPUT,INPUT.check_email)) return false;//是否为电子邮件
			if (INPUT.check_Customer && !checkCustomer(INPUT,INPUT.check_Customer)) return false;//只能为数字和英文
			//if (INPUT.check_idcard && !checidcard(INPUT,INPUT.check_idcard)) return false;//是否为身份证
			if (INPUT.check_post && !checpost(INPUT,INPUT.check_post)) return false;//是否为邮编
			if (INPUT.check_filename && INPUT.sufname && !checfilename(INPUT,INPUT.check_filename,INPUT.sufname)) return false;//文件名检查
			if (INPUT.check_len && !checlen(INPUT,INPUT.check_len,INPUT.len)) return false;//检查字符串长度
			if (INPUT.check_pass && INPUT.pass && !eval('checpass(INPUT,INPUT.check_pass,INPUT.form.'+INPUT.pass+')')) return false;//检查密码是否一致
			if (INPUT.check_date && !checdate(INPUT,INPUT.check_date)) return false;//是否为日期
			if (INPUT.check_money && !isMoney(INPUT,INPUT.check_money)) return false;//判断是否是钱的形式
		}
		
	}
	return true;
}