using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Download status
    /// </summary>
    public enum DownloadStatus
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
