using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceFuturesAccountInfo
    {
        /// <summary>
        /// Information about an account assets
        /// </summary>
        public IEnumerable<BinanceFuturesAccountAsset> Assets { get; set; } = new List<BinanceFuturesAccountAsset>();
        
        /// <summary>
        /// Boolean indicating if this account can deposite
        /// </summary>
        public bool CanDeposite { get; set; }

        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        public bool CanTrade { get; set; }

        /// <summary>
        /// Boolean indicating if this account can withdraw
        /// </summary>
        public bool CanWithdraw { get; set; }

        /// <summary>
        /// Fee tier
        /// </summary>
        public int FeeTier { get; set; }

        /// <summary>
        /// Maximum withdraw amount
        /// </summary>
        public decimal MaxWithdrawAmount { get; set; }

        /// <summary>
        /// Information about an account positions
        /// </summary>
        public IEnumerable<BinanceFuturesAccountPosition> Positions { get; set; } = new List<BinanceFuturesAccountPosition>();

        /// <summary>
        /// Total initial margin
        /// </summary>
        public decimal TotalInitialMargin { get; set; }

        /// <summary>
        /// Total maint margin
        /// </summary>
        public decimal TotalMaintMargin { get; set; }

        /// <summary>
        /// Total margin balance
        /// </summary>
        public decimal TotalMarginBalance { get; set; }

        /// <summary>
        /// Total open order initial margin
        /// </summary>
        public decimal TotalOpenOrderInitialMargin { get; set; }

        /// <summary>
        /// Total positional initial margin
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

        /// <summary>
        /// The time of account info was updated
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }
}
