CREATE TABLE [dbo].[PURefund] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
	[PUOrderId]    BIGINT	      NOT NULL,
    [Created]      DATETIME       NOT NULL,
	[Updated]      DATETIME       NULL,
    [ExtRefundId]  NVARCHAR (100) NOT NULL,
    [RefundId]     NVARCHAR (100) NULL,
    [Status]       NVARCHAR (100) NULL,
    [RefundObject] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_PURefund] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_PURefund_PUOrder] FOREIGN KEY ([PUOrderId]) REFERENCES [PUOrder]([Id])
);
GO

CREATE UNIQUE INDEX [IX_PURefund_ExtRefundId] ON [dbo].[PURefund] ([ExtRefundId])
GO
CREATE INDEX [IX_PURefund_RefundId] ON [dbo].[PURefund] ([RefundId])
GO
