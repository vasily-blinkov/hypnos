-- This scripts creates the Hypnos database.

USE [master];

-- Hypnos.
-- USE master; DROP DATABASE Hypnos;
IF DB_ID('Hypnos') IS NULL
BEGIN
	PRINT N'Creating database ''Hypnos''';
	EXEC ('CREATE DATABASE Hypnos COLLATE Cyrillic_General_CI_AS');
END
GO

USE [Hypnos];

-- Data Type: Name.
IF TYPE_ID('dbo.Name') IS NULL
BEGIN
	PRINT N'Creating type ''Name''.';
	CREATE TYPE dbo.Name FROM nvarchar(100);
END
GO

-- Data Type: Description.
IF TYPE_ID('dbo.Description') IS NULL
BEGIN
	PRINT N'Creating type ''Description''.';
	CREATE TYPE dbo.Description FROM nvarchar(MAX);
END
GO

-- Data Type: Flag.
IF TYPE_ID('dbo.Flag') IS NULL
BEGIN
	PRINT N'Creating type ''Flag''.';
	CREATE TYPE dbo.Flag FROM BIT;
END
GO

-- Data Type: Color.
IF TYPE_ID('dbo.Color') IS NULL
BEGIN
	PRINT N'Creating type ''Color''.';
	CREATE TYPE dbo.Color FROM nvarchar(6);
	EXEC sp_addextendedproperty
		@name = N'MS_Description', @value = N'HEX color code without leading #',
		@level0type = N'SCHEMA', @level0name = N'dbo',
		@level1type = N'TYPE', @level1name = N'Color';
END
GO

-- Data Type: Emoji.
IF TYPE_ID('dbo.Emoji') IS NULL
BEGIN
	PRINT N'Creating type ''Emoji''.';
	CREATE TYPE dbo.Emoji FROM nvarchar(1);
END
GO

-- Hypnos.Administration.
IF SCHEMA_ID('Administration') IS NULL
BEGIN
	PRINT N'Creating schema ''Administration''.';
	EXEC (N'CREATE SCHEMA Administration');
END
GO

-- Hypnos.Administration.User.
IF OBJECT_ID('Administration.User') IS NULL
BEGIN
	PRINT N'Creating table ''User''.'
	CREATE TABLE Administration.[User] (
		ID smallint IDENTITY(-32768, 1) PRIMARY KEY NOT NULL,
		FullName Name NOT NULL,
		LoginName Name NOT NULL/* UNIQUE*/,
		PasswordHash nvarchar(128) NOT NULL,
		Description Description,
		CreatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		CreatedDate datetime DEFAULT GETDATE() NOT NULL,
		UpdatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		UpdatedDate datetime DEFAULT GETDATE() NOT NULL,
		IsDeleted Flag DEFAULT 0 NOT NULL
	);
	EXEC sp_addextendedproperty
		@name = N'MS_Description', @value = N'SHA2-512',
		@level0type = N'SCHEMA',   @level0name = N'Administration',
		@level1type = N'TABLE',    @level1name = N'User',
		@level2type = N'COLUMN',   @level2name = N'PasswordHash';
END
GO

-- Hypnos.Administration.User: seed.
IF NOT EXISTS(SELECT * FROM Administration.[User] u WHERE u.ID = -32768)
BEGIN
	PRINT 'Creating user ''seed'' (password: seed).';
	DECLARE @password_salt nvarchar(20) = N'woTdzTfu5VUxUjtnr8fJ';
	SET IDENTITY_INSERT Administration.[User] ON;
	INSERT INTO Administration.[User](ID, FullName, LoginName, PasswordHash, CreatedBy, UpdatedBy)
	VALUES(
		-32768, N'Seed', N'seed',
		CONVERT(nvarchar(128), HashBytes('sha2_512', @password_salt + 'seed'), 2),
		-32768, -32768
	);
	SET IDENTITY_INSERT Administration.[User] OFF;
END
GO


-- Hypnos.Administration.Role.
IF OBJECT_ID('Administration.Role') IS NULL
BEGIN
	PRINT N'Creating table ''Role''.'
	CREATE TABLE Administration.[Role] (
		ID smallint IDENTITY(-32768, 1) PRIMARY KEY NOT NULL,
		Name Name NOT NULL,
		Description Description,
		CreatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		CreatedDate datetime DEFAULT GETDATE() NOT NULL,
		UpdatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		UpdatedDate datetime DEFAULT GETDATE() NOT NULL,
		IsDeleted Flag DEFAULT 0 NOT NULL
	);
END
GO

-- Hypnos.Administration.Role: Администратор.
IF NOT EXISTS(SELECT * FROM Administration.[Role] r WHERE r.ID = -32768)
BEGIN
	PRINT 'Creating role ''Администратор''.';
	SET IDENTITY_INSERT Administration.[Role] ON;
	INSERT INTO Administration.[Role](ID, Name, CreatedBy, UpdatedBy)
	VALUES(-32768, N'Администратор', -32768, -32768);
	SET IDENTITY_INSERT Administration.[User] OFF;
END
GO

-- Hypnos.Administration.Role: Руководитель.
IF NOT EXISTS(SELECT * FROM Administration.[Role] u WHERE u.ID = -32767)
BEGIN
	PRINT 'Creating role ''Руководитель''.';
	SET IDENTITY_INSERT Administration.[Role] ON;
	INSERT INTO Administration.[Role](ID, Name, CreatedBy, UpdatedBy)
	VALUES(-32767, N'Руководитель', -32768, -32768);
	SET IDENTITY_INSERT Administration.[User] OFF;
END
GO

-- Hypnos.Administration.Role: Специалист.
IF NOT EXISTS(SELECT * FROM Administration.[Role] u WHERE u.ID = -32766)
BEGIN
	PRINT 'Creating role ''Специалист''.';
	SET IDENTITY_INSERT Administration.[Role] ON;
	INSERT INTO Administration.[Role](ID, Name, CreatedBy, UpdatedBy)
	VALUES(-32766, N'Специалист', -32768, -32768);
	SET IDENTITY_INSERT Administration.[User] OFF;
END
GO

-- Hypnos.Administration.UserRole.
IF OBJECT_ID('Administration.UserRole') IS NULL
BEGIN
	PRINT N'Creating table ''UserRole''.'
	CREATE TABLE Administration.UserRole (
		UserID smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		RoleID smallint FOREIGN KEY REFERENCES Administration.[Role](ID) NOT NULL,
		CreatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		CreatedDate datetime DEFAULT GETDATE() NOT NULL,
		UpdatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		UpdatedDate datetime DEFAULT GETDATE() NOT NULL,
		IsDeleted Flag DEFAULT 0 NOT NULL,
		PRIMARY KEY (UserID, RoleID)
	);
END
GO

-- Granting role Администратор to user 'seed'.
IF NOT EXISTS(SELECT * FROM Administration.UserRole ur WHERE ur.UserID = -32768 AND ur.RoleID = -32768)
BEGIN
	PRINT 'Granting role ''Администратор'' to user ''seed''.'
	INSERT INTO Administration.UserRole(UserID, RoleID, CreatedBy, UpdatedBy)
	VALUES(-32768, -32768, -32768, -32768);
END
GO

-- Hypnos.Management.
IF SCHEMA_ID('Management') IS NULL
BEGIN
	PRINT N'Creating schema ''Management''.';
	EXEC (N'CREATE SCHEMA Management');
END
GO

-- Hypnos.Management.Workflow.
IF OBJECT_ID('Management.Workflow') IS NULL
BEGIN
	PRINT N'Creating table ''Workflow''.'
	CREATE TABLE Management.Workflow (
		ID smallint IDENTITY(-32768, 1) PRIMARY KEY NOT NULL,
		Name Name NOT NULL,
		Description Description,
		Color Color DEFAULT N'f026a5' NOT NULL,
		CreatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		CreatedDate datetime DEFAULT GETDATE() NOT NULL,
		UpdatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		UpdatedDate datetime DEFAULT GETDATE() NOT NULL,
		IsDeleted Flag DEFAULT 0 NOT NULL
	);
END
GO

-- Hypnos.Management.WorkflowItem.
IF OBJECT_ID('Management.WorkflowItem') IS NULL
BEGIN
	PRINT N'Creating table ''WorkflowItem''.'
	CREATE TABLE Management.WorkflowItem (
		ID smallint IDENTITY(-32768, 1) PRIMARY KEY NOT NULL,
		WorkflowID smallint FOREIGN KEY REFERENCES Management.Workflow(ID) NOT NULL,
		ParentWorkflowItemID smallint FOREIGN KEY REFERENCES Management.WorkflowItem(ID),
		Name Name NOT NULL,
		Description Description,
		Emoji Emoji DEFAULT NCHAR(0x26D3) NOT NULL,
		CreatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		CreatedDate datetime DEFAULT GETDATE() NOT NULL,
		UpdatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		UpdatedDate datetime DEFAULT GETDATE() NOT NULL,
		IsDeleted Flag DEFAULT 0 NOT NULL
	);
END
GO

-- Hypnos.Management.Status.
IF OBJECT_ID('Management.Status') IS NULL
BEGIN
	PRINT N'Creating table ''Status''.'
	CREATE TABLE Management.Status (
		ID smallint IDENTITY(-32768, 1) PRIMARY KEY NOT NULL,
		Name Name NOT NULL,
		Description Description,
		CreatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		CreatedDate datetime DEFAULT GETDATE() NOT NULL,
		UpdatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		UpdatedDate datetime DEFAULT GETDATE() NOT NULL,
		IsDeleted Flag DEFAULT 0 NOT NULL
	);
END
GO

-- Hypnos.Management.WorkflowItemStatus.
IF OBJECT_ID('Management.WorkflowItemStatus') IS NULL
BEGIN
	PRINT N'Creating table ''WorkflowItemStatus''.'
	CREATE TABLE Management.WorkflowItemStatus (
		WorkflowItemID smallint FOREIGN KEY REFERENCES Management.WorkflowItem(ID) NOT NULL,
		StatusID smallint FOREIGN KEY REFERENCES Management.Status(ID) NOT NULL,
		Description Description,
		CreatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		CreatedDate datetime DEFAULT GETDATE() NOT NULL,
		UpdatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		UpdatedDate datetime DEFAULT GETDATE() NOT NULL,
		IsDeleted Flag DEFAULT 0 NOT NULL,
		PRIMARY KEY (WorkflowItemID, StatusID)
	);
END
GO

-- Hypnos.Workload.
IF SCHEMA_ID('Workload') IS NULL
BEGIN
	PRINT N'Creating schema ''Workload''.';
	EXEC (N'CREATE SCHEMA Workload');
END
GO

-- Hypnos.Workload.WorkItem.
IF OBJECT_ID('Workload.WorkItem') IS NULL
BEGIN
	PRINT N'Creating table ''WorkItem''.'
	CREATE TABLE Workload.WorkItem (
		ID smallint IDENTITY(-32768, 1) PRIMARY KEY NOT NULL,
		WorkflowItemID smallint FOREIGN KEY REFERENCES Management.WorkflowItem(ID) NOT NULL,
		ParentWorkItemID smallint FOREIGN KEY REFERENCES Workload.WorkItem(ID),
		StatusID smallint FOREIGN KEY REFERENCES Management.Status(ID) NOT NULL,
		AssigneeID smallint FOREIGN KEY REFERENCES Administration.[User](ID),
		Name Name NOT NULL,
		Description Description,
		CreatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		CreatedDate datetime DEFAULT GETDATE() NOT NULL,
		UpdatedBy smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		UpdatedDate datetime DEFAULT GETDATE() NOT NULL,
		IsDeleted Flag DEFAULT 0 NOT NULL
	);
END
GO

-- Hypnos.Auth.
IF SCHEMA_ID('Auth') IS NULL
BEGIN
	PRINT N'Creating schema ''Auth''.';
	EXEC (N'CREATE SCHEMA Auth');
END
GO

-- Hypnos.Auth.Session.
IF OBJECT_ID('Auth.Session') IS NULL
BEGIN
	PRINT N'Creating table ''Session''.'
	CREATE TABLE Auth.Session (
		Token nvarchar(128) PRIMARY KEY NOT NULL,
		UserID smallint FOREIGN KEY REFERENCES Administration.[User](ID) NOT NULL,
		UpdatedDate datetime DEFAULT GETDATE() NOT NULL
	);
END
GO

-- Function: Hypnos.Auth.DoesLoginExist.
PRINT N'Creating of altering function ''DoesLoginExist''';
CREATE OR ALTER FUNCTION Auth.DoesLoginExist(@login_name Name) RETURNS bit
BEGIN
	DECLARE @exists bit;
	SELECT @exists = IIF(COUNT(1) = 1, 1, 0) FROM Administration.[User] u
		WHERE u.LoginName = @login_name;
	RETURN @exists;
END
GO

-- Procedure: Hypnos.Auth.Authenticate.
PRINT N'Creating of altering function ''Authenticate''';
CREATE OR ALTER PROCEDURE Auth.Authenticate 
	@login_name Name,
	@password_hash nvarchar(128),
	@token nvarchar(128) OUTPUT
AS BEGIN
	SET NOCOUNT ON; -- for output parameters to be returned to outside
	DECLARE @expected_password_hash nvarchar(128);
	DECLARE @user_id smallint;
	SELECT @expected_password_hash = u.PasswordHash, @user_id = u.ID
		FROM Administration.[User] u WHERE u.LoginName = @login_name;
	IF @expected_password_hash IS NOT NULL AND @expected_password_hash = @password_hash
	BEGIN
		DECLARE @token_salt nvarchar(20) = N'CjWvXV7ZXtHDPyH8y4LV';
		DECLARE @date datetime = GETDATE();
		SET @token = CONVERT(nvarchar(128), HashBytes('SHA2_512',
			@token_salt
			+ CONVERT(nvarchar(6), @user_id)
			+ CONVERT(nvarchar(26), @date, 9)), 2);
		INSERT INTO Auth.[Session] (Token, UserID, UpdatedDate)
			VALUES (@token, @user_id, @date);
	END
END
GO

-- Procedure: Hypnos.Auth.LogOut.
PRINT N'Creating of altering function ''LogOut''';
CREATE OR ALTER PROCEDURE Auth.LogOut
	@token nvarchar(128)
AS BEGIN
	DELETE FROM Auth.[Session] WHERE Token = @token;
END
GO

/*
-- authentication 
print N'';
declare @password_hash nvarchar(128);
declare @result nvarchar(128);
select @password_hash = CONVERT(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'seed'), 2);
exec auth.authenticate
	@login_name = N'seed',
	@password_hash = @password_hash,
	--@password_hash = N'wrong',
	@token = @result output;
print N'Token: ' + @result + N'.';

-- logging out
exec auth.logout @token = N'1B2DD975736BAD0C6062DD09A0626D5D13E5B0DEFBA41A6D2B6B38B197CD494A58D9212EADA3BF06B9DEC296A1B7CEF852E649EA7CE3A952FE75D4A3C23E0676'
*/





























