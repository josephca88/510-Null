
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarType' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
BEGIN
	exec ('alter table cardevent add CarType tinyint')
    exec ('update cardevent set carType=0')
    exec ('alter table CardEvent alter column CarType tinyint not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='LostCardCost' AND id = OBJECT_ID(N'[dbo].[CardLostRestore]')) 
BEGIN
	exec ('alter table cardlostRestore add LostCardCost decimal(10,2)')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaymentMode' AND id = OBJECT_ID(N'[dbo].[CardLostRestore]')) 
BEGIN
	exec ('alter table cardlostRestore add PaymentMode tinyint')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SettleDateTime' AND id = OBJECT_ID(N'[dbo].[CardLostRestore]')) 
BEGIN
	exec ('alter table cardlostRestore add SettleDateTime datetime')
end
go

--���¸���¼�Ĳ���ԱIDΪ����Ա����
exec  ('
update CardPaymentRecord set OperatorNum=OperatorName 
   from Operator where CardPaymentRecord.OperatorNum=Operator.OperatorID and CardPaymentRecord.SettleDateTime is null
update CardPaymentRecord set StationID=StationName 
   from WorkStation where CardPaymentRecord.StationID=WorkStation.StationID and CardPaymentRecord.SettleDateTime is null

update CardRelease set OperatorID=OperatorName
   from Operator where CardRelease.OperatorID=Operator.OperatorID and CardRelease.SettleDateTime is null
update CardRelease set StationID=StationName 
   from WorkStation where CardRelease.StationID=WorkStation.StationID and CardRelease.SettleDateTime is null

update CardDefer set OperatorID=OperatorName
   from Operator where CardDefer.OperatorID=Operator.OperatorID and CardDefer.SettleDateTime is null
update CardDefer set StationID=StationName 
   from WorkStation where CardDefer.StationID=WorkStation.StationID and CardDefer.SettleDateTime is null

update CardCharge set OperatorID=OperatorName
   from Operator where CardCharge.OperatorID=Operator.OperatorID and CardCharge.SettleDateTime is null
update CardCharge set StationID=StationName 
   from WorkStation where CardCharge.StationID=WorkStation.StationID and CardCharge.SettleDateTime is null

update CardRecycle set OperatorID=OperatorName
   from Operator where CardRecycle.OperatorID=Operator.OperatorID and CardRecycle.SettleDateTime is null
update CardRecycle set StationID=StationName 
   from WorkStation where CardRecycle.StationID=WorkStation.StationID and CardRecycle.SettleDateTime is null

update CardRecycle set OperatorID=OperatorName
   from Operator where CardRecycle.OperatorID=Operator.OperatorID and CardRecycle.SettleDateTime is null
update CardRecycle set StationID=StationName 
   from WorkStation where CardRecycle.StationID=WorkStation.StationID and CardRecycle.SettleDateTime is null

update CardEvent set OperatorNum=OperatorName
   from Operator where CardEvent.OperatorNum=Operator.OperatorID and CardEvent.SettleDateTime is null
update CardEvent set StationID=StationName 
   from WorkStation where CardEvent.StationID=WorkStation.StationID and CardEvent.SettleDateTime is null
')
go


if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOfCardLost' AND id = OBJECT_ID(N'[dbo].[OperatorSettlelog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add CashOfCardLost decimal(10,2)')
    exec ('update OperatorSettleLog set CashOfCardLost=0')
    exec ('alter table OperatorSettleLog alter column CashOfCardLost decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='NonCashOfCardLost' AND id = OBJECT_ID(N'[dbo].[OperatorSettlelog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add NonCashOfCardLost decimal(10,2)')
    exec ('update OperatorSettleLog set NonCashOfCardLost=0')
    exec ('alter table OperatorSettleLog alter column NonCashOfCardLost decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='HandInCash' AND id = OBJECT_ID(N'[dbo].[OperatorSettlelog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add HandInCash decimal(10,2)')
end
go

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDeleteRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardDeleteRecord](
	[CardID] [nvarchar](50) NOT NULL,
	[DeleteDateTime] [datetime] NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[CardType] [tinyint] NOT NULL,
	[Balance] [decimal](10, 2) NOT NULL,
	[ValidDate] [datetime] NOT NULL,
	[Deposit] [decimal](10, 2) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_CardDeleteRecord] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[DeleteDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='IsCenterCharge' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardpaymentRecord add IsCenterCharge bit')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='IsNested' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add IsNested bit')
	exec ('update Park set IsNested=0')
    exec ('alter table Park alter column IsNested bit not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ParentID' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add ParentID int')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ForceWithCount' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add ForceWithCount bit')
    exec ('update Park set ForceWithCount=0')
    exec ('alter table Park alter column ForceWithCount bit not null')
end
go

--2013-4-28 ����һ�����������,���ͳ�λ���ı�
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ParkCarPort]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[ParkCarPort](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ParkID] [int] NOT NULL,
		[CardType] [tinyint] NULL,
		[CarType] [tinyint] NULL,
		[CarPort] [smallint] NOT NULL,
		[Vacant] [smallint] NOT NULL,
	 CONSTRAINT [PK_ParkCarPort] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaidDateTime' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add PaidDateTime datetime')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ParkFee' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add ParkFee decimal(10,2)')
    exec ('update Card set ParkFee=0')
    exec ('alter table Card alter column ParkFee decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='TotalPaidFee' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add TotalPaidFee decimal(10,2)')
    exec ('update Card set TotalPaidFee=0')
    exec ('alter table Card alter column TotalPaidFee decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ParkFee' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add ParkFee decimal(10,2)')
    exec ('update CardPaymentRecord set ParkFee=0')
    exec ('alter table CardPaymentRecord alter column ParkFee decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaymentCode' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add PaymentCode tinyint')
    exec ('update CardPaymentRecord set PaymentCode=0xB1')
    exec ('alter table CardPaymentRecord alter column PaymentCode tinyint not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OperatorCardID' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add OperatorCardID nvarchar(50)')
end
go


if not exists (SELECT * FROM dbo.syscolumns WHERE name ='WorkMode' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add WorkMode tinyint')
    exec ('update Park set WorkMode=1')
    exec ('alter table Park alter column WorkMode tinyint not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DeviceType' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add DeviceType tinyint')
    exec ('update Park set DeviceType=0')
    exec ('alter table Park alter column DeviceType tinyint not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Options' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add Options int')
    exec ('update Park set Options=0')
    exec ('alter table Park alter column Options int not null')
end
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='ForceWithCount' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park drop column ForceWithCount')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaymentEventIndex' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add PaymentEventIndex int')
    exec ('update Entrance set PaymentEventIndex=0')
    exec ('alter table Entrance alter column PaymentEventIndex int not null')
end
go

--2013-5-17 �޸�WaitingCommand�������
if exists (SELECT * FROM dbo.syscolumns WHERE name ='ParkID' AND id = OBJECT_ID(N'[dbo].[WaitingCommand]')) 
BEGIN
	exec ('sp_rename N''[dbo].[WaitingCommand].ParkID'',N''EntranceID'',''COLUMN''')
end
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='Action' AND id = OBJECT_ID(N'[dbo].[WaitingCommand]')) 
BEGIN
	exec ('sp_rename N''[dbo].[WaitingCommand].Action'',N''Command'',''COLUMN''')
end
go

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'PK_WaitingCommand') AND type in (N'PK'))
BEGIN
	exec ('alter table WaitingCommand drop constraint PK_WaitingCommand')
end
go

IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'PK_WaitingCommand') AND type in (N'PK'))
BEGIN
	exec ('alter table WaitingCommand add constraint PK_WaitingCommand primary key(EntranceID,Command,CardID)')
end
go

--2013-6-3 Entrance�������������ֶΣ����ڱ��泵��ʶ��һ�����IP����ƵID�ͳ���ʶ�����跢�͵Ŀ�����IP
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlateIP' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add CarPlateIP nvarchar(20)')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='VideoID' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add VideoID int')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlateNotifyIP' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add CarPlateNotifyIP nvarchar(60)')
end
else
BEGIN
	exec ('alter table Entrance alter column CarPlateNotifyIP nvarchar(60)')
end
go

--2013-7-3 OperatorSettleLog ����CashOperatorCard�ֶ����ڱ������Ա���շѵ��ֽ�
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOperatorCard' AND id = OBJECT_ID(N'[dbo].[OperatorSettleLog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add CashOperatorCard decimal(10,2)')
    exec ('update OperatorSettleLog set CashOperatorCard=0')
    exec ('alter table OperatorSettleLog alter column CashOperatorCard decimal(10,2) not null')
end
go

--2013-7-11 Entrance ����CardTypeProperty�ֶ����ڱ�����ư�Ŀ�Ƭ��������
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardTypeProperty' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add CardTypeProperty nvarchar(200)')
end
go

--2013-7-11 ����һ���������ͨ�۷Ѽ�¼�ı�
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YangChenTongLog]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[YangChenTongLog](
		[LogID] [int] IDENTITY(1,1) NOT NULL,
		[LogDateTime] [datetime] NOT NULL,
		[CardID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[LogicalID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[Payment] [decimal](10, 2) NOT NULL,
		[Balance] [decimal](10, 2) NOT NULL,
		[Data] [nvarchar](1000) COLLATE Chinese_PRC_CI_AS NOT NULL,
	 CONSTRAINT [PK_YangChenTongLog] PRIMARY KEY CLUSTERED 
	(
		[LogID] ASC
	)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

--2013-7-24 ��Ƭ������һ�У����ڼ�¼��Ƭ���볡ʱ�䣬���ڼ���ͣ������,��֮ǰ��LASTDATETIME�ֶ������ڼ�¼���һ��ˢ��������ʱ�䣬
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='LastNestParkDateTime' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add LastNestParkDateTime datetime null')
end
go

--2013-8-7 ��Ƭ��ͽɷѼ�¼��ֱ�����һ�У����ڼ�¼��Ƭ��Ϣ�ͽɷѼ�¼�Ƿ����ϴ��������ݿ�
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='UpdateFlag' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add UpdateFlag bit null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='UpdateFlag' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add UpdateFlag bit null')
end
go

--2013-10-6 ��Ƶͼ��ץ�ı�����һ�У����ڼ�¼��Ƶͼ��ץ��ʱ�Ŀ��ţ������������еĿ��Ŵӿ�Ƭ�����¼����л�ȡ
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardID' AND id = OBJECT_ID(N'[dbo].[SnapShot]')) 
BEGIN
	exec ('alter table SnapShot add CardID nvarchar(50)')
	exec ('update SnapShot set CardID=CardEvent.CardID from CardEvent where SnapShot.ShotAt=CardEvent.EventDateTime')
end
go

--2014-01-09 ˢ���¼�������һ�У����ڼ�¼ˢ���¼���¼�Ƿ����ϴ��������ݿ�
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='UpdateFlag' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
BEGIN
	exec ('alter table CardEvent add UpdateFlag bit null')
end
go

--2014-01-09 ��Ƭ������һ�У����ڼ�¼��Ƭ�����ʱ������
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='FreeDateTime' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add FreeDateTime Datetime null')
end
go


--2014-1-9 ����һ��ͨ��·�ڵı�
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoadWay]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoadWay](
	[RoadID] [int] IDENTITY(1,1) NOT NULL,
	[RoadName] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[EntranceIDs] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[Mode] [int] NOT NULL,
 CONSTRAINT [PK_RoadWay] PRIMARY KEY CLUSTERED 
(
	[RoadID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--2014-01-21 ����վ��ͨ��ID����200���ַ�����Ϊ8000���ַ�
if exists (SELECT * FROM dbo.syscolumns WHERE name ='EntranceID' AND id = OBJECT_ID(N'[dbo].[WorkStation]')) 
BEGIN
	exec ('alter table WorkStation alter column EntranceID varchar(8000) null')
end
go


--WaitingCommand����һ���·�״̬�ֶ� 2014-01-26
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Status' AND id = OBJECT_ID(N'[dbo].[WaitingCommand]')) 
BEGIN
	exec ('alter table WaitingCommand add Status tinyint')
    exec ('update WaitingCommand set Status=0')
    exec ('alter table WaitingCommand alter column Status tinyint not null')
end
go


--2014-12-11 WaitingCommand������һ�����������ֶ�
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardIDType' AND id = OBJECT_ID(N'[dbo].[WaitingCommand]')) 
BEGIN
	exec ('alter table WaitingCommand add CardIDType tinyint null')
end
go



--���������Ȩ��¼�� 2014-02-14 Jan�� 2014-05-26 ���ӹ���վ�� Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FreeAuthorizationLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FreeAuthorizationLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[LogDateTime] [datetime] NOT NULL,
	[CardID] [nvarchar](50) NOT NULL,
	[BeginDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[InPark] [bit] NOT NULL,
	[CheckOut] [bit] NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_FreeAuthorizationLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
go

--2014-02-16 entrance������һ������ǹ���ڶ��ֶΣ�����֧��һ��ͨ��������ǹ���龰
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='TicketReaderCOMPort2' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add TicketReaderCOMPort2 tinyint null')
end
go

--2014-02-27 ��ɫ��Ȩ���б�����200���ַ�����Ϊ4000���ַ� Jan
if exists (SELECT * FROM dbo.syscolumns WHERE name ='Permission' AND id = OBJECT_ID(N'[dbo].[Role]')) 
BEGIN
	exec ('alter table Role alter column Permission nvarchar(4000) null')
end
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--���������ɷѻ����˼�¼�� 2014-03-13 Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APMCheckOutRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APMCheckOutRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MID] [nvarchar](50) NOT NULL,
	[CheckOutDateTime] [datetime] NOT NULL,
	[LastDateTime] [datetime] NOT NULL,
	[LastBalance] [decimal](10, 2) NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[ActualAmount] [decimal](10, 2) NOT NULL,
	[Balance] [decimal](10, 2) NOT NULL,
	[PayMoney] [decimal](10, 2) NOT NULL,
	[IncomeMoneny] [decimal](10, 2) NOT NULL,
	[Hundred] [int] NOT NULL,
	[Fifty] [int] NOT NULL,
	[Twenty] [int] NOT NULL,
	[Ten] [int] NOT NULL,
	[Cash] [int] NOT NULL,
	[CashAmount] [decimal](10, 2) NOT NULL,
	[Coin] [int] NOT NULL,
	[APMOperator] [nvarchar](50) NOT NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_APMCheckOutRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
Go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--���������ɷѻ��˿��¼�� 2014-03-17 Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APMRefundRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APMRefundRecord](
	[CardID] [nvarchar](50) NOT NULL,
	[RefundDateTime] [datetime] NOT NULL,
	[MID] [nvarchar](50) NOT NULL,
	[PaymentSerialNumber] [nvarchar](50) NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[CardType] [tinyint] NOT NULL,
	[EnterDateTime] [datetime] NOT NULL,
	[PaidDateTime] [datetime] NULL,
	[ParkFee] [decimal](10, 2) NOT NULL,
	[TotalPaidFee] [decimal](10, 2) NOT NULL,
	[RefundMoney] [decimal](10, 2) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[Memo] [nvarchar](200) NULL,
	[SettleDateTime] [datetime] NULL,
 CONSTRAINT [PK_CardPaymentRefund] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[RefundDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
Go

--2014-03-17 ����Ա�����¼�������շ��˿��� Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOfRefund' AND id = OBJECT_ID(N'[dbo].[OperatorSettleLog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add CashOfRefund decimal(10, 2) null')
end
go

--2014-04-18 ����Ա�����¼�������շ�pos�������� bruce
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOfPOS' AND id = OBJECT_ID(N'[dbo].[OperatorSettleLog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add CashOfPOS decimal(10, 2) null')
end
go

--2014-05-26 �������Ȩ��¼�����ӹ���վ���� Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='StationID' AND id = OBJECT_ID(N'[dbo].[FreeAuthorizationLog]')) 
BEGIN
	exec ('alter table FreeAuthorizationLog add StationID nvarchar(50) null')
end
go


--2014-08-11 ��ͣ����������GPS������ Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='GPS' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add GPS nvarchar(200) null')
end
go


--2014-08-13 ����Ƭ�����Ӳ��������� Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Department' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add Department nvarchar(100) null')
end
go


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


--�������л���¼
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServerSwitchRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ServerSwitchRecord](
	[RecordID] [int] IDENTITY(1,1) NOT NULL,
	[ParkID] [int] NOT NULL,
	[SwitchDateTime] [datetime] NOT NULL,
	[LastDateTime] [datetime] NULL,
	[SwitchServerIP] [nvarchar](50) NOT NULL,
	[LastIP] [nvarchar](50) NULL,
	[SwitchStatus] [tinyint] NOT NULL,
	[LastStatus] [tinyint] NOT NULL,
	[SMSStatus] [tinyint] NOT NULL,
	[Operator] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[Memo] [nvarchar](2000) NULL,
 CONSTRAINT [PK_ServerSwitchRecord] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO


--��CardPaymentRecord������һ����λ
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CurrDiscountHour' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add CurrDiscountHour int')
end
go










/** =======================================================================================================================
 *  ���麣��¡����������ӵ�ͣ����ϵͳ���ɴ��룬�����麣��¡��֧�ļ�������
 *  2014-09-28 
 *  Mark
 **/
 
--2014-7-31 ����һ�����ű�
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dept]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dept](
	[DeptID] [uniqueidentifier] NOT NULL,
	[DeptName] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Descrption] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[ParentID] [uniqueidentifier] NULL,
CONSTRAINT [PK_Dept] PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--2014-08-01 ����վ������һ��������
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DeptID' AND id = OBJECT_ID(N'[dbo].[WorkStation]')) 
BEGIN
	exec ('alter table WorkStation add DeptID uniqueidentifier null')
end
go

--2014-08-01 ����Ա������һ��������
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DeptID' AND id = OBJECT_ID(N'[dbo].[Operator]')) 
BEGIN
	exec ('alter table Operator add DeptID uniqueidentifier null')
end
go

--2014-08-01 �����¼������һ��������
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DeptID' AND id = OBJECT_ID(N'[dbo].[OperatorSettleLog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add DeptID uniqueidentifier null')
end
go

--2014-10-28 ͣ����ϸ���ü�¼������һ������Ա������ Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OperatorDeptID' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add OperatorDeptID uniqueidentifier null')
end
go

--2014-10-28 ͣ����ϸ���ü�¼������һ������վ������ Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='StationDeptID' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add StationDeptID uniqueidentifier null')
end
go

--2014-11-04 ����������������Ŀ����Υ�½�ֹ�������������ı� Jan
--2014-10-24 ����δ�����ټ�¼ Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpeedingRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SpeedingRecord](
	[SpeedingID] [uniqueidentifier] NOT NULL,
	[SpeedingDateTime] [datetime] NOT NULL,
	[PlateNumber] [nvarchar](50) NOT NULL,
	[Place] [nvarchar](50) NOT NULL,
	[Speed] [nvarchar](50) NOT NULL,
	[Photo] [nvarchar](1000) NOT NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_SpeedingRecord] PRIMARY KEY CLUSTERED 
(
	[SpeedingID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--2014-10-24 �����Ѵ����ټ�¼ Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpeedingLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SpeedingLog](
	[SpeedingID] [uniqueidentifier] NOT NULL,
	[SpeedingDateTime] [datetime] NOT NULL,
	[PlateNumber] [nvarchar](50) NOT NULL,
	[Place] [nvarchar](50) NOT NULL,
	[Speed] [nvarchar](50) NOT NULL,
	[Photo] [nvarchar](1000) NOT NULL,
	[Memo] [nvarchar](200) NULL,
	[DealOperatorID] [nvarchar](50) NULL,
	[DealDateTime] [datetime] NULL,
	[DealMemo] [nvarchar](200) NULL,
 CONSTRAINT [PK_SpeedingLog] PRIMARY KEY CLUSTERED 
(
	[SpeedingID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--2014-11-21 ��Ƭ������һ����������
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ListType' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add ListType tinyint not null default 0')
end
go
--2014-11-21 ͣ����������һ������ƾ֤����ģʽ
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ListMode' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add ListMode tinyint not null default 0')
end
go


--2014-12-19 ����Ա���������HandInPOS ����������POS�շѽ��
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='HandInPOS' AND id = OBJECT_ID(N'[dbo].[OperatorSettlelog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add HandInPOS decimal(10,2)')
end
go


--2015-01-22 ��Ƭ���ڱ�����CardType �����ڼ�¼���ڿ�Ƭ�Ŀ�����
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardType' AND id = OBJECT_ID(N'[dbo].[CardDefer]')) 
BEGIN
	exec ('alter table CardDefer add CardType tinyint')
	exec ('update CardDefer set CardType=(select Card.CardType from Card where CardDefer.CardID=Card.CardID)')
end
go

--2015-02-09 �����¼�������Department �����ڼ�¼����
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Department' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
BEGIN
	exec ('alter table CardEvent add Department nvarchar(100) null')
	exec ('update CardEvent set Department=(select Card.Department from Card where CardEvent.CardID=Card.CardID)')
end
go

--2015-05-19 ��Ƶ������VideoType����Ƶ�������ͣ�
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='VideoType' AND id = OBJECT_ID(N'[dbo].[VideoSource]')) 
BEGIN
	exec ('alter table VideoSource add VideoType int null')
end
go

--2015-05-22 ��������������300���Ǻ����ַ�����Ϊ300�������ַ� Jan
if exists (SELECT * FROM dbo.syscolumns WHERE name ='AlarmDescr' AND id = OBJECT_ID(N'[dbo].[Alarm]')) 
BEGIN
	exec ('alter table Alarm alter column AlarmDescr nvarchar(300) null')
end
go

--���ɴ�����������������������У���Ϊ�п��ܴ��������������ӵ��У�ǰ��Ҫ����������ӵ��У��������������ɳɹ�
--���Ӳ����Ƭ���������� 2014-12-11
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
if exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[T_AddCardToDevice]') and OBJECTPROPERTY(id, N'IsTrigger') = 1) 
drop trigger T_AddCardToDevice
go
CREATE TRIGGER T_AddCardToDevice
   ON  dbo.Card
   AFTER INSERT
AS 
BEGIN	
	declare @Status tinyint,
		@cardID nvarchar(50), --������cardid
		@Options int,
		@listType tinyint,
        @carPlate nvarchar(50)

	select @Status=Status,@cardID=CardID,@Options=Options,@listType=ListType,@carPlate=CarPlate from inserted
	
	if @cardID is null --�������Ϊ�յģ�ֱ�ӷ���
	begin
		return
    end	

	if (@Options&0x01)=0 --������ѻ���ʱ�����ߴ���Ŀ�Ƭ������Ҫ�·�������������
	begin
		return --ֱ�ӷ���
	end

	if @listType=1 --����ǳ�������
	begin
		if @carPlate is null or @carPlate='' --������������ĳ��ƺ�Ϊ�գ�ֱ�ӷ���
		begin
			return
		end
	end

	if @Status=1 --״̬1����Ƭ�ѷ���
	begin
		declare @entranceID int,
		@command tinyint
		set @command=0 --������Ƭ���0				

		--ֻ���ѻ�ģʽ�Ŀ��������в���
		declare entranceCur cursor for select a.EntranceID from Entrance a,Park b where a.ParkID=b.ParkID and b.WorkMode=0
		open entranceCur
		fetch next from entranceCur into @entranceID
		while @@fetch_Status<>-1
		begin
			--��ɾ��������֮ǰ������
			delete from WaitingCommand where EntranceID=@entranceID and ( CardID=@cardID or (@listType=1 and CardID=@carPlate))
			insert 	WaitingCommand(EntranceID,Command,CardID,Status,CardIDType) values(@entranceID,@command,@cardID,0,null) --ֱ���ÿ������·�
			fetch next from entranceCur into @entranceID
		end
		close entranceCur
		deallocate entranceCur
	end
END
GO

--�޸�Ӳ����Ƭ���������� 2014-12-11
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
if exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[T_UpdateCardToDevice]') and OBJECTPROPERTY(id, N'IsTrigger') = 1) 
drop trigger T_UpdateCardToDevice
go
CREATE TRIGGER T_UpdateCardToDevice
   ON  dbo.Card
   AFTER UPDATE
AS 
BEGIN
	if update(Status) or update(CardType) or update(CarType) or update(Options) or Update(ActivationDate) or update(ValidDate) or update(AccessID) or update(CarPlate)
	begin
		declare @entranceID int,
		@command tinyint,
		@cardIDType tinyint,
		@wcCardID nvarchar(50), --�·������cardid
		@cardID nvarchar(50), --������cardid
		@Status tinyint,
		@OldOptions int,
		@NewOptions int,
		@oldListType tinyint,
        @oldCarPlate nvarchar(50)
		
		select @OldOptions=Options,@oldListType=ListType,@oldCarPlate=CarPlate from deleted
		select @Status=Status,@cardID=CardID,@NewOptions=Options from inserted

        if @cardID is null --�������Ϊ�յģ�ֱ�ӷ���
		begin
			return
        end

		if (@OldOptions&0x01)=0 --���֮ǰ���ѻ���ʱ�����ߴ����
		begin
			if (@NewOptions&0x01)=0 --��������ѻ���ʱ�����ߴ���ģ�����Ҫ�·�������������
			begin
				return
			end
		end

		if @Status=1 --״̬1����Ƭ�ѷ���
			set @command=1 --�޸Ŀ�Ƭ���1
		else 
			set @command=2 --ɾ����Ƭ���2
		
		set @wcCardID=@cardID	--cardidΪ����	

		if @oldListType=1 --����ǳ�������
		begin
			if @oldCarPlate is null or @oldCarPlate='' --������������ĳ��ƺ�Ϊ�գ�ֱ�ӷ���
			begin
				return
			end

			if @command=2 --�����ɾ�������
			begin
				set @cardIDType=1 --���ó�cardidΪ���ƺ�
				set @wcCardID=@oldCarPlate --cardidΪ���ƺ�
			end
		end
		
		--ֻ���ѻ�ģʽ�Ŀ��������в���
		declare entranceCur cursor for select a.EntranceID from Entrance a,Park b where a.ParkID=b.ParkID and b.WorkMode=0
		open entranceCur
		fetch next from entranceCur into @entranceID
		while @@fetch_Status<>-1
		begin
			--��ɾ��������֮ǰ������
			delete from WaitingCommand where EntranceID=@entranceID and ( CardID=@cardID or (@oldListType=1 and CardID=@oldCarPlate))
			insert 	WaitingCommand(EntranceID,Command,CardID,Status,CardIDType) values(@entranceID,@command,@wcCardID,0,@cardIDType)
			fetch next from entranceCur into @entranceID
		end
		close entranceCur
		deallocate entranceCur
	end

END
GO

--ɾ��Ӳ����Ƭ���������� 2014-12-11
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
if exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[T_DeleteCardToDevice]') and OBJECTPROPERTY(id, N'IsTrigger') = 1) 
drop trigger T_DeleteCardToDevice
go
CREATE TRIGGER T_DeleteCardToDevice
   ON  dbo.Card
   AFTER DELETE
AS 
BEGIN
	declare @entranceID int,
	@command tinyint,
	@wcCardID nvarchar(50), --�·������cardid
	@cardIDType tinyint,
	@cardID nvarchar(50), --������cardid
	@listType tinyint,
    @carPlate nvarchar(50)

	set @command=2 --ɾ����Ƭ���2
	select @cardID=CardID,@listType=ListType,@carPlate=CarPlate from deleted

	if @cardID is null --�������Ϊ�յģ�ֱ�ӷ���
	begin
		return
    end

	set @wcCardID=@cardID	--cardidΪ����	

	if @listType=1 --����ǳ�������
	begin
		if @carPlate is null or @carPlate='' --������������ĳ��ƺ�Ϊ�գ�ֱ�ӷ���
		begin
			return
		end
		
		set @cardIDType=1 --���ó�cardidΪ���ƺ�
		set @wcCardID=@carPlate --cardidΪ���ƺ�
	end	

	--ֻ���ѻ�ģʽ�Ŀ��������в���
	declare entranceCur cursor for select a.EntranceID from Entrance a,Park b where a.ParkID=b.ParkID and b.WorkMode=0
	open entranceCur
	fetch next from entranceCur into @entranceID
	while @@fetch_Status<>-1
	begin
		--��ɾ��������֮ǰ������
		delete from WaitingCommand where EntranceID=@entranceID and ( CardID=@cardID or (@listType=1 and CardID=@carPlate))
		insert 	WaitingCommand(EntranceID,Command,CardID,Status,CardIDType) values(@entranceID,@command,@wcCardID,0,@cardIDType)
		fetch next from entranceCur into @entranceID
	end
	close entranceCur
	deallocate entranceCur
END
GO

--���Entrance����WaitingCommand��ļ���ɾ�� 2014-01-26
if not exists(select * from sysobjects where name='FK_WaitingCommand_Entrance' and xtype='F')
begin
--���WaitingCommand��ɾ����EntranceID����Ȼ���ܳɹ�������
--delete from WaitingCommand where EntranceID=(select b.EntranceID from Entrance a right join WaitingCommand b on a.EntranceID=b.EntranceID where a.EntranceID is null)
--�������WaitingCommand����
delete from WaitingCommand
ALTER TABLE [dbo].[WaitingCommand]  WITH CHECK ADD  CONSTRAINT [FK_WaitingCommand_Entrance] FOREIGN KEY([EntranceID])
REFERENCES [dbo].[Entrance] ([EntranceID])
ON DELETE CASCADE
end
go

-- �������������