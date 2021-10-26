using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.UserStream
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceStreamAccountInfo: BinanceStreamEvent
    {
        /// <summary>
        /// Time of last account update
        /// </summary>
        [JsonProperty("u"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Fee percentage to pay when making trades
        /// </summary>
        [JsonProperty("m")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Fee percentage to pay when taking trades
        /// </summary>
        [JsonProperty("t")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// Fee percentage to pay when buying
        /// </summary>
        [JsonProperty("b")]
        public decimal BuyerFee { get; set; }
        /// <summary>
        /// Fee percentage to pay when selling
        /// </summary>
        [JsonProperty("s")]
        public decimal SellerFee { get; set; }
        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        [JsonProperty("T")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Boolean indicating if this account can withdraw
        /// </summary>
        [JsonProperty("W")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Boolean indicating if this account can deposit
        /// </summary>
        [JsonProperty("D")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Permissions types
        /// </summary>
        [JsonProperty("P", ItemConverterType = typeof(AccountTypeConverter))]
        public IEnumerable<AccountType> Permissions { get; set; } = Array.Empty<AccountType>();
        /// <summary>
        /// List of assets with their current balances
        /// </summary>
        [JsonProperty("B")]
        public IEnumerable<BinanceStreamBalance> Balances { get; set; } = Array.Empty<BinanceStreamBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class BinanceStreamBalance: ICommonBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonProperty("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        [JsonProperty("f")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity that is currently locked in a trade
        /// </summary>
        [JsonProperty("l")]
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
