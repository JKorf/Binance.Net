using Binance.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

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
            var obj = JObject.Load(reader);
            var type = new SymbolFilterTypeConverter(false).ReadString(obj["filterType"].ToString());
            BinanceSymbolFilter result;
            switch (type)
            {
                case SymbolFilterType.LotSize:
                    result = new BinanceSymbolLotSizeFilter
                    {
                        MaxQuantity = JsonConvert.DeserializeObject<decimal>(obj["maxQty"].ToString()),
                        MinQuantity = JsonConvert.DeserializeObject<decimal>(obj["minQty"].ToString()),
                        StepSize = JsonConvert.DeserializeObject<decimal>(obj["stepSize"].ToString())
                    };
                    break;
                case SymbolFilterType.MarketLotSize:
                    result = new BinanceSymbolMarketLotSizeFilter
                    {
                        MaxQuantity = JsonConvert.DeserializeObject<decimal>(obj["maxQty"].ToString()),
                        MinQuantity = JsonConvert.DeserializeObject<decimal>(obj["minQty"].ToString()),
                        StepSize = JsonConvert.DeserializeObject<decimal>(obj["stepSize"].ToString())
                    };
                    break;
                case SymbolFilterType.MinNotional:
                    result = new BinanceSymbolMinNotionalFilter
                    {
                        MinNotional = JsonConvert.DeserializeObject<decimal>(obj["minNotional"].ToString()),
                        ApplyToMarketOrders = (bool)obj["applyToMarket"],
                        AveragePriceMinutes = JsonConvert.DeserializeObject<int>(obj["avgPriceMins"].ToString())
                    };
                    break;
                case SymbolFilterType.Price:
                    result = new BinanceSymbolPriceFilter
                    {
                        MaxPrice = JsonConvert.DeserializeObject<decimal>(obj["maxPrice"].ToString()),
                        MinPrice = JsonConvert.DeserializeObject<decimal>(obj["minPrice"].ToString()),
                        TickSize = JsonConvert.DeserializeObject<decimal>(obj["tickSize"].ToString())
                    };
                    break;
                case SymbolFilterType.MaxNumberAlgorithmicOrders:
                    result = new BinanceSymbolMaxAlgorithmicOrdersFilter
                    {
                        MaxNumberAlgorithmicOrders = JsonConvert.DeserializeObject<int>(obj["maxNumAlgoOrders"].ToString())
                    };
                    break;

                case SymbolFilterType.MaxNumberIcebergOrders:
                    result = new BinanceSymbolMaxIcebergOrdersFilter
                    {
                        MaxNumberIcebergOrders = JsonConvert.DeserializeObject<int>(obj["maxNumIcebergOrders"].ToString())
                    };
                    break;

                case SymbolFilterType.MaxNumberOrders:
                    result = new BinanceSymbolMaxOrdersFilter
                    {
                        MaxNumberOrders = JsonConvert.DeserializeObject<int>(obj["limit"].ToString())
                    };
                    break;

                case SymbolFilterType.IcebergParts:
                    result = new BinanceSymbolIcebergPartsFilter
                    {
                        Limit = JsonConvert.DeserializeObject<int>(obj["limit"].ToString())
                    };
                    break;
                case SymbolFilterType.PricePercent:
                    result = new BinanceSymbolPercentPriceFilter
                    {
                        MultiplierUp = JsonConvert.DeserializeObject<decimal>(obj["multiplierUp"].ToString()),
                        MultiplierDown = JsonConvert.DeserializeObject<decimal>(obj["multiplierDown"].ToString()),
                        AveragePriceMinutes = JsonConvert.DeserializeObject<int>(obj["avgPriceMins"].ToString())
                    };
                    break;
                default:
                    Debug.WriteLine("Can't parse symbol filter of type: " + obj["filterType"]);
                    result = new BinanceSymbolFilter();
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
