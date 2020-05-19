using System.Collections.Generic;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class MinerStatusConverter : BaseConverter<MinerStatus>
    {
        public MinerStatusConverter(): this(true) { }
        public MinerStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<MinerStatus, string>> Mapping => new List<KeyValuePair<MinerStatus, string>>
        {
            new KeyValuePair<MinerStatus, string>(MinerStatus.All, "0"),
            new KeyValuePair<MinerStatus, string>(MinerStatus.Valid, "1"),
            new KeyValuePair<MinerStatus, string>(MinerStatus.Invalid, "2"),
            new KeyValuePair<MinerStatus, string>(MinerStatus.Failure, "3"),
        };
    }
}
