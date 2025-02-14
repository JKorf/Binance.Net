using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Mining earnings type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<EarningType>))] public  enum EarningType
    {
        /// <summary>
        /// Mining wallet
        /// </summary>
        [Map("0")]
        MiningWallet,
        /// <summary>
        /// Merged mining
        /// </summary>
        [Map("1")]
        MergedMining,
        /// <summary>
        /// Activity bonus
        /// </summary>
        [Map("2")]
        ActivityBonus,
        /// <summary>
        /// Rebate
        /// </summary>
        [Map("3")]
        Rebate,
        /// <summary>
        /// Smart pool
        /// </summary>
        [Map("4")]
        SmartPool,
        /// <summary>
        /// Mining address
        /// </summary>
        [Map("5")]
        MiningAddress,
        /// <summary>
        /// Pool savings
        /// </summary>
        [Map("7")]
        PoolSavings,
        /// <summary>
        /// Transferred
        /// </summary>
        [Map("8")]
        Transferred,
        /// <summary>
        /// Income transfer
        /// </summary>
        [Map("6", "31")]
        IncomeTransfer,
        /// <summary>
        /// Hashrate resale - mining wallet
        /// </summary>
        [Map("32")]
        HashrateResaleMiningWallet,
        /// <summary>
        /// Hashrate resale - pool savings
        /// </summary>
        [Map("33")]
        HashrateResalePoolSavings
    }
}
