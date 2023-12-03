using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects
{
    public class BinanceRateLimitError : ServerRateLimitError
    {
        public BinanceRateLimitError(string message) : base(message)
        {
        }

        public BinanceRateLimitError(int? code, string message, object? data) : base(code, message, data)
        {
        }
    }
}
