
------------------------------------------------------------------------------------
/** 
 * ����PREǰ׺�ı���Щ����ͣ�����Ż�¼��ϵͳ����ʹ�õı�  2014-08-20 Mark
 **/
------------------------------------------------------------------------------------
--����Ա��
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PREOperator]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PREOperator](
	[OperatorID] [nvarchar](50) NOT NULL,
	[OperatorName] [nvarchar](50) NOT NULL,
	[OperatorPwd] [nvarchar](50) NOT NULL,
	[RoleID] [nvarchar](50) NOT NULL,
	[OperatorNum] [int] NULL,
 CONSTRAINT [PK_PREOperator] PRIMARY KEY CLUSTERED 
(
	[OperatorID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

if not exists (select * from PREOperator where OperatorID='admin')
insert into PREOperator (operatorID,OperatorName,OperatorPwd,RoleID,OperatorNum) values('admin','admin','123','Admin',1)

--��ɫ��
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRERole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PRERole](
	[RoleID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Permission] [nvarchar](4000) NULL,
 CONSTRAINT [PK_PRERole] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

if not exists (select * from PRERole where RoleID='Admin')
insert into PRERole (RoleID,[Name],Description,Permission) values('Admin','ϵͳ����Ա','ϵͳ����Ա����ϵͳ���е�Ȩ��.','all')

--�̼���Ϣ��
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PREBusinesses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PREBusinesses](
	[BusinessesID] [uniqueidentifier] NOT NULL,
	[BusinessesName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_PREBusinesses] PRIMARY KEY CLUSTERED 
(
	[BusinessesID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--�Ż���Ϣ��
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PREPreferential]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PREPreferential](
	[PreferentialID] [uniqueidentifier] NOT NULL,
	[CardID] [nvarchar](50) NOT NULL,
	[PreferentialHour] [int] NOT NULL,
	[BusinessesName1] [nvarchar](200) NULL,
	[BusinessesMoney1] [decimal](10,2) NULL,
	[BusinessesName2] [nvarchar](200) NULL,
	[BusinessesMoney2] [decimal](10,2) NULL,
	[BusinessesName3] [nvarchar](200) NULL,
	[BusinessesMoney3] [decimal](10,2) NULL,
	[Notes] [nvarchar](2000) NULL,
	[WorkstationID] [uniqueidentifier] NOT NULL,
	[WorkstationName] [nvarchar](200) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[PreferentialTime] [datetime] NOT NULL,
	[EntranceTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PREPreferential] PRIMARY KEY CLUSTERED 
(
	[PreferentialID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--�Żݲ�����¼��
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PREPreferentialLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PREPreferentialLog](
	[PreferentialID] [uniqueidentifier] NOT NULL,
	[CardID] [nvarchar](50) NOT NULL,
	[PreferentialHour] [int] NOT NULL,
	[BusinessesName1] [nvarchar](200) NULL,
	[BusinessesMoney1] [decimal](10,2) NULL,
	[BusinessesName2] [nvarchar](200) NULL,
	[BusinessesMoney2] [decimal](10,2) NULL,
	[BusinessesName3] [nvarchar](200) NULL,
	[BusinessesMoney3] [decimal](10,2) NULL,
	[Notes] [nvarchar](2000) NULL,
	[WorkstationID] [uniqueidentifier] NOT NULL,
	[WorkstationName] [nvarchar](200) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[OperatorTime] [datetime] NOT NULL,
	[EntranceTime] [datetime] NOT NULL,
	[IsCancel] [bit] NULL,
	[CancelReason] [nvarchar](2000) NULL,
 CONSTRAINT [PK_PREPreferentialLog] PRIMARY KEY CLUSTERED 
(
	[PreferentialID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--��Card������һ����λ
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PreferentialTime' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add PreferentialTime datetime')
end
go

--��CardPaymentRecord������һ����λ
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CurrDiscountHour' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add CurrDiscountHour int')
end
go
