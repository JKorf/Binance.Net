
namespace Binance.Net.Objects.Models.Spot.IsolatedMargin
{
    /// <summary>
    /// Enabled account limit
    /// </summary>
    [SerializationModel]
    public record IsolatedMarginAccountLimit
    {
        /// <summary>
        /// ["<c>enabledAccount</c>"] Current enabled accounts
        /// </summary>
        [JsonPropertyName("enabledAccount")]
        public int EnabledAccount { get; set; }
        /// <summary>
        /// ["<c>maxAccount</c>"] Max accounts
        /// </summary>
        [JsonPropertyName("maxAccount")]
        public int MaxAccount { get; set; }
    }
}