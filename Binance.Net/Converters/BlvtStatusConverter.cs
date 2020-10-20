using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class BlvtStatusConverter : BaseConverter<BlvtStatus>
    {
        public BlvtStatusConverter() : this(true) { }
        public BlvtStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BlvtStatus, string>> Mapping => new List<KeyValuePair<BlvtStatus, string>>
        {
            new KeyValuePair<BlvtStatus, string>(BlvtStatus.Pending, "P"),
            new KeyValuePair<BlvtStatus, string>(BlvtStatus.Success, "S"),
            new KeyValuePair<BlvtStatus, string>(BlvtStatus.Failure, "F")
        };
    }
}
