using SeoRankTracker.Domain.DTOs;

namespace SeoRankTracker.Application.Services;

public interface IGoogleScrapingService
{
    public Task<List<WebsiteRankDto>> GetWebsiteRanksAsync(string searchKeyword, string websiteUrl);
}