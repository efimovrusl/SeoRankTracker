﻿using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SeoRankTracker.Application.Services;
using SeoRankTracker.Shared.DTOs;

namespace SeoRankTracker.Infrastructure.Services;

public class GoogleScrapingService(ILogger<GoogleScrapingService> logger, 
    IHttpClientFactory httpClientFactory) : IGoogleScrapingService
{
    private readonly ILogger _logger = logger;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Constants.GoogleScraperHttpClientName);

    public async Task<List<WebsiteRankDto>> GetWebsiteRanksAsync(SeoRequestDto seoRequestDto)
    {
        var encodedSearchKeyword = Uri.EscapeDataString(seoRequestDto.SearchKeyword);
        var url = $"/search?num=100&q={encodedSearchKeyword}";
        string response;
        try
        {
            response = await _httpClient.GetStringAsync(url);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            throw;
        }
        return ParseWebsiteRanksFromGoogleHtml(response, seoRequestDto);
    }

    private static List<WebsiteRankDto> ParseWebsiteRanksFromGoogleHtml(string html, SeoRequestDto seoRequestDto)
    {
        var ranks = new List<WebsiteRankDto>();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        // Find the <div> element with id "search"
        var searchDiv = doc.DocumentNode.SelectSingleNode("//div[@id='search']");
        if (searchDiv == null) return ranks;
        
        // Find the <div> element containing the actual search results, which should be a sibling to the <h1>
        var resultsContainer = searchDiv
            .Descendants("div")
            .FirstOrDefault(d => d.GetAttributeValue("id", "") == "rso");
        if (resultsContainer == null) return ranks;

        var resultNodes = resultsContainer.ChildNodes.Nodes();
        
        int position = 1;
        foreach (var resultNode in resultNodes)
        {
            // Find the <a> tag and <h3> tag within the hierarchy of the resultNode
            var anchorNode = resultNode.Descendants("a").FirstOrDefault();
            var h3Node = resultNode.Descendants("h3").FirstOrDefault();
        
            if (anchorNode != null && h3Node != null)
            {
                // Extract the URL from the href attribute
                var url = anchorNode.GetAttributeValue("href", string.Empty);
                var title = h3Node.InnerText.Trim();
        
                if (url.Contains(seoRequestDto.WebsiteUrl))
                {
                    // Add the result to the list
                    ranks.Add(new WebsiteRankDto
                    {
                        WebsiteUrl = seoRequestDto.WebsiteUrl,
                        ResultUrl = url,
                        SearchKeyword = seoRequestDto.SearchKeyword,
                        Description = title,
                        Position = position,
                        Date = DateTime.Now
                    });
                }
            }
            position++;
        }
        
        return ranks;
    }
}
