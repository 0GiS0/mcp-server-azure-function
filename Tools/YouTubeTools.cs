using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Text.Json;

namespace mcp_azure_function.Tools;

public class YouTubeTool
{
    private readonly ILogger<YouTubeTool> logger;
    private readonly YouTubeService youTubeService;

    public YouTubeTool(ILogger<YouTubeTool> logger)
    {
        this.logger = logger;
        youTubeService = new YouTubeService(new BaseClientService.Initializer
        {
            ApiKey = Environment.GetEnvironmentVariable("YouTubeApiKey")
        });
    }


    [Function(nameof(GetYouTubeVideo))]
    public async Task<string> GetYouTubeVideo(
        [McpToolTrigger("searchVideo", "Search for a video on YouTube")]
            ToolInvocationContext context,
        [McpToolProperty("topic", "string", "The topic to search for")]
            string topic
    )
    {
        logger.LogInformation($"Looking for videos related with {topic}");



        var searchRequest = youTubeService.Search.List("snippet");
        searchRequest.Q = topic;
        searchRequest.MaxResults = 5;

        var searchResponse = await searchRequest.ExecuteAsync();

        // return title and url
        var videoDetails = searchResponse.Items
            .Where(item => item.Id.Kind == "youtube#video")
            .Select(item => new
            {
                Title = item.Snippet.Title,
                Url = $"https://www.youtube.com/watch?v={item.Id.VideoId}",
                Description = item.Snippet.Description,
                PublishedAt = item.Snippet.PublishedAtDateTimeOffset,
                ChannelTitle = item.Snippet.ChannelTitle,
                ThumbnailUrl = item.Snippet.Thumbnails.Default__.Url
            })
            .ToList();


        return JsonSerializer.Serialize(videoDetails);
    }
}