using System.Text.Json.Serialization;
using Ghost.Api.Services.Ghost.Models.Authors;
using Ghost.Api.Services.Ghost.Models.Tags;

namespace Ghost.Api.Services.Ghost.Models.Posts;

/// <summary>
/// A post from a Ghost blog
/// </summary>
public class Post : IGhostObject
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonPropertyName("uuid")]
    public string Uuid { get; set; } = string.Empty;
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    [JsonPropertyName("html")]
    public string Html { get; set; } = string.Empty;

    /// <summary>
    /// Url of the featured image
    /// </summary>
    [JsonPropertyName("feature_image")]
    public string? FeatureImage { get; set; } = string.Empty;

    [JsonPropertyName("excerpt")]
    public string? Excerpt { get; set; } = string.Empty;

    [JsonPropertyName("authors")]
    public List<Author> Authors { get; set; } = [];

    [JsonPropertyName("primary_author")]
    public Author PrimaryAuthor { get; set; } = new();

    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; set; } = [];

    [JsonPropertyName("primary_tag")]
    public Tag? PrimaryTag { get; set; }
}