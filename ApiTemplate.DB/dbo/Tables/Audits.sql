CREATE TABLE [dbo].[Audits] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [CreatedAt] DATETIME2 (7) NOT NULL,
    [TableName] NVARCHAR (50) NOT NULL,
    [AuditType] TINYINT       NOT NULL,
    [UserName]  NVARCHAR (50) NULL,
    [TableId]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

