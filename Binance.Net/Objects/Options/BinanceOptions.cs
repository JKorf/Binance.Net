using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Options
{
    /// <summary>
    /// Binance options
    /// </summary>
    public class BinanceOptions
    {
        /// <summary>
        /// Rest client options
        /// </summary>
        public BinanceRestOptions Rest { get; set; } = new BinanceRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public BinanceSocketOptions Socket { get; set; } = new BinanceSocketOptions();

        /// <summary>
        /// Trade environment. Contains info about URL's to use to connect to the API. Use `BinanceEnvironment` to swap environment, for example `Environment = BinanceEnvironment.Live`
        /// </summary>
        public BinanceEnvironment? Environment { get; set; }

        /// <summary>
        /// The api credentials used for signing requests.
        /// </summary>
        public ApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The DI service lifetime for the IBinanceSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
