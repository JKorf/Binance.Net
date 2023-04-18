using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class OrderStatusConverter : BaseConverter<OrderStatus>
    {
        public OrderStatusConverter(): this(true) { }
        public OrderStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderStatus, string>> Mapping => new List<KeyValuePair<OrderStatus, string>>
        {
            new KeyValuePair<OrderStatus, string>(OrderStatus.New, "NEW"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.PartiallyFilled, "PARTIALLY_FILLED"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Filled, "FILLED" ),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Canceled, "CANCELED"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.PendingCancel, "PENDING_CANCEL"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Rejected, "REJECTED"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Insurance, "NEW_INSURANCE" ),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Adl, "NEW_ADL" ),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Expired, "EXPIRED" ),
            new KeyValuePair<OrderStatus, string>(OrderStatus.ExpiredInMatch, "EXPIRED_IN_MATCH " ),
        };
    }
}
