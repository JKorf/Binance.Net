﻿using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class SymbolStatusConverter : BaseConverter<SymbolStatus>
    {
        public SymbolStatusConverter(): this(true) { }
        public SymbolStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SymbolStatus, string>> Mapping => new List<KeyValuePair<SymbolStatus, string>>
        {
            new KeyValuePair<SymbolStatus, string>(SymbolStatus.AuctionMatch, "AUCTION_MATCH"),
            new KeyValuePair<SymbolStatus, string>(SymbolStatus.Break, "BREAK"),
            new KeyValuePair<SymbolStatus, string>(SymbolStatus.EndOfDay, "END_OF_DAY"),
            new KeyValuePair<SymbolStatus, string>(SymbolStatus.Halt, "HALT"),
            new KeyValuePair<SymbolStatus, string>(SymbolStatus.PostTrading, "POST_TRADING"),
            new KeyValuePair<SymbolStatus, string>(SymbolStatus.PreTrading, "PRE_TRADING"),
            new KeyValuePair<SymbolStatus, string>(SymbolStatus.Trading, "TRADING")
        };
    }
}
