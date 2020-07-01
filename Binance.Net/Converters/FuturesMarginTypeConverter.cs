﻿using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class FuturesMarginTypeConverter : BaseConverter<FuturesMarginType>
    {
        public FuturesMarginTypeConverter(): this(false) { }
        public FuturesMarginTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FuturesMarginType, string>> Mapping => new List<KeyValuePair<FuturesMarginType, string>>
        {
            new KeyValuePair<FuturesMarginType, string>(FuturesMarginType.Isolated, "ISOLATED"),
            new KeyValuePair<FuturesMarginType, string>(FuturesMarginType.Cross, "CROSSED"),
            new KeyValuePair<FuturesMarginType, string>(FuturesMarginType.Cross, "cross") //return on BinanceFuturesStreamPosition
        };
    }
}
