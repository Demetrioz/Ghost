using System.Text.Json.Serialization;

namespace Ghost.Api.Services.Ghost.Models.Tags;

/// <summary>
/// A tag for a Ghost post or page
/// </summary>
public class Tag : IGhostObject
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; } = string.Empty;
    
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Url of the featured image
    /// </summary>
    [JsonPropertyName("feature_image")]
    public string? FeatureImage { get; set; } = string.Empty;
}