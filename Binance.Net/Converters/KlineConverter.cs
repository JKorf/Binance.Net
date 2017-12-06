using System;
using Binance.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Binance.Net.Converters
{
    public class KlineConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BinanceKline);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray arr = JArray.Load(reader);
            BinanceKline entry = new BinanceKline
            {
                OpenTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((long) arr[0]),
                Open = (decimal) arr[1],
                High = (decimal) arr[2],
                Low = (decimal) arr[3],
                Close = (decimal) arr[4],
                Volume = (decimal) arr[5],
                CloseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((long) arr[6]),
                AssetVolume = (decimal) arr[7],
                Trades = (int) arr[8],
                TakerBuyBaseAssetVolume = (decimal) arr[9],
                TakerBuyQuoteAssetVolume = (decimal) arr[10]
            };

            return entry;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var obj = (BinanceKline)value;
            JArray array = new JArray(
                obj.OpenTime.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                obj.Open,
                obj.High,
                obj.Low,
                obj.Close,
                obj.Volume,
                obj.CloseTime.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                obj.AssetVolume,
                obj.Trades,
                obj.TakerBuyBaseAssetVolume,
                obj.TakerBuyQuoteAssetVolume
                );
            array.WriteTo(writer);
        }
    }
}
