using System.Text.Json.Serialization;

namespace WordsApp.Authentication.Internals;

internal class UserClaim
{
    [JsonPropertyName("typ")]
    public string Type { get; set; } = string.Empty;
    [JsonPropertyName("val")]
    public string Value { get; set; } = string.Empty;
}