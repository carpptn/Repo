CREATE TABLE [dbo].[PUCommLog] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [PUOrderId]		  BIGINT         NOT NULL,
	[RequestType]     NVARCHAR(100)  NULL,
    [RequestDate]     DATETIME       NULL,
    [RequestContent]  NVARCHAR (MAX) NULL,
	[ResponseType]    NVARCHAR(100)  NULL,
    [ResponseDate]    DATETIME       NULL,
    [ResponseContent] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_PUCommLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PUCommLog_PUOrder] FOREIGN KEY ([PUOrderId]) REFERENCES [dbo].[PUOrder] ([Id])
);

