using System.Text.Json.Serialization;

namespace Ghost.Api.Services.Ghost.Models.Posts;

/// <summary>
/// The payload from Ghost when a post event is triggered
/// </summary>
public class PostEventPayload
{
    [JsonPropertyName("post")]
    public PostEvent Post { get; set; } = new(); 
}