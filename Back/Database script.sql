/*Lembrar de criar a conexão*/
CREATE DATABASE Ecommerce
GO
USE Ecommerce
GO
CREATE TABLE [dbo].[Carrinho] (
    [Id]   INT  IDENTITY (1, 1) NOT NULL,
    [Data] DATE NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
CREATE TABLE [dbo].[Produto] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Nome]    NVARCHAR (100) NOT NULL,
    [Valor]   FLOAT (53)     NOT NULL,
    [Estoque] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[ProdutoCarrinho] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [Quantidade]  INT NOT NULL,
    [Produto_Id]  INT NULL,
    [Carrinho_Id] INT NULL,
    [Subtotal] FLOAT NULL,	
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([Produto_Id]) REFERENCES [dbo].[Produto] ([Id]),
    FOREIGN KEY ([Carrinho_Id]) REFERENCES [dbo].[Carrinho] ([Id])
);
GO
CREATE TABLE [dbo].[Venda] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Total]       FLOAT (53)    NOT NULL,
    [Pagamento]   NVARCHAR (20) NOT NULL,
    [Carrinho_Id] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([Carrinho_Id]) REFERENCES [dbo].[Carrinho] ([Id])
);
GO
SET IDENTITY_INSERT [dbo].[Produto] ON
INSERT INTO [dbo].[Produto] ([Nome], [Valor], [Estoque]) VALUES (N'Martelo', 10, 10)
INSERT INTO [dbo].[Produto] ([Nome], [Valor], [Estoque]) VALUES (N'Alicate', 15, 10)
INSERT INTO [dbo].[Produto] ([Nome], [Valor], [Estoque]) VALUES (N'Chave de Fenda', 8, 10)
INSERT INTO [dbo].[Produto] ([Nome], [Valor], [Estoque]) VALUES (N'Serrote', 25, 10)
INSERT INTO [dbo].[Produto] ([Nome], [Valor], [Estoque]) VALUES (N'Trena', 12, 10)
SET IDENTITY_INSERT [dbo].[Produto] OFF

