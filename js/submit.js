/*==============================
���ߣ�������
���ܣ���Ҫ���ǰ̨������֤
����ʱ�䣺2002.10
�޸����ڣ�2004.10
����˵����frm:Ϊ�����Ķ���,str:Ϊ��ʾ���,len:Ϊ��ĳ���
�޸��ߣ�
�޸����ڣ�
˵����������Ϊ���ɴ��룬�û���������ʹ�ã����뱣�������ļ�ͷ��˵��
��������ʹ�ù����������ʲô�����뼰ʱ��������ϵ��
��������ϵ��ʽ�������ʼ���405121224@qq.com QQ:405121224
================================*/
//��������Ƿ�Ϊ��
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
//��������Ƿ�ȫΪ����
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
//��������Ƿ�ȫΪ����
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


//�ж��Ƿ���Ǯ����ʽ
function isMoney(obj,errMsg){
 strRef = "1234567890.";
 for (i=0;i<obj.value.length;i++) {
  tempChar= obj.value.substring(i,i+1);
  if (strRef.indexOf(tempChar,0)==-1) {
   if (errMsg == null || errMsg =="")
    alert("���ݲ�����Ҫ��,���� \n��ʽΪ1234.56(����С�������������λ)!");
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
      alert("���ݲ�����Ҫ��,���� \n��ʽΪ1234.56(����С�������������λ)!");
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
//��������Ƿ�ȫΪ������
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

//���绰����

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
 //����ֻ�����

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



//����ʱ�

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
//�ļ������,mode�ĸ�ʽӦΪ��html|htm|word|xls��

function checfilename(frm,str,mode)
{
  if(frm.value!="")
	{
	  re="/^"+mode+"$/"
	  var checstr=frm.value;
	  var checarray=checstr.split(".");//�����ļ�����ȡ�ļ���׺��
	  var checmode=mode.split("|");//ȡ�������
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
//����ַ�������

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
//��������Ƿ�һ��

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
//����Ƿ�Ϊ����
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
//ֻ��Ϊ���ֺ�Ӣ��
function checkCustomer(frm,str){
	var strSource ="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	var ch;
	var i;
	var temp;
	if(frm.value.length<6 ||frm.value.length>30){//�ַ�������6----30֮��		
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

//���Զ���֤����(text,password,select)
function checknull(form)
{
	var INPUT;//�������ڷ��ʵı�Ԫ��
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
			if (INPUT.check_null && !checnull(INPUT,INPUT.check_null)) return false;//�Ƿ�Ϊ��
			if (INPUT.check_number && !checnumber(INPUT,INPUT.check_number)) return false;//�Ƿ�Ϊ����
			if (INPUT.check_int && !checint(INPUT,INPUT.check_int)) return false;//�Ƿ�Ϊ����
			if (INPUT.check_plusint && !checplusint(INPUT,INPUT.check_plusint)) return false;//�Ƿ�Ϊ������
			if (INPUT.check_telphone && !chectelphone(INPUT,INPUT.check_telphone)) return false;//�Ƿ�Ϊ�绰����
			if (INPUT.check_mobilephone && !checmobilephone(INPUT,INPUT.check_mobilephone)) return false;//�Ƿ�Ϊ�ֻ�����
			if (INPUT.check_email && !checemail(INPUT,INPUT.check_email)) return false;//�Ƿ�Ϊ�����ʼ�
			if (INPUT.check_Customer && !checkCustomer(INPUT,INPUT.check_Customer)) return false;//ֻ��Ϊ���ֺ�Ӣ��
			//if (INPUT.check_idcard && !checidcard(INPUT,INPUT.check_idcard)) return false;//�Ƿ�Ϊ���֤
			if (INPUT.check_post && !checpost(INPUT,INPUT.check_post)) return false;//�Ƿ�Ϊ�ʱ�
			if (INPUT.check_filename && INPUT.sufname && !checfilename(INPUT,INPUT.check_filename,INPUT.sufname)) return false;//�ļ������
			if (INPUT.check_len && !checlen(INPUT,INPUT.check_len,INPUT.len)) return false;//����ַ�������
			if (INPUT.check_pass && INPUT.pass && !eval('checpass(INPUT,INPUT.check_pass,INPUT.form.'+INPUT.pass+')')) return false;//��������Ƿ�һ��
			if (INPUT.check_date && !checdate(INPUT,INPUT.check_date)) return false;//�Ƿ�Ϊ����
			if (INPUT.check_money && !isMoney(INPUT,INPUT.check_money)) return false;//�ж��Ƿ���Ǯ����ʽ
		}
		
	}
	return true;
}