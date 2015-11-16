CREATE TABLE [dbo].[Reports]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Path] NVARCHAR(MAX) NULL, 
    [FileName] NVARCHAR(MAX) NULL 
)
