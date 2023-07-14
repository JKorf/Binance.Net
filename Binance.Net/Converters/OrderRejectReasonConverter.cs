using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class OrderRejectReasonConverter : BaseConverter<OrderRejectReason>
    {
        public OrderRejectReasonConverter() : this(true) { }
        public OrderRejectReasonConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderRejectReason, string>> Mapping => new List<KeyValuePair<OrderRejectReason, string>>
        {
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.None, "NONE"),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.UnknownInstrument, "UNKNOWN_INSTRUMENT"),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.MarketClosed, "MARKET_CLOSED"),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.PriceQuantityExceedsHardLimits, "PRICE_QTY_EXCEED_HARD_LIMITS"),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.UnknownOrder, "UNKNOWN_ORDER"),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.DuplicateOrder, "DUPLICATE_ORDER"),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.UnknownAccount, "UNKNOWN_ACCOUNT" ),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.InsufficientBalance, "INSUFFICIENT_BALANCE" ),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.AccountInactive, "ACCOUNT_INACTIVE" ),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.AccountCannotSettle, "ACCOUNT_CANNOT_SETTLE"),
            new KeyValuePair<OrderRejectReason, string>(OrderRejectReason.StopPriceWouldTrigger, "STOP_PRICE_WOULD_TRIGGER_IMMEDIATELY")
        };
    }
}
