using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Buy/sell volume ratio
    /// </summary>
    public record BinanceFuturesCoinBuySellVolumeRatio
    {
        /// <summary>
        /// The pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// The taker buy volume
        /// </summary>
        [JsonPropertyName("takerBuyVol")]
        public decimal TakerBuyVolume { get; set; }
        /// <summary>
        /// The taker sell volume
        /// </summary>
        [JsonPropertyName("takerSellVol")]
        public decimal TakerSellVolume { get; set; }
        /// <summary>
        /// The taker buy value
        /// </summary>
        [JsonPropertyName("takerBuyVolValue")]
        public decimal TakerBuyVolumeValue { get; set; }
        /// <summary>
        /// The taker sell value
        /// </summary>
        [JsonPropertyName("takerSellVolValue")]
        public decimal TakerSellVolumeValue { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
