using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net;

namespace Binance.Net.Converters
{
    public class SymbolStatusConverter : BaseConverter<SymbolStatus>
    {
        public SymbolStatusConverter(): this(true) { }
        public SymbolStatusConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<SymbolStatus, string> Mapping => new Dictionary<SymbolStatus, string>()
        {
            { SymbolStatus.AuctionMatch, "AUCTION_MATCH" },
            { SymbolStatus.Break, "BREAK" },
            { SymbolStatus.EndOfDay, "END_OF_DAY" },
            { SymbolStatus.Halt, "HALT" },
            { SymbolStatus.PostTrading, "POST_TRADING" },
            { SymbolStatus.PreTrading, "PRE_TRADING" },
            { SymbolStatus.Trading, "TRADING" },
        };
    }
}
