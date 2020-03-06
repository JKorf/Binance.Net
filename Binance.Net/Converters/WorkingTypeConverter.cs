using Binance.Net.Objects;
using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Converters
{
    internal class WorkingTypeConverter : BaseConverter<WorkingType>
    {
        public WorkingTypeConverter() : this(true) { }
        public WorkingTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<WorkingType, string>> Mapping => new List<KeyValuePair<WorkingType, string>>
        {
            new KeyValuePair<WorkingType, string>(WorkingType.Mark, "MARK_PRICE"),
            new KeyValuePair<WorkingType, string>(WorkingType.Contract, "CONTRACT_PRICE"),
        };
    }
}
