using Microsoft.AspNetCore.Mvc;
using SeoRankTracker.Application.Services;
using SeoRankTracker.Shared.DTOs;

namespace SeoRankTracker.API.Controllers;

[ApiController]
[Route("api/website-ranks")]
public class WebsiteRanksController(ILogger<WebsiteRanksController> logger, 
    IWebsiteRankService websiteRankService) : ControllerBase
{
    private ILogger<WebsiteRanksController> _logger = logger;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WebsiteRankDto>>> Get([FromQuery] SeoRequestDto seoRequestDto)
    {
        List<WebsiteRankDto> ranks = await websiteRankService
            .GetWebsiteRanksAsync(seoRequestDto);
        return ranks;
    }

    [HttpGet("highest-per-day")]
    public async Task<ActionResult<IEnumerable<WebsiteRankDto>>> GetHighestWebsiteRanksPerDayAsync(
        [FromQuery] SeoRequestDto seoRequestDto)
    {
        List<WebsiteRankDto> highestRanksPerDay = await websiteRankService
            .GetHighestWebsiteRanksPerDayAsync(seoRequestDto);
        return highestRanksPerDay;
    }
    
    [HttpGet("search-history")]
    public async Task<ActionResult<IEnumerable<SeoRequestDto>>> GetHighestWebsiteRanksPerDayAsync()
    {
        List<SeoRequestDto> distinctSearches = await websiteRankService.GetDistinctKeywordUrlPairsAsync();
        return distinctSearches;
    }
}