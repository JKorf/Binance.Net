using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Resale status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<HashrateResaleStatus>))]
    public enum HashrateResaleStatus
    {
        /// <summary>
        /// ["<c>0</c>"] Processing
        /// </summary>
        [Map("0")]
        Processing,
        /// <summary>
        /// ["<c>1</c>"] Canceled
        /// </summary>
        [Map("1")]
        Canceled,
        /// <summary>
        /// ["<c>2</c>"] Terminated
        /// </summary>
        [Map("2")]
        Terminated
    }
}

