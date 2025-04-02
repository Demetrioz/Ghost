using Ghost.Api.Services.Ghost.Models.Posts;

namespace Ghost.Api.Core.Messages.Events;

/// <summary>
/// Events that occur outside the Actor system
/// </summary>
public static class ExternalEvents
{
    /// <summary>
    /// A new post was published on a Ghost blog
    /// </summary>
    /// <param name="Post"></param>
    public sealed record PostPublished(Post Post);
}