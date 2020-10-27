using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class TransferDirectionTypeConverter : BaseConverter<TransferDirectionType>
    {
        public TransferDirectionTypeConverter() : this(true)
        {
        }

        public TransferDirectionTypeConverter(bool quotes) : base(quotes)
        {
        }

        protected override List<KeyValuePair<TransferDirectionType, string>> Mapping => new List<KeyValuePair<TransferDirectionType, string>>
        {
            new KeyValuePair<TransferDirectionType, string>( TransferDirectionType.MainToMargin, "1"),
            new KeyValuePair<TransferDirectionType, string>( TransferDirectionType.MarginToMain, "2")
        };
    }
}
