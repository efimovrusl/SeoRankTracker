using SeoRankTracker.Shared.DTOs;

namespace SeoRankTracker.Application.Services;

public interface IGoogleScrapingService
{
    public Task<List<WebsiteRankDto>> GetWebsiteRanksAsync(SeoRequestDto seoRequestDto);
}