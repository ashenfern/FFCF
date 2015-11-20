CREATE TABLE [dbo].[ReportSchedules]
(
	[ReportScheduleId] INT NOT NULL PRIMARY KEY IDENTITY,
	 [ReportSubscriptionId] NVARCHAR(MAX) NOT NULL,  
    [ReportId] INT NOT NULL, 
    [ScheduleType] NVARCHAR(MAX) NOT NULL, 
    [ScheduleDay] INT NOT NULL, 
    [ScheduleTime] TIME NOT NULL, 
    [ScheduleDescription] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_ReportSchedules_Reports] FOREIGN KEY (ReportId) REFERENCES Reports(ReportId)
)
