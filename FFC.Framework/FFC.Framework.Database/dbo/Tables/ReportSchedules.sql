CREATE TABLE [dbo].[ReportSchedules]
(
	[ReportScheduleId] INT NOT NULL PRIMARY KEY IDENTITY,
	[ReportSubscriptionId] NVARCHAR(MAX) NOT NULL,  
    [ReportId] INT NOT NULL,
	[DeliveryTypeId] INT NOT NULL,  
    [SchedulePeriod] NVARCHAR(MAX) NOT NULL, 
    [ScheduleDay] INT NOT NULL, 
    [ScheduleTime] TIME NOT NULL, 
    [ScheduleDescription] NVARCHAR(MAX) NULL, 
   
    CONSTRAINT [FK_ReportSchedules_ToReports] FOREIGN KEY (ReportId) REFERENCES Reports(ReportId), 
    CONSTRAINT [FK_ReportSchedules_ToFileDeliveryType] FOREIGN KEY (DeliveryTypeId) REFERENCES [DeliveryTypes](DeliveryTypeId)
)
