using CryptoExchange.Net.Objects;
using System;

namespace Binance.Net.Objects.Brokerage
{
    public class BinanceBrokerageClientOptions : RestClientOptions
    {
        /// <summary>
        /// Whether or not to automatically sync the local time with the server time
        /// </summary>
        public bool AutoTimestamp { get; set; } = true;

        /// <summary>
        /// Interval for refreshing the auto timestamp calculation
        /// </summary>
        public TimeSpan AutoTimestampRecalculationInterval { get; set; } = TimeSpan.FromHours(3);

        /// <summary>
        /// A manual offset for the timestamp. Should only be used if AutoTimestamp and regular time synchronization on the OS is not reliable enough
        /// </summary>
        public TimeSpan TimestampOffset { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        public BinanceBrokerageClientOptions() : base("https://api.binance.com")
        {
        }

        public BinanceBrokerageClientOptions(string baseAddress) : base(baseAddress)
        {
        }
        
        public BinanceBrokerageClientOptions Copy()
        {
            var copy = Copy<BinanceBrokerageClientOptions>();
            copy.ReceiveWindow = ReceiveWindow;
            return copy;
        }
    }
}