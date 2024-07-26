CREATE OR ALTER PROCEDURE USP_GetDistinctKeywordUrlPairsAsync
AS
BEGIN
    SELECT DISTINCT SearchKeyword, WebsiteUrl
    FROM HighestWebsiteRanks
END;