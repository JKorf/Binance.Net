using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Other
{
    internal class BinanceExchangeApiWrapper<T>
    {
        public int Code { get; set; }
        public string Message { get; set; } = "";
        public string MessageDetail { get; set; } = "";

        public T Data { get; set; }

        public bool Success { get; set; }
    }
}
