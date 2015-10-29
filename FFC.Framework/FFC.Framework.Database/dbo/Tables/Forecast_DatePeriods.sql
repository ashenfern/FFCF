CREATE TABLE [dbo].[Forecast_DatePeriods] (
    [DatePeriodID] INT            IDENTITY (1, 1) NOT NULL,
    [DatePeriod]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([DatePeriodID] ASC)
);

