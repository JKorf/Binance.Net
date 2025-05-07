using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.Converters
{
    internal class ClientOrderIdReplaceConverter : ReplaceConverter
    {
        public ClientOrderIdReplaceConverter(): base(
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")
        {
        }
    }
}
