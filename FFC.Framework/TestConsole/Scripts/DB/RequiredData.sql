USE [FFC]
GO
SET IDENTITY_INSERT [dbo].[Branches] ON 

INSERT [dbo].[Branches] ([BranchID], [BranchName], [BranchAddress], [StartTime], [EndTime]) VALUES (1, N'Moratuwa', N'Moratuwa', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Branches] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [Address], [City], [Region], [PostalCode], [Country], [Phone], [Fax]) VALUES (1, N'Customer111', N'aa', N'aa', N'aaa', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [TitleOfCourtesy], [BirthDate], [HireDate], [Address], [City], [Region], [PostalCode], [Country], [HomePhone], [Extension], [Photo], [Notes], [ReportsTo], [PhotoPath]) VALUES (1, N'aa', N'aaa', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Picture]) VALUES (1, N'Buns', N'Represents Buns', NULL)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Picture]) VALUES (2, N'Pastries', N'Represents Cakes', NULL)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Picture]) VALUES (3, N'Rolls', N'Represents Pastries', NULL)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Picture]) VALUES (4, N'Other', N'Represents Other Items', NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (1, N'Fish Bun', 1, 30.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (2, N'Chicken Bun', 1, 50.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (3, N'Vegetale Bun', 1, 30.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (4, N'Seenisambol Bun', 1, 35.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (5, N'Sausage Bun', 1, 35.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (6, N'Chicken Burger', 1, 75.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (7, N'Fish Burger', 1, 75.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (8, N'Mini Submarine', 1, 80.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (9, N'Tea Bun', 1, 25.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (10, N'Egg Bun', 1, 35.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (11, N'Fish Pastry', 2, 40.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (12, N'Chicken Pastry', 2, 50.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (13, N'Sausage Pastry', 2, 40.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (14, N'Vegetable Pastry', 2, 50.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (15, N'Egg and Ham Pastry', 2, 40.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (16, N'Fish Patty', 2, 40.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (17, N'Fish Roll', 3, 40.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (18, N'Chicken Roll', 3, 50.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (19, N'Egg Roll', 3, 50.0000)
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice]) VALUES (20, N'Vegetable Roll', 3, 40.0000)
SET IDENTITY_INSERT [dbo].[Products] OFF
