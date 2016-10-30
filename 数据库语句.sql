-----------------------------------------------------------------------------------------------------------------------
------------------------------------------------系统表-----------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------
--用户表
--drop table tbl_usr
CREATE TABLE tbl_usr
(
	id numeric(18, 0) IDENTITY(1,1) NOT NULL,
	usr_login varchar(100) NULL,				--用户登录名
	usr_pwd varchar(100) NULL,					--登录密码
	dept_no varchar(100) NULL,					--部门,暂时不用
	role_id varchar(100) NULL,					--所属的角色id
	role_name varchar(100) NULL,				--所属的角色名
	last_log datetime NULL						--上次登录时间,暂时不用
);
--insert into tbl_usr(usr_login,usr_pwd,dept_no,role_id,role_name,last_log)values('sys','123','0','1','Administrator',getdate());
insert into tbl_usr(usr_login,usr_pwd,dept_no,role_id,role_name,last_log)values('encldap','123','0','1','Administrator',getdate());
insert into tbl_usr(usr_login,usr_pwd,dept_no,role_id,role_name,last_log)values('ELLGYYG','123','0','1','Administrator',getdate());


--角色表
--drop table TBL_ROLE
CREATE TABLE TBL_ROLE
(
	id numeric (18, 0) IDENTITY(1,1) NOT NULL,
	role_na varchar(100) NULL					--角色名
);
insert into tbl_role(role_na) values('Administrator');


--角色权限表,主要是控制菜单
--drop table Purview
CREATE TABLE Purview
(
	pid numeric(18, 0) IDENTITY(1,1) NOT NULL,
	userid numeric(18, 0) NULL,					--用户Id,暂时不用
	roleid numeric(18, 0) NULL,					--角色id
	permission varchar(1000) NULL				--角色具有的菜单，以"1,5,4,7,8"的形式存放
);
insert into Purview(roleid,permission) values('1','400,401,402,403');--默认给Administrator角色分配"系统管理"菜单全选

--角色权限表，主要是控制页面上的按钮
--drop table UserPurview
CREATE TABLE UserPurview
(
	id numeric(18, 0) IDENTITY(1,1) NOT NULL,
	UserId numeric(18, 0) NULL,     --暂时不用
	ModuleId numeric(18, 0) NULL,	--菜单id
	roleid numeric(18, 0) NULL,		--角色id
	IsSelected numeric(18, 0) NULL	--表示角色是否有某菜单的权限
);

--系统菜单表
--drop TABLE Menu
CREATE TABLE Menu
(
	menuid int NOT NULL primary key,--菜单id
	menu varchar(200) NULL,			--菜单名
	orderid numeric(18, 0) NULL,	--菜单排序用
	url varchar(300) NULL,			--菜单对应的跳转页面,主菜单为空,下级菜单不为空
	parentid int NULL,				--上级菜单id		
	menuNode int NULL,				--菜单的级别  0表示一级菜单，1表示二级或三级菜单
);
--DataMaintain菜单
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(100,'DataBase Maintain',100,NULL,0,0);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(101,'General Information',101,NULL,100,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(102,'Product Class',102,'DataMaintain/ProductClassMaintain.aspx',101,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(103,'Product Category',103,'DataMaintain/ProductCategoryMaintain.aspx',101,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(104,'Product Family',104,'DataMaintain/ProductFamilyMaintain.aspx',101,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(105,'Process Category',105,'DataMaintain/ProcessCategoryNumberdefineMaintain.aspx',101,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(106,'Process Name',106,'DataMaintain/ProcessNumberdefineMaintain.aspx',101,2);


insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(107,'Product Information',107,NULL,100,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(108,'Product',108,'DataMaintain/ProductMaintain.aspx',107,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(109,'Product Process flow',109,'DataMaintain/ProductProcessflowdefineMaintain.aspx',107,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(110,'Process—Resource',110,'DataMaintain/ProcessResourceReplace.aspx',107,2);


insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(111,'Resource',111,NULL,100,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(112,'Resource Location',112,'DataMaintain/PurposeNumberdefineMaintain.aspx',111,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(113,'Resource Qty',113,'DataMaintain/ResourceMaintain.aspx',111,2);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(114,'MRP',114,'DataMaintain/MRPMaintain.aspx',100,1);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(115,'Parameter',115,NULL,100,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(116,'Productivity Parameter Name',116,'DataMaintain/ProductivityparameterNumberMaintain.aspx',115,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(117,'Productivity Parameter Value',117,'DataMaintain/ProductivityparametervalueMaintain.aspx',115,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(118,'Capacity Steps',118,'DataMaintain/CapacityStepsMaintain.aspx',115,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(119,'Available Time',119,'DataMaintain/AvailableTime.aspx',115,2);


--Formula Define菜单
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(200,'Formula Define',200,NULL,0,0);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(201,'Productivity',201,NULL,200,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(202,'Module',202,'DataMaintain/FormulaDefineModule.aspx',201,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(203,'Node',203,'DataMaintain/FormulaDefineNode.aspx',201,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(204,'Capacity',204,'DataMaintain/FormulaDefineCapacity.aspx',200,1);


--2012-3-21添加的
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(205,'Productivity',205,'DataMaintain/FormulaDefine.aspx',200,1);

--Report菜单
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(300,'Report',300,NULL,0,0);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(301,'Resource',301,'DataQuery/ResourceQuery.aspx',300,1);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(302,'Productivity',302,NULL,300,1);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(303,'Productivity Module',303,'DataQuery/ProductivityOutput-ENC.aspx',302,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(304,'Productivity Node',304,'DataQuery/ProductivityOutput-MIC.aspx',302,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(305,'Productivity Second Model',305,'DataQuery/ProductivityOutput-SEC.aspx',302,2);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(306,'Module Capacity',306,NULL,300,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(307,'Capacity',307,'DataQuery/ReportQuery.aspx',306,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(308,'Technical Capacity',308,'DataQuery/ReportDiagramQuery.aspx',306,2);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(309,'Node Capacity',309,NULL,300,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(310,'Capacity',310,'DataQuery/ReportNodeQuery.aspx',309,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(311,'Technical Capacity',311,'DataQuery/ReportNodeDiagramQuery.aspx',309,2);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(312,'Report Compute',312,'DataQuery/ReportCompute.aspx',300,1);


--System菜单
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(400,'System',400,NULL,0,0);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(401,'Role Management',401,'System/RoleManagement.aspx',400,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(402,'User Management',402,'System/UserManagement.aspx',400,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(403,'History ',403,'System/LogManagement.aspx',400,1);



--用户上传文件表
--drop  table UploadFile;
create table UploadFile
(
	username varchar(100) null,		--用户名
	up_file varchar(200) null		--系统预处理后的上传文件名
);
insert into UploadFile values('sys','a');
insert into UploadFile values('encldap','a');
insert into UploadFile values('ELLGYYG','a');

--系统日志表
-- drop table tbl_log
CREATE TABLE tbl_log
(
	id numeric(18, 0) IDENTITY(1,1) NOT NULL,
	usr_id varchar(50) NULL,				--用户id
	usr_name varchar(100) NULL,				--用户名
	opt varchar(100) NULL,					--用户操作 
	opt_date datetime NULL,					--操作时间
	detail varchar(500) NULL				--操作具体细节
);


-----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------业务表-------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------
--Product Class Number define
--drop table tbl_ProductClass;
create table tbl_ProductClass
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductClassNumber varchar(20) not null,
	ProductClass varchar(50) null
);

--Product Category Number define
--drop table tbl_ProductCategory;
create table tbl_ProductCategory
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductCategoryNumber varchar(20) not null,
	ProductCategory varchar(50) null
);

--Product Family Number define
--drop table tbl_ProductFamily;
create table tbl_ProductFamily
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductFamilyNumber varchar(20) not null,
	ProductFamily varchar(50) null
);


--Product define
--drop table tbl_Product;
create table tbl_Product
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(20) not null,
	ProductClassNumber varchar(20) null,
	ProductCategoryNumber varchar(20) null,
	ProductFamilyNumber varchar(20) null,
	ProductDescription varchar(200) null
);

--上传数据时，Name向Number转化的临时表
--drop table tbl_Producttemp;
create table tbl_Producttemp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(20) not null,
	ProductClassName varchar(20) null,
	ProductCategoryName varchar(20) null,
	ProductFamilyName varchar(20) null,
	ProductDescription varchar(200) null,
	username varchar(50) null
);

--Capacity steps
--drop table tbl_CapacitySteps;
create table tbl_CapacitySteps
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductFamily varchar(20) not null,
	CapacitySteps varchar(50) null,
	Date varchar(20) null					--新添的列
);


--Process Number define
--drop table tbl_Process;
create table tbl_Process
(		
	id numeric(18, 0) identity(1,1) not null primary key,
	ProcessNumber varchar(20) not null,
	ProcessName varchar(50) null,
	ProcessCategoryNumber varchar(20) null
);

--Process Category Number define
--drop table tbl_ProcessCategory;
create table tbl_ProcessCategory
(		
	id numeric(18, 0) identity(1,1) not null primary key,
	ProcessCategoryNumber varchar(20) not null,
	ProcessCategoryName varchar(50) null
);

--Product Process flow define
--drop table tbl_ProductProcessFlow;
create table tbl_ProductProcessFlow
(			
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(20) not null,
	ProcessNumber varchar(20) not null
);
alter table tbl_ProductProcessFlow add HourPerShift varchar(20); --新添的列
alter table tbl_ProductProcessFlow add xh numeric(12,0) null; --用于ProcessName的排序   2012-4-11


--Product Process flow 上传时的临时表，将Name转化为Number
--drop table tbl_ProductProcessFlowtemp
create table tbl_ProductProcessFlowtemp
(			
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(20) not null,
	ProcessName varchar(50) not null,				-- 2012-5-15,将长度由varchar(20)-->varchar(50)
	HourPerShift  varchar(20) null,
	username varchar(50) null
);


alter table tbl_ProductProcessFlowtemp add xh numeric(12,0) null; --用于ProcessName的排序   2012-5-15

--Purpose Number define
--drop table tbl_Purpose
create table tbl_Purpose
(			
	id numeric(18, 0) identity(1,1) not null primary key,
	PurposeNumber varchar(20) not null,
	PurposeName varchar(50) null,
	Type varchar(20) null,
	xh numeric(12,0) default(0)	,--用于排序，CHN total用2表示，ENC total用1表示，普通的用0表示
);
insert into tbl_Purpose(PurposeNumber,PurposeName,Type,xh)values('ZZZ','CHN total',NULL,2);
insert into tbl_Purpose(PurposeNumber,PurposeName,Type,xh)values('YYY','ENC Total',NULL,1);

-- Resource maintenance
--drop table tbl_Resource
create table tbl_Resource
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProcessNumber varchar(20) not null,
	PurposeNumber varchar(20) null,
	QTY numeric(18, 2) null,
	Date varchar(20) null
);

--resource 查询 output 使用的表
--drop table tbl_Resource_temp
create table tbl_Resource_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProcessNumber varchar(20) not null,
	PurposeNumber varchar(20) null,
	QTY numeric(18, 2) null,
	Date varchar(20) null
);



--Productivity parameter Number
--drop table tbl_ProductivityParameter
create table tbl_ProductivityParameter
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ParameterNumber varchar(200) not null,
	ParameterName varchar(1000) null,
	ProcessCategoryNumber varchar(20) null
);
--2012-3-9将 ParameterName 长度由varchar(50)-->varchar(1000)

--Productivity parameter value
--drop table tbl_ProductivityParameterValue
create table tbl_ProductivityParameterValue
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(200) not null,	--  20->200
	ProcessName  varchar(500) null,			--  50->500
	ParameterName varchar(1000) null,		--  50->500
	ParameterValue varchar(500) null,		--  numeric(18, 4)-> varchar(500)
	Date varchar(20) null
);


--系统零星参数存放表
--drop table tbl_SysParameter
create table tbl_SysParameter                     --新添的表
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ParameterName varchar(50) null,
	ParameterValue varchar(20) null
);
alter table tbl_SysParameter add Type varchar(20); --新添的列,用于区分Test和Assembly类
insert into tbl_SysParameter(ParameterName,ParameterValue,Type) values('Hour per shift','7.2','Test');
insert into tbl_SysParameter(ParameterName,ParameterValue,Type) values('Hour per shift','7.2','Assembly');

--Productivity计算 ：ENC格式   ENC for Module
--drop table tbl_ProductivityENC
create table tbl_ProductivityENC
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(20) not null,
	ProcessName  varchar(50) null,
	Type varchar(50) null,			--用于区分Average time(min) 和Productivity/shift/station
	Date varchar(20) null,			--日期
	value numeric(30,4) null,		--计算值
	xh numeric(12,0) null			--排序用,Test类为'0',Assembly类为'1'
);

--Productivity计算 ：SEC格式   
--drop table tbl_ProductivitySEC
create table tbl_ProductivitySEC
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(20) not null,
	ProcessName  varchar(50) null,
	Type varchar(50) null,			--用于区分Average time(min) 和Productivity/shift/station
	Date varchar(20) null,			--日期
	value numeric(30,4) null,		--计算值
	xh numeric(12,0) null			--排序用,Test类为'0',Assembly类为'1'
);



--Productivity计算 ：MIC格式   ENC for Node
--drop table tbl_ProductivityMIC
create table tbl_ProductivityMIC
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,
	ProcessName  varchar(50) null,
	Type varchar(50) null,			--用于区分Average time(min) 和Productivity/shift/station
	Date varchar(20) null,			--日期
	value numeric(30,4) null,		--计算值
	dlxh numeric(12,0) null,    	--大类排序,Test类为'0',Assembly Line类为'1'，Assembly single(single station)类为'2'
	xlxh numeric(12,0) null			--小类排序,分两类：Average time 用1表示、Productivity/shift/station用2表示
);


-----------------------------------Productivity报表新添加的列------------------------------
alter table tbl_ProductivityENC add ProcessXH  numeric(12,0) null;
alter table tbl_ProductivitySEC add ProcessXH  numeric(12,0) null;
alter table tbl_ProductivityMIC add ProcessXH  numeric(12,0) null;



--Report 计算表
--drop table tbl_ReportCompute
create table tbl_ReportCompute
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,		--Product
	ProcessName  varchar(50) null,			--Process
	Type varchar(50) null, 					--类别，可能的取值：Units/shift/station、Production efficiency、
											--					Actual output/shift/station、No.of station	
											--					Units/shift、Bottleneck、Regular、Max
	Date varchar(20) null,					--日期
	value numeric(18, 4) null, 				--数值
	dlxh numeric(12,0) null,				--大类序号：分四类：Process的五个值为第一类，用'1'表示、Bottleneck为第二类，用'2'表示、
											--					Process对应的Regular和Max为第三类，用'3'表示、BottleneckCapacity为第四类，用'4'表示
												
	xlxh numeric(12,0) null					--小类序号：第一大类分5小类：Units/shift/station为'1',Production efficiency为'2',Actual output/shift/station为'3'，No.of station为'4',Units/shift为'5'
											--		    第二大类分1小类: Bottleneck为'1'
											--		    第三大类分2小类：Regular为'1'，Max为'2'
											--		    第四大类分2小类: Regular为'1'，Max为'2'
);





--Report 计算表，Total查询辅助表
--drop table tbl_ReportCompute_temp
create table tbl_ReportCompute_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(1000) not null,		--Product
	ProcessName  varchar(50) null,			--Process
	Type varchar(50) null, 					--类别，可能的取值：Units/shift/station、Production efficiency、
											--					Actual output/shift/station、No.of station	
											--					Units/shift、Bottleneck、Regular、Max
	Date varchar(20) null,					--日期
	value numeric(18, 4) null, 				--数值
	dlxh numeric(12,0) null,				--大类序号：分四类：Process的五个值为第一类，用'1'表示、Bottleneck为第二类，用'2'表示、
											--					Process对应的Regular和Max为第三类，用'3'表示、BottleneckCapacity为第四类，用'4'表示
												
	xlxh numeric(12,0) null,				--小类序号：第一大类分5小类：Units/shift/station为'1',Production efficiency为'2',Actual output/shift/station为'3'，No.of station为'4',Units/shift为'5'
											--		    第二大类分1小类: Bottleneck为'1'
											--		    第三大类分2小类：Regular为'1'，Max为'2'
											--		    第四大类分2小类: Regular为'1'，Max为'2'
	username varchar(50) null
);




--Report diagram计算
--drop table tbl_ReportDiagram
 create table tbl_ReportDiagram
(
	id numeric(18, 0) identity(1,1) not null primary key,
	Number varchar(50) not null,				--此处不一定仅仅是Product，用Number代替
	ProcessName varchar(50) null,				--Process名称
	Type varchar(50) null, 						--类别，可能的取值：Flexibility、No.of station、Total Station、MRP previous、MRP、
												--					Max Capacity、Regular Capacity、Capacity steps

	Date varchar(20) null,						--日期
	value varchar(20) null, 					--数值
	xh numeric(12,0) null						--序号：Flexibility=1、No.of station=2、Total Station=3、MRP previous=4、MRP=5、
												--		Max Capacity=6、Regular Capacity=7、Capacity steps=8
);


--Report diagram计算 ，大类查询辅助表（Class,Catagory,Family）
--drop table tbl_ReportDiagram_temp
 create table tbl_ReportDiagram_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	Number varchar(50) not null,				--此处不一定仅仅是Product，用Number代替
	ProcessName varchar(50) null,				--Process名称
	Type varchar(50) null, 						--类别，可能的取值：Flexibility、No.of station、Total Station、MRP previous、MRP、
												--					Max Capacity、Regular Capacity、Capacity steps

	Date varchar(20) null,						--日期
	value varchar(20) null, 					--数值
	xh numeric(12,0) null,						--序号：Flexibility=1、No.of station=2、Total Station=3、MRP previous=4、MRP=5、
												--		Max Capacity=6、Regular Capacity=7、Capacity steps=8
	username varchar(50) null
);

--Module部分的Working Day
--drop table tbl_WorkingDayModule;
create table tbl_WorkingDayModule
(
	id numeric(18, 0) identity(1,1) not null primary key,
	type varchar(20)  null,
	Date varchar(20) null,					--日期,新添的列
	value varchar(20) null
);
insert into tbl_WorkingDayModule(type,value)values('Regular','22');
insert into tbl_WorkingDayModule(type,value)values('Max','28');



-------------------------------------------------Node部分表------------------------------------------------
--Node部分的Working Day
--drop table tbl_WorkingDayNode;
create table tbl_WorkingDayNode
(
	id numeric(18, 0) identity(1,1) not null primary key,
	type varchar(20)  null,
	Date varchar(20) null,					--日期,新添的列
	value varchar(20) null
);
insert into tbl_WorkingDayNode(type,value)values('Regular','25');


--Node部分的Report 计算表  
--drop table tbl_ReportComputeNode
create table tbl_ReportComputeNode
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) null,			--Product    -- 2012-3-27 将 not null ->null
	ProcessName varchar(50) null,			--Process名称
	Type varchar(100) null, 				--类别，共13种，可能的取值：Productivity of Assembly line (units/shift/line)、No. Of lines、
											--					Productivity of Single Station (units/shift/station)、No. Of Stations		
											--					Productivity test/station、No.of testers、Node Assembly 2-shift (max)、
											--					Node Assembly 3-shift (max)、Capacity Test 2-shift (max)、Capacity Test 3-shift (max)、
											--					Bottleneck of Capacity 2-shifts、Bottleneck of Capacity 3-shifts、Production Line Efficiency%
	Date varchar(20) null,					--日期
	value numeric(18, 4) null, 				--数值
	dlxh numeric(12,0) null,				--大类序号：分六类：Productivity Assembly line为第一大类，用'1'表示、
											--					Productivity Single Station为第二大类，用'2'表示、
											--					Productivity test为第三类，用'3'表示、
											--					Node Assembly为第四类，用'4'表示、
											--					Capacity Test为第五类，用'5'表示、
											--					Bottleneck为第六类，用'6'表示、
											--					Production Line Efficiency为第七类，用'7'表示
											--					Actual need为第八类，用'8'表示
											--					Allocated为第九类，用'9'表示
											--					Balance为第十类，用'10'表示，只有Assembly类有
											--					Gap against MRP为第十一类，用'11'表示
												
	xlxh numeric(12,0) null					--小类序号：第一大类分2小类：Productivity为'1',No. Of lines为'2'
											--		    第二大类分2小类: Productivity为'1',No. Of Stations为'2'
											--		    第三大类分3小类：Productivity为'1',Stations of testers为'2'
											--		    第四大类分2小类: 2-shift为'1'，3-shift为'2'
											--		    第五大类分2小类: 2-shift为'1'，3-shift为'2'
											--		    第六大类分2小类: 2-shift为'1'，3-shift为'2'
											--			第七大类分1小类: Production Line Efficiency为'1'
											--			第八大类分2小类：Actual need值为'1',Total actual need为'2'
											--			第九大类分2小类: Allocated 值为'1',Total available为'2'
											--			第十大类不分小类
											--			第十一大类分4小类：Gap against MRP值为'1',Total gap为'2',equal to small single station为'3',Equal to small single station gap为'4'
											
											
);


alter table tbl_ReportComputeNode add ResourceName varchar(50)  null; --ResourceName列



--Node部分的Report Total查询辅助表
--drop table tbl_ReportComputeNode_temp
create table tbl_ReportComputeNode_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,		--Product
	ProcessName varchar(50) null,			--Process名称
	Type varchar(100) null, 					--类别，共13种，可能的取值：Productivity of Assembly line (units/shift/line)、No. Of lines、
											--					Productivity of Single Station (units/shift/station)、No. Of Stations		
											--					Productivity test/station、No.of testers、No. Of common、Node Assembly 2-shift (max)、
											--					Node Assembly 3-shift (max)、Capacity Test 2-shift (max)、Capacity Test 3-shift (max)、
											--					Bottleneck of Capacity 2-shifts、Bottleneck of Capacity 3-shifts
	Date varchar(20) null,					--日期
	value numeric(18, 4) null, 				--数值
	dlxh numeric(12,0) null,				--大类序号：分六类：Productivity Assembly line为第一大类，用'1'表示、
											--					Productivity Single Station为第二大类，用'2'表示、
											--					Productivity test为第三类，用'3'表示、
											--					Node Assembly为第四类，用'4'表示、
											--					Capacity Test为第五类，用'5'表示、
											--					Bottleneck为第六类，用'6'表示、
												
	xlxh numeric(12,0) null,				--小类序号：第一大类分2小类：Productivity为'1',No. Of lines为'2'
											--		    第二大类分2小类: Productivity为'1',No. Of Stations为'2'
											--		    第三大类分3小类：Productivity为'1',No.of testers为'2'、No. Of common为'3'
											--		    第四大类分2小类: 2-shift为'1'，3-shift为'2'
											--		    第五大类分2小类: 2-shift为'1'，3-shift为'2'
											--		    第六大类分2小类: 2-shift为'1'，3-shift为'2'
	username varchar(50) null
);

alter table tbl_ReportComputeNode_temp add ResourceName varchar(50)  null; --ResourceName列


--Report Node diagram计算
--drop table tbl_ReportDiagramNode
 create table tbl_ReportDiagramNode
(
	id numeric(18, 0) identity(1,1) not null primary key,
	Number varchar(50) not null,				--此处不一定仅仅是Product，用Number代替
	ProcessName varchar(50) null,				--Process名称
	Type varchar(50) null, 						--类别，可能的取值：allocated testers、MRP Previous、MRP、Flexibility、Assembly Normal Capacity、
												--					Assembly Max Capacity、Test Normal Capacity、Test Max Capacity、Capacity steps

	Date varchar(20) null,						--日期
	value varchar(20) null, 					--数值
	xh numeric(12,0) null						--序号：allocated testers=1、MRP Previous=2、MRP=3、Flexibility=4、Assembly Normal Capacity=5、
												--		Assembly Max Capacity=6、Test Normal Capacity=7、Test Max Capacity=8、Capacity steps=9
);


--Report Node diagram计算 ，大类查询辅助表（Class,Catagory,Family）
--drop table tbl_ReportDiagramNode_temp
 create table tbl_ReportDiagramNode_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	Number varchar(50) not null,				--此处不一定仅仅是Product，用Number代替
	ProcessName varchar(50) null,				--Process名称
	Type varchar(50) null, 						--类别，可能的取值：allocated testers、MRP Previous、MRP、Flexibility、Assembly Normal Capacity、
												--					Assembly Max Capacity、Test Normal Capacity、Test Max Capacity、Capacity steps

	Date varchar(20) null,						--日期
	value varchar(20) null, 					--数值
	xh numeric(12,0) null,						--序号：allocated testers=1、MRP Previous=2、MRP=3、Flexibility=4、Assembly Normal Capacity=5、
												--		Assembly Max Capacity=6、Test Normal Capacity=7、Test Max Capacity=8、Capacity steps=9
	username varchar(50) null
);








------------------------------------------------------------------------------------------------------------
-----------------------------------------------卞华星-------------------------------------------------------
------------------------------------------------------------------------------------------------------------
--MRP  
--drop table tbl_MRP
create table tbl_MRP
(
	ProductRange varchar(50) not null,
	ProductNumber varchar(50) not null,
    username varchar(50) null,
    Date varchar(20) null,			--日期
	Tvalue varchar(20) null,
    TType varchar(20) null
);


--动态公式
--drop table tbl_expression
create table tbl_expression
(
	id numeric(18, 0) identity(1,1) not null primary key,
	FamilyName varchar(20) null,		--所属Family
	Type varchar(100) null,				--类型，区别是ENC、SEC、MIC等
	Expression varchar(4000) null		--公式表达式,    2012-3-21  将长度有1000-》4000
);

--2012-3-22
alter table tbl_expression add newtype varchar(50)  null; --类别列，取值为Test和Assembly
alter table tbl_expression add newnewtype varchar(50)  null; --类别列，取值为singlestation和assemblyline,只有Node部分的Family才写入值


--ProcessResource替代关系
--drop table tbl_ProcessResourceReplace;
create table tbl_ProcessResourceReplace
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,
	ProcessName varchar(50)  null,
	ResourceName varchar(50)  null,
	Date varchar(20) null				--日期
);


--ProcessResource替代关系  临时表
--drop table tbl_ProcessResourceReplace_temp;
create table tbl_ProcessResourceReplace_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,
	ProcessName varchar(50)  null,
	ResourceName varchar(50)  null,
	Date varchar(20) null,				--日期
	username varchar(50) null
);


alter table tbl_ProcessResourceReplace add xh numeric(12,0) null; --用于ProcessName的排序
alter table tbl_ProcessResourceReplace_temp add xh numeric(12,0) null; --用于ProcessName的排序



------------------------------------------Resource新表-----------------------------------------------------
-- Resource maintenance
--drop table tbl_ResourceNew
create table tbl_ResourceNew
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ResourceName varchar(50) not null,
	PurposeNumber varchar(50) null,
	QTY numeric(18, 2) null,
	Date varchar(20) null
);

--resource 查询 output 使用的表
--drop table tbl_ResourceNew_temp
create table tbl_ResourceNew_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ResourceName varchar(50) not null,
	PurposeNumber varchar(20) null,
	QTY numeric(18, 2) null,
	Date varchar(20) null
);





-----------------------------------------------------------------------------
--2012-3-9 以下的表是针对Harry zhou提到的Product Package建立的,但是目前没有使用Package的概念

--产品包
--drop table tbl_Package
create table tbl_Package
(
	id numeric(18, 0) identity(1,1) not null primary key,
	PackageNumber varchar(20) not null,		--产品包id
	PackageName varchar(50)  null,			--产品包名	
	username varchar(50) null   			--创建者
);

--产品包和Product的关联表
--drop table tbl_ProductPackage
create table tbl_ProductPackage
(
	id numeric(18, 0) identity(1,1) not null primary key,
	PackageNumber varchar(20) not null,		--产品包名
	ProductNumber varchar(20) null,			--ProductNumber
);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(121,'Package',121,'DataMaintain/PackageMaintain.aspx',101,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(122,'Product Package',122,'DataMaintain/ProductPackageMaintain.aspx',107,2);



------------------------------------------------------------------------------
--2012-3-19 针对添加Product时，Class和Category可以不全选

--用于Product添加时不选择Class和Category时使用，
insert into tbl_ProductClass(ProductClassNumber,ProductClass) values('0',NULL);
insert into tbl_ProductCategory(ProductCategoryNumber,ProductCategory)values('0',NULL);



------------------------------------------------------------------------------
--2012-4-11 Capacity数据用于ProcessName排序用的
alter table tbl_ReportCompute add ProcessXH  numeric(12,0) null; 
alter table tbl_ReportCompute_temp add ProcessXH  numeric(12,0) null;
alter table tbl_ReportDiagram add ProcessXH  numeric(12,0) null;
alter table tbl_ReportDiagram_temp add ProcessXH  numeric(12,0) null;
alter table tbl_ReportComputeNode add ProcessXH  numeric(12,0) null;
alter table tbl_ReportComputeNode_temp add ProcessXH  numeric(12,0) null;
alter table tbl_ReportDiagramNode add ProcessXH  numeric(12,0) null;
alter table tbl_ReportDiagramNode_temp add ProcessXH  numeric(12,0) null;

------------------------------------------------------------------------------
--2012-4-19 动态公式Capacity部分的分台子用的表
--drop table tbl_ConfigParameter
create table tbl_ConfigParameter
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ClassNumber varchar(20)  null,			--ClassNumber
	ClassName varchar(20)  null,			--ClassName
	ProcessName varchar(50) null,			--ProductName
	Parametervalue varchar(20) null,		--系数值
);

alter table tbl_ReportCompute add newproductivity  numeric(18,4) null; 


