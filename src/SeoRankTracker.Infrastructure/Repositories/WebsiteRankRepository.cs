using System.Data;
using Microsoft.Extensions.Configuration;
using SeoRankTracker.Application.Repositories;
using SeoRankTracker.Shared.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SeoRankTracker.Infrastructure.Repositories;

public class WebsiteRankRepository(IConfiguration configuration) : IWebsiteRankRepository
{
    private string _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    
    public async Task RegisterHighestWebsiteRankFoundAsync(WebsiteRankDto dto)
    {
        await using var cnn = new SqlConnection(_connectionString);

        const string sql = @"
            INSERT INTO HighestWebsiteRanks (SearchKeyword, ResultUrl, WebsiteUrl, Description, Position, Date)
            VALUES (@SearchKeyword, @ResultUrl, @WebsiteUrl, @Description, @Position, @Date)";
        
        await cnn.ExecuteAsync(sql, new
        {
            dto.SearchKeyword,
            ResultUrl = dto.ResultUrl,
            WebsiteUrl = dto.WebsiteUrl,
            Description = dto.Description,
            Position = dto.Position,
            Date = dto.Date
        });
    }

    public async Task<List<WebsiteRankDto>> GetHighestWebsiteRanksPerDayAsync(SeoRequestDto seoRequestDto)
    {
        await using var cnn = new SqlConnection(_connectionString);
        var parameters = new DynamicParameters();
        
        parameters.Add("searchKeyword", seoRequestDto.SearchKeyword);
        parameters.Add("websiteUrl", seoRequestDto.WebsiteUrl);
        
        var dtos = cnn.Query<WebsiteRankDto>(
            "USP_GetHighestRanksPerDay", parameters, commandType: CommandType.StoredProcedure);
        return dtos.ToList();
    }

    public async Task<List<SeoRequestDto>> GetDistinctKeywordUrlPairsAsync()
    {
        await using var cnn = new SqlConnection(_connectionString);
        
        var dtos = cnn.Query<SeoRequestDto>(
            "USP_GetDistinctKeywordUrlPairsAsync", commandType: CommandType.StoredProcedure);
        return dtos.ToList();
    }
}