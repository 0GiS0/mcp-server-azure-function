using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Text.Json;

namespace McpTools;

public class YouTubeTool(ILogger<YouTubeTool> logger)
{
    [Function(nameof(GetYouTubeVideo))]
    public async Task<string> GetYouTubeVideo(
        [McpToolTrigger("searchVideo", "Search for a video on YouTube")]
            ToolInvocationContext context,
        [McpToolProperty("topic", "string", "The topic to search for")]
            string topic
    )
    {
        logger.LogInformation($"Looking for videos related with {topic}");
        
        var apiKey = Environment.GetEnvironmentVariable("YouTubeApiKey");
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("YouTube API key is not configured.");
        }

        var youTubeService = new YouTubeService(new BaseClientService.Initializer
        {
            ApiKey = apiKey,
        });

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
                Url = $"https://www.youtube.com/watch?v={item.Id.VideoId}"
            })
            .ToList();


        return System.Text.Json.JsonSerializer.Serialize(videoDetails);
    }
}