using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot.Socket;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Wrapper for continuous kline information for a symbol
    /// </summary>
    [SerializationModel]
    public record BinanceStreamContinuousKlineData : BinanceStreamEvent, IBinanceStreamKlineData
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonPropertyName("ps")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The contract type
        /// </summary>
        [JsonPropertyName("ct")]
        public ContractType ContractType { get; set; } = ContractType.Unknown;

        /// <summary>
        /// The data
        /// </summary>
        [JsonPropertyName("k")]
        [JsonConverter(typeof(InterfaceConverter<BinanceStreamKline, IBinanceStreamKline>))]
        public IBinanceStreamKline Data { get; set; } = default!;
    }
}
