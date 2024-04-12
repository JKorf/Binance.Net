using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot.Socket;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Wrapper for continuous kline information for a symbol
    /// </summary>
    public class BinanceStreamContinuousKlineData: BinanceStreamEvent, IBinanceStreamKlineData
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonProperty("ps")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The contract type
        /// </summary>
        [JsonProperty("ct")]
        public ContractType ContractType { get; set; } = ContractType.Unknown;

        /// <summary>
        /// The data
        /// </summary>
        [JsonProperty("k")]
        [JsonConverter(typeof(InterfaceConverter<BinanceStreamKline>))]
        public IBinanceStreamKline Data { get; set; } = default!;
    }
}
