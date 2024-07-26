CREATE TABLE HighestWebsiteRanks
(
    Id            UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    SearchKeyword NVARCHAR(500) not null,
    WebsiteUrl    NVARCHAR(MAX) not null,
    ResultUrl     NVARCHAR(MAX) not null,
    Description   NVARCHAR(MAX) not null,
    Position      INT           not null,
    Date          DATETIME      not null
);