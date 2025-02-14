using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public record BinanceAccountInfo
    {
        /// <summary>
        /// Fee percentage to pay when making trades
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Fee percentage to pay when taking trades
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// Fee percentage to pay when buying
        /// </summary>
        [JsonPropertyName("buyerCommission")]
        public decimal BuyerFee { get; set; }
        /// <summary>
        /// Fee percentage to pay when selling
        /// </summary>
        [JsonPropertyName("sellerCommission")]
        public decimal SellerFee { get; set; }
        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Boolean indicating if this account can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Boolean indicating if this account can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Account is brokered
        /// </summary>
        [JsonPropertyName("brokered")]
        public bool Brokered { get; set; }
        /// <summary>
        /// Require self trade prevention
        /// </summary>
        [JsonPropertyName("requireSelfTradePrevention")]
        public bool RequireSelfTradePrevention { get; set; }
        /// <summary>
        /// Prevent smart order routing
        /// </summary>
        [JsonPropertyName("preventSor")]
        public bool PreventSmartOrderRouting { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
        /// <summary>
        /// The time of the update
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The type of account
        /// </summary>
        [JsonConverter(typeof(PocAOTEnumConverter<AccountType>))]
        [JsonPropertyName("accountType")]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// Permissions types
        /// </summary>
        [JsonPropertyName("permissions")]
        public IEnumerable<PermissionType> Permissions { get; set; } = Array.Empty<PermissionType>();
        /// <summary>
        /// List of assets with their current balances
        /// </summary>
        [JsonPropertyName("balances")]
        public IEnumerable<BinanceBalance> Balances { get; set; } = Array.Empty<BinanceBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public record BinanceBalance : IBinanceBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity that is currently locked in a trade
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Available + Locked;
    }
}
