using Binance.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Binance.Net.Converters
{
    public class SymbolFilterConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            SymbolFilterType type = new SymbolFilterTypeConverter(false).ReadString(obj["filterType"].ToString());
            BinanceSymbolFilter result = null;
            switch (type)
            {
                case SymbolFilterType.LotSize:
                    result = new BinanceSymbolLotSizeFilter()
                    {
                        MaxQuantity = JsonConvert.DeserializeObject<decimal>(obj["maxQty"].ToString()),
                        MinQuantity = JsonConvert.DeserializeObject<decimal>(obj["minQty"].ToString()),
                        StepSize = JsonConvert.DeserializeObject<decimal>(obj["stepSize"].ToString())
                    };
                    break;
                case SymbolFilterType.MinNotional:
                    result = new BinanceSymbolMinNotionalFilter()
                    {
                        MinNotional = JsonConvert.DeserializeObject<decimal>(obj["minNotional"].ToString())
                    };
                    break;
                case SymbolFilterType.PriceFilter:
                    result = new BinanceSymbolPriceFilter()
                    {
                        MaxPrice = JsonConvert.DeserializeObject<decimal>(obj["maxPrice"].ToString()),
                        MinPrice = JsonConvert.DeserializeObject<decimal>(obj["minPrice"].ToString()),
                        TickSize = JsonConvert.DeserializeObject<decimal>(obj["tickSize"].ToString()),
                    };
                    break;
            }
            result.FilterType = type;
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
