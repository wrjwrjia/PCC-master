-----------------------------------------------------------------------------------------------------------------------
------------------------------------------------ϵͳ��-----------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------
--�û���
--drop table tbl_usr
CREATE TABLE tbl_usr
(
	id numeric(18, 0) IDENTITY(1,1) NOT NULL,
	usr_login varchar(100) NULL,				--�û���¼��
	usr_pwd varchar(100) NULL,					--��¼����
	dept_no varchar(100) NULL,					--����,��ʱ����
	role_id varchar(100) NULL,					--�����Ľ�ɫid
	role_name varchar(100) NULL,				--�����Ľ�ɫ��
	last_log datetime NULL						--�ϴε�¼ʱ��,��ʱ����
);
--insert into tbl_usr(usr_login,usr_pwd,dept_no,role_id,role_name,last_log)values('sys','123','0','1','Administrator',getdate());
insert into tbl_usr(usr_login,usr_pwd,dept_no,role_id,role_name,last_log)values('encldap','123','0','1','Administrator',getdate());
insert into tbl_usr(usr_login,usr_pwd,dept_no,role_id,role_name,last_log)values('ELLGYYG','123','0','1','Administrator',getdate());


--��ɫ��
--drop table TBL_ROLE
CREATE TABLE TBL_ROLE
(
	id numeric (18, 0) IDENTITY(1,1) NOT NULL,
	role_na varchar(100) NULL					--��ɫ��
);
insert into tbl_role(role_na) values('Administrator');


--��ɫȨ�ޱ�,��Ҫ�ǿ��Ʋ˵�
--drop table Purview
CREATE TABLE Purview
(
	pid numeric(18, 0) IDENTITY(1,1) NOT NULL,
	userid numeric(18, 0) NULL,					--�û�Id,��ʱ����
	roleid numeric(18, 0) NULL,					--��ɫid
	permission varchar(1000) NULL				--��ɫ���еĲ˵�����"1,5,4,7,8"����ʽ���
);
insert into Purview(roleid,permission) values('1','400,401,402,403');--Ĭ�ϸ�Administrator��ɫ����"ϵͳ����"�˵�ȫѡ

--��ɫȨ�ޱ���Ҫ�ǿ���ҳ���ϵİ�ť
--drop table UserPurview
CREATE TABLE UserPurview
(
	id numeric(18, 0) IDENTITY(1,1) NOT NULL,
	UserId numeric(18, 0) NULL,     --��ʱ����
	ModuleId numeric(18, 0) NULL,	--�˵�id
	roleid numeric(18, 0) NULL,		--��ɫid
	IsSelected numeric(18, 0) NULL	--��ʾ��ɫ�Ƿ���ĳ�˵���Ȩ��
);

--ϵͳ�˵���
--drop TABLE Menu
CREATE TABLE Menu
(
	menuid int NOT NULL primary key,--�˵�id
	menu varchar(200) NULL,			--�˵���
	orderid numeric(18, 0) NULL,	--�˵�������
	url varchar(300) NULL,			--�˵���Ӧ����תҳ��,���˵�Ϊ��,�¼��˵���Ϊ��
	parentid int NULL,				--�ϼ��˵�id		
	menuNode int NULL,				--�˵��ļ���  0��ʾһ���˵���1��ʾ�����������˵�
);
--DataMaintain�˵�
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
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(110,'Process��Resource',110,'DataMaintain/ProcessResourceReplace.aspx',107,2);


insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(111,'Resource',111,NULL,100,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(112,'Resource Location',112,'DataMaintain/PurposeNumberdefineMaintain.aspx',111,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(113,'Resource Qty',113,'DataMaintain/ResourceMaintain.aspx',111,2);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(114,'MRP',114,'DataMaintain/MRPMaintain.aspx',100,1);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(115,'Parameter',115,NULL,100,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(116,'Productivity Parameter Name',116,'DataMaintain/ProductivityparameterNumberMaintain.aspx',115,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(117,'Productivity Parameter Value',117,'DataMaintain/ProductivityparametervalueMaintain.aspx',115,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(118,'Capacity Steps',118,'DataMaintain/CapacityStepsMaintain.aspx',115,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(119,'Available Time',119,'DataMaintain/AvailableTime.aspx',115,2);


--Formula Define�˵�
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(200,'Formula Define',200,NULL,0,0);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(201,'Productivity',201,NULL,200,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(202,'Module',202,'DataMaintain/FormulaDefineModule.aspx',201,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(203,'Node',203,'DataMaintain/FormulaDefineNode.aspx',201,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(204,'Capacity',204,'DataMaintain/FormulaDefineCapacity.aspx',200,1);


--2012-3-21��ӵ�
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(205,'Productivity',205,'DataMaintain/FormulaDefine.aspx',200,1);

--Report�˵�
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


--System�˵�
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(400,'System',400,NULL,0,0);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(401,'Role Management',401,'System/RoleManagement.aspx',400,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(402,'User Management',402,'System/UserManagement.aspx',400,1);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(403,'History ',403,'System/LogManagement.aspx',400,1);



--�û��ϴ��ļ���
--drop  table UploadFile;
create table UploadFile
(
	username varchar(100) null,		--�û���
	up_file varchar(200) null		--ϵͳԤ�������ϴ��ļ���
);
insert into UploadFile values('sys','a');
insert into UploadFile values('encldap','a');
insert into UploadFile values('ELLGYYG','a');

--ϵͳ��־��
-- drop table tbl_log
CREATE TABLE tbl_log
(
	id numeric(18, 0) IDENTITY(1,1) NOT NULL,
	usr_id varchar(50) NULL,				--�û�id
	usr_name varchar(100) NULL,				--�û���
	opt varchar(100) NULL,					--�û����� 
	opt_date datetime NULL,					--����ʱ��
	detail varchar(500) NULL				--��������ϸ��
);


-----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------ҵ���-------------------------------------------------------------
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

--�ϴ�����ʱ��Name��Numberת������ʱ��
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
	Date varchar(20) null					--�������
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
alter table tbl_ProductProcessFlow add HourPerShift varchar(20); --�������
alter table tbl_ProductProcessFlow add xh numeric(12,0) null; --����ProcessName������   2012-4-11


--Product Process flow �ϴ�ʱ����ʱ����Nameת��ΪNumber
--drop table tbl_ProductProcessFlowtemp
create table tbl_ProductProcessFlowtemp
(			
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(20) not null,
	ProcessName varchar(50) not null,				-- 2012-5-15,��������varchar(20)-->varchar(50)
	HourPerShift  varchar(20) null,
	username varchar(50) null
);


alter table tbl_ProductProcessFlowtemp add xh numeric(12,0) null; --����ProcessName������   2012-5-15

--Purpose Number define
--drop table tbl_Purpose
create table tbl_Purpose
(			
	id numeric(18, 0) identity(1,1) not null primary key,
	PurposeNumber varchar(20) not null,
	PurposeName varchar(50) null,
	Type varchar(20) null,
	xh numeric(12,0) default(0)	,--��������CHN total��2��ʾ��ENC total��1��ʾ����ͨ����0��ʾ
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

--resource ��ѯ output ʹ�õı�
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
--2012-3-9�� ParameterName ������varchar(50)-->varchar(1000)

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


--ϵͳ���ǲ�����ű�
--drop table tbl_SysParameter
create table tbl_SysParameter                     --����ı�
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ParameterName varchar(50) null,
	ParameterValue varchar(20) null
);
alter table tbl_SysParameter add Type varchar(20); --�������,��������Test��Assembly��
insert into tbl_SysParameter(ParameterName,ParameterValue,Type) values('Hour per shift','7.2','Test');
insert into tbl_SysParameter(ParameterName,ParameterValue,Type) values('Hour per shift','7.2','Assembly');

--Productivity���� ��ENC��ʽ   ENC for Module
--drop table tbl_ProductivityENC
create table tbl_ProductivityENC
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(20) not null,
	ProcessName  varchar(50) null,
	Type varchar(50) null,			--��������Average time(min) ��Productivity/shift/station
	Date varchar(20) null,			--����
	value numeric(30,4) null,		--����ֵ
	xh numeric(12,0) null			--������,Test��Ϊ'0',Assembly��Ϊ'1'
);

--Productivity���� ��SEC��ʽ   
--drop table tbl_ProductivitySEC
create table tbl_ProductivitySEC
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(20) not null,
	ProcessName  varchar(50) null,
	Type varchar(50) null,			--��������Average time(min) ��Productivity/shift/station
	Date varchar(20) null,			--����
	value numeric(30,4) null,		--����ֵ
	xh numeric(12,0) null			--������,Test��Ϊ'0',Assembly��Ϊ'1'
);



--Productivity���� ��MIC��ʽ   ENC for Node
--drop table tbl_ProductivityMIC
create table tbl_ProductivityMIC
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,
	ProcessName  varchar(50) null,
	Type varchar(50) null,			--��������Average time(min) ��Productivity/shift/station
	Date varchar(20) null,			--����
	value numeric(30,4) null,		--����ֵ
	dlxh numeric(12,0) null,    	--��������,Test��Ϊ'0',Assembly Line��Ϊ'1'��Assembly single(single station)��Ϊ'2'
	xlxh numeric(12,0) null			--С������,�����ࣺAverage time ��1��ʾ��Productivity/shift/station��2��ʾ
);


-----------------------------------Productivity��������ӵ���------------------------------
alter table tbl_ProductivityENC add ProcessXH  numeric(12,0) null;
alter table tbl_ProductivitySEC add ProcessXH  numeric(12,0) null;
alter table tbl_ProductivityMIC add ProcessXH  numeric(12,0) null;



--Report �����
--drop table tbl_ReportCompute
create table tbl_ReportCompute
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,		--Product
	ProcessName  varchar(50) null,			--Process
	Type varchar(50) null, 					--��𣬿��ܵ�ȡֵ��Units/shift/station��Production efficiency��
											--					Actual output/shift/station��No.of station	
											--					Units/shift��Bottleneck��Regular��Max
	Date varchar(20) null,					--����
	value numeric(18, 4) null, 				--��ֵ
	dlxh numeric(12,0) null,				--������ţ������ࣺProcess�����ֵΪ��һ�࣬��'1'��ʾ��BottleneckΪ�ڶ��࣬��'2'��ʾ��
											--					Process��Ӧ��Regular��MaxΪ�����࣬��'3'��ʾ��BottleneckCapacityΪ�����࣬��'4'��ʾ
												
	xlxh numeric(12,0) null					--С����ţ���һ�����5С�ࣺUnits/shift/stationΪ'1',Production efficiencyΪ'2',Actual output/shift/stationΪ'3'��No.of stationΪ'4',Units/shiftΪ'5'
											--		    �ڶ������1С��: BottleneckΪ'1'
											--		    ���������2С�ࣺRegularΪ'1'��MaxΪ'2'
											--		    ���Ĵ����2С��: RegularΪ'1'��MaxΪ'2'
);





--Report �����Total��ѯ������
--drop table tbl_ReportCompute_temp
create table tbl_ReportCompute_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(1000) not null,		--Product
	ProcessName  varchar(50) null,			--Process
	Type varchar(50) null, 					--��𣬿��ܵ�ȡֵ��Units/shift/station��Production efficiency��
											--					Actual output/shift/station��No.of station	
											--					Units/shift��Bottleneck��Regular��Max
	Date varchar(20) null,					--����
	value numeric(18, 4) null, 				--��ֵ
	dlxh numeric(12,0) null,				--������ţ������ࣺProcess�����ֵΪ��һ�࣬��'1'��ʾ��BottleneckΪ�ڶ��࣬��'2'��ʾ��
											--					Process��Ӧ��Regular��MaxΪ�����࣬��'3'��ʾ��BottleneckCapacityΪ�����࣬��'4'��ʾ
												
	xlxh numeric(12,0) null,				--С����ţ���һ�����5С�ࣺUnits/shift/stationΪ'1',Production efficiencyΪ'2',Actual output/shift/stationΪ'3'��No.of stationΪ'4',Units/shiftΪ'5'
											--		    �ڶ������1С��: BottleneckΪ'1'
											--		    ���������2С�ࣺRegularΪ'1'��MaxΪ'2'
											--		    ���Ĵ����2С��: RegularΪ'1'��MaxΪ'2'
	username varchar(50) null
);




--Report diagram����
--drop table tbl_ReportDiagram
 create table tbl_ReportDiagram
(
	id numeric(18, 0) identity(1,1) not null primary key,
	Number varchar(50) not null,				--�˴���һ��������Product����Number����
	ProcessName varchar(50) null,				--Process����
	Type varchar(50) null, 						--��𣬿��ܵ�ȡֵ��Flexibility��No.of station��Total Station��MRP previous��MRP��
												--					Max Capacity��Regular Capacity��Capacity steps

	Date varchar(20) null,						--����
	value varchar(20) null, 					--��ֵ
	xh numeric(12,0) null						--��ţ�Flexibility=1��No.of station=2��Total Station=3��MRP previous=4��MRP=5��
												--		Max Capacity=6��Regular Capacity=7��Capacity steps=8
);


--Report diagram���� �������ѯ������Class,Catagory,Family��
--drop table tbl_ReportDiagram_temp
 create table tbl_ReportDiagram_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	Number varchar(50) not null,				--�˴���һ��������Product����Number����
	ProcessName varchar(50) null,				--Process����
	Type varchar(50) null, 						--��𣬿��ܵ�ȡֵ��Flexibility��No.of station��Total Station��MRP previous��MRP��
												--					Max Capacity��Regular Capacity��Capacity steps

	Date varchar(20) null,						--����
	value varchar(20) null, 					--��ֵ
	xh numeric(12,0) null,						--��ţ�Flexibility=1��No.of station=2��Total Station=3��MRP previous=4��MRP=5��
												--		Max Capacity=6��Regular Capacity=7��Capacity steps=8
	username varchar(50) null
);

--Module���ֵ�Working Day
--drop table tbl_WorkingDayModule;
create table tbl_WorkingDayModule
(
	id numeric(18, 0) identity(1,1) not null primary key,
	type varchar(20)  null,
	Date varchar(20) null,					--����,�������
	value varchar(20) null
);
insert into tbl_WorkingDayModule(type,value)values('Regular','22');
insert into tbl_WorkingDayModule(type,value)values('Max','28');



-------------------------------------------------Node���ֱ�------------------------------------------------
--Node���ֵ�Working Day
--drop table tbl_WorkingDayNode;
create table tbl_WorkingDayNode
(
	id numeric(18, 0) identity(1,1) not null primary key,
	type varchar(20)  null,
	Date varchar(20) null,					--����,�������
	value varchar(20) null
);
insert into tbl_WorkingDayNode(type,value)values('Regular','25');


--Node���ֵ�Report �����  
--drop table tbl_ReportComputeNode
create table tbl_ReportComputeNode
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) null,			--Product    -- 2012-3-27 �� not null ->null
	ProcessName varchar(50) null,			--Process����
	Type varchar(100) null, 				--��𣬹�13�֣����ܵ�ȡֵ��Productivity of Assembly line (units/shift/line)��No. Of lines��
											--					Productivity of Single Station (units/shift/station)��No. Of Stations		
											--					Productivity test/station��No.of testers��Node Assembly 2-shift (max)��
											--					Node Assembly 3-shift (max)��Capacity Test 2-shift (max)��Capacity Test 3-shift (max)��
											--					Bottleneck of Capacity 2-shifts��Bottleneck of Capacity 3-shifts��Production Line Efficiency%
	Date varchar(20) null,					--����
	value numeric(18, 4) null, 				--��ֵ
	dlxh numeric(12,0) null,				--������ţ������ࣺProductivity Assembly lineΪ��һ���࣬��'1'��ʾ��
											--					Productivity Single StationΪ�ڶ����࣬��'2'��ʾ��
											--					Productivity testΪ�����࣬��'3'��ʾ��
											--					Node AssemblyΪ�����࣬��'4'��ʾ��
											--					Capacity TestΪ�����࣬��'5'��ʾ��
											--					BottleneckΪ�����࣬��'6'��ʾ��
											--					Production Line EfficiencyΪ�����࣬��'7'��ʾ
											--					Actual needΪ�ڰ��࣬��'8'��ʾ
											--					AllocatedΪ�ھ��࣬��'9'��ʾ
											--					BalanceΪ��ʮ�࣬��'10'��ʾ��ֻ��Assembly����
											--					Gap against MRPΪ��ʮһ�࣬��'11'��ʾ
												
	xlxh numeric(12,0) null					--С����ţ���һ�����2С�ࣺProductivityΪ'1',No. Of linesΪ'2'
											--		    �ڶ������2С��: ProductivityΪ'1',No. Of StationsΪ'2'
											--		    ���������3С�ࣺProductivityΪ'1',Stations of testersΪ'2'
											--		    ���Ĵ����2С��: 2-shiftΪ'1'��3-shiftΪ'2'
											--		    ��������2С��: 2-shiftΪ'1'��3-shiftΪ'2'
											--		    ���������2С��: 2-shiftΪ'1'��3-shiftΪ'2'
											--			���ߴ����1С��: Production Line EfficiencyΪ'1'
											--			�ڰ˴����2С�ࣺActual needֵΪ'1',Total actual needΪ'2'
											--			�ھŴ����2С��: Allocated ֵΪ'1',Total availableΪ'2'
											--			��ʮ���಻��С��
											--			��ʮһ�����4С�ࣺGap against MRPֵΪ'1',Total gapΪ'2',equal to small single stationΪ'3',Equal to small single station gapΪ'4'
											
											
);


alter table tbl_ReportComputeNode add ResourceName varchar(50)  null; --ResourceName��



--Node���ֵ�Report Total��ѯ������
--drop table tbl_ReportComputeNode_temp
create table tbl_ReportComputeNode_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,		--Product
	ProcessName varchar(50) null,			--Process����
	Type varchar(100) null, 					--��𣬹�13�֣����ܵ�ȡֵ��Productivity of Assembly line (units/shift/line)��No. Of lines��
											--					Productivity of Single Station (units/shift/station)��No. Of Stations		
											--					Productivity test/station��No.of testers��No. Of common��Node Assembly 2-shift (max)��
											--					Node Assembly 3-shift (max)��Capacity Test 2-shift (max)��Capacity Test 3-shift (max)��
											--					Bottleneck of Capacity 2-shifts��Bottleneck of Capacity 3-shifts
	Date varchar(20) null,					--����
	value numeric(18, 4) null, 				--��ֵ
	dlxh numeric(12,0) null,				--������ţ������ࣺProductivity Assembly lineΪ��һ���࣬��'1'��ʾ��
											--					Productivity Single StationΪ�ڶ����࣬��'2'��ʾ��
											--					Productivity testΪ�����࣬��'3'��ʾ��
											--					Node AssemblyΪ�����࣬��'4'��ʾ��
											--					Capacity TestΪ�����࣬��'5'��ʾ��
											--					BottleneckΪ�����࣬��'6'��ʾ��
												
	xlxh numeric(12,0) null,				--С����ţ���һ�����2С�ࣺProductivityΪ'1',No. Of linesΪ'2'
											--		    �ڶ������2С��: ProductivityΪ'1',No. Of StationsΪ'2'
											--		    ���������3С�ࣺProductivityΪ'1',No.of testersΪ'2'��No. Of commonΪ'3'
											--		    ���Ĵ����2С��: 2-shiftΪ'1'��3-shiftΪ'2'
											--		    ��������2С��: 2-shiftΪ'1'��3-shiftΪ'2'
											--		    ���������2С��: 2-shiftΪ'1'��3-shiftΪ'2'
	username varchar(50) null
);

alter table tbl_ReportComputeNode_temp add ResourceName varchar(50)  null; --ResourceName��


--Report Node diagram����
--drop table tbl_ReportDiagramNode
 create table tbl_ReportDiagramNode
(
	id numeric(18, 0) identity(1,1) not null primary key,
	Number varchar(50) not null,				--�˴���һ��������Product����Number����
	ProcessName varchar(50) null,				--Process����
	Type varchar(50) null, 						--��𣬿��ܵ�ȡֵ��allocated testers��MRP Previous��MRP��Flexibility��Assembly Normal Capacity��
												--					Assembly Max Capacity��Test Normal Capacity��Test Max Capacity��Capacity steps

	Date varchar(20) null,						--����
	value varchar(20) null, 					--��ֵ
	xh numeric(12,0) null						--��ţ�allocated testers=1��MRP Previous=2��MRP=3��Flexibility=4��Assembly Normal Capacity=5��
												--		Assembly Max Capacity=6��Test Normal Capacity=7��Test Max Capacity=8��Capacity steps=9
);


--Report Node diagram���� �������ѯ������Class,Catagory,Family��
--drop table tbl_ReportDiagramNode_temp
 create table tbl_ReportDiagramNode_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	Number varchar(50) not null,				--�˴���һ��������Product����Number����
	ProcessName varchar(50) null,				--Process����
	Type varchar(50) null, 						--��𣬿��ܵ�ȡֵ��allocated testers��MRP Previous��MRP��Flexibility��Assembly Normal Capacity��
												--					Assembly Max Capacity��Test Normal Capacity��Test Max Capacity��Capacity steps

	Date varchar(20) null,						--����
	value varchar(20) null, 					--��ֵ
	xh numeric(12,0) null,						--��ţ�allocated testers=1��MRP Previous=2��MRP=3��Flexibility=4��Assembly Normal Capacity=5��
												--		Assembly Max Capacity=6��Test Normal Capacity=7��Test Max Capacity=8��Capacity steps=9
	username varchar(50) null
);








------------------------------------------------------------------------------------------------------------
-----------------------------------------------�廪��-------------------------------------------------------
------------------------------------------------------------------------------------------------------------
--MRP  
--drop table tbl_MRP
create table tbl_MRP
(
	ProductRange varchar(50) not null,
	ProductNumber varchar(50) not null,
    username varchar(50) null,
    Date varchar(20) null,			--����
	Tvalue varchar(20) null,
    TType varchar(20) null
);


--��̬��ʽ
--drop table tbl_expression
create table tbl_expression
(
	id numeric(18, 0) identity(1,1) not null primary key,
	FamilyName varchar(20) null,		--����Family
	Type varchar(100) null,				--���ͣ�������ENC��SEC��MIC��
	Expression varchar(4000) null		--��ʽ���ʽ,    2012-3-21  ��������1000-��4000
);

--2012-3-22
alter table tbl_expression add newtype varchar(50)  null; --����У�ȡֵΪTest��Assembly
alter table tbl_expression add newnewtype varchar(50)  null; --����У�ȡֵΪsinglestation��assemblyline,ֻ��Node���ֵ�Family��д��ֵ


--ProcessResource�����ϵ
--drop table tbl_ProcessResourceReplace;
create table tbl_ProcessResourceReplace
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,
	ProcessName varchar(50)  null,
	ResourceName varchar(50)  null,
	Date varchar(20) null				--����
);


--ProcessResource�����ϵ  ��ʱ��
--drop table tbl_ProcessResourceReplace_temp;
create table tbl_ProcessResourceReplace_temp
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ProductNumber varchar(50) not null,
	ProcessName varchar(50)  null,
	ResourceName varchar(50)  null,
	Date varchar(20) null,				--����
	username varchar(50) null
);


alter table tbl_ProcessResourceReplace add xh numeric(12,0) null; --����ProcessName������
alter table tbl_ProcessResourceReplace_temp add xh numeric(12,0) null; --����ProcessName������



------------------------------------------Resource�±�-----------------------------------------------------
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

--resource ��ѯ output ʹ�õı�
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
--2012-3-9 ���µı������Harry zhou�ᵽ��Product Package������,����Ŀǰû��ʹ��Package�ĸ���

--��Ʒ��
--drop table tbl_Package
create table tbl_Package
(
	id numeric(18, 0) identity(1,1) not null primary key,
	PackageNumber varchar(20) not null,		--��Ʒ��id
	PackageName varchar(50)  null,			--��Ʒ����	
	username varchar(50) null   			--������
);

--��Ʒ����Product�Ĺ�����
--drop table tbl_ProductPackage
create table tbl_ProductPackage
(
	id numeric(18, 0) identity(1,1) not null primary key,
	PackageNumber varchar(20) not null,		--��Ʒ����
	ProductNumber varchar(20) null,			--ProductNumber
);

insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(121,'Package',121,'DataMaintain/PackageMaintain.aspx',101,2);
insert into menu(menuid,menu,orderid,url,parentid,menuNode)values(122,'Product Package',122,'DataMaintain/ProductPackageMaintain.aspx',107,2);



------------------------------------------------------------------------------
--2012-3-19 ������Productʱ��Class��Category���Բ�ȫѡ

--����Product���ʱ��ѡ��Class��Categoryʱʹ�ã�
insert into tbl_ProductClass(ProductClassNumber,ProductClass) values('0',NULL);
insert into tbl_ProductCategory(ProductCategoryNumber,ProductCategory)values('0',NULL);



------------------------------------------------------------------------------
--2012-4-11 Capacity��������ProcessName�����õ�
alter table tbl_ReportCompute add ProcessXH  numeric(12,0) null; 
alter table tbl_ReportCompute_temp add ProcessXH  numeric(12,0) null;
alter table tbl_ReportDiagram add ProcessXH  numeric(12,0) null;
alter table tbl_ReportDiagram_temp add ProcessXH  numeric(12,0) null;
alter table tbl_ReportComputeNode add ProcessXH  numeric(12,0) null;
alter table tbl_ReportComputeNode_temp add ProcessXH  numeric(12,0) null;
alter table tbl_ReportDiagramNode add ProcessXH  numeric(12,0) null;
alter table tbl_ReportDiagramNode_temp add ProcessXH  numeric(12,0) null;

------------------------------------------------------------------------------
--2012-4-19 ��̬��ʽCapacity���ֵķ�̨���õı�
--drop table tbl_ConfigParameter
create table tbl_ConfigParameter
(
	id numeric(18, 0) identity(1,1) not null primary key,
	ClassNumber varchar(20)  null,			--ClassNumber
	ClassName varchar(20)  null,			--ClassName
	ProcessName varchar(50) null,			--ProductName
	Parametervalue varchar(20) null,		--ϵ��ֵ
);

alter table tbl_ReportCompute add newproductivity  numeric(18,4) null; 


