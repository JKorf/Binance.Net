using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Positions update
    /// </summary>
    [SerializationModel]
    public record BinanceStreamPositionsUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>u</c>"] Time of last account update
        /// </summary>
        [JsonPropertyName("u"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// API key this update was for.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>B</c>"] Balances
        /// </summary>
        [JsonPropertyName("B")]
        public BinanceStreamBalance[] Balances { get; set; } = Array.Empty<BinanceStreamBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public record BinanceStreamBalance : IBinanceBalance
    {
        /// <summary>
        /// ["<c>a</c>"] The asset this balance is for
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>f</c>"] The quantity that isn't locked in a trade
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>l</c>"] The quantity that is currently locked in a trade
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Available + Locked;
    }
}

