CREATE PROCEDURE [dbo].[sp_Forecast_GetProductCountYearDayByProductId]
@ProductId int
AS
	--Year Day Produts
	SELECT P.ProductID, DATEPART(Year, TransactionDate) as Year,DATEPART(MONTH, TransactionDate) as Month,DATEPART(Day, TransactionDate) as Day, count(TD.TransactionID) as Count FROM Transactions T
	INNER JOIN TransactionDetails TD on T.TransactionID = TD.TransactionID
	INNER JOIN Products P ON TD.ProductID = P.ProductID
	INNER JOIN Categories C ON P.CategoryID = C.CategoryID
	WHERE P.ProductID = @ProductId AND T.BranchID = 1
	Group BY DATEPART(Year, TransactionDate),DATEPART(MONTH, TransactionDate),DATEPART(Day, TransactionDate), P.ProductID
	Order By DATEPART(Year, TransactionDate),DATEPART(MONTH, TransactionDate),DATEPART(Day, TransactionDate)