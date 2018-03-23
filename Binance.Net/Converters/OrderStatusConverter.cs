using System.Collections.Generic;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class OrderStatusConverter : BaseConverter<OrderStatus>
    {
        public OrderStatusConverter(): this(true) { }
        public OrderStatusConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<OrderStatus, string> Mapping => new Dictionary<OrderStatus, string>()
        {
            { OrderStatus.New, "NEW" },
            { OrderStatus.PartiallyFilled, "PARTIALLY_FILLED" },
            { OrderStatus.Filled, "FILLED" },
            { OrderStatus.Canceled, "CANCELED" },
            { OrderStatus.PendingCancel, "PENDING_CANCEL" },
            { OrderStatus.Rejected, "REJECTED" },
            { OrderStatus.Expired, "EXPIRED" }
        };
    }
}
