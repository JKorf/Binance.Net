using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Multi asset mode info
    /// </summary>
    public class BinanceFuturesMultiAssetMode
    {
        /// <summary>
        /// Is multi assets mode enabled
        /// </summary>
        [JsonProperty("multiAssetsMargin")]
        public bool MultiAssetMode { get; set; }
    }
}
