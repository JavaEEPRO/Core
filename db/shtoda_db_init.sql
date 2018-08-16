//sql commands for building DB tables for Core project for Touch 1.0

CREATE DATABASE [shtoda];

CREATE TABLE [shtoda_acts]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[created] [datetime] NOT NULL,
[number] [int] NOT NULL,
[contragent] [NVARCHAR] (150) NOT NULL,
[total] [int],
[remark] [NVARCHAR] (240) NOT NULL,
[status] [NVARCHAR] (70) NOT NULL,
[parent] [NVARCHAR] (240) NOT NULL
); 

CREATE TABLE [shtoda_contracts]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[created] [datetime] NOT NULL,
[number] [int] NOT NULL,
[contragent] [NVARCHAR] (150) NOT NULL,
[total] [int],
[remark] [NVARCHAR] (240) NOT NULL,
[status] [NVARCHAR] (70) NOT NULL,
[parent] [NVARCHAR] (240) NOT NULL
); 

CREATE TABLE [shtoda_statuses_contracts]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[created] [datetime] NOT NULL,
[number] [int] NOT NULL,
[contragent] [NVARCHAR] (150) NOT NULL,
[total] [int],
[remark] [NVARCHAR] (240) NOT NULL,
[status] [NVARCHAR] (70) NOT NULL,
[parent] [NVARCHAR] (240) NOT NULL
); 

CREATE TABLE [shtoda_invoices]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[created] [datetime] NOT NULL,
[number] [int] NOT NULL,
[contragent] [NVARCHAR] (150) NOT NULL,
[remark] [NVARCHAR] (240) NOT NULL,
[status] [NVARCHAR] (70) NOT NULL
);

CREATE TABLE [shtoda_mails]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[created] [datetime] NOT NULL,
[sender] [NVARCHAR] (240) NOT NULL,
[address] [NVARCHAR] (240) NOT NULL,
[description] [NVARCHAR] (240),
[sendSystem] [NVARCHAR] (150) NOT NULL,
[trackNumber] [int] NOT NULL,
[status] [NVARCHAR] (70) NOT NULL,
[returnTrackNumber] [int],
[returnDeliveryExpected] [datetime],
);

INSERT INTO [shtoda_acts] ([created], [number], [contragent], [total], [remark], [status])
VALUES ('12/08/2018','1', 'Test Partner', '0', 'new act: population test','created ok');			    //shtoda_acts population test

INSERT INTO [shtoda_contracts] ([created], [number], [contragent], [total], [remark], [status])
VALUES ('13/08/2018','1', 'Test Partner', '0', 'new agreement: population test','created ok');			//shtoda_contracts population test

INSERT INTO [shtoda_statuses_contracts] ([created], [number], [contragent], [total], [remark], [status])
VALUES ('14/08/2018','1', 'Test Partner', '0', 'new addon: population test','created ok');			  //shtoda_statuses_contracts population test

INSERT INTO [shtoda_invoices] ([created], [number], [contragent], [remark], [status])
VALUES ('15/08/2018','1', 'Test Partner', '0', 'new bill: population test','created ok');		    	//shtoda_invoices population test

INSERT INTO [shtoda_mails] ([created], [sender], [address], [description], [sendSystem], [trackNumber], [status], [returnTrackNumber], [returnDeliveryExpected] )
VALUES ('16/08/2018', 'Test Sender', 'test address', 'new mail: population test', 'smtp', '0', 'created ok', '0', '17/08/2018');
																																														    	//shtoda_mails population test
