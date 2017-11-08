IF  NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Nonfraud')
CREATE DATABASE Nonfraud

GO

USE Nonfraud
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Profile')
BEGIN

CREATE TABLE [dbo].[Profile]
(
	[ProfileID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[ProfileName] VARCHAR(20) NOT NULL
)

INSERT INTO Profile (ProfileName) VALUES('Assistant')
INSERT INTO Profile (ProfileName) VALUES('Manager')
INSERT INTO Profile (ProfileName) VALUES('Superintendent')
INSERT INTO Profile (ProfileName) VALUES('Administrator')

END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'User')
BEGIN

CREATE TABLE [dbo].[User]
(
	[UserID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[UserName] VARCHAR(20) NOT NULL,
	[Password] VARCHAR(20) NOT NULL,
	[ProfileID] INT NOT NULL,
	[CreationDate] DATETIME NOT NULL,
	CONSTRAINT [FK_User_Profile] FOREIGN KEY ([ProfileID]) REFERENCES [Profile]([ProfileID])
)

END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Customer')
BEGIN

CREATE TABLE [dbo].[Customer]
(
	[CustomerID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[CustomerName] VARCHAR(20) NOT NULL,
	[CreationDate] DATETIME NOT NULL
)

END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TransactionType')
BEGIN

CREATE TABLE [dbo].[TransactionType]
(
	[TransactionTypeID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[TransactionTypeName] VARCHAR(20) NOT NULL
)

INSERT INTO TransactionType (TransactionTypeName) VALUES('CASH-IN')
INSERT INTO TransactionType (TransactionTypeName) VALUES('CASH-OUT')
INSERT INTO TransactionType (TransactionTypeName) VALUES('DEBIT')
INSERT INTO TransactionType (TransactionTypeName) VALUES('PAYMENT')
INSERT INTO TransactionType (TransactionTypeName) VALUES('TRANSFER')

END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Transaction')
BEGIN

CREATE TABLE [dbo].[Transaction]
(
	[TransactionID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[TransactionTypeID] INT NOT NULL,
	[IsFraud] BIT NOT NULL,
	[IsFlagged] BIT NOT NULL,
	[Amount] NUMERIC(25,4) NOT NULL,
	[CustomerOrigID] INT NOT NULL,
	[CustomerDestID] INT NOT NULL,
	[TransactionDate] DATETIME NOT NULL,
	CONSTRAINT [FK_TransactionType] FOREIGN KEY ([TransactionTypeID]) REFERENCES [TransactionType]([TransactionTypeID]),
	CONSTRAINT [FK_CustomerOrig] FOREIGN KEY ([CustomerOrigID]) REFERENCES [Customer]([CustomerID]),
	CONSTRAINT [FK_CustomerDest] FOREIGN KEY ([CustomerDestID]) REFERENCES [Customer]([CustomerID])
)

END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CustomerBalance')
BEGIN

CREATE TABLE [dbo].[CustomerBalance]
(
	[CustomerBalanceID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[CustomerID] INT NOT NULL,
	[OldBalance] NUMERIC(25,4) NOT NULL,
	[NewBalance] NUMERIC(25,4) NOT NULL,
	CONSTRAINT [FK_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID])
)

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetTransactions')
BEGIN
DROP PROCEDURE GetTransactions
END
GO

CREATE PROCEDURE GetTransactions
AS
	SELECT SELECT tr.TransactionID,
	tr.TransactionDate, 
	trtp.TransactionTypeName AS 'Type',
	tr.Amount, 
	cto.CustomerName AS 'NameOrig', 
	ctbo.OldBalance AS 'OldBalanceOrig', 
	ctbo.NewBalance AS 'NewBalanceOrig',
	ctd.CustomerName AS 'NameDest', 
	ctbd.OldBalance AS 'OldBalanceDest', 
	ctbd.NewBalance AS 'NewBalanceDest',
	tr.IsFraud, 
	tr.IsFlagged
	FROM [Transaction] tr
	INNER JOIN TransactionType trtp ON tr.TransactionTypeID = trtp.TransactionTypeID
	INNER JOIN Customer cto ON cto.CustomerID = tr.CustomerOrigID
	INNER JOIN Customer ctd ON ctd.CustomerID = tr.CustomerDestID
	INNER JOIN CustomerBalance ctbo ON ctbo.CustomerID = tr.CustomerOrigID
	INNER JOIN CustomerBalance ctbd ON ctbd.CustomerID = tr.CustomerDestID
GO
