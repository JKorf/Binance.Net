namespace Binance.Net.Objects.Models.Spot;

/// <summary>
/// Listen token
/// </summary>
public class BinanceListenToken
{
    /// <summary>
    /// ["<c>token</c>"] The listen token.
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>expirationTime</c>"] Expiration time
    /// </summary>
    [JsonPropertyName("expirationTime")]
    public DateTime ExpirationTime { get; set; }
}