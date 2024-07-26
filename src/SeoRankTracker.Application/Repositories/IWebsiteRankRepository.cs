using SeoRankTracker.Shared.DTOs;

namespace SeoRankTracker.Application.Repositories;

public interface IWebsiteRankRepository
{
    public Task RegisterHighestWebsiteRankFoundAsync(WebsiteRankDto dto);
    public Task<List<WebsiteRankDto>> GetHighestWebsiteRanksPerDayAsync(SeoRequestDto seoRequestDto);
    public Task<List<SeoRequestDto>> GetDistinctKeywordUrlPairsAsync();
}