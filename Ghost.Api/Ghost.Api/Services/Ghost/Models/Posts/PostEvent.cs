using System.Text.Json.Serialization;

namespace Ghost.Api.Services.Ghost.Models.Posts;

/// <summary>
/// The current and previous versions of a Post
/// </summary>
public class PostEvent : IGhostEvent<Post>
{
    [JsonPropertyName("current")]
    public Post Current { get; set; } = new();
    
    [JsonPropertyName("previous")]
    public Post Previous { get; set; } = new();
}