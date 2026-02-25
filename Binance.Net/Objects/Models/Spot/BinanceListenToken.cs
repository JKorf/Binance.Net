namespace Binance.Net.Objects.Models.Spot;

/// <summary>
/// Listen token
/// </summary>
public class BinanceListenToken
{
    /// <summary>
    /// Token
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
    /// <summary>
    /// Expiration time
    /// </summary>
    [JsonPropertyName("expirationTime")]
    public DateTime ExpirationTime { get; set; }
}