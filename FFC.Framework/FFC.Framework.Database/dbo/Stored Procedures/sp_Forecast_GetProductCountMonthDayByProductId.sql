CREATE PROCEDURE [dbo].[sp_Forecast_GetProductCountMonthDayByProductId]
@ProductId int
AS
	--Month Day Produts
	SELECT P.ProductID,DATEPART(MONTH, TransactionDate) as Month,DATEPART(Day, TransactionDate) as Day, count(TD.TransactionID) as Count FROM Transactions T
	INNER JOIN TransactionDetails TD on T.TransactionID = TD.TransactionID
	INNER JOIN Products P ON TD.ProductID = P.ProductID
	INNER JOIN Categories C ON P.CategoryID = C.CategoryID
	WHERE p.ProductID = @ProductId
	Group BY DATEPART(MONTH, TransactionDate),DATEPART(Day, TransactionDate), P.ProductID
	Order By DATEPART(MONTH, TransactionDate),DATEPART(Day, TransactionDate)