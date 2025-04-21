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


    [Function(nameof(SearchYouTubeVideo))]
    public async Task<string> SearchYouTubeVideo(
        [McpToolTrigger("search_youtube_video", "Search for a video on YouTube")]
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


    [Function(nameof(GetYouTubeChannel))]
    public async Task<string> GetYouTubeChannel(
        [McpToolTrigger("get_youtube_channel", "Get channel information")]
            ToolInvocationContext context,
        [McpToolProperty("channel_name", "string", "The channel name")]
            string channelName
    )
    {
        logger.LogInformation($"Getting channel information for {channelName}");

        var channelsRequest = youTubeService.Channels.List("snippet");
        channelsRequest.ForHandle = channelName;

        var channelsResponse = await channelsRequest.ExecuteAsync();

        if (channelsResponse.Items == null || channelsResponse.Items.Count == 0)
        {
            return JsonSerializer.Serialize(new { Error = "Channel not found" });
        }

        // return title and url
        var channelDetails = channelsResponse.Items
            .Select(item => new
            {
                Title = item.Snippet.Title,
                Url = $"https://www.youtube.com/{item.Snippet.CustomUrl}",
                Description = item.Snippet.Description,
                PublishedAt = item.Snippet.PublishedAtDateTimeOffset,
                ThumbnailUrl = item.Snippet.Thumbnails.Default__.Url,
                Language = item.Snippet.DefaultLanguage,
                Country = item.Snippet.Country
            })
            .ToList();

        return JsonSerializer.Serialize(channelDetails);
    }
}