﻿using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Options
{
    /// <summary>
    /// Binance options
    /// </summary>
    public class BinanceOptions : LibraryOptions<BinanceRestOptions, BinanceSocketOptions, ApiCredentials, BinanceEnvironment>
    {
        /// <summary>
        /// Whether to allow the client to adjust the clientOrderId parameter send by the user when placing orders to include a client reference. This reference is used by the exchange to allocate a small percentage of the paid trading fees to developer of this library. Defaults to false.<br />
        /// Note that:<br />
        /// * It does not impact the amount of fees a user pays in any way<br />
        /// * It does not impact functionality. The reference is added just before sending the request and removed again during data deserialization<br />
        /// * It does respect client order id field limitations. For example if the user provided client order id parameter is too long to fit the reference it will not be added<br />
        /// * Toggling this option might fail operations using a clientOrderId parameter for pre-existing orders which were placed before the toggle. Operations on orders placed after the toggle will work as expected. It's adviced to toggle when there are no open orders
        /// </summary>
        public bool AllowAppendingClientOrderId { get; set; } = false;
    }
}
