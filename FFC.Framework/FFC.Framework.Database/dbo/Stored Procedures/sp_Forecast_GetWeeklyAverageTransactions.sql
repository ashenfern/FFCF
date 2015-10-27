CREATE PROCEDURE [dbo].[sp_Forecast_GetWeeklyAverageTransactions]
AS
	-- GET average Weekly Transactions
	SELECT DATEPART(WEEKDAY, TransactionDate) as Day, COUNT(TD.TransactionID)/datediff(ww,Min(TransactionDate),MAX(TransactionDate)) AS TransactionCount FROM Transactions T
	INNER JOIN TransactionDetails TD on T.TransactionID = TD.TransactionID
	INNER JOIN Products P ON TD.ProductID = P.ProductID
	INNER JOIN Categories C ON P.CategoryID = C.CategoryID
	GROUP BY DATEPART(WEEKDAY, TransactionDate)