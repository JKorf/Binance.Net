using CryptoExchange.Net.Objects.Options;
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
    }
}
