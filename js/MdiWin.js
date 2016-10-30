/*
	��    ��: �ര�ڹ���ʵ��
	����ʱ��: 2008-01-15	
*/

function mywin()
{
	this.winlist = new Array();   //�����б�
	this.maxWins = 5;				//��󴰿���
	this.tagTitleWidth = 167;		//��ǩ���
	this.indentWidth = 0;			//��ǩ�������
	this.currentwin = null;
	this.addwin = addwin;                        //�½����ڷ���
	this.removewin = removewin;                        //�Ƴ�����
	this.removeall = removeall;                        //�Ƴ����д���
	this.activewin = activewin;                        //�����
	this.container = container;
	this.padLeft = padLeft;					// ���������Ե�ľ���
	this.padRight = padRight;				// �������ұ�Ե�ľ���
	this.scrollWidth = scrollWidth;
        var number =0;
	function container(url,title)
	{
		for(var i=0;i<this.winlist.length;i++)
		{
			if(this.winlist[i].title == title && this.winlist[i].url == url)
			{
				return i;
			}
		}
		return -1;
	}
	function activewin(oEl)                 //�����
	{
		if(oEl == null){
			this.currentwin = null;
			return
		}
		var tempzindex = this.currentwin.style.zIndex;
		this.currentwin.wintitle.style.zIndex = this.currentwin.index;
		this.currentwin.style.display = "none";
		this.currentwin.wintitle.style.backgroundImage = 'url(images/tab1.jpg)';

		oEl.wintitle.style.zIndex = tempzindex;
		oEl.style.display = "";
		oEl.wintitle.style.backgroundImage = 'url(images/tab2.jpg)';
		this.currentwin = oEl;
                 var x = document.getElementsByName('del');
                     for(i=0;i<x.length;i++)
                        x[i].style.visibility = 'hidden';                                
                document.getElementById(oEl.num).style.visibility = 'visible';   
		//���������ʾ������
		var mleft = parseInt(titlelist.style.marginLeft);
		if (isNaN(mleft))
			mleft = 0;
		var padleft = this.padLeft(oEl);
		var padright = this.padRight(oEl);
		var clientwidth = titlelist.parentElement.clientWidth
		if(padleft + mleft > clientwidth)
		{
			titlelist.style.marginLeft = clientwidth - padleft;			
		}
		if(padright < clientwidth && mleft < 0)
		{
			mleft = clientwidth - this.scrollWidth();
			if(mleft>0)
				mleft = 0;
			titlelist.style.marginLeft = mleft;
		}
		if(padleft + mleft < this.tagTitleWidth)
		{
			titlelist.style.marginLeft = - (padleft - this.tagTitleWidth);
		}
	}

	function padLeft(oEl)
	{
		var padleft = oEl.index * this.tagTitleWidth - this.indentWidth*(oEl.index-1);
		return padleft;
	}

	function padRight(oEl)
	{
		var count = (this.winlist.length - oEl.index) + 1;
		var padright = this.tagTitleWidth * count - this.indentWidth*(count-1);
		return padright;
	}

	function addwin(url,title)                                        //�����ľ���ʵ��
	{
		var con = this.container(url,title);
		if(con>-1)
		{
			//this.activewin(this.winlist[con]);
			//return;
			this.removewin(this.winlist[con])
		}
		if(this.winlist.length >= this.maxWins)
		{
			this.removewin(this.winlist[1]);		//������󴰿������ƺ�,�ȹر����ȿ��Ĵ���.�������޸���:08-09-15
		//	alert("������󴰿������ƣ�"+this.maxWins+"�������ȹرղ��ִ���");
		//	return false;
		}
		number++;
		oDIV = window.document.createElement( "TABLE" );
		this.winlist[this.winlist.length] = oDIV;                //���б�����Ӵ������
		oDIV.url = url;
		oDIV.title = title;
		oDIV.index = this.winlist.length;
                oDIV.num = number; 
		oDIV.className = "win";
		//oDIV.width = "100%";
		oDIV.height = "100%";
		oDIV.cellSpacing=0;
		oDIV.insertRow().insertCell().innerHTML = "<iframe src='"+url+"' scrolling='auto' name='tab_win"+oDIV.index+"' class = 'win1' width='90%' height='100%' frameborder='0'></iframe>";

		var oTitle = window.document.createElement( "SPAN" );
		oTitle.className ='wintitle';
		oTitle.style.cursor = 'pointer';
		oTitle.style.valign = 'middle';
		oTitle.style.width = this.tagTitleWidth;
		oTitle.style.backgroundImage = 'url(images/tab2.jpg)';
		oTitle.style.left = this.winlist.length == 1 ? 0 : this.winlist[this.winlist.length-2].wintitle.style.pixelLeft - this.indentWidth;
		oTitle.title = title;
		title = subStr(title,14);
		var str_k="&nbsp;";
		if(title.length==6) str_k="&nbsp;";
		if(title.length<5) str_k="&nbsp;&nbsp;&nbsp;"
		oTitle.innerHTML= title == null ? "δ֪" : "<table width='100%' align='right' ><tr><td>"+str_k+title+"</td><td><img name='del'  id ='"+number+"' src='images/del.gif' style='cursor:hand' width='15' height='15' alt='Close' onClick='win.removewin(win.currentwin)' align='right' onMouseOver=this.src='images/top_5.jpg' onMouseOut=this.src='images/del.gif'></td><td></td></tr></table>";//��ǩ��˫���رմ���
		
		oTitle.win=oDIV;
		oTitle.onclick = new Function("win.activewin(this.win)")

		if(this.currentwin != null) {
			this.currentwin.wintitle.style.backgroundImage = 'url(images/tab1.jpg)';
			this.currentwin.style.display = "none";
			this.currentwin.wintitle.style.zIndex = this.currentwin.index;
                        var x = document.getElementsByName('del');
                     for(i=0;i<x.length;i++)
                        x[i].style.visibility = 'hidden';                                              
		}
		oDIV.style.zIndex = this.maxWins+1;
		oTitle.style.zIndex = this.maxWins+1;
		oDIV.wintitle = oTitle;
		titlelist.insertAdjacentElement( "beforeEnd" , oTitle );

		var scrollwidth = this.scrollWidth();
		if(scrollwidth > titlelist.parentElement.clientWidth)
		{
			titlelist.style.marginLeft = titlelist.parentElement.clientWidth - scrollwidth;
		}
		mywindows.insertAdjacentElement( "beforeEnd" , oDIV );

		this.currentwin = oDIV;
		return oDIV;
	}

	function scrollWidth()
	{
		var n = this.winlist.length;
		var scrollwidth = this.tagTitleWidth*n - this.indentWidth*(n-1);
		return scrollwidth;
	}

	function removewin(obj)        //�Ƴ�����
	{
		if(obj == null)return;
		var temparr = new Array();
		var afterwin = false;
		for(var i=0;i<this.winlist.length;i++)
		{
			if(afterwin) this.winlist[i].wintitle.style.left =  this.winlist[i].wintitle.style.pixelLeft + this.indentWidth;
			if(this.winlist[i] != obj)
				temparr[temparr.length] = this.winlist[i];
			else
				afterwin = true;
		}
		this.winlist = temparr;
		if(this.currentwin == obj){
			this.activewin(this.winlist[this.winlist.length-1]);
		}
		obj.wintitle.removeNode(true) ;
		obj.removeNode(true) ;
		obj = null;

	}
	function removeall()        //�Ƴ����д���
	{
		var wincount = this.winlist.length;
		for(var i=wincount-1;i>=0;i--)
			this.removewin(this.winlist[i]);
	}
}

function tabScroll(direction)
{
	tabScrollStop();
	direction == "right" ? tabMoveRight() : tabMoveLeft();
}

function tabMoveRight()
{
	tabMove("right",8);
	timer=setTimeout(tabMoveRight,10);
}

function tabMoveLeft()
{
	tabMove("left",8);
	timer=setTimeout(tabMoveLeft,10);
}

function tabScrollStop()
{
	clearTimeout(timer);
	timer = null;
}

function tabMove(direction,speed)
{
	var mleft = parseInt(titlelist.style.marginLeft);
	if (isNaN(mleft))
		mleft = 0;
	if(direction=="right")
	{
		if(titlelist.parentElement.clientWidth >= titlelist.parentElement.scrollWidth)
		{
			tabScrollStop();
			return;
		}
		else
		{
			titlelist.style.marginLeft = mleft - speed;
		}
	}
	else
	{
		if(mleft + speed >=0)
		{
			titlelist.style.marginLeft = 0;
			tabScrollStop();
			return;
		}
		else
		{
			titlelist.style.marginLeft = mleft + speed;
		}
	}
}
var timer = null;
var win = null;
var wins = new Array();

function init()
{
	win =  new mywin();                        //�½�����
}

function AddWin(Url,Title)
{
	wins[wins.length] = win.addwin(Url, Title);                        //��Ӵ��壻
}

function subStr(str,len)
{
	var strlength=0;
	var newstr = "";
	for (var i=0;i<str.length;i++)
	{
		if(str.charCodeAt(i)>=1000)
			strlength += 2;
		else
			strlength += 1;
		if(strlength > len)
		{
			newstr += "..";
			break;
		}
		else
		{
			newstr += str.substr(i,1);
		}
	}
	return newstr;
}
function getLocalRect(el, e) {
    e = e || event;
    var z = el.getBoundingClientRect(),d = document,x=e.clientX,y=e.clientY,
    v = /BackCompat/i.test(d.compatMode) ? d.body: d.documentElement,
    T=v.scrollTop,L=v.scrollLeft;
    return {left: x - z.left + L,top:y - z.top + T,right:-(x-z.right+L),bottom:-(y-z.bottom+T)}
}
function onclicklb(e){
    var r=getLocalRect(this,e);
    if(r.top<20 && r.right<20){
        win.removewin(win.currentwin);
     } 
}

