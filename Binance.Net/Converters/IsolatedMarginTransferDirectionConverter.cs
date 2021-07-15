using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class IsolatedMarginTransferDirectionConverter : BaseConverter<IsolatedMarginTransferDirection>
    {
        public IsolatedMarginTransferDirectionConverter() : this(true) { }
        public IsolatedMarginTransferDirectionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<IsolatedMarginTransferDirection, string>> Mapping => new List<KeyValuePair<IsolatedMarginTransferDirection, string>>
        {
            new KeyValuePair<IsolatedMarginTransferDirection, string>(IsolatedMarginTransferDirection.Spot, "SPOT"),
            new KeyValuePair<IsolatedMarginTransferDirection, string>(IsolatedMarginTransferDirection.IsolatedMargin, "ISOLATED_MARGIN"),
        };
    }
}
