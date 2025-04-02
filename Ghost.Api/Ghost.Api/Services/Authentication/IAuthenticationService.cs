namespace Ghost.Api.Services.Authentication;

/// <summary>
/// Service for authenticating requests made to the API
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Validate that a request to a Ghost-related webhook endpoint is legitimate
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="clientSecret"></param>
    /// <returns></returns>
    bool ValidateGhostCredentials(string clientId, string clientSecret);
}