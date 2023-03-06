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

-- Auth.Authenticate.
DECLARE @password_hash nvarchar(128) = Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'seed'), 2),
	@token nvarchar(128),
	@user_id smallint;
EXEC Auth.Authenticate @login_name = N'seed', @password_hash = @password_hash, @token = @token OUTPUT, @user_id = @user_id OUTPUT;
PRINT N'
User ID: ' + IIF(@user_id IS NULL, N'<not found>', Convert(nvarchar(6), @user_id)) + N'
Token: ' + ISNULL(@token, N'<unauthorized>');

/*
	"LoginName": "sa",
	"FullName": "Волкова София",
	"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '3'), 2) + N'",
	"Description": "Системный администратор",
	"Roles": [-32768, -32767, -32766],
*/
DECLARE @user_json nvarchar(max) = N'{
	"LoginName": "osdev",
	"FullName": "Панов Всеволод",
	"Description": "Разработчик команды аутсорсинга",
	"Roles": [-32766],
	"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '4'), 2) + N'"
}';
EXEC Administration.AddUser
	@user_json = @user_json,
	@token = N'108F8F31030FE9B6AF40D93520DBCE79D02316934E9D4490FE712B7967AFACDD3F80390ECC24A40668057BF43CF32BAF0E425429E3A26CF75B5ABF390F6395F8';

-- Select all roles for a specific user.
select u.LoginName, r.name
	from Administration.UserRole ur
	left join Administration.[User] u on ur.UserID = u.ID
	left JOIN Administration.[Role] r on ur.RoleID = r.ID
	WHERE u.LoginName = 'sa';

-- Select a specific user.
select u.id, u.fullname, u.CreatedBy, u.CreatedDate, u.UpdatedBy, u.UpdatedDate, u.IsDeleted, u.Description, u.LoginName
	from Administration.[User] u WHERE u.LoginName = 'osdev';

-- Drop a user.
DELETE FROM Administration.UserRole WHERE UserID = (SELECT u.id from Administration.[User] u WHERE u.LoginName = 'sa');
DELETE FROM Administration.[User] WHERE LoginName = N'sa';

select 1 where isnull(null, N'<empty>') = N'<empty>'

-- Select from JSON slim example.
-- PasswordHash' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '2'), 2) + '
DECLARE @user_json nvarchar(MAX) = N'{
	"FullName": "Волкова София",
	"LoginName": "sa",
	"Description": "Системный администратор",
	"Roles": [-32768, -32767, -32766],
	"PasswordHash": " "
}';
SELECT '[' + j.PasswordHash + ']', ISNULL(j.PasswordHash, N'<empty>')
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
SELECT u.ID, u.FullName, u.CreatedBy,
	u.CreatedDate at time zone 'UTC' at time zone 'Russian Standard Time',
	u.UpdatedBy,
	u.UpdatedDate at time zone 'UTC' at time zone 'Russian Standard Time'
	FROM Administration.[User] u WHERE u.LoginName = N'sa';


/*
 * Administration.EditUser
 */

-- Prepare to drop.
USE master;

-- Switch to the database under development.
USE Hypnos;

-- Multiple variables declaration prohibited.
DECLARE @roles TABLE(ID smallint); @roles_json nvarchar(max);

-- Passed
declare @changes table (Roles nvarchar(max));
insert @changes (Roles) select r.Roles from openjson('{"Roles": [-2, -4]}') with(Roles nvarchar(max) as json) r;
select c.Roles from @changes c;
declare @roles_json nvarchar(max);
select @roles_json = c.Roles from @changes c;
if @roles_json is not null
	print 'Roles: ' + @roles_json;
else
	print 'Roles are NULL';

-- []
declare @changes table (Roles nvarchar(max));
insert @changes (Roles) select r.Roles from openjson('{"Roles": []}') with(Roles nvarchar(max) as json) r;
select c.Roles from @changes c;
declare @roles_json nvarchar(max);
select @roles_json = c.Roles from @changes c;
if @roles_json is not null
	print 'Roles: ' + @roles_json;
else
	print 'Roles are NULL';

-- NULL
declare @changes table (Roles nvarchar(max), ID smallint);
insert @changes (Roles, ID) select r.Roles, r.ID from openjson('{"ID": -2}') with(ID smallint, Roles nvarchar(max) as json) r;
select c.Roles, c.ID from @changes c;
declare @roles_json nvarchar(max);
select @roles_json = c.Roles from @changes c;
if @roles_json is not null
	print 'Roles: ' + @roles_json;
else
	print 'Roles are NULL';

-- get user roles
select u.id from Administration.[User] u where u.LoginName = 'ustl'; -- -32666
EXEC Administration.GetRoles
	@user_id = -32666,
	@token = N'5F2790A26DF1EB94142259DF852B1F6B0670FBCA595405AFD026C8500FAF77B1ADC01CA3471A72352E5F26BD95F7696671553339999E09BDBD20A5473ACF1FA4';
select ur.userid, ur.roleid, ur.isdeleted from Administration.UserRole ur where ur.UserID = -32666;

-- get all roles.
EXEC Administration.GetRoles
	@token = N'5F2790A26DF1EB94142259DF852B1F6B0670FBCA595405AFD026C8500FAF77B1ADC01CA3471A72352E5F26BD95F7696671553339999E09BDBD20A5473ACF1FA4';

-- Auth.Authenticate.
DECLARE @password_hash nvarchar(128) = Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'seed'), 2),
	@token nvarchar(128),
	@user_id smallint;
EXEC Auth.Authenticate @login_name = N'seed', @password_hash = @password_hash, @token = @token OUTPUT, @user_id = @user_id OUTPUT;
PRINT N'
User ID: ' + IIF(@user_id IS NULL, N'<not found>', Convert(nvarchar(6), @user_id)) + N'
Token: ' + ISNULL(@token, N'<unauthorized>');

-- Create.
DECLARE @user_json nvarchar(max) = N'{
	"LoginName": "ustl",
	"FullName": "Белова Ирина",
	"Description": "Лидер команды поддержки пользователей",
	"Roles": [-32768, -32767, -32766],
	"PasswordHash": "' + Convert(nvarchar(128), HashBytes('sha2_512', N'woTdzTfu5VUxUjtnr8fJ' + '7'), 2) + N'"
}';
EXEC Administration.AddUser
	@user_json = @user_json,
	@token = N'5F2790A26DF1EB94142259DF852B1F6B0670FBCA595405AFD026C8500FAF77B1ADC01CA3471A72352E5F26BD95F7696671553339999E09BDBD20A5473ACF1FA4';

-- Update.
-- "PasswordHash": "' + Convert(nvarchar(128), HashBytes('SHA2_512', N'woTdzTfu5VUxUjtnr8fJ' + N'1'), 2) + N'"
DECLARE @user_json nvarchar(max) = N'{
	"ID": -32667
}';
EXEC Administration.EditUser
	@user_json = @user_json,
	@token = N'5F2790A26DF1EB94142259DF852B1F6B0670FBCA595405AFD026C8500FAF77B1ADC01CA3471A72352E5F26BD95F7696671553339999E09BDBD20A5473ACF1FA4';

declare @change table(id smallint);
insert @change (id) values (null);
with [user] as (select -1 id)
	select u.id, c.id from [user] u join @change c on u.id = c.id;

declare @error nvarchar(MAX) = 'error' + convert(nvarchar(2), -1);
throw 53000, @error, 1;

-- Inspect a user.
select u.id, u.FullName from Administration.[User] u WHERE u.LoginName = 'osba';

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

-- Edit roles.
DECLARE @user_json nvarchar(max) = N'{ "ID": -32565, "Roles": [-32768] }';
DECLARE @changes table (id smallint, roles nvarchar(max));
declare @id smallint;
declare @roles_json nvarchar(max);
declare @roles table (ID smallint);
insert @changes (id, roles) select j.id, j.roles from openjson(@user_json) with (ID smallint, Roles nvarchar(max) as json) j;
select @id = c.id from @changes c;
select @roles_json = c.Roles from @changes c;
--print 'Roles JSON: ' + @roles_json;
insert @roles (ID) select r.value ID from OpenJson(@roles_json) r;
--select r.id from @roles r; -- IDs of roles to NOT revoke
-- Roles to delete:
/*select r.name from Administration.[Role] r left join Administration.UserRole ur on ur.RoleID = r.ID
	WHERE ur.UserID = @id AND not EXISTS (SELECT 1 from @roles j WHERE j.ID = ur.RoleID);*/
select ur.userid, ur.roleid from -- DELETE
	Administration.UserRole ur
	where ur.userid = @id and not exists (select 1 from @roles r where r.id = ur.roleid);
--select c.id, c.roles from @changes c;

-- WTF?
select r.value id from openjson('[-1]') r

-- Remove a couple of roles.
EXEC Administration.EditUser
	@user_json = '{"ID": -32565, "Roles": [-32768]}',
	@token = N'C9B34C80786008A834B32526567749FAC0CEBCC8B2AB25891159CBA9B2A3E5658226490DD081801BE2C14EA4CA8D3311560D2DA4A3D47E7BAC2C60BF8DEEDEEA';

-- Roles of a user.
select ur.roleid, r.name, ur.UserID
	from Administration.UserRole ur
	left join Administration.[User] u on u.ID = ur.UserID
	left join Administration.[Role] r on r.ID = ur.RoleID 
	WHERE u.LoginName = 'ceo';

declare @table table (x smallint);
insert @table (x)
	select -1 union
	select 0 union
	select 1;
delete @table
	select 0 union
	select 1;
select t.x r from @table t;
