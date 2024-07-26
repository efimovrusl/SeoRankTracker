namespace SeoRankTracker.Domain.DTOs;

public class WebsiteRankDto
{
    public required string SearchKeyword { get; set; }
    public required string FullUrl { get; set; }
    public required string Description { get; set; }
    public int Position { get; set; }
}