using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Buy/sell volume ratio
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinBuySellVolumeRatio
    {
        /// <summary>
        /// ["<c>pair</c>"] The futures pair.
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractType</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>takerBuyVol</c>"] The taker buy volume
        /// </summary>
        [JsonPropertyName("takerBuyVol")]
        public decimal TakerBuyVolume { get; set; }
        /// <summary>
        /// ["<c>takerSellVol</c>"] The taker sell volume
        /// </summary>
        [JsonPropertyName("takerSellVol")]
        public decimal TakerSellVolume { get; set; }
        /// <summary>
        /// ["<c>takerBuyVolValue</c>"] The taker buy value
        /// </summary>
        [JsonPropertyName("takerBuyVolValue")]
        public decimal TakerBuyVolumeValue { get; set; }
        /// <summary>
        /// ["<c>takerSellVolValue</c>"] The taker sell value
        /// </summary>
        [JsonPropertyName("takerSellVolValue")]
        public decimal TakerSellVolumeValue { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] The data timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}

