using Akka.Actor;
using Akka.DependencyInjection;
using Ghost.Api.Core.Messages.Events;

namespace Ghost.Api.Core.Actors.Events;

/// <summary>
/// A root actor responsible for handling incoming webhooks to the API
/// </summary>
public class EventManager : ReceiveActor
{
    private readonly IServiceProvider _serviceProvider;
    
    public EventManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        
        ReceiveAny(ForwardToHandler);
    }

    protected override void PreStart()
    {
        InitializeHandlers();
    }

    /// <summary>
    /// Creates handlers for various event types
    /// </summary>
    private void InitializeHandlers()
    {
        var props = DependencyResolver.For(Context.System).Props<GhostEventHandler>();
        _ = Context.ActorOf(props, nameof(GhostEventHandler));
    }

    /// <summary>
    /// Forwards a request to the appropriate handler
    /// </summary>
    /// <param name="message"></param>
    private void ForwardToHandler(object message)
    {
        switch (message)
        {
            case ExternalEvents.PostPublished post:
                Context.Child(nameof(GhostEventHandler)).Tell(message);
                break;
        }
    }
}