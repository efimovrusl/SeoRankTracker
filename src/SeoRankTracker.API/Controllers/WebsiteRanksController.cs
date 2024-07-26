using Microsoft.AspNetCore.Mvc;
using SeoRankTracker.Application.Services;
using SeoRankTracker.Domain.DTOs;

namespace SeoRankTracker.API.Controllers;

[ApiController]
[Route("api/website-ranks")]
public class WebsiteRanksController(ILogger<WebsiteRanksController> logger, 
    IGoogleScrapingService scrapingService) : ControllerBase
{
    private ILogger<WebsiteRanksController> _logger = logger;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WebsiteRankDto>>> Get(string searchKeyword, string website)
    {
        List<WebsiteRankDto> ranks;
        ranks = await scrapingService.GetWebsiteRanksAsync(searchKeyword, website);
        // some services' calls
        return ranks;
    }
}