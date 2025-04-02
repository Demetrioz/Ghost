using System.Text.Json.Serialization;

namespace Ghost.Api.Services.Ghost.Models.Authors;

/// <summary>
/// An author of a Ghost page or post
/// </summary>
public class Author : IGhostObject
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}