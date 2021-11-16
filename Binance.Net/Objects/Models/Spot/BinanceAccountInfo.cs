using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Binance.Net.Objects.Spot.SpotData
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceAccountInfo
    {
        /// <summary>
        /// Fee percentage to pay when making trades
        /// </summary>
        [JsonProperty("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Fee percentage to pay when taking trades
        /// </summary>
        [JsonProperty("takerCommission")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// Fee percentage to pay when buying
        /// </summary>
        [JsonProperty("buyerCommission")]
        public decimal BuyerFee { get; set; }
        /// <summary>
        /// Fee percentage to pay when selling
        /// </summary>
        [JsonProperty("sellerCommission")]
        public decimal SellerFee { get; set; }
        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        public bool CanTrade { get; set; }
        /// <summary>
        /// Boolean indicating if this account can withdraw
        /// </summary>
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Boolean indicating if this account can deposit
        /// </summary>
        public bool CanDeposit { get; set; }
        /// <summary>
        /// The time of the update
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The type of account
        /// </summary>
        [JsonConverter(typeof(AccountTypeConverter))]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// Permissions types
        /// </summary>
        [JsonProperty(ItemConverterType = typeof(AccountTypeConverter))]
        public IEnumerable<AccountType> Permissions { get; set; } = Array.Empty<AccountType>();
        /// <summary>
        /// List of assets with their current balances
        /// </summary>
        public IEnumerable<BinanceBalance> Balances { get; set; } = Array.Empty<BinanceBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class BinanceBalance: ICommonBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        [JsonProperty("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity that is currently locked in a trade
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Available + Locked;

        string ICommonBalance.CommonAsset => Asset;
        decimal ICommonBalance.CommonAvailable => Available;
        decimal ICommonBalance.CommonTotal => Total;
    }
}
