using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Download status
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<DownloadStatus>))] public  enum DownloadStatus
    {
        /// <summary>
        /// Processing
        /// </summary>
        [Map("processing")]
        Processing,
        /// <summary>
        /// Ready for download
        /// </summary>
        [Map("completed")]
        Completed
    }
}
