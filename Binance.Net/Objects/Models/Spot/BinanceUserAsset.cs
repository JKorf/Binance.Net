namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about an asset for a user
    /// </summary>
    public class BinanceUserAsset
    {
        /// <summary>
        /// Asset code
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Deposit all is enabled
        /// </summary>
        public bool DepositAllEnable { get; set; }
        /// <summary>
        /// Quantity free
        /// </summary>
        [JsonProperty("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// Quantity frozen
        /// </summary>
        public decimal Freeze { get; set; }
        /// <summary>
        /// Ipo-able
        /// </summary>
        public decimal Ipoable { get; set; }
        /// <summary>
        /// Ipo-ing
        /// </summary>
        public decimal Ipoing { get; set; }
        /// <summary>
        /// Is the asset legally money
        /// </summary>
        public bool IsLegalMoney { get; set; }
        /// <summary>
        /// Quantity locked
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// Storage
        /// </summary>
        public decimal Storage { get; set; }
        /// <summary>
        /// Is trading
        /// </summary>
        public bool Trading { get; set; }
        /// <summary>
        /// Withdraw all enabled
        /// </summary>
        public bool WithdrawAllEnable { get; set; }
        /// <summary>
        /// Name of the asset
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Currently withdrawing
        /// </summary>
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// Networks
        /// </summary>
        public IEnumerable<BinanceNetwork> NetworkList { get; set; } = Array.Empty<BinanceNetwork>();
    }

    /// <summary>
    /// Network for an asset
    /// </summary>
    public class BinanceNetwork
    {
        /// <summary>
        /// Regex for an address on the network
        /// </summary>
        public string AddressRegex { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Deposit description
        /// </summary>
        [JsonProperty("depositDesc")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// Deposit enabled
        /// </summary>
        [JsonProperty("depositEnable")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// Is default network
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// Regex for a memo
        /// </summary>
        public string MemoRegex { get; set; } = string.Empty;
        /// <summary>
        /// Minimal confirmations for balance confirmation
        /// </summary>
        [JsonProperty("minConfirm")]
        public int MinConfirmations { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Tips
        /// </summary>
        public string SpecialTips { get; set; } = string.Empty;
        /// <summary>
        /// Confirmation number for balance unlock
        /// </summary>
        public int UnlockConfirm { get; set; }

        /// <summary>
        /// Withdraw description
        /// </summary>
        [JsonProperty("withdrawDesc")]
        public string WithdrawDescription { get; set; } = string.Empty;
        /// <summary>
        /// Withdraw is enabled
        /// </summary>
        [JsonProperty("withdrawEnable")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// Fee for withdrawing
        /// </summary>
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// Minimal withdraw quantity
        /// </summary>
        public decimal WithdrawMin { get; set; }
        /// <summary>
        /// Min withdraw step
        /// </summary>
        public decimal WithdrawIntegerMultiple { get; set; }
        /// <summary>
        /// Max withdraw quantity
        /// </summary>
        public decimal WithdrawMax { get; set; }
        /// <summary>
        /// If the asset needs to provide memo to withdraw
        /// </summary>
        public bool SameAddress { get; set; }
        /// <summary>
        /// Estimated arrival time
        /// </summary>
        [JsonProperty("estimatedArrivalTime")]
        public int? EstimatedArrivalTime { get; set; }
        /// <summary>
        /// Whether the network is busy
        /// </summary>
        [JsonProperty("busy")]
        public bool Busy { get; set; }
    }
}
