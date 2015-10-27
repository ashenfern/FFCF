CREATE TABLE [dbo].[Branches] (
    [BranchID]      INT           IDENTITY (1, 1) NOT NULL,
    [BranchName]    VARCHAR (MAX) NULL,
    [BranchAddress] VARCHAR (MAX) NULL,
    [StartTime]     DATETIME      NULL,
    [EndTime]       DATETIME      NULL,
    CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED ([BranchID] ASC)
);

