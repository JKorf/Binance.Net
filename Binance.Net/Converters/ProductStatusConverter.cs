using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class ProductStatusConverter : BaseConverter<ProductStatus>
    {
        public ProductStatusConverter() : this(true) { }
        public ProductStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ProductStatus, string>> Mapping => new List<KeyValuePair<ProductStatus, string>>
        {
            new KeyValuePair<ProductStatus, string>(ProductStatus.All, "ALL"),
            new KeyValuePair<ProductStatus, string>(ProductStatus.Subscribable, "SUBSCRIBABLE"),
            new KeyValuePair<ProductStatus, string>(ProductStatus.Unsubscribable, "UNSUBSCRIBABLE")
        };
    }
}
