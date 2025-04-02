using Akka.Actor;
using Akka.Event;
using Ghost.Api.Core.Messages.Events;
using Ghost.Api.Services.X;

namespace Ghost.Api.Core.Actors.Events;

/// <summary>
/// A handler for Ghost-related events
/// </summary>
public class GhostEventHandler : ReceiveActor
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILoggingAdapter _logger;
    
    public GhostEventHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = Context.GetLogger();
        
        ReceiveAsync<ExternalEvents.PostPublished>(HandlePublishedPost);
    }

    /// <summary>
    /// Create social media posts for a newly published Post
    /// </summary>
    /// <param name="message"></param>
    private async Task HandlePublishedPost(ExternalEvents.PostPublished message)
    {
        try
        {
            _logger.Info(
                "Publishing post {0} at {1}",
                message.Post.Title,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            );
            
            using var scope = _serviceProvider.CreateScope();
            var xService = scope.ServiceProvider.GetRequiredService<IXService>();

            var postContent = $"{message.Post.Excerpt}\n\nRead more at {message.Post.Url}";
            await xService.PublishTweet(postContent);
            
            _logger.Info(
                "{0} posted at {1}",
                message.Post.Title,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            );
        }
        catch (Exception ex)
        {
            _logger.Error(
                "Error publishing post: {0}",
                ex.Message
            );
        }
    }
}