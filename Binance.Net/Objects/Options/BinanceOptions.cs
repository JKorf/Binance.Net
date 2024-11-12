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
        public BinanceRestOptions RestOptions { get; set; } = new BinanceRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public BinanceSocketOptions SocketOptions { get; set; } = new BinanceSocketOptions();

        /// <summary>
        /// The DI service lifetime for the IBinanceSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
