using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Other
{
    /// <summary>
    /// Permissions of the current API key
    /// </summary>
    public class BinanceAPIKeyPermissions
    {
        /// <summary>
        /// Whether the key is restricted to certain IP's or not
        /// </summary>
        public bool IpRestrict { get; set; }
        /// <summary>
        /// Creation time of the key
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// This option allows you to withdraw via API. You must apply the IP Access Restriction filter in order to enable withdrawals
        /// </summary>
        public bool EnableWithdrawals { get; set; }
        /// <summary>
        /// This option authorizes this key to transfer funds between your master account and your sub account instantly
        /// </summary>
        public bool PermitsUniversalTransfer { get; set; }
        /// <summary>
        /// Authorizes this key to be used for a dedicated universal transfer API to transfer multiple supported currencies. Each business's own transfer API rights are not affected by this authorization
        /// </summary>
        public bool EnableInternalTransfer { get; set; }
        /// <summary>
        /// Authorizes this key to Vanilla options trading
        /// </summary>
        public bool EnableVanillaOptions { get; set; }
        /// <summary>
        /// Authorizes the reading of account info
        /// </summary>
        public bool EnableReading { get; set; }
        /// <summary>
        /// Authorizes futures trading. API Key created before your futures account opened does not support futures API service
        /// </summary>
        public bool EnableFutures { get; set; }
        /// <summary>
        /// Authorizes margin. This option can be adjusted after the Cross Margin account transfer is completed
        /// </summary>
        public bool EnableMargin { get; set; }
        /// <summary>
        /// Spot and margin trading allowed
        /// </summary>
        public bool EnableSpotAndMarginTrading { get; set; }
        /// <summary>
        /// Expiration time for spot and margin trading permission
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime? TradingAuthorityExpirationTime { get; set; }
    }
}
