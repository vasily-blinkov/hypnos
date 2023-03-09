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
	
	-- IDs of the roles.
	DECLARE
		@administrator_id smallint,
		@manager_id smallint,
		@worker_id smallint;
	SELECT @administrator_id = r.ID FROM Administration.[Role] r WHERE r.Name = N'Администратор';
	SELECT @manager_id = r.ID FROM Administration.[Role] r WHERE r.Name = N'Руководитель';
	SELECT @worker_id = r.ID FROM Administration.[Role] r WHERE r.Name = N'Специалист';
	
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
			"PasswordHash": "' + CONVERT(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '1'), 2) + '",
			"Roles": [' + CONVERT(nvarchar(6), @administrator_id) + N', ' + CONVERT(nvarchar(6), @manager_id) + N', ' + CONVERT(nvarchar(6), @worker_id) + N'],
			"Description": "Директор"
		}';
		EXEC Administration.AddUser
			@user_json = @user_json,
			@token = @token;
	END
	
	-- Adding the system administrator.
	IF NOT EXISTS(SELECT 1 FROM Administration.[User] u WHERE u.LoginName = N'sa')
	BEGIN
		-- Create a user.
		SET @user_json = N'{
			"FullName": "Волкова София",
			"LoginName": "sa",
			"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '2'), 2) + '",
			"Description": "Системный администратор"
		}';
		EXEC Administration.AddUser
			@user_json = @user_json,
			@token = @token;
		
		-- Assign roles.
		SELECT @user_id = u.ID FROM Administration.[User] u WHERE u.LoginName = N'sa';
		SET @user_json = N'{
			"ID": ' + CONVERT(nvarchar(6), @user_id) + N',
			"Roles": [' + CONVERT(nvarchar(6), @administrator_id) + N', ' + CONVERT(nvarchar(6), @manager_id) + N', ' + CONVERT(nvarchar(6), @worker_id) + N']
		}';
		EXEC Administration.EditUser
			@user_json = @user_json,
			@token = @token;
	END
	
	-- Adding the outsource team leader.
	IF NOT EXISTS(SELECT 1 FROM Administration.[User] u WHERE u.LoginName = N'ostl')
	BEGIN
		SET @user_json = N'{
			"FullName": "Соколова Мария",
			"LoginName": "ostl",
			"PasswordHash": "' + CONVERT(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '3'), 2) + '",
			"Roles": [' + CONVERT(nvarchar(6), @manager_id) + N', ' + CONVERT(nvarchar(6), @worker_id) + N'],
			"Description": "Лидер команды аутсорсинга"
		}';
		EXEC Administration.AddUser
			@user_json = @user_json,
			@token = @token;
	END
	
	-- Log out user with login 'seed'.
	EXEC Auth.LogOut @token = @token;

END
