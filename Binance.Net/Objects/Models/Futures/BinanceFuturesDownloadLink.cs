using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Transaction download info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesDownloadLink
    {
        /// <summary>
        /// Download id
        /// </summary>
        [JsonPropertyName("downloadId")]
        public string DownloadId { get; set; } = string.Empty;
        /// <summary>
        /// Is ready to download
        /// </summary>
        [JsonPropertyName("status")]
        public DownloadStatus Status { get; set; }
        /// <summary>
        /// Download url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// Link expiration time
        /// </summary>
        [JsonPropertyName("expirationTimestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExpirationTime { get; set; }
        /// <summary>
        /// Is expired
        /// </summary>
        [JsonPropertyName("isExpired")]
        public bool? IsExpired { get; set; }
    }
}
