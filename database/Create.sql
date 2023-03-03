-- This scripts creates the Hypnos database.
-- To populate the database with sample data, execute Sample.sql.

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

-- To prevent unauthorized access to data in the tables, I create a role allowing only to execute stored procedures.
IF DATABASE_PRINCIPAL_ID('executor') IS NULL
BEGIN
	PRINT N'Setting up role ''executor''.';
	CREATE ROLE executor;
	GRANT EXECUTE TO executor;
END

-- TODO: CREATE LOGIN executor WITH PASSWORD = N'Ver$1l0Ff';
-- TODO: CREATE USER executor FOR LOGIN executor;
-- TODO: connectionString="Persist Security Info=False;User ID=executor;Password=Ver$1l0Ff;Initial Catalog=Hypnos;Server=localhost;"

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
/*
USE master; DROP DATABASE Hypnos;
*/
IF OBJECT_ID('Administration.User') IS NULL
BEGIN
	PRINT N'Creating table ''User''.'
	CREATE TABLE Administration.[User] (
		ID smallint IDENTITY(-32768, 1) PRIMARY KEY NOT NULL,
		FullName Name NOT NULL,
		LoginName Name NOT NULL UNIQUE,
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

-- IX_User_FullName (Hypnos.Administration).
IF NOT EXISTS(SELECT 1 FROM sys.indexes i WHERE i.name = N'IX_User_FullName')
BEGIN
	PRINT 'Creating index ''IX_User_FullName''.';
	CREATE NONCLUSTERED INDEX IX_User_FullName
		ON Administration.[User] (FullName);
END
GO

-- Hypnos.Administration.User: seed.
/*
SELECT Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '1'), 2);
*/
IF NOT EXISTS(SELECT * FROM Administration.[User] u WHERE u.ID = -32768)
BEGIN
	PRINT 'Creating user ''seed'' (password: seed).';
	DECLARE @password_salt nvarchar(20) = N'woTdzTfu5VUxUjtnr8fJ';
	INSERT INTO Administration.[User](FullName, LoginName, PasswordHash, CreatedBy, UpdatedBy)
	VALUES(
		N'Seed', N'seed',
		CONVERT(nvarchar(128), HashBytes('sha2_512', @password_salt + 'seed'), 2),
		-32768, -32768
	);
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
	DECLARE @user_id smallint;
	SELECT @user_id = u.ID FROM Administration.[User] u WHERE u.LoginName = N'seed';
	INSERT INTO Administration.[Role](Name, Description, CreatedBy, UpdatedBy)
		VALUES(N'Администратор', N'Управление учётными данными пользователей', @user_id, @user_id);
END
GO

-- Hypnos.Administration.Role: Руководитель.
IF NOT EXISTS(SELECT * FROM Administration.[Role] u WHERE u.ID = -32767)
BEGIN
	PRINT 'Creating role ''Руководитель''.';
	DECLARE @user_id smallint;
	SELECT @user_id = u.ID FROM Administration.[User] u WHERE u.LoginName = N'seed';
	INSERT INTO Administration.[Role](Name, Description, CreatedBy, UpdatedBy)
		VALUES(N'Руководитель', N'Управление рабочими процессами', @user_id, @user_id);
END
GO

-- Hypnos.Administration.Role: Специалист.
IF NOT EXISTS(SELECT * FROM Administration.[Role] u WHERE u.ID = -32766)
BEGIN
	PRINT 'Creating role ''Специалист''.';
	DECLARE @user_id smallint;
	SELECT @user_id = u.ID FROM Administration.[User] u WHERE u.LoginName = N'seed';
	INSERT INTO Administration.[Role](Name, Description, CreatedBy, UpdatedBy)
		VALUES(N'Специалист', N'Ведение рабочих нагрузок', @user_id, @user_id);
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
PRINT N'Creating or altering function ''DoesLoginExist''';
CREATE OR ALTER FUNCTION Auth.DoesLoginExist(@login_name Name) RETURNS bit
BEGIN
	DECLARE @exists bit;
	SELECT @exists = IIF(COUNT(1) = 1, 1, 0) FROM Administration.[User] u
		WHERE u.LoginName = @login_name;
	RETURN @exists;
END
GO

-- Procedure: Hypnos.Auth.Authenticate.
/*
DECLARE @password_hash nvarchar(128) = Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'2'), 2),
	@token nvarchar(128),
	@user_id smallint;
EXEC Auth.Authenticate @login_name = N'sa1', @password_hash = @password_hash, @token = @token OUTPUT, @user_id = @user_id OUTPUT;
PRINT N'
User ID: ' + IIF(@user_id IS NULL, N'<not found>', Convert(nvarchar(6), @user_id)) + N'
Token: ' + ISNULL(@token, N'<unauthorized>');
*/
PRINT N'Creating or altering procedure ''Authenticate''';
CREATE OR ALTER PROCEDURE Auth.Authenticate 
	@login_name Name,
	@password_hash nvarchar(128),
	@token nvarchar(128) OUTPUT,
	@user_id smallint OUTPUT
AS BEGIN
	SET NOCOUNT ON; -- for output parameters to be returned to outside
	DECLARE @expected_password_hash nvarchar(128);
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
PRINT N'Creating or altering procedure ''LogOut''';
CREATE OR ALTER PROCEDURE Auth.LogOut
	@token nvarchar(128)
AS BEGIN
	DELETE FROM Auth.[Session] WHERE Token = @token;
END
GO

-- Procedure: Hypnos.Auth.CleanupSessions.
-- Removes all sessions older than 2 hours.
PRINT N'Creating or altering procedure ''CleanupSessions''';
CREATE OR ALTER PROCEDURE Auth.CleanupSessions
AS BEGIN
	DELETE FROM Auth.[Session] WHERE DATEADD(hour, 2, UpdatedDate) < GETDATE();
END
GO

-- Procedure: Hypnos.Auth.ValidateToken.
/*
EXEC Auth.ValidateToken
	@token = N'8D19B063F914ECA763ED1A42D7CFEB8C2989E232A5B46F2C3958B6973DAA8FCD4E0F223DF9D60793D884508DC8EB51B85E521E2617ED6DA721FA4BF5D74FF6B5';
*/
PRINT N'Creating or altering procedure ''ValidateToken''';
CREATE OR ALTER PROCEDURE Auth.ValidateToken
	@token nvarchar(128)
AS BEGIN
	DECLARE @count smallint;
	SELECT @count = COUNT(1) FROM Auth.[Session] s
		WHERE s.Token = @token AND DATEADD(hour, 2, s.UpdatedDate) >= GETDATE();
	IF @count = 1
		UPDATE Auth.[Session] SET UpdatedDate = GETDATE() WHERE Token = @token;
	ELSE
	BEGIN
		DELETE FROM Auth.[Session] WHERE Token = @token;
		THROW 51000, N'Ваша сессия истекла, пройдите повторную аутентификацию', 1;
	END
END
GO

-- Procedure: Hypnos.Administration.GetRoles.
/*
EXEC Administration.GetRoles
	@token = N'948A9634B3539AA83D0C22195614BC8F115B54908CC5A582911798C6D2E7B2656459657AE4FC0D0179DA6EEA5A258BD3E3C97D616895DF5C889FCD3C75143253';
EXEC Administration.GetRoles
	--@user_id = -32768,
	@user_id = -32767,
	@token = N'948A9634B3539AA83D0C22195614BC8F115B54908CC5A582911798C6D2E7B2656459657AE4FC0D0179DA6EEA5A258BD3E3C97D616895DF5C889FCD3C75143253';
*/
PRINT N'Creating or altering procedure ''GetRoles''';
CREATE OR ALTER PROCEDURE Administration.GetRoles
	@user_id smallint = NULL,
	@token nvarchar(128)
AS BEGIN
	EXEC Auth.ValidateToken @token = @token;
	SET NOCOUNT ON;
	IF @user_id IS NOT NULL
		-- Select roles for a specific user.
		SELECT ur.RoleID ID, r.Name Name, r.Description Description
		FROM Administration.UserRole ur
		JOIN Administration.Role r ON r.ID = ur.RoleID
		WHERE ur.UserID = @user_id AND r.IsDeleted = 0;
	ELSE
		-- Select all roles available.
	SELECT r.ID ID, r.Name Name, r.Description Description
		FROM Administration.[Role] r
		WHERE r.IsDeleted = 0;
END
GO

-- Procedure: Hypnos.Administration.GetUsers
/*
DECLARE @token nvarchar(128), @user_id smallint;
EXEC Auth.Authenticate
	@login_name = N'sa',
	@password_hash = N'117F2E2FCCF33CBF62C17363B4AE91521C35A60320D09642D0954993FA5B712D78F6781AC4C8A6FB8418A7F440E7E5BBCF4DE001088E0D4E7DB4599916BC88F0',
	@token = @token OUTPUT,
	@user_id = @user_id OUTPUT;
PRINT N'Token: ' + @token;
EXEC Administration.GetUsers
	@token = N'2EC22F23DF896B3A391CEF6AF511D076EF4C5989F4DCA1B397EDAE2CB28CE2F8112ADFCA6025D5A4C7DD2DA7F89D0044E35E6DF86C31DD1A8FCAB98A942C09AB',
	@query = N's';
 */
PRINT N'Creating or altering procedure ''GetUsers''';
CREATE OR ALTER PROCEDURE Administration.GetUsers
	@query Name = NULL,
	@token nvarchar(128)
AS BEGIN
	EXEC Auth.ValidateToken @token = @token;
	SELECT u.ID, u.FullName, u.LoginName FROM Administration.[User] u
		WHERE @query IS NULL OR u.LoginName LIKE N'%' + @query + '%' OR u.FullName LIKE N'%' + @query + '%';
END
GO

-- Procedure: Hypnos.Administration.GetSignleUser
/*
EXEC Administration.GetSignleUser
	--@user_id = -32768,
	@user_id = -32767,
	@token = N'75771CC84FFAFCCAFEEB8F4D5C815403EBD91754020F0F72ABE247A80CD804C0F86854C59A2A42892E4C906189DD94AEC89ECC1C78EF92A9044EFA96C978FE05';
*/
PRINT N'Creating or altering procedure ''GetSignleUser''';
CREATE OR ALTER PROCEDURE Administration.GetSignleUser
	@user_id smallint,
	@token nvarchar(128)
AS BEGIN
	EXEC Auth.ValidateToken @token = @token;
	SELECT TOP(1)
		u.ID, u.FullName, u.LoginName, u.Description
		FROM Administration.[User] u
		WHERE u.ID = @user_id;
END
GO

-- Procedure: Hypnos.Administration.AddUser
/*
DECLARE @user_json nvarchar(max) = N'{
	"FullName": "Волкова София",
	"LoginName": "sa",
	"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '2'), 2) + '",
	"Description": "Системный администратор"
}';
EXEC Administration.AddUser
	@user_json = @user_json,
	@token = N'2C7E8109F6A9329594A3750F43E622044BB34201E26583A299314872AFB62D25C8B842B4B47990389B4BE93DD9525FF4E395E4565EFF12893E3E8C9C44432794';
SELECT Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '2'), 2);
SELECT j.FullName, LoginName, PasswordHash, Description, -32768 CreatedBy
	FROM OpenJson(N'{
		"FullName": "Волкова София",
		"LoginName": "sa",
		"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '2'), 2) + '",
		"Description": "Системный администратор"
	}')
	WITH (FullName Name, LoginName Name, PasswordHash nvarchar(128), Description Description) AS j;
*/
PRINT N'Creating or altering procedure ''AddUser''';
CREATE OR ALTER PROCEDURE Administration.AddUser
	@user_json nvarchar(max),
	@token nvarchar(128)
AS BEGIN
	EXEC Auth.ValidateToken @token = @token;
	DECLARE @user_id smallint;
	SELECT @user_id = s.UserID FROM Auth.[Session] s WHERE s.Token = @token;
	INSERT INTO Administration.[User] (FullName, LoginName, PasswordHash, Description, CreatedBy, UpdatedBy, IsDeleted)
		SELECT j.FullName, j.LoginName, j.PasswordHash, j.Description, @user_id CreatedBy, @user_id UpdatedBy, 0 IsDeleted
		FROM OpenJson(@user_json)
		WITH (FullName Name, LoginName Name, PasswordHash nvarchar(128), Description Description) AS j
END
GO

-- Procedure: Hypnos.Administration.UpdateUser
/*
DECLARE @password_hash nvarchar(128) = Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'2'), 2),
	@token nvarchar(128),
	@user_id smallint;
EXEC Auth.Authenticate @login_name = N'sa', @password_hash = @password_hash, @token = @token OUTPUT, @user_id = @user_id OUTPUT;
PRINT N'
User ID: ' + IIF(@user_id IS NULL, N'<not found>', Convert(nvarchar(6), @user_id)) + N'
Token: ' + ISNULL(@token, N'<unauthorized>');
DECLARE @user_json nvarchar(max) = N'{
	"ID": -32766,
	"LoginName": "sa",
	"Description": "Системный администратор",
	"PasswordHash": "' + Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'2'), 2) + N'",
	"FullName": "Волкова София"
}';
EXEC Administration.EditUser
	@user_json = @user_json,
	@token = N'708604B27C16E411826298E0BCE39373E4C9FA0D7E5B5225F10A45AFD894DC533D53574F3D70EAFAB5A761670C36CDB62CC50A8D6F55CF27683C6B375A4A59BB';
UPDATE Administration.[User]
	SET FullName = json.FullName
	FROM (
		SELECT j.ID, j.FullName
		FROM OpenJson(N'{ "ID": -32766, "FullName": "Волкова София 4" }')
		WITH (ID smallint, FullName Name) AS j
	) AS json
	WHERE Administration.[User].ID = json.ID;
SELECT Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'1'), 2);
*/
PRINT N'Creating or altering procedure ''EditUser''';
CREATE OR ALTER PROCEDURE Administration.EditUser
	@user_json nvarchar(max),
	@token nvarchar(128)
AS BEGIN
	EXEC Auth.ValidateToken @token = @token;
	DECLARE @user_id smallint;
	SELECT @user_id = s.UserID FROM Auth.[Session] s WHERE s.Token = @token;
	UPDATE Administration.[User]
		SET
			FullName = ISNULL(json.FullName, Administration.[User].FullName),
			LoginName = ISNULL(json.LoginName, Administration.[User].LoginName),
			Description = ISNULL(json.Description, Administration.[User].Description),
			PasswordHash = ISNULL(json.PasswordHash, Administration.[User].PasswordHash),
			UpdatedBy = @user_id,
			UpdatedDate = GetDate()
		FROM (
			SELECT j.ID, j.FullName, j.LoginName, j.Description, j.PasswordHash
			FROM OpenJson(@user_json)
			WITH (ID smallint, FullName Name, LoginName Name, Description Description, PasswordHash nvarchar(128)) AS j
		) AS json
		WHERE Administration.[User].ID = json.ID;
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































