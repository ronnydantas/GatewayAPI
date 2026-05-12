using System.Text.Json.Serialization;

namespace Domain.UseCases.Login;

public class SignInViewModel
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("expiration")]
    public DateTime Expiration { get; set; }
}
