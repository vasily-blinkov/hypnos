-- This script populates the Hypnos database with sample data.
-- Execute it after the Create.sql.

-- USE master;
USE Hypnos;

BEGIN

	-- Getting the ID of the initial user.
	DECLARE @seed_id smallint;
	SELECT @seed_id = u.ID FROM Administration.[User] u WHERE u.LoginName = N'seed';

	-- Variables for reuse.
	DECLARE @token nvarchar(128),
		@user_id smallint,
		@password_hash nvarchar(128) = Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'seed'), 2),
		@user_json nvarchar(max);
	
	-- Authenticate to be able to call stored procedures to simplify inserts.
	EXEC Auth.Authenticate
		@login_name = N'seed',
		@password_hash = @password_hash,
		@token = @token OUTPUT,
		@user_id = @user_id OUTPUT;
	
	-- Adding the director.
	IF NOT EXISTS(SELECT 1 FROM Administration.[User] u WHERE u.LoginName = N'ceo')
	BEGIN
		SET @user_json = N'{
			"FullName": "Архипова Василиса",
			"LoginName": "ceo",
			"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '1'), 2) + '",
			"Description": "Директор"
		}';
		EXEC Administration.AddUser
			@user_json = @user_json,
			@token = @token;
	END
	
	-- Adding the system administrator.
	IF NOT EXISTS(SELECT 1 FROM Administration.[User] u WHERE u.LoginName = N'sa')
	BEGIN
		SET @user_json = N'{
			"FullName": "Волкова София",
			"LoginName": "sa",
			"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '2'), 2) + '",
			"Description": "Системный администратор"
		}';
		EXEC Administration.AddUser
			@user_json = @user_json,
			@token = @token;
	END
	
	-- IDs of the roles.
	DECLARE
		@administrator_id smallint,
		@manager_id smallint,
		@worker_id smallint;
	SELECT @administrator_id = r.ID FROM Administration.[Role] r WHERE r.Name = N'Администратор';
	SELECT @manager_id = r.ID FROM Administration.[Role] r WHERE r.Name = N'Руководитель';
	SELECT @worker_id = r.ID FROM Administration.[Role] r WHERE r.Name = N'Специалист';
	
	-- ID of the director.
	DECLARE @ceo_id smallint;
	SELECT @ceo_id = u.ID FROM Administration.[User] u WHERE u.LoginName = N'ceo';

	-- Granting the director the Administrator role.
	IF NOT EXISTS(SELECT 1 FROM Administration.UserRole ur WHERE ur.UserID = @ceo_id AND ur.RoleID = @administrator_id)
		INSERT INTO Administration.UserRole (UserID, RoleID, CreatedBy, UpdatedBy)
		VALUES (@ceo_id, @administrator_id, @seed_id, @seed_id);

	-- Granting the director the Manager role.
	IF NOT EXISTS(SELECT 1 FROM Administration.UserRole ur WHERE ur.UserID = @ceo_id AND ur.RoleID = @manager_id)
		INSERT INTO Administration.UserRole (UserID, RoleID, CreatedBy, UpdatedBy)
		VALUES (@ceo_id, @manager_id, @seed_id, @seed_id);

	-- Granting the director the Worker role.
	IF NOT EXISTS(SELECT 1 FROM Administration.UserRole ur WHERE ur.UserID = @ceo_id AND ur.RoleID = @worker_id)
		INSERT INTO Administration.UserRole (UserID, RoleID, CreatedBy, UpdatedBy)
		VALUES (@ceo_id, @worker_id, @seed_id, @seed_id);
	
	-- Log out user with login 'seed'.
	EXEC Auth.LogOut @token = @token;

END
