-- MISCELLANEOUS SAMPLES OF SQL CONSTRUCTIONS AND USER DEFINED STORED
-- PROCEDURES AND FUNCTIONS USAGE AND TESTING.

/*
 * Auth.CleanupSessions
 */

-- Cleaning sessions up.
EXEC Auth.CleanupSessions;

/*
 * Administration.AddUser
 */

-- Create user.
use master;
use hypnos;

/*
	"LoginName": "sa",
	"FullName": "Волкова София",
	"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '3'), 2) + N'",
	"Description": "Системный администратор",
	"Roles": ["Администратор", "Руководитель", "Специалист"]
*/
DECLARE @user_json nvarchar(max) = N'{
	"LoginName": "sa",
	"FullName": "Волкова София",
	"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '3'), 2) + N'",
	"Roles": [-32768, -32767],
	"Description": "Системный администратор"
}';
EXEC Administration.AddUser
	@user_json = @user_json,
	@token = N'133DB25060284E7826FA469551FC3182BC91D2B8C8C8C460E059BF95DEEE36B2EB6E8B9D8953E419EC282171B800783C9F412B902962547C28E36F257339DA35';

-- Select all roles for a specific user.
select u.LoginName, r.name
	from Administration.UserRole ur
	left join Administration.[User] u on ur.UserID = u.ID
	left JOIN Administration.[Role] r on ur.RoleID = r.ID
	WHERE u.LoginName = 'seed';

-- Drop a user.
DELETE FROM Administration.UserRole WHERE UserID = (SELECT u.id from Administration.[User] u WHERE u.LoginName = 'ceo');
DELETE FROM Administration.[User] WHERE LoginName = N'ceo';

-- Select from JSON slim example.
DECLARE @user_json nvarchar(MAX) = N'{
	"FullName": "Волкова София",
	"LoginName": "sa",
	"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '2'), 2) + '",
	"Description": "Системный администратор",
	"Roles": [-32768, -32767, -32766]
}';
DECLARE @roles_json nvarchar(MAX);
SELECT j.FullName, j.LoginName, j.PasswordHash, j.Description, -32768 CreatedBy
	FROM OpenJson(@user_json)
	WITH (FullName Name, LoginName Name, PasswordHash nvarchar(128), Description Description) AS j;

-- Roles.
select r.id, r.name from Administration.[Role] r;

-- Extract roles from JSON.
DECLARE @user_json nvarchar(MAX) = N'{
	"Roles": [1, 2, 3]
}';
SELECT j.value [Role], N'sa' [User] FROM OpenJson(@user_json, N'$.Roles') j;

-- Parse a JSON array.
SELECT j.value [Role] FROM OpenJson(N'["Администратор", "Руководитель"]') j;

-- Open JSON array.
SELECT j.[Array] FROM OpenJson(N'{"Array": ["Администратор", "Руководитель"]}') WITH ([Array] nvarchar(MAX) AS JSON) AS j;

-- Select from a JSON array.
SELECT J.value FROM OpenJson(N'{"Array": [1, 2, 3]}', N'$.Array') j;

-- Select an added user.
SELECT u.ID, u.LoginName, u.FullName, u.Description FROM Administration.[User] u WHERE u.LoginName = N'sa';


/*
 * Administration.EditUser
 */

-- Prepare to drop.
USE master;

-- Switch to the database under development.
USE Hypnos;

-- Authenticate.
DECLARE @password_hash nvarchar(128) = Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'1'), 2),
	@token nvarchar(128),
	@user_id smallint;
EXEC Auth.Authenticate @login_name = N'ceo', @password_hash = @password_hash, @token = @token OUTPUT, @user_id = @user_id OUTPUT;
PRINT N'
User ID: ' + IIF(@user_id IS NULL, N'<not found>', Convert(nvarchar(6), @user_id)) + N'
Token: ' + ISNULL(@token, N'<unauthorized>');

-- Update.
-- PasswordHash' + Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'2'), 2) + N'
DECLARE @user_json nvarchar(max) = N'{
	"ID": -32766,
	"LoginName": "sa",
	"Description": "Сисадминша",
	"FullName": "Волкова София"
}';
EXEC Administration.EditUser
	@user_json = @user_json,
	@token = N'E3E7A825F4C14F4370683E2DC44C3F6D823C118C691909567A56A9039BD298A92978749F793739C47B54D63B0BBAE75A6BDF6B80C5A6A42C0D9DCCC6FBCFF75A';

-- Update from JSON prototype query.
UPDATE Administration.[User]
	SET FullName = json.FullName
	FROM (
		SELECT j.ID, j.FullName
		FROM OpenJson(N'{ "ID": -32766, "FullName": "Волкова София 4" }')
		WITH (ID smallint, FullName Name) AS j
	) AS json
	WHERE Administration.[User].ID = json.ID;
SELECT Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'1'), 2);
