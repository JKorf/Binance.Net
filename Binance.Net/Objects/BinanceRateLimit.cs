using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects
{
    public class BinanceRateLimit
    {
        public RateLimitInterval Interval { get; set; }
        public RateLimitType Type { get; set; }
        public int Limit { get; set; }
    }
}
