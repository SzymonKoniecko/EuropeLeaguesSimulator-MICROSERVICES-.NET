CREATE TABLE AppLogs (
    Id UNIQUEIDENTIFIER PRIMARY KEY not null,
    Application NVARCHAR(255),
    LoggedTime DATETIME,
    Level NVARCHAR(50),
    Message NVARCHAR(MAX),
    Logger NVARCHAR(255),
    Callsite NVARCHAR(255),
    Exception NVARCHAR(MAX)
);
