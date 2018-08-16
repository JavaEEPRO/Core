//sql commands for building DB tables for Core project for Touch 1.0

CREATE DATABASE [shtoda];

CREATE TABLE [Shtoda_acts]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[date] [datetime] NOT NULL,
[number] [int] NOT NULL,
[contragent] [NVARCHAR] (150) NOT NULL,
[total] [int],
[description] [NVARCHAR] (240) NOT NULL,
[status] [NVARCHAR] (70) NOT NULL,
[parent] [NVARCHAR] (240),
[typeID] [int] (10),
[isDeleted] [BOOLEAN]
); 

CREATE TABLE [Shtoda_contracts]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[date] [datetime] NOT NULL,
[number] [int] NOT NULL,
[contragent] [NVARCHAR] (150) NOT NULL,
[total] [int],
[description] [NVARCHAR] (240) NOT NULL,
[status] [NVARCHAR] (70) NOT NULL,
[parent] [NVARCHAR] (240),
[typeID] [int] (10),
[isDeleted] [BOOLEAN]
); 

CREATE TABLE [Shtoda_addons]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[date] [datetime] NOT NULL,
[number] [int] NOT NULL,
[contragent] [NVARCHAR] (150) NOT NULL,
[total] [int],
[description] [NVARCHAR] (240) NOT NULL,
[status] [NVARCHAR] (70) NOT NULL,
[parent] [NVARCHAR] (240),
[typeID] [int] (10),
[isDeleted] [BOOLEAN]
); 

CREATE TABLE [Shtoda_invoices]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[date] [datetime] NOT NULL,
[number] [int] NOT NULL,
[contragent] [NVARCHAR] (150) NOT NULL,
[description] [NVARCHAR] (240) NOT NULL,
[status] [NVARCHAR] (70) NOT NULL,
[isDeleted] [BOOLEAN]
);

CREATE TABLE [Shtoda_mails]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[date] [datetime] NOT NULL,
[sender] [NVARCHAR] (240) NOT NULL,
[address] [NVARCHAR] (240) NOT NULL,
[description] [NVARCHAR] (240),
[sendSystem] [NVARCHAR] (150) NOT NULL,
[trackNumber] [int] NOT NULL,
[status] [NVARCHAR] (70) NOT NULL,
[returnTrackNumber] [int],
[returnDeliveryExpected] [datetime],
[isDeleted] [BOOLEAN]
);

CREATE TABLE [Shtoda_contragents]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[name] [NVARCHAR] (240) NOT NULL
);

CREATE TABLE [Shtoda_docTypes]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[name] [NVARCHAR] (240) NOT NULL,
[code] [NVARCHAR] (240) NOT NULL
);

CREATE TABLE [Shtoda_docTypeTemplates]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[typeID] [int] (10),
[parent] [NVARCHAR] (240),
[name] [NVARCHAR] (240) NOT NULL,
[ord] [int]
);

CREATE TABLE [Shtoda_statuses_contracts]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[name] [NVARCHAR] (240) NOT NULL,
[code] [NVARCHAR] (240) NOT NULL,
[color] [NVARCHAR] (50)
);

CREATE TABLE [Shtoda_statuses_invoices]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[name] [NVARCHAR] (240) NOT NULL,
[code] [NVARCHAR] (240) NOT NULL,
[color] [NVARCHAR] (50)
);

CREATE TABLE [Shtoda_statuses_mails]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[name] [NVARCHAR] (240) NOT NULL,
[code] [NVARCHAR] (240) NOT NULL,
[color] [NVARCHAR] (50)
);

INSERT INTO [Shtoda_acts] ([date], [number], [contragent], [total], [description], [status])
VALUES ('12/08/2018','1', 'Test Partner', '0', 'new act: population test','created ok');			    //Shtoda_acts population test

INSERT INTO [Shtoda_contracts] ([date], [number], [contragent], [total], [description], [status])
VALUES ('13/08/2018','1', 'Test Partner', '0', 'new agreement: population test','created ok');			//Shtoda_contracts population test

INSERT INTO [Shtoda_addons] ([date], [number], [contragent], [total], [description], [status])
VALUES ('14/08/2018','1', 'Test Partner', '0', 'new addon: population test','created ok');			  //Shtoda_addons population test

INSERT INTO [Shtoda_invoices] ([date], [number], [contragent], [description], [status])
VALUES ('15/08/2018','1', 'Test Partner', '0', 'new bill: population test','created ok');		    	//Shtoda_invoices population test

INSERT INTO [Shtoda_mails] ([date], [sender], [address], [description], [sendSystem], [trackNumber], [status], [returnTrackNumber], [returnDeliveryExpected] )
VALUES ('16/08/2018', 'Test Sender', 'test address', 'new mail: population test', 'smtp', '0', 'created ok', '0', '17/08/2018');
																																														    	//shtoda_mails population test
