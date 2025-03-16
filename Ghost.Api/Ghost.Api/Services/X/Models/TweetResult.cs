using System.Text.Json.Serialization;

namespace Ghost.Api.Services.X.Models;

public class TweetResult
{
    [JsonPropertyName("edit_history_tweet_ids")]
    public string[] EditHistoryTweetIds { get; set; } = [];
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}