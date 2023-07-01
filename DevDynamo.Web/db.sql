IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230624050405_Update001')
BEGIN
    CREATE TABLE [Projects] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230624050405_Update001')
BEGIN
    CREATE TABLE [Tickets] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(100) NOT NULL,
        [Description] nvarchar(max) NULL,
        [ProjectId] uniqueidentifier NULL,
        CONSTRAINT [PK_Tickets] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tickets_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230624050405_Update001')
BEGIN
    CREATE TABLE [WorkflowSteps] (
        [Id] int NOT NULL IDENTITY,
        [FromStatus] nvarchar(50) NOT NULL,
        [ToStatus] nvarchar(50) NOT NULL,
        [Action] nvarchar(50) NOT NULL,
        [ProjectId] uniqueidentifier NULL,
        CONSTRAINT [PK_WorkflowSteps] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_WorkflowSteps_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230624050405_Update001')
BEGIN
    CREATE INDEX [IX_Tickets_ProjectId] ON [Tickets] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230624050405_Update001')
BEGIN
    CREATE INDEX [IX_WorkflowSteps_ProjectId] ON [WorkflowSteps] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230624050405_Update001')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230624050405_Update001', N'6.0.16');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230624063929_DD-001')
BEGIN
    ALTER TABLE [Tickets] ADD [Status] nvarchar(50) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230624063929_DD-001')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230624063929_DD-001', N'6.0.16');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230629034650_Update002')
BEGIN
    ALTER TABLE [Projects] ADD [TemplateName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230629034650_Update002')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230629034650_Update002', N'6.0.16');
END;
GO

COMMIT;
GO

