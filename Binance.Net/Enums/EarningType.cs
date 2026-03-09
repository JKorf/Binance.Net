using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Mining earnings type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<EarningType>))]
    public enum EarningType
    {
        /// <summary>
        /// ["<c>0</c>"] Mining wallet
        /// </summary>
        [Map("0")]
        MiningWallet,
        /// <summary>
        /// ["<c>1</c>"] Merged mining
        /// </summary>
        [Map("1")]
        MergedMining,
        /// <summary>
        /// ["<c>2</c>"] Activity bonus
        /// </summary>
        [Map("2")]
        ActivityBonus,
        /// <summary>
        /// ["<c>3</c>"] Rebate
        /// </summary>
        [Map("3")]
        Rebate,
        /// <summary>
        /// ["<c>4</c>"] Smart pool
        /// </summary>
        [Map("4")]
        SmartPool,
        /// <summary>
        /// ["<c>5</c>"] Mining address
        /// </summary>
        [Map("5")]
        MiningAddress,
        /// <summary>
        /// ["<c>7</c>"] Pool savings
        /// </summary>
        [Map("7")]
        PoolSavings,
        /// <summary>
        /// ["<c>8</c>"] Transferred
        /// </summary>
        [Map("8")]
        Transferred,
        /// <summary>
        /// Income transfer
        /// </summary>
        [Map("6", "31")]
        IncomeTransfer,
        /// <summary>
        /// ["<c>32</c>"] Hashrate resale - mining wallet
        /// </summary>
        [Map("32")]
        HashrateResaleMiningWallet,
        /// <summary>
        /// ["<c>33</c>"] Hashrate resale - pool savings
        /// </summary>
        [Map("33")]
        HashrateResalePoolSavings
    }
}

