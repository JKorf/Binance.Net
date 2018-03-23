using System.Collections.Generic;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class OrderRejectReasonConverter: BaseConverter<OrderRejectReason>
    {
        public OrderRejectReasonConverter(): this(true) { }
        public OrderRejectReasonConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<OrderRejectReason, string> Mapping => new Dictionary<OrderRejectReason, string>()
        {
            { OrderRejectReason.None, "NONE" },
            { OrderRejectReason.UnknownInstrument, "UNKNOWN_INSTRUMENT" },
            { OrderRejectReason.MarketClosed, "MARKET_CLOSED" },
            { OrderRejectReason.PriceQuantityExceedsHardLimits, "PRICE_QTY_EXCEED_HARD_LIMITS" },
            { OrderRejectReason.UnknownOrder, "UNKNOWN_ORDER" },
            { OrderRejectReason.DuplicateOrder, "DUPLICATE_ORDER" },
            { OrderRejectReason.UnknownAccount, "UNKNOWN_ACCOUNT" },
            { OrderRejectReason.InsufficientBalance, "INSUFFICIENT_BALANCE" },
            { OrderRejectReason.AccountInactive, "ACCOUNT_INACTIVE" },
            { OrderRejectReason.AccountCannotSettle, "ACCOUNT_CANNOT_SETTLE" }
        };
    }
}
