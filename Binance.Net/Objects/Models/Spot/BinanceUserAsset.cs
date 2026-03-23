namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about an asset for a user
    /// </summary>
    [SerializationModel]
    public record BinanceUserAsset
    {
        /// <summary>
        /// ["<c>coin</c>"] Asset code
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositAllEnable</c>"] Deposit all is enabled
        /// </summary>
        [JsonPropertyName("depositAllEnable")]
        public bool DepositAllEnable { get; set; }
        /// <summary>
        /// ["<c>free</c>"] Quantity free
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>freeze</c>"] Quantity frozen
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Freeze { get; set; }
        /// <summary>
        /// ["<c>ipoable</c>"] Ipo-able
        /// </summary>
        [JsonPropertyName("ipoable")]
        public decimal Ipoable { get; set; }
        /// <summary>
        /// ["<c>ipoing</c>"] Ipo-ing
        /// </summary>
        [JsonPropertyName("ipoing")]
        public decimal Ipoing { get; set; }
        /// <summary>
        /// ["<c>isLegalMoney</c>"] Is the asset legally money
        /// </summary>
        [JsonPropertyName("isLegalMoney")]
        public bool IsLegalMoney { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Quantity locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// ["<c>storage</c>"] Storage
        /// </summary>
        [JsonPropertyName("storage")]
        public decimal Storage { get; set; }
        /// <summary>
        /// ["<c>trading</c>"] Is trading
        /// </summary>
        [JsonPropertyName("trading")]
        public bool Trading { get; set; }
        /// <summary>
        /// ["<c>withdrawAllEnable</c>"] Withdraw all enabled
        /// </summary>
        [JsonPropertyName("withdrawAllEnable")]
        public bool WithdrawAllEnable { get; set; }
        /// <summary>
        /// ["<c>name</c>"] Name of the asset
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawing</c>"] Currently withdrawing
        /// </summary>
        [JsonPropertyName("withdrawing")]
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// ["<c>networkList</c>"] Networks
        /// </summary>
        [JsonPropertyName("networkList")]
        public BinanceNetwork[] NetworkList { get; set; } = Array.Empty<BinanceNetwork>();
    }

    /// <summary>
    /// Network for an asset
    /// </summary>
    public record BinanceNetwork
    {
        /// <summary>
        /// ["<c>addressRegex</c>"] Regex for an address on the network
        /// </summary>
        [JsonPropertyName("addressRegex")]
        public string AddressRegex { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coin</c>"] Asset name
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositDesc</c>"] Deposit description
        /// </summary>
        [JsonPropertyName("depositDesc")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositEnable</c>"] Deposit enabled
        /// </summary>
        [JsonPropertyName("depositEnable")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// ["<c>isDefault</c>"] Is default network
        /// </summary>
        [JsonPropertyName("isDefault")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// ["<c>memoRegex</c>"] Regex for a memo
        /// </summary>
        [JsonPropertyName("memoRegex")]
        public string MemoRegex { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>minConfirm</c>"] Minimal confirmations for balance confirmation
        /// </summary>
        [JsonPropertyName("minConfirm")]
        public int MinConfirmations { get; set; }
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractAddress</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractAddressUrl</c>"] Contract address URL
        /// </summary>
        [JsonPropertyName("contractAddressUrl")]
        public string ContractAddressUrl { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>specialTips</c>"] Tips
        /// </summary>
        [JsonPropertyName("specialTips")]
        public string SpecialTips { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>specialWithdrawTips</c>"] Withdraw tips
        /// </summary>
        [JsonPropertyName("specialWithdrawTips")]
        public string SpecialWithdrawTips { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>unLockConfirm</c>"] Confirmation number for balance unlock
        /// </summary>
        [JsonPropertyName("unLockConfirm")]
        public int UnlockConfirm { get; set; }
        /// <summary>
        /// ["<c>resetAddressStatus</c>"] Reset address status
        /// </summary>
        [JsonPropertyName("resetAddressStatus")]
        public bool ResetAddressStatus { get; set; }
        /// <summary>
        /// ["<c>withdrawDesc</c>"] Withdraw description
        /// </summary>
        [JsonPropertyName("withdrawDesc")]
        public string WithdrawDescription { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawEnable</c>"] Withdraw is enabled
        /// </summary>
        [JsonPropertyName("withdrawEnable")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>withdrawFee</c>"] Fee for withdrawing
        /// </summary>
        [JsonPropertyName("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// ["<c>withdrawMin</c>"] Minimal withdraw quantity
        /// </summary>
        [JsonPropertyName("withdrawMin")]
        public decimal WithdrawMin { get; set; }
        /// <summary>
        /// ["<c>withdrawIntegerMultiple</c>"] Min withdraw step
        /// </summary>
        [JsonPropertyName("withdrawIntegerMultiple")]
        public decimal WithdrawIntegerMultiple { get; set; }
        /// <summary>
        /// ["<c>withdrawMax</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("withdrawMax")]
        public decimal WithdrawMax { get; set; }
        /// <summary>
        /// ["<c>sameAddress</c>"] If the asset needs to provide memo to withdraw
        /// </summary>
        [JsonPropertyName("sameAddress")]
        public bool SameAddress { get; set; }
        /// <summary>
        /// ["<c>withdrawTag</c>"] If a tag needs to be provided when withdrawing the asset
        /// </summary>
        [JsonPropertyName("withdrawTag")]
        public bool WithdrawNeedsTag { get; set; }
        /// <summary>
        /// ["<c>estimatedArrivalTime</c>"] Estimated arrival time
        /// </summary>
        [JsonPropertyName("estimatedArrivalTime")]
        public int? EstimatedArrivalTime { get; set; }
        /// <summary>
        /// ["<c>busy</c>"] Whether the network is busy
        /// </summary>
        [JsonPropertyName("busy")]
        public bool Busy { get; set; }
        /// <summary>
        /// ["<c>withdrawInternalMin</c>"] Min withdraw quantity for internal withdrawals
        /// </summary>
        [JsonPropertyName("withdrawInternalMin")]
        public decimal WithdrawInternalMin { get; set; }
        /// <summary>
        /// ["<c>denomination</c>"] The denomination of the asset. For example if 100000 it means that 1 of this asset means 100000 of the underlying asset
        /// </summary>
        [JsonPropertyName("denomination")]
        public decimal? Denomination { get; set; }
        /// <summary>
        /// ["<c>depositDust</c>"] Deposit dust
        /// </summary>
        [JsonPropertyName("depositDust")]
        public decimal DepositDust { get; set; }
    }
}

