namespace Ghost.Api.Services.X;

public class XSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string AccessTokenSecret { get; set; } = string.Empty;
    public string BearerToken { get; set; } = string.Empty;
}