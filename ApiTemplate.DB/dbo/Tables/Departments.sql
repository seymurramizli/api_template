CREATE TABLE [dbo].[Departments] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [CreatedAt] DATETIME2 (7) NOT NULL,
    [UpdatedAt] DATETIME2 (7) NOT NULL,
    [Status]    TINYINT       NOT NULL,
    [Name]      NCHAR (10)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

