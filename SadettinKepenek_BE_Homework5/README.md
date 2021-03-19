##  SQL Server Backup
Ödevde Adventureworks 2017 kullanmak durumunda kaldım,2019 versiyonunda problem yaşadım.Şurdaki linkten indirebilirsiniz : https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2017.bak

## SP
Ödevde kullanmak üzere bir tane SP yazdım.Bunu da aşağıya yazıyorum,execute ederseniz sevinirim.

```` sql
USE [AdventureWorks2017]
GO
/****** Object:  StoredProcedure [dbo].[InsertUnitMeasure]    Script Date: 26.02.2021 19:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUnitMeasure] 
	-- Add the parameters for the stored procedure here
	@UnitMeasureCode NCHAR(3),
	@Name NVARCHAR(50),
	@ModifiedDate DATETIME
AS
BEGIN
	 INSERT INTO Production.UnitMeasure (UnitMeasureCode,Name,ModifiedDate) VALUES (@UnitMeasureCode,@Name,@ModifiedDate);
END

````

## Notlar
- Id listesi kullandığım yerlerde, verdiğim Id, Db'de yoksa koddan Id'leri değiştirmenizi rica ediyorum.
