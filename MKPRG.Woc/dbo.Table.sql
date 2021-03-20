CREATE TABLE [dbo].[Authors]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(255) NULL, 
    [LastName] NVARCHAR(255) NOT NULL, 
    [Birthday] DATE NULL, 
    [City] NVARCHAR(255) NULL
)
