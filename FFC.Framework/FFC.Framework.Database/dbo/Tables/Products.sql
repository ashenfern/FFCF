CREATE TABLE [dbo].[Products] (
    [ProductID]   INT           IDENTITY (1, 1) NOT NULL,
    [ProductName] NVARCHAR (40) NOT NULL,
    [CategoryID]  INT           NULL,
    [UnitPrice]   MONEY         CONSTRAINT [DF_Products_UnitPrice] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [CK_Products_UnitPrice] CHECK ([UnitPrice]>=(0)),
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID])
);

