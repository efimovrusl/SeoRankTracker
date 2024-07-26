CREATE OR ALTER PROCEDURE USP_GetDistinctKeywordUrlPairsAsync
AS
BEGIN
    SELECT SearchKeyword, WebsiteUrl, MAX(Date) AS LastUsed
    FROM HighestWebsiteRanks
    GROUP BY SearchKeyword, WebsiteUrl
    ORDER BY LastUsed DESC
END;