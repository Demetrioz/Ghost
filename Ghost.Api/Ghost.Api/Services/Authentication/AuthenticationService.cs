using Ghost.Api.Configurations;
using Microsoft.Extensions.Options;

namespace Ghost.Api.Services.Authentication;

/// <inheritdoc />
public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationSettings _settings;

    public AuthenticationService(IOptions<AuthenticationSettings> options)
    {
        _settings = options.Value;
    }
    
    /// <inheritdoc />
    public bool ValidateGhostCredentials(string clientId, string clientSecret)
        => clientId == _settings.GhostClientId && clientSecret == _settings.GhostClientSecret;
}