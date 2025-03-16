using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Ghost.Api.Services.X.Models;
using Microsoft.Extensions.Options;

namespace Ghost.Api.Services.X;

/// <inheritdoc />
public class XService : IXService
{
    private readonly XSettings _settings;
    private readonly HttpClient _httpClient;

    private readonly DateTime _epochUtc;

    private readonly string _xBaseUrl = "https://api.twitter.com/2";

    public XService(
        IOptions<XSettings> options,
        HttpClient httpClient
    )
    {
        _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        _httpClient.DefaultRequestHeaders.Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        _epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
    
    /// <inheritdoc />
    public async Task<TweetResult> PublishTweet(string message)
    {
        var request = BuildXRequest(message);
        var response = await _httpClient.SendAsync(request);
        
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Error publishing tweet: {response.StatusCode} :: {responseBody}");
        
        var typedResponse = JsonSerializer.Deserialize<XResponse>(responseBody);
        if (typedResponse == null)
            throw new ApplicationException($"Error deserializing response content: {responseBody}");
        
        return typedResponse.Data;
    }

    /// <summary>
    /// Builds a request to send to the X Api
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private HttpRequestMessage BuildXRequest(string message)
    {
        var fullUrl = $"{_xBaseUrl}/tweets";
        var timestamp = (int)(DateTime.UtcNow - _epochUtc).TotalSeconds;

        Dictionary<string, string> data = new()
        {
            { "oauth_consumer_key", _settings.ApiKey },
            { "oauth_signature_method", "HMAC-SHA1" },
            { "oauth_timestamp", timestamp.ToString() },
            { "oauth_nonce", Guid.NewGuid().ToString() },
            { "oauth_token", _settings.AccessToken },
            { "oauth_version", "1.0" },
        };
        
        data.Add("oauth_signature", GenerateSignature(fullUrl, data));
        
        var header = GenerateHeader(data);

        var bodyContent = new { text = message };
        var body = new StringContent(
            JsonSerializer.Serialize(bodyContent),
            Encoding.UTF8,
            "application/json"
        );
        
        var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
        request.Headers.Add("Authorization", header);
        request.Content = body;
        
        return request;
    }
    
    /// <summary>
    /// Generates a signature for publishing a tweet
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private string GenerateSignature(string url, Dictionary<string, string> data)
    {
        var signatureString = string.Join(
            "&", 
            data
                .Union(data)
                .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}")
                .OrderBy(s => s)
        );

        var fullSignatureData =
            $"POST&{Uri.EscapeDataString(url)}&{Uri.EscapeDataString(signatureString ?? string.Empty)}";

        var secretKey = $"{_settings.ApiSecret}&{_settings.AccessTokenSecret}";
        var secretKeyBytes = new ASCIIEncoding().GetBytes(secretKey);
        using var hasher = new HMACSHA1(secretKeyBytes);
        
        var signature = hasher.ComputeHash(new ASCIIEncoding().GetBytes(fullSignatureData ?? string.Empty));
        return Convert.ToBase64String(signature);
    }
    
    /// <summary>
    /// Generates a header for publishing a tweet
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private string GenerateHeader(Dictionary<string, string> data)
    {
        return "OAuth " + string.Join(
            ", ",
            data
                .Where(kvp => kvp.Key.StartsWith("oauth_"))
                .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}=\"{Uri.EscapeDataString(kvp.Value)}\"")
                .OrderBy(s => s)
        );
    }
}