using SeoRankTracker.Application.Repositories;
using SeoRankTracker.Shared.DTOs;

namespace SeoRankTracker.Application.Services;

public interface IWebsiteRankService
{
    public Task<List<WebsiteRankDto>> GetWebsiteRanksAsync(SeoRequestDto seoRequestDto);
    public Task<List<WebsiteRankDto>> GetHighestWebsiteRanksPerDayAsync(SeoRequestDto seoRequestDto);
    public Task<List<SeoRequestDto>> GetDistinctKeywordUrlPairsAsync();
}

public class WebsiteRankService(IGoogleScrapingService googleScrapingService,
    IWebsiteRankRepository websiteRankRepository) : IWebsiteRankService
{
    private IGoogleScrapingService _scraper = googleScrapingService;
    private IWebsiteRankRepository _websiteRankRepository = websiteRankRepository;

    public async Task<List<WebsiteRankDto>> GetWebsiteRanksAsync(SeoRequestDto seoRequestDto)
    {
        var results = await _scraper.GetWebsiteRanksAsync(seoRequestDto);
        
        // saving the highest ranked search result to database
        if (results.Count > 0) _websiteRankRepository?.RegisterHighestWebsiteRankFoundAsync(
            results.MinBy(result => result.Position)!);
        
        return results;
    }

    public async Task<List<WebsiteRankDto>> GetHighestWebsiteRanksPerDayAsync(SeoRequestDto seoRequestDto)
    {
        return await _websiteRankRepository.GetHighestWebsiteRanksPerDayAsync(seoRequestDto);
    }

    public async Task<List<SeoRequestDto>> GetDistinctKeywordUrlPairsAsync()
    {
        return await _websiteRankRepository.GetDistinctKeywordUrlPairsAsync();
    }
}