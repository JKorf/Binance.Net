namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about an asset for a user
    /// </summary>
    public record BinanceUserAsset
    {
        /// <summary>
        /// Asset code
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Deposit all is enabled
        /// </summary>
        [JsonPropertyName("depositAllEnable")]
        public bool DepositAllEnable { get; set; }
        /// <summary>
        /// Quantity free
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// Quantity frozen
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Freeze { get; set; }
        /// <summary>
        /// Ipo-able
        /// </summary>
        [JsonPropertyName("ipoable")]
        public decimal Ipoable { get; set; }
        /// <summary>
        /// Ipo-ing
        /// </summary>
        [JsonPropertyName("ipoing")]
        public decimal Ipoing { get; set; }
        /// <summary>
        /// Is the asset legally money
        /// </summary>
        [JsonPropertyName("isLegalMoney")]
        public bool IsLegalMoney { get; set; }
        /// <summary>
        /// Quantity locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// Storage
        /// </summary>
        [JsonPropertyName("storage")]
        public decimal Storage { get; set; }
        /// <summary>
        /// Is trading
        /// </summary>
        [JsonPropertyName("trading")]
        public bool Trading { get; set; }
        /// <summary>
        /// Withdraw all enabled
        /// </summary>
        [JsonPropertyName("withdrawAllEnable")]
        public bool WithdrawAllEnable { get; set; }
        /// <summary>
        /// Name of the asset
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Currently withdrawing
        /// </summary>
        [JsonPropertyName("withdrawing")]
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// Networks
        /// </summary>
        [JsonPropertyName("networkList")]
        public IEnumerable<BinanceNetwork> NetworkList { get; set; } = Array.Empty<BinanceNetwork>();
    }

    /// <summary>
    /// Network for an asset
    /// </summary>
    public record BinanceNetwork
    {
        /// <summary>
        /// Regex for an address on the network
        /// </summary>
        [JsonPropertyName("addressRegex")]
        public string AddressRegex { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Deposit description
        /// </summary>
        [JsonPropertyName("depositDesc")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// Deposit enabled
        /// </summary>
        [JsonPropertyName("depositEnable")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// Is default network
        /// </summary>
        [JsonPropertyName("isDefault")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// Regex for a memo
        /// </summary>
        [JsonPropertyName("memoRegex")]
        public string MemoRegex { get; set; } = string.Empty;
        /// <summary>
        /// Minimal confirmations for balance confirmation
        /// </summary>
        [JsonPropertyName("minConfirm")]
        public int MinConfirmations { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Tips
        /// </summary>
        [JsonPropertyName("specialTips")]
        public string SpecialTips { get; set; } = string.Empty;
        /// <summary>
        /// Confirmation number for balance unlock
        /// </summary>
        [JsonPropertyName("unlockConfirm")]
        public int UnlockConfirm { get; set; }

        /// <summary>
        /// Withdraw description
        /// </summary>
        [JsonPropertyName("withdrawDesc")]
        public string WithdrawDescription { get; set; } = string.Empty;
        /// <summary>
        /// Withdraw is enabled
        /// </summary>
        [JsonPropertyName("withdrawEnable")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// Fee for withdrawing
        /// </summary>
        [JsonPropertyName("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// Minimal withdraw quantity
        /// </summary>
        [JsonPropertyName("withdrawMin")]
        public decimal WithdrawMin { get; set; }
        /// <summary>
        /// Min withdraw step
        /// </summary>
        [JsonPropertyName("withdrawIntegerMultiple")]
        public decimal WithdrawIntegerMultiple { get; set; }
        /// <summary>
        /// Max withdraw quantity
        /// </summary>
        [JsonPropertyName("withdrawMax")]
        public decimal WithdrawMax { get; set; }
        /// <summary>
        /// If the asset needs to provide memo to withdraw
        /// </summary>
        [JsonPropertyName("sameAddress")]
        public bool SameAddress { get; set; }
        /// <summary>
        /// Estimated arrival time
        /// </summary>
        [JsonPropertyName("estimatedArrivalTime")]
        public int? EstimatedArrivalTime { get; set; }
        /// <summary>
        /// Whether the network is busy
        /// </summary>
        [JsonPropertyName("busy")]
        public bool Busy { get; set; }
    }
}
