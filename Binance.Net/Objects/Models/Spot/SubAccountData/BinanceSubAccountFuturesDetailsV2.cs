using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account futures details
    /// </summary>
    public class BinanceSubAccountFuturesDetailsV2
    {
        /// <summary>
        /// Futures account response (USDT margined)
        /// </summary>
        [JsonProperty("futureAccountResp")]
        public BinanceSubAccountFuturesDetails UsdtMarginedFutures { get; set; }

        /// <summary>
        /// Delivery account response (COIN margined)
        /// </summary>
        [JsonProperty("deliveryAccountResp")]
        public BinanceSubAccountFuturesDetails CoinMarginedFutures { get; set; }
    }
}
