using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Futures.UserStream
{
    /// <summary>
    /// Information about levrage of symbol changed
    /// </summary>
    public class BinanceFuturesStreamConfigUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Update data
        /// </summary>
        [JsonProperty("ac")]
        public BinanceFuturesStreamConfigUpdateData? AccountSymbolConfiguration { get; set; }

        /// <summary>
        /// Update data
        /// </summary>
        [JsonProperty("ai")]
        public BinanceFuturesStreamUserConfigUpdateData? UserConfiguration { get; set; }

        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime TransactionTime { get; set; }
    }

    /// <summary>
    /// Config update data
    /// </summary>
    public class BinanceFuturesStreamConfigUpdateData
    {
        /// <summary>
        /// The symbol this balance is for
        /// </summary>
        [JsonProperty("s")]
        public string? Symbol { get; set; }

        /// <summary>
        /// The symbol this leverage is for
        /// </summary>
        [JsonProperty("l")]
        public int Leverage { get; set; }
    }

    /// <summary>
    /// User account config update
    /// </summary>
    public class BinanceFuturesStreamUserConfigUpdateData 
    { 
        /// <summary>
        /// Multi-asset mode enabled
        /// </summary>
        [JsonProperty("j")]
        public bool MultiAssetMode { get; set; }
    }
}
