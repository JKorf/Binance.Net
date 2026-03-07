using Binance.Net.Enums;
using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about an account
    /// </summary>
    [SerializationModel]
    public record BinanceAccountInfo
    {
        /// <summary>
        /// ["<c>makerCommission</c>"] Fee percentage to pay when making trades
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>takerCommission</c>"] Fee percentage to pay when taking trades
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// ["<c>buyerCommission</c>"] Fee percentage to pay when buying
        /// </summary>
        [JsonPropertyName("buyerCommission")]
        public decimal BuyerFee { get; set; }
        /// <summary>
        /// ["<c>sellerCommission</c>"] Fee percentage to pay when selling
        /// </summary>
        [JsonPropertyName("sellerCommission")]
        public decimal SellerFee { get; set; }
        /// <summary>
        /// ["<c>canTrade</c>"] Boolean indicating if this account can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// ["<c>canWithdraw</c>"] Boolean indicating if this account can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// ["<c>canDeposit</c>"] Boolean indicating if this account can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// ["<c>brokered</c>"] Account is brokered
        /// </summary>
        [JsonPropertyName("brokered")]
        public bool Brokered { get; set; }
        /// <summary>
        /// ["<c>requireSelfTradePrevention</c>"] Require self trade prevention
        /// </summary>
        [JsonPropertyName("requireSelfTradePrevention")]
        public bool RequireSelfTradePrevention { get; set; }
        /// <summary>
        /// ["<c>preventSor</c>"] Prevent smart order routing
        /// </summary>
        [JsonPropertyName("preventSor")]
        public bool PreventSmartOrderRouting { get; set; }
        /// <summary>
        /// ["<c>uid</c>"] The user identifier.
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] The time of the update
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>accountType</c>"] The type of account
        /// </summary>
        [JsonPropertyName("accountType")]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// ["<c>permissions</c>"] Permissions types
        /// </summary>
        [JsonPropertyName("permissions")]
        public PermissionType[] Permissions { get; set; } = Array.Empty<PermissionType>();
        /// <summary>
        /// ["<c>balances</c>"] List of assets with their current balances
        /// </summary>
        [JsonPropertyName("balances")]
        public BinanceBalance[] Balances { get; set; } = Array.Empty<BinanceBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public record BinanceBalance : IBinanceBalance
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset this balance is for
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>free</c>"] The quantity that isn't locked in a trade
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] The quantity that is currently locked in a trade
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Available + Locked;
    }
}

