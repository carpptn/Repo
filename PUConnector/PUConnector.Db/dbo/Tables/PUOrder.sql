CREATE TABLE [dbo].[PUOrder] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Created]     DATETIME       NOT NULL,
	[Updated]     DATETIME       NULL,
    [ExtOrderId]  NVARCHAR (100) NOT NULL,
    [OrderId]     NVARCHAR (100) NULL,
    [Status]      NVARCHAR (100) NULL,
    [OrderObject] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_PUOrder] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE UNIQUE INDEX [IX_PUOrder_ExtOrderId] ON [dbo].[PUOrder] ([ExtOrderId])
GO
CREATE INDEX [IX_PUOrder_OrderId] ON [dbo].[PUOrder] ([OrderId])
GO
