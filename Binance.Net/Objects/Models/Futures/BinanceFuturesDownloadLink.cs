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
        /// ["<c>downloadId</c>"] Download id
        /// </summary>
        [JsonPropertyName("downloadId")]
        public string DownloadId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Is ready to download
        /// </summary>
        [JsonPropertyName("status")]
        public DownloadStatus Status { get; set; }
        /// <summary>
        /// ["<c>url</c>"] Download url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>expirationTimestamp</c>"] Link expiration time
        /// </summary>
        [JsonPropertyName("expirationTimestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExpirationTime { get; set; }
        /// <summary>
        /// ["<c>isExpired</c>"] Is expired
        /// </summary>
        [JsonPropertyName("isExpired")]
        public bool? IsExpired { get; set; }
    }
}

