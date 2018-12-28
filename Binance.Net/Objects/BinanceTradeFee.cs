using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceTradeFeeWrapper
    {
        [JsonOptionalProperty]
        [JsonProperty("msg")]
        public string Message { get; set; }
        public bool Success { get; set; }
        [JsonProperty("tradeFee")]
        public BinanceTradeFee[] Data { get; set; }
    }

    public class BinanceTradeFee
    {
        /// <summary>
        /// The symbol this fee is for
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The fee for trades where you're the maker
        /// </summary>
        [JsonProperty("maker")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// The fee for trades where you're the taker
        /// </summary>
        [JsonProperty("taker")]
        public decimal TakerFee { get; set; }
    }
}
