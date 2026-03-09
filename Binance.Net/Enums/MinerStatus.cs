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
        /// ["<c>0</c>"] All miners
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// ["<c>1</c>"] Valid
        /// </summary>
        [Map("1")]
        Valid,
        /// <summary>
        /// ["<c>2</c>"] Invalid
        /// </summary>
        [Map("2")]
        Invalid,
        /// <summary>
        /// ["<c>3</c>"] Failure
        /// </summary>
        [Map("3")]
        Failure
    }
}

