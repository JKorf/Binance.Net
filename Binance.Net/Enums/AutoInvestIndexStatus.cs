using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Index status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestIndexStatus>))]
    public enum AutoInvestIndexStatus
    {
        /// <summary>
        /// ["<c>RUNNING</c>"] Running
        /// </summary>
        [Map("RUNNING")]
        Running,
        /// <summary>
        /// ["<c>REBALANCING</c>"] Rebalancing
        /// </summary>
        [Map("REBALANCING")]
        Rebalancing,
        /// <summary>
        /// ["<c>PAUSED</c>"] Paused
        /// </summary>
        [Map("PAUSED")]
        Paused
    }
}

