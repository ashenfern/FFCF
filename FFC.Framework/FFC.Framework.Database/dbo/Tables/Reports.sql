CREATE TABLE [dbo].[Reports]
(
	[ReportId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ReportName] NVARCHAR(MAX) NULL, 
    [ReportDescription] NVARCHAR(MAX) NULL, 
    [ReportPath] NVARCHAR(MAX) NULL, 
    [ReportFileName] NVARCHAR(MAX) NULL 
)
