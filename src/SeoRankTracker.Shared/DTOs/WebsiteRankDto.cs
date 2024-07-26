namespace SeoRankTracker.Shared.DTOs;

public class WebsiteRankDto
{
    public required string SearchKeyword { get; set; }
    public required string WebsiteUrl { get; set; }
    public required string ResultUrl { get; set; }
    public required string Description { get; set; }
    public required int Position { get; set; }
    public required DateTime Date { get; set; }
}