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
        public BinanceSubAccountFuturesDetailV2Usdt UsdtMarginedFutures { get; set; }

        /// <summary>
        /// Delivery account response (COIN margined)
        /// </summary>
        [JsonProperty("deliveryAccountResp")]
        public BinanceSubAccountFuturesDetailV2 CoinMarginedFutures { get; set; }
    }

    /// <summary>
    /// Sub account futures details
    /// </summary>
    public class BinanceSubAccountFuturesDetailV2
    {
        /// <summary>
        /// Email of the sub account
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// List of asset details
        /// </summary>
        public IEnumerable<BinanceSubAccountFuturesAsset> Assets { get; set; } = Array.Empty<BinanceSubAccountFuturesAsset>();
        /// <summary>
        /// Can deposit
        /// </summary>
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Can trade
        /// </summary>
        public bool CanTrade { get; set; }
        /// <summary>
        /// Can withdraw
        /// </summary>
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Fee tier
        /// </summary>
        public int FeeTier { get; set; }
        /// <summary>
        /// Time of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Sub account futures details
    /// </summary>
    public class BinanceSubAccountFuturesDetailV2Usdt : BinanceSubAccountFuturesDetailV2
    {
        /// <summary>
        /// Max quantity which can be withdrawn
        /// </summary>
        [JsonProperty("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Total initial margin
        /// </summary>
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// Total maintenance margin
        /// </summary>
        public decimal TotalMaintenanceMargin { get; set; }
        /// <summary>
        /// Total margin balance
        /// </summary>
        public decimal TotalMarginBalance { get; set; }
        /// <summary>
        /// Total open order initial margin
        /// </summary>
        public decimal TotalOpenOrderInitialMargin { get; set; }
        /// <summary>
        /// Total position initial margin
        /// </summary>
        public decimal TotalPositionInitialMargin { get; set; }
        /// <summary>
        /// Total unrealized profit
        /// </summary>
        public decimal TotalUnrealizedProfit { get; set; }
        /// <summary>
        /// Total wallet balance
        /// </summary>
        public decimal TotalWalletBalance { get; set; }
    }
}
