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
	"Roles": [-32768, -32767, -32766],
*/
DECLARE @user_json nvarchar(max) = N'{
	"LoginName": "sa",
	"FullName": "Волкова София",
	"Description": "Системный администратор",
	"Roles": [-32768, -32767, -32766],
	"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '3'), 2) + N'"
}';
EXEC Administration.AddUser
	@user_json = @user_json,
	@token = N'BDB35CFDE19D9558B2AEB36741B5198F318066719278BE001B6885D4ED83A48E5307EE385072A0F98597E4917B9956A5B35D52AC3E29C08CA469AB3099BBD575';

-- Select all roles for a specific user.
select u.LoginName, r.name
	from Administration.UserRole ur
	left join Administration.[User] u on ur.UserID = u.ID
	left JOIN Administration.[Role] r on ur.RoleID = r.ID
	WHERE u.LoginName = 'sa';

-- Select a specific user.
select u.id, u.fullname, u.CreatedBy, u.CreatedDate, u.UpdatedBy, u.UpdatedDate, u.IsDeleted, u.Description, u.LoginName
	from Administration.[User] u WHERE u.LoginName = 'sa';

-- Drop a user.
DELETE FROM Administration.UserRole WHERE UserID = (SELECT u.id from Administration.[User] u WHERE u.LoginName = 'sa');
DELETE FROM Administration.[User] WHERE LoginName = N'sa';

-- Select from JSON slim example.
-- PasswordHash' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '2'), 2) + '
DECLARE @user_json nvarchar(MAX) = N'{
	"FullName": "Волкова София",
	"LoginName": "sa",
	"Description": "Системный администратор",
	"Roles": [-32768, -32767, -32766]
}';
SELECT 1
	FROM OpenJson(@user_json)
	WITH (FullName Name, LoginName Name, PasswordHash nvarchar(128), Description Description) AS j
	WHERE ISNULL(j.LoginName, N'') = N'' OR ISNULL(j.FullName, N'') = N'' OR ISNULL(j.PasswordHash, N'') = N'';

SELECT isnull(NULL, 'yes')

-- Insert into a table variable
declare @table table(id smallint);
insert into @table(id) values(-1);
select t.id from @table t;

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

-- Auth.Authenticate.
DECLARE @password_hash nvarchar(128) = Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'seed'), 2),
	@token nvarchar(128),
	@user_id smallint;
EXEC Auth.Authenticate @login_name = N'seed', @password_hash = @password_hash, @token = @token OUTPUT, @user_id = @user_id OUTPUT;
PRINT N'
User ID: ' + IIF(@user_id IS NULL, N'<not found>', Convert(nvarchar(6), @user_id)) + N'
Token: ' + ISNULL(@token, N'<unauthorized>');

-- Update.
-- "PasswordHash": "' + Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'1'), 2) + N'"
DECLARE @user_json nvarchar(max) = N'{
	"ID": -32661
}';
EXEC Administration.EditUser
	@user_json = @user_json,
	@token = N'835A453F002CD7B92D1C0531BB2C31A730FA3807B81C88A25843916C594B7BD1C61F77BC3847B92748C2D45F16103A59DBC7A0E92158F3D85B0CDDA55934ECE9';

select u.passwordhash from Administration.[User] u WHERE u.LoginName = 'sa';

-- Get single user.
DECLARE @user_id smallint;
SELECT @user_id = u.ID FROM Administration.[User] u WHERE u.LoginName = N'sa';
EXEC Administration.GetSignleUser
	@user_id = @user_id,
	@token = N'835A453F002CD7B92D1C0531BB2C31A730FA3807B81C88A25843916C594B7BD1C61F77BC3847B92748C2D45F16103A59DBC7A0E92158F3D85B0CDDA55934ECE9';

-- See the user.
SELECT * from sys.time_zone_info;
select SYSDATETIME() at time zone 'UTC' at time zone 'Russian Standard Time'; 
SELECT u.LoginName, u.ID, u.FullName,
	u.UpdatedDate at time zone 'UTC' at time zone 'Russian Standard Time', -- 2023-03-05 15:27:19.97 +03:00,
	u2.FullName UpdatedBy
	from Administration.[User] u
	left join Administration.[User] u2 on u2.ID = u.UpdatedBy
 	WHERE u.LoginName = 'sa';

-- IIF
with changes as (
SELECT 'sa' as LoginName, 1 id
UNION SELECT '' as LoginName, 2 id
union select null as LoginName, 3 id
union select ' ' AS LoginName, 4 id
)
	select c.id, IIF(TRIM(ISNULL(c.LoginName, N'')) = N'', '<empty>', c.LoginName), '['+c.LoginName+']'
	from changes c
	order by c.id;

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
