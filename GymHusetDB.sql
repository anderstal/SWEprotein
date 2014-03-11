USE GymHusetDB

GO

CREATE TABLE [tbUser] (
	[iID] INT IDENTITY(1,1) NOT NULL,
	[iRole] INT NOT NULL,
	[iShippingInfo] INT NOT NULL,
	[sUsername] NVARCHAR(50) NOT NULL,
	[sPassword] NVARCHAR(50) NOT NULL,
	[sEmail] NVARCHAR(50) NOT NULL,
	[sTelephone] NVARCHAR (50),
	[iTotalPurchase] INT,
	PRIMARY KEY ([iID])
)

CREATE TABLE [tbRole] ( 
	[iID] INT IDENTITY(1,1) NOT NULL,
	[sNamn] NVARCHAR(50) NOT NULL,
	PRIMARY KEY ([iID])
)

CREATE TABLE [tbShippingInfo] (
	[iID] INT IDENTITY(1,1) NOT NULL,
	[sAddress] NVARCHAR(70) NOT NULL,
	[sPostalNumber] NVARCHAR(50) NOT NULL,
	[sCity] NVARCHAR(50) NOT NULL,
	PRIMARY KEY ([iID])
)

CREATE TABLE [tbProduct] (
	[iID] INT IDENTITY(1,1) NOT NULL,
	[iProductType] INT NOT NULL,
	[sName] NVARCHAR(50) NOT NULL,
	[iPrice] INT NOT NULL,
	[sPicture] NVARCHAR(200),
	[sDescription] NVARCHAR(2000),
	[iStockBalance] INT NOT NULL,
	[iItemsSold] INT NOT NULL,
[iCount] INT NOT NULL,
	PRIMARY KEY ([iID])
)

CREATE TABLE [tbProductType] (
	[iID] INT IDENTITY(1,1) NOT NULL,
	[sName] NVARCHAR(100) NOT NULL,
	PRIMARY KEY ([iID])
)

CREATE TABLE [tbOrder] (
	[iID] INT IDENTITY(1,1) NOT NULL,
	[iUserID] INT NOT NULL,
	[iStatus] INT NOT NULL,
	[iSum] INT,
	[dtOrderDate] DATETIME,
	PRIMARY KEY ([iID])
)

CREATE TABLE [tbStatus] (
	[iID] INT IDENTITY(1,1) NOT NULL,
	[sOrderStatus] NVARCHAR(50) NOT NULL,
	PRIMARY KEY ([iID])
)

CREATE TABLE [tbProductOrder] ( 
	[iID] INT IDENTITY(1,1) NOT NULL,
	[iOrderID] INT NOT NULL,
	[iProductID] INT NOT NULL,
	[iQuantity] INT NOT NULL,
	[iPrice] INT,
	PRIMARY KEY ([iID])
)

GO

ALTER TABLE [tbUser] 
ADD FOREIGN KEY ([iRole]) REFERENCES [tbRole](iID)

ALTER TABLE [tbUser]
ADD FOREIGN KEY ([iShippingInfo]) REFERENCES [tbShippingInfo](iID)

ALTER TABLE [tbProduct]
ADD FOREIGN KEY ([iProductType]) REFERENCES [tbProductType](iID)

ALTER TABLE [tbOrder]
ADD FOREIGN KEY ([iUserID]) REFERENCES [tbUser](iID)

ALTER TABLE [tbOrder]
ADD FOREIGN KEY ([iStatus]) REFERENCES [tbStatus](iID)

ALTER TABLE [tbProductOrder]
ADD FOREIGN KEY ([iOrderID]) REFERENCES [tbOrder](iID)

ALTER TABLE [tbProductOrder]
ADD FOREIGN KEY ([iProductID]) REFERENCES [tbProduct](iID)


GO
