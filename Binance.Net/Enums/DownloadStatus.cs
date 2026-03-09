using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Download status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DownloadStatus>))]
    public enum DownloadStatus
    {
        /// <summary>
        /// ["<c>processing</c>"] Processing
        /// </summary>
        [Map("processing")]
        Processing,
        /// <summary>
        /// ["<c>completed</c>"] Ready for download
        /// </summary>
        [Map("completed")]
        Completed
    }
}

