using Ghost.Api.Services.X.Models;

namespace Ghost.Api.Services.X;

/// <summary>
/// A service for interacting with the X platform (Twitter)
/// </summary>
public interface IXService
{
    /// <summary>
    /// Publishes a new tweet to X
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<TweetResult> PublishTweet(string message);
}