using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Miner status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MinerStatus>))]
    public enum MinerStatus
    {
        /// <summary>
        /// All miners
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// Valid
        /// </summary>
        [Map("1")]
        Valid,
        /// <summary>
        /// Invalid
        /// </summary>
        [Map("2")]
        Invalid,
        /// <summary>
        /// Failure
        /// </summary>
        [Map("3")]
        Failure
    }
}
