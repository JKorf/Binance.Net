using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class TransferDirectionConverter : BaseConverter<TransferDirection>
    {
        public TransferDirectionConverter() : this(true) { }
        public TransferDirectionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<TransferDirection, string>> Mapping => new List<KeyValuePair<TransferDirection, string>>
        {
            new KeyValuePair<TransferDirection, string>(TransferDirection.RollIn, "ROLL_IN"),
            new KeyValuePair<TransferDirection, string>(TransferDirection.RollOut, "ROLL_OUT"),
        };
    }
}
