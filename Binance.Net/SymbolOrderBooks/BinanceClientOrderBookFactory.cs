using CryptoExchange.Net.OrderBook.V2;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.SymbolOrderBooks
{
    public class BinanceClientOrderBookFactory : ClientOrderBookFactory
    {
        public BinanceClientOrderBookFactory(ILoggerFactory logger, ISharedApiClientBase api) : base(logger, api)
        {
        }
    }
}
