USE [BankDB]
GO

BEGIN TRANSACTION
BEGIN TRY
	SET IDENTITY_INSERT [Cities] ON;
	INSERT INTO [dbo].[Cities] (ZipCode, Name) VALUES (3770, 'Riemst');
	INSERT INTO [dbo].[Cities] (Zipcode, Name) VALUES (3700, 'Tongeren');
	INSERT INTO [dbo].[Cities] (Zipcode, Name) VALUES (3740, 'Bilzen');
	INSERT INTO [dbo].[Cities] (Zipcode, Name) VALUES (3600, 'Genk');
	INSERT INTO [dbo].[Customers] ([Name], FirstName, [Address], ZipCode, CellPhone) VALUES ('Willems', 'Marijke', 'Beemdstraat 10', 3770, '0499/12.32.54');
	INSERT INTO [dbo].[Customers] ([Name], FirstName, [Address], ZipCode, CellPhone) VALUES ('Hendrikx', 'Wesley', 'Vaartstraat 31', 3500, '0494/16.54.87');
	INSERT INTO [dbo].[Customers] ([Name], FirstName, [Address], ZipCode, CellPhone) VALUES ('Hermans', 'Kris', 'Overweg 115', 3700, '0476/12.82.15');
	INSERT INTO [dbo].[Accounts] (AccountNumber, Balance, AccountType, CustomerId) VALUES ('123-654651-156', 2500, 1, 1);
	INSERT INTO [dbo].[Accounts] (AccountNumber, Balance, AccountType, CustomerId) VALUES ('351-854321-123', 500, 2, 1);
	INSERT INTO [dbo].[Accounts] (AccountNumber, Balance, AccountType, CustomerId) VALUES ('546-642135-546', 3000, 4, 2);
	INSERT INTO [dbo].[Accounts] (AccountNumber, Balance, AccountType, CustomerId) VALUES ('632-126845-741', 4500, 1, 3);
	COMMIT;
END TRY
BEGIN CATCH
	ROLLBACK;
END CATCH


