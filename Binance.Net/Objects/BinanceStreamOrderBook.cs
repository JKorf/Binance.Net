using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects
{
    public class BinanceStreamOrderBook: BinanceOrderBook
    {
        public string Symbol { get; set; }
    }
}
