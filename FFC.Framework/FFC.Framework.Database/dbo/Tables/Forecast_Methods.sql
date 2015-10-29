CREATE TABLE [dbo].[Forecast_Methods] (
    [ForecastMethodID]   INT            IDENTITY (1, 1) NOT NULL,
    [ForecastMethod]     NVARCHAR (MAX) NULL,
    [ForecastIdentifier] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ForecastMethodID] ASC)
);

