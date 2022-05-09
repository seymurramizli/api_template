CREATE TABLE [dbo].[AuditDetails] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]  DATETIME2 (7)  NOT NULL,
    [AuditId]    INT            NOT NULL,
    [ColumnName] NVARCHAR (50)  NOT NULL,
    [OldValue]   NVARCHAR (500) NULL,
    [NewValue]   NVARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditDetails_ToAudits] FOREIGN KEY ([AuditId]) REFERENCES [dbo].[Audits] ([Id])
);

