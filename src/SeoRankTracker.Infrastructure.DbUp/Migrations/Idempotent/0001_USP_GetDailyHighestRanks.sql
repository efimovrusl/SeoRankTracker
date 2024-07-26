CREATE OR ALTER PROCEDURE USP_GetHighestRanksPerDay
    @searchKeyword NVARCHAR(500),
    @websiteUrl    NVARCHAR(500)
AS
BEGIN
    SELECT SearchKeyword,
           WebsiteUrl,
           ResultUrl,
           Description,
           MIN(Position)      AS Position,
           CAST(Date AS DATE) AS Date -- Group ranks for same days
    FROM HighestWebsiteRanks
    WHERE WebsiteUrl = @websiteUrl
      AND SearchKeyword = @searchKeyword
    GROUP BY SearchKeyword,
             WebsiteUrl,
             ResultUrl,
             Description,
             CAST(Date AS DATE)
    ORDER BY Date
END;