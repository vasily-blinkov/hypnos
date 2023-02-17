-- This script creates an empty Hypnos database.

-- You can set values of the variables below to customize the database.
DECLARE @database_name NVARCHAR(10) = N'Hypnos';

-- Variables for internal usage.
DECLARE @sql_string NVARCHAR(500);

USE [master];

IF DB_ID(@database_name) IS NULL
BEGIN
	PRINT N'Creating database ''' + @database_name + '''';
	SET @sql_string = N'CREATE DATABASE ' + @database_name;
	EXECUTE sp_executesql @sql_string;
END
