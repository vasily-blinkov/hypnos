-- This script creates an empty Hypnos database.

-- You can set values of the variables below to customize the database.
DECLARE @database_name NVARCHAR(10) = N'Hypnos';

USE [master];

IF DB_ID(@database_name) IS NULL
BEGIN
	PRINT 'Creating database ''Hypnos''.';
END

