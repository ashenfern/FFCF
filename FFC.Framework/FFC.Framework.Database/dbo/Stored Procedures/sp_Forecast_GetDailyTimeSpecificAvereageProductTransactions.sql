CREATE PROCEDURE [dbo].[sp_Forecast_GetDailyTimeSpecificAvereageProductTransactions]
	
AS
	SELECT DATEPART(WEEKDAY, TransactionDate) as Day,P.ProductID, COUNT(TD.TransactionID)/COUNT(DISTINCT Day(T.TransactionDate)) AS TransactionCount FROM Transactions T
	INNER JOIN TransactionDetails TD on T.TransactionID = TD.TransactionID
	INNER JOIN Products P ON TD.ProductID = P.ProductID
	INNER JOIN Categories C ON P.CategoryID = C.CategoryID
	WHERE   (DATEPART(hh,TransactionDate) >= 14 AND DATEPART(hh,TransactionDate) <= 18 )
	GROUP BY DATEPART(WEEKDAY, TransactionDate), P.ProductID
	ORDER BY P.ProductID