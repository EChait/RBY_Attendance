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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230815134350_mssql.local_migration_420')
BEGIN
    CREATE TABLE [Enrollment] (
        [Id] int NOT NULL IDENTITY,
        [Teacher] varchar(250) NOT NULL,
        [Subject] varchar(250) NOT NULL,
        [Student] varchar(250) NOT NULL,
        CONSTRAINT [PK_Enrollment] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230815134350_mssql.local_migration_420')
BEGIN
    CREATE TABLE [Semester] (
        [Id] int NOT NULL IDENTITY,
        [ClientId] int NOT NULL,
        [Name] varchar(250) NOT NULL,
        [FromDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [Default] bit NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [Weeks] int NOT NULL,
        CONSTRAINT [PK_Semester] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230815134350_mssql.local_migration_420')
BEGIN
    CREATE TABLE [SemesterTeacherSubject] (
        [Id] int NOT NULL IDENTITY,
        [ClientId] int NOT NULL,
        [SemesterId] int NOT NULL,
        [TeacherId] int NOT NULL,
        [SubjectId] int NOT NULL,
        CONSTRAINT [PK_SemesterTeacherSubject] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230815134350_mssql.local_migration_420')
BEGIN
    CREATE TABLE [StudentAbsense] (
        [Id] int NOT NULL IDENTITY,
        [ClientId] int NOT NULL,
        [StudentId] int NOT NULL,
        [Subject] nvarchar(50) NOT NULL,
        [Teacher] nvarchar(50) NOT NULL,
        [Date] datetime2 NOT NULL,
        [AbsenseType] int NOT NULL,
        [AuditDate] datetime2 NOT NULL,
        [Note] string NOT NULL,
        CONSTRAINT [PK_StudentAbsense] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230815134350_mssql.local_migration_420')
BEGIN
    CREATE TABLE [Subject] (
        [Id] int NOT NULL IDENTITY,
        [Name] varchar(250) NOT NULL,
        [PeriodCount] int NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Subject] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230815134350_mssql.local_migration_420')
BEGIN
    CREATE TABLE [User] (
        [Id] int NOT NULL IDENTITY,
        [ClientId] int NOT NULL,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Title] nvarchar(50) NOT NULL,
        [Email] nvarchar(50) NOT NULL,
        [Password] nvarchar(50) NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230815134350_mssql.local_migration_420')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230815134350_mssql.local_migration_420', N'7.0.0');
END;
GO

COMMIT;
GO

