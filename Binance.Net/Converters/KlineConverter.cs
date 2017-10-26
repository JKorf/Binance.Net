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
                Open = (double) arr[1],
                High = (double) arr[2],
                Low = (double) arr[3],
                Close = (double) arr[4],
                Volume = (double) arr[5],
                CloseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((long) arr[6]),
                AssetVolume = (double) arr[7],
                Trades = (int) arr[8],
                TakerBuyBaseAssetVolume = (double) arr[9],
                TakerBuyQuoteAssetVolume = (double) arr[10]
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
