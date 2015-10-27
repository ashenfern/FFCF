CREATE TABLE [dbo].[Transactions] (
    [TransactionID]   INT      IDENTITY (1, 1) NOT NULL,
    [CustomerID]      INT      NULL,
    [EmployeeID]      INT      NULL,
    [TransactionDate] DATETIME NULL,
    [BranchID]        INT      NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([TransactionID] ASC),
    CONSTRAINT [FK_Transactions_Branches] FOREIGN KEY ([BranchID]) REFERENCES [dbo].[Branches] ([BranchID]),
    CONSTRAINT [FK_Transactions_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID]),
    CONSTRAINT [FK_Transactions_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

