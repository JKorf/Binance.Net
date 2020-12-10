using Binance.Net.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using Binance.Net.Objects.Spot.MarketData;

namespace Binance.Net.Converters
{
    internal class SymbolFilterConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
#pragma warning disable 8604, 8602
            var obj = JObject.Load(reader);
            var type = new SymbolFilterTypeConverter(false).ReadString(obj["filterType"].ToString());
            BinanceSymbolFilter result;
            switch (type)
            {
                case SymbolFilterType.LotSize:
                    result = new BinanceSymbolLotSizeFilter
                    {
                        MaxQuantity = (decimal)obj["maxQty"],
                        MinQuantity = (decimal)obj["minQty"],
                        StepSize = (decimal)obj["stepSize"]
                    };
                    break;
                case SymbolFilterType.MarketLotSize:
                    result = new BinanceSymbolMarketLotSizeFilter
                    {
                        MaxQuantity = (decimal)obj["maxQty"],
                        MinQuantity = (decimal)obj["minQty"],
                        StepSize = (decimal)obj["stepSize"]
                    };
                    break;
                case SymbolFilterType.MinNotional:
                    result = new BinanceSymbolMinNotionalFilter
                    {
                        MinNotional = (decimal)obj["minNotional"],
                        ApplyToMarketOrders = (bool)obj["applyToMarket"],
                        AveragePriceMinutes = (int)obj["avgPriceMins"]
                    };
                    break;
                case SymbolFilterType.Price:
                    result = new BinanceSymbolPriceFilter
                    {
                        MaxPrice = (decimal)obj["maxPrice"],
                        MinPrice = (decimal)obj["minPrice"],
                        TickSize = (decimal)obj["tickSize"]
                    };
                    break;
                case SymbolFilterType.MaxNumberAlgorithmicOrders:
                    result = new BinanceSymbolMaxAlgorithmicOrdersFilter
                    {
                        MaxNumberAlgorithmicOrders = (int)obj["maxNumAlgoOrders"]
                    };
                    break;
                case SymbolFilterType.MaxNumberOrders:
                    result = new BinanceSymbolMaxOrdersFilter
                    {
                        MaxNumberOrders = (int)obj["maxNumOrders"]
                    };
                    break;

                case SymbolFilterType.IcebergParts:
                    result = new BinanceSymbolIcebergPartsFilter
                    {
                        Limit = (int)obj["limit"]
                    };
                    break;
                case SymbolFilterType.PricePercent:
                    result = new BinanceSymbolPercentPriceFilter
                    {
                        MultiplierUp = (decimal)obj["multiplierUp"],
                        MultiplierDown = (decimal)obj["multiplierDown"],
                        AveragePriceMinutes = (int)obj["avgPriceMins"]
                    };
                    break;
                case SymbolFilterType.MaxPosition:
                    result = new BinanceSymbolMaxPositionFilter
                    {
                        MaxPosition = (decimal)obj["maxPosition"]
                    };
                    break;
                default:
                    Debug.WriteLine("Can't parse symbol filter of type: " + obj["filterType"]);
                    result = new BinanceSymbolFilter();
                    break;
            }
#pragma warning restore 8604
            result.FilterType = type;
            return result;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var filter = (BinanceSymbolFilter)value!;
            writer.WriteStartObject();

            writer.WritePropertyName("filterType");
            writer.WriteValue(JsonConvert.SerializeObject(filter.FilterType, new SymbolFilterTypeConverter(false)));

            switch (filter.FilterType)
            {
                case SymbolFilterType.LotSize:
                    var lotSizeFilter = (BinanceSymbolLotSizeFilter)filter;
                    writer.WritePropertyName("maxQty");
                    writer.WriteValue(lotSizeFilter.MaxQuantity);
                    writer.WritePropertyName("minQty");
                    writer.WriteValue(lotSizeFilter.MinQuantity);
                    writer.WritePropertyName("stepSize");
                    writer.WriteValue(lotSizeFilter.StepSize);
                    break;
                case SymbolFilterType.MarketLotSize:
                    var marketLotSizeFilter = (BinanceSymbolMarketLotSizeFilter)filter;
                    writer.WritePropertyName("maxQty");
                    writer.WriteValue(marketLotSizeFilter.MaxQuantity);
                    writer.WritePropertyName("minQty");
                    writer.WriteValue(marketLotSizeFilter.MinQuantity);
                    writer.WritePropertyName("stepSize");
                    writer.WriteValue(marketLotSizeFilter.StepSize);
                    break;
                case SymbolFilterType.MinNotional:
                    var minNotionalFilter = (BinanceSymbolMinNotionalFilter)filter;
                    writer.WritePropertyName("minNotional");
                    writer.WriteValue(minNotionalFilter.MinNotional);
                    writer.WritePropertyName("applyToMarket");
                    writer.WriteValue(minNotionalFilter.ApplyToMarketOrders);
                    writer.WritePropertyName("avgPriceMins");
                    writer.WriteValue(minNotionalFilter.AveragePriceMinutes);
                    break;
                case SymbolFilterType.Price:
                    var priceFilter = (BinanceSymbolPriceFilter)filter;
                    writer.WritePropertyName("maxPrice");
                    writer.WriteValue(priceFilter.MaxPrice);
                    writer.WritePropertyName("minPrice");
                    writer.WriteValue(priceFilter.MinPrice);
                    writer.WritePropertyName("tickSize");
                    writer.WriteValue(priceFilter.TickSize);
                    break;
                case SymbolFilterType.MaxNumberAlgorithmicOrders:
                    var algoFilter = (BinanceSymbolMaxAlgorithmicOrdersFilter)filter;
                    writer.WritePropertyName("maxNumAlgoOrders");
                    writer.WriteValue(algoFilter.MaxNumberAlgorithmicOrders);
                    break;
                case SymbolFilterType.MaxNumberOrders:
                    var orderFilter = (BinanceSymbolMaxOrdersFilter)filter;
                    writer.WritePropertyName("maxNumOrders");
                    writer.WriteValue(orderFilter.MaxNumberOrders);
                    break;
                case SymbolFilterType.IcebergParts:
                    var icebergPartsFilter = (BinanceSymbolIcebergPartsFilter)filter;
                    writer.WritePropertyName("limit");
                    writer.WriteValue(icebergPartsFilter.Limit);
                    break;
                case SymbolFilterType.PricePercent:
                    var pricePercentFilter = (BinanceSymbolPercentPriceFilter)filter;
                    writer.WritePropertyName("multiplierUp");
                    writer.WriteValue(pricePercentFilter.MultiplierUp);
                    writer.WritePropertyName("multiplierDown");
                    writer.WriteValue(pricePercentFilter.MultiplierDown);
                    writer.WritePropertyName("avgPriceMins");
                    writer.WriteValue(pricePercentFilter.AveragePriceMinutes);
                    break;
                default:
                    Debug.WriteLine("Can't write symbol filter of type: " + filter.FilterType);
                    break;
            }

            writer.WriteEndObject();
        }
    }
}
