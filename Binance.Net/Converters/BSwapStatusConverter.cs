using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class BSwapStatusConverter : BaseConverter<BSwapStatus>
    {
        public BSwapStatusConverter() : this(true) { }
        public BSwapStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BSwapStatus, string>> Mapping => new List<KeyValuePair<BSwapStatus, string>>
        {
            new KeyValuePair<BSwapStatus, string>(BSwapStatus.Pending, "0"),
            new KeyValuePair<BSwapStatus, string>(BSwapStatus.Success, "1"),
            new KeyValuePair<BSwapStatus, string>(BSwapStatus.Failure, "2")
        };
    }
}
