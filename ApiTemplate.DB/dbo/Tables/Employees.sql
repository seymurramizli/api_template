CREATE TABLE [dbo].[Employees] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [CreatedAt]    DATETIME2 (7) NOT NULL,
    [UpdatedAt]    DATETIME2 (7) NOT NULL,
    [Status]       TINYINT       NOT NULL,
    [Name]         NVARCHAR (50) NULL,
    [Surname]      NVARCHAR (50) NULL,
    [BirthDate]    DATETIME      NULL,
    [DepartmentId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employees_ToDepartments] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id])
);

