using System;
using System.Collections.Generic;
using System.Linq;
using Binance.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.Converters
{
    public class OrderRejectReasonConverter: JsonConverter
    {
        private Dictionary<OrderRejectReason, string> values = new Dictionary<OrderRejectReason, string>()
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
            { OrderRejectReason.AccountCannotSettle, "ACCOUNT_CANNOT_SETTLE" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OrderRejectReason);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(values[(OrderRejectReason)value]);
        }
    }
}
