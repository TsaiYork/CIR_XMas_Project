--Create Table
IF OBJECT_ID(N'dbo.Info', N'U') IS NOT NULL
BEGIN
	DROP TABLE dbo.Info
END
CREATE TABLE Info(
	[Id]			INT				PRIMARY KEY IDENTITY	NOT NULL,
	[RandomCode]	CHAR(10)		NOT NULL,
	[PlayerName]	VARCHAR(40)		NOT NULL,
	[Desire]		VARCHAR(40),
	[Tip1]			VARCHAR(100),
	[Tip2]			VARCHAR(100),
	[Tip3]			VARCHAR(100),
	[Apply]			BIT				NOT NULL,
	[Draw]			BIT				NOT NULL ,
	[Receiver]		INT				NOT NULL  );
GO

--Insert Initial Test Data
INSERT INTO dbo.Info ([RandomCode], [PlayerName], [Apply],[Draw],[Receiver]) VALUES ('ABC123', 'TEST', 1, 0, 0);