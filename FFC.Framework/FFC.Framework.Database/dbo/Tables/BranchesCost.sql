CREATE TABLE [dbo].[BranchesCost]
(
	[SourceBranchID] INT NOT NULL , 
    [DestinationBranchID] INT NOT NULL  , 
    [Cost] DECIMAL NOT NULL, 
    CONSTRAINT [FK_BranchesCost_Branches1] FOREIGN KEY (SourceBranchId) REFERENCES [Branches](BranchID),
    CONSTRAINT [FK_BranchesCost_Branches2] FOREIGN KEY (DestinationBranchID) REFERENCES [Branches](BranchID), 
    CONSTRAINT [PK_BranchesCost] PRIMARY KEY ([DestinationBranchID], [SourceBranchID])
)
