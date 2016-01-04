CREATE PROCEDURE [dbo].[sp_GetBranchesCostBySourceAndDestination]
	@sourceBranchID int,
	@destinationBranchID int
AS
	SELECT Cost
	FROM BranchesCost 
	WHERE SourceBranchID = @sourceBranchID AND DestinationBranchID = @destinationBranchID