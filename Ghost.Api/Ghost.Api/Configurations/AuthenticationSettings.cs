namespace Ghost.Api.Configurations;

/// <summary>
/// Settings for authenticating requests to the API
/// </summary>
public class AuthenticationSettings
{
    /// <summary>
    /// A clientId that is provided in the query of Ghost webhooks
    /// </summary>
    public string GhostClientId { get; set; } = string.Empty;
    
    /// <summary>
    /// A clientSecret that is provided in the query of Ghost webhooks
    /// </summary>
    public string GhostClientSecret { get; set; } = string.Empty;
}