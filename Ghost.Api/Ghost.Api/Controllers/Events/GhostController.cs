using Akka.Actor;
using Akka.Hosting;
using Ghost.Api.Core.Actors.Events;
using Ghost.Api.Core.Messages.Events;
using Ghost.Api.Services.Authentication;
using Ghost.Api.Services.Ghost.Models.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Ghost.Api.Controllers.Events;

/// <summary>
/// Controller for handling events from Ghost
/// </summary>
[ApiController]
[Route("Events/Ghost")]
public class GhostController : ControllerBase
{
    private readonly ILogger<GhostController> _logger;
    private readonly IAuthenticationService _authenticationService;
    private readonly IActorRef _eventManager;

    public GhostController(
        ILogger<GhostController> logger,
        IAuthenticationService authenticationService,
        IActorRegistry registry
    )
    {
        _logger = logger;
        _authenticationService = authenticationService;

        if (!registry.TryGet<EventManager>(out _eventManager))
            throw new ApplicationException($"{nameof(EventManager)} not registered");
    }

    /// <summary>
    /// Perform any required actions for newly published posts
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="clientSecret"></param>
    /// <param name="payload"></param>
    /// <returns></returns>
    [HttpPost("Post/Published")]
    public IActionResult PostPublished(
        [FromQuery] string? clientId,
        [FromQuery] string? clientSecret,
        [FromBody] PostEventPayload payload
    )
    {
        if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
        {
            _logger.LogWarning("Unauthorized attempt to publish a ghost blog with missing credentials");
            return Unauthorized();
        }

        if (!_authenticationService.ValidateGhostCredentials(clientId, clientSecret))
        {
            _logger.LogWarning(
                "Unauthorized attempt to publish a ghost blog with clientId [{0}] and secret [{1}]",
                clientId, 
                clientSecret
            );
            return Unauthorized();
        }
        
        _eventManager.Tell(new ExternalEvents.PostPublished(payload.Post.Current));
        return Ok();
    }
}