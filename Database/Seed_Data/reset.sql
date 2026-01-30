USE phongkhamdalieu;
GO
DECLARE @sql NVARCHAR(MAX) = N'';

SELECT @sql +=
    'ALTER TABLE ' 
    + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) 
    + '.' 
    + QUOTENAME(OBJECT_NAME(parent_object_id)) 
    + ' DROP CONSTRAINT ' 
    + QUOTENAME(name) + ';' + CHAR(13)
FROM sys.foreign_keys;

EXEC sp_executesql @sql;
GO
DECLARE @sql NVARCHAR(MAX) = N'';

SELECT @sql +=
    'DROP TABLE ' 
    + QUOTENAME(SCHEMA_NAME(schema_id)) 
    + '.' 
    + QUOTENAME(name) + ';' + CHAR(13)
FROM sys.tables;

EXEC sp_executesql @sql;
GO

--------------------------

DECLARE @sql NVARCHAR(MAX) = '';

SELECT @sql += 
    'DBCC CHECKIDENT (''' 
    + SCHEMA_NAME(schema_id) + '.' + name 
    + ''', RESEED, 0);'
FROM sys.tables;

EXEC sp_executesql @sql;
