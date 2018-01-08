using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects
{
    public class BinanceDepositAddress
    {
        public string Address { get; set; }
        public bool Success { get; set; }
        public string AddressTag { get; set; }
        public string Asset { get; set; }
    }
}
