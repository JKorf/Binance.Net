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
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray arr = JArray.Load(reader);
            BinanceKline entry = new BinanceKline();
            entry.OpenTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((long)arr[0]); 
            entry.Open = (double)arr[1];
            entry.High = (double)arr[2];
            entry.Low = (double)arr[3];
            entry.Close = (double)arr[4];
            entry.Volume = (double)arr[5];
            entry.CloseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((long)arr[6]);
            entry.AssetVolume = (double)arr[7];
            entry.Trades = (int)arr[8];
            entry.TakerBuyBaseAssetVolume = (double)arr[9];
            entry.TakerBuyQuoteAssetVolume = (double)arr[10];
            return entry;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
