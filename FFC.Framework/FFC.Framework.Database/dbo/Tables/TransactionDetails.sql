CREATE TABLE [dbo].[TransactionDetails] (
    [TransactionID] INT      NOT NULL,
    [ProductID]     INT      NOT NULL,
    [UnitPrice]     MONEY    CONSTRAINT [DF_Transaction_Details_UnitPrice] DEFAULT ((0)) NOT NULL,
    [Quantity]      SMALLINT CONSTRAINT [DF_Transaction_Details_Quantity] DEFAULT ((1)) NOT NULL,
    [Discount]      REAL     CONSTRAINT [DF_Transaction_Details_Discount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Transaction_Details] PRIMARY KEY CLUSTERED ([TransactionID] ASC, [ProductID] ASC),
    CONSTRAINT [CK_Discount] CHECK ([Discount]>=(0) AND [Discount]<=(1)),
    CONSTRAINT [CK_Quantity] CHECK ([Quantity]>(0)),
    CONSTRAINT [CK_UnitPrice] CHECK ([UnitPrice]>=(0)),
    CONSTRAINT [FK_Transaction_Details_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]),
    CONSTRAINT [FK_Transaction_Details_Transactions] FOREIGN KEY ([TransactionID]) REFERENCES [dbo].[Transactions] ([TransactionID])
);

