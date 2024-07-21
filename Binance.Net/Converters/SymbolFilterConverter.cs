using Binance.Net.Enums;
using System.Diagnostics;
using Binance.Net.Objects.Models.Spot;
using System.Text.Json;

namespace Binance.Net.Converters
{
    internal class SymbolFilterConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        /// <inheritdoc />
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type converterType = typeof(SymbolFilterConverterImp<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType);
        }

        private class SymbolFilterConverterImp<T> : JsonConverter<T> where T : BinanceSymbolFilter
        {
            public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var obj = JsonDocument.ParseValue(ref reader).RootElement;
                var type = obj.GetProperty("filterType").Deserialize<SymbolFilterType>();
                BinanceSymbolFilter result;
                switch (type)
                {
                    case SymbolFilterType.LotSize:
                        result = new BinanceSymbolLotSizeFilter
                        {
                            MaxQuantity = obj.GetProperty("maxQty").GetDecimal(),
                            MinQuantity = obj.GetProperty("minQty").GetDecimal(),
                            StepSize = obj.GetProperty("stepSize").GetDecimal()
                        };
                        break;
                    case SymbolFilterType.MarketLotSize:
                        result = new BinanceSymbolMarketLotSizeFilter
                        {
                            MaxQuantity = obj.GetProperty("maxQty").GetDecimal(),
                            MinQuantity = obj.GetProperty("minQty").GetDecimal(),
                            StepSize = obj.GetProperty("stepSize").GetDecimal()
                        };
                        break;
                    case SymbolFilterType.MinNotional:
                        result = new BinanceSymbolMinNotionalFilter
                        {
                            MinNotional = obj.GetProperty("minNotional").GetDecimal(),
                            ApplyToMarketOrders = obj.GetProperty("applyToMarket").GetBoolean(),
                            AveragePriceMinutes = obj.GetProperty("avgPriceMins").GetInt32()
                        };
                        break;
                    case SymbolFilterType.Notional:
                        result = new BinanceSymbolNotionalFilter
                        {
                            MinNotional = obj.GetProperty("minNotional").GetDecimal(),
                            MaxNotional = obj.GetProperty("maxNotional").GetDecimal(),
                            ApplyMinToMarketOrders = obj.GetProperty("applyMinToMarket").GetBoolean(),
                            ApplyMaxToMarketOrders = obj.GetProperty("applyMaxToMarket").GetBoolean(),
                            AveragePriceMinutes = obj.GetProperty("avgPriceMins").GetInt32()
                        };
                        break;
                    case SymbolFilterType.Price:
                        result = new BinanceSymbolPriceFilter
                        {
                            MaxPrice = obj.GetProperty("maxPrice").GetDecimal(),
                            MinPrice = obj.GetProperty("minPrice").GetDecimal(),
                            TickSize = obj.GetProperty("tickSize").GetDecimal(),
                        };
                        break;
                    case SymbolFilterType.MaxNumberAlgorithmicOrders:
                        result = new BinanceSymbolMaxAlgorithmicOrdersFilter
                        {
                            MaxNumberAlgorithmicOrders = obj.GetProperty("maxNumAlgoOrders").GetInt32()
                        };
                        break;
                    case SymbolFilterType.MaxNumberOrders:
                        result = new BinanceSymbolMaxOrdersFilter
                        {
                            MaxNumberOrders = obj.GetProperty("maxNumOrders").GetInt32()
                        };
                        break;

                    case SymbolFilterType.IcebergParts:
                        result = new BinanceSymbolIcebergPartsFilter
                        {
                            Limit = obj.GetProperty("limit").GetInt32()
                        };
                        break;
                    case SymbolFilterType.PricePercent:
                        result = new BinanceSymbolPercentPriceFilter
                        {
                            MultiplierUp = obj.GetProperty("multiplierUp").GetInt32(),
                            MultiplierDown = obj.GetProperty("multiplierDown").GetInt32(),
                            AveragePriceMinutes = obj.GetProperty("avgPriceMins").GetInt32()
                        };
                        break;
                    case SymbolFilterType.MaxPosition:
                        result = new BinanceSymbolMaxPositionFilter
                        {
                            MaxPosition = obj.TryGetProperty("maxPosition", out var el) ? el.GetDecimal() : 0
                        };
                        break;
                    case SymbolFilterType.PercentagePriceBySide:
                        result = new BinanceSymbolPercentPriceBySideFilter
                        {
                            AskMultiplierUp = obj.GetProperty("askMultiplierUp").GetDecimal(),
                            AskMultiplierDown = obj.GetProperty("askMultiplierDown").GetDecimal(),
                            BidMultiplierUp = obj.GetProperty("bidMultiplierUp").GetDecimal(),
                            BidMultiplierDown = obj.GetProperty("bidMultiplierDown").GetDecimal(),
                            AveragePriceMinutes = obj.GetProperty("avgPriceMins").GetInt32()
                        };
                        break;
                    case SymbolFilterType.TrailingDelta:
                        result = new BinanceSymbolTrailingDeltaFilter
                        {
                            MaxTrailingAboveDelta = obj.GetProperty("maxTrailingAboveDelta").GetInt32(),
                            MaxTrailingBelowDelta = obj.GetProperty("maxTrailingBelowDelta").GetInt32(),
                            MinTrailingAboveDelta = obj.GetProperty("minTrailingAboveDelta").GetInt32(),
                            MinTrailingBelowDelta = obj.GetProperty("minTrailingBelowDelta").GetInt32(),
                        };
                        break;
                    case SymbolFilterType.IcebergOrders:
                        result = new BinanceMaxNumberOfIcebergOrdersFilter
                        {
                            MaxNumIcebergOrders = obj.TryGetProperty("maxNumIcebergOrders", out var ele) ? ele.GetInt32() : 0
                        };
                        break;
                    default:
                        Trace.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss:fff} | Warning | Can't parse symbol filter of type: " + obj.GetProperty("filterType").GetString());
                        result = new BinanceSymbolFilter();
                        break;
                }
#pragma warning restore 8604
                result.FilterType = type;
                return (T)result;
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value, value.GetType());
            }
        }

//        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
//        {
//#pragma warning disable 8604, 8602
            
//        }

//        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
//        {
//            var filter = (BinanceSymbolFilter)value!;
//            writer.WriteStartObject();

//            writer.WritePropertyName("filterType");
//            writer.WriteValue(JsonConvert.SerializeObject(filter.FilterType, new SymbolFilterTypeConverter(false)));

//            switch (filter.FilterType)
//            {
//                case SymbolFilterType.LotSize:
//                    var lotSizeFilter = (BinanceSymbolLotSizeFilter)filter;
//                    writer.WritePropertyName("maxQty");
//                    writer.WriteValue(lotSizeFilter.MaxQuantity);
//                    writer.WritePropertyName("minQty");
//                    writer.WriteValue(lotSizeFilter.MinQuantity);
//                    writer.WritePropertyName("stepSize");
//                    writer.WriteValue(lotSizeFilter.StepSize);
//                    break;
//                case SymbolFilterType.MarketLotSize:
//                    var marketLotSizeFilter = (BinanceSymbolMarketLotSizeFilter)filter;
//                    writer.WritePropertyName("maxQty");
//                    writer.WriteValue(marketLotSizeFilter.MaxQuantity);
//                    writer.WritePropertyName("minQty");
//                    writer.WriteValue(marketLotSizeFilter.MinQuantity);
//                    writer.WritePropertyName("stepSize");
//                    writer.WriteValue(marketLotSizeFilter.StepSize);
//                    break;
//                case SymbolFilterType.MinNotional:
//                    var minNotionalFilter = (BinanceSymbolMinNotionalFilter)filter;
//                    writer.WritePropertyName("minNotional");
//                    writer.WriteValue(minNotionalFilter.MinNotional);
//                    writer.WritePropertyName("applyToMarket");
//                    writer.WriteValue(minNotionalFilter.ApplyToMarketOrders);
//                    writer.WritePropertyName("avgPriceMins");
//                    writer.WriteValue(minNotionalFilter.AveragePriceMinutes);
//                    break;
//                case SymbolFilterType.Price:
//                    var priceFilter = (BinanceSymbolPriceFilter)filter;
//                    writer.WritePropertyName("maxPrice");
//                    writer.WriteValue(priceFilter.MaxPrice);
//                    writer.WritePropertyName("minPrice");
//                    writer.WriteValue(priceFilter.MinPrice);
//                    writer.WritePropertyName("tickSize");
//                    writer.WriteValue(priceFilter.TickSize);
//                    break;
//                case SymbolFilterType.MaxNumberAlgorithmicOrders:
//                    var algoFilter = (BinanceSymbolMaxAlgorithmicOrdersFilter)filter;
//                    writer.WritePropertyName("maxNumAlgoOrders");
//                    writer.WriteValue(algoFilter.MaxNumberAlgorithmicOrders);
//                    break;
//                case SymbolFilterType.MaxPosition:
//                    var maxPositionFilter = (BinanceSymbolMaxPositionFilter)filter;
//                    writer.WritePropertyName("maxPosition");
//                    writer.WriteValue(maxPositionFilter.MaxPosition);
//                    break;
//                case SymbolFilterType.MaxNumberOrders:
//                    var orderFilter = (BinanceSymbolMaxOrdersFilter)filter;
//                    writer.WritePropertyName("maxNumOrders");
//                    writer.WriteValue(orderFilter.MaxNumberOrders);
//                    break;
//                case SymbolFilterType.IcebergParts:
//                    var icebergPartsFilter = (BinanceSymbolIcebergPartsFilter)filter;
//                    writer.WritePropertyName("limit");
//                    writer.WriteValue(icebergPartsFilter.Limit);
//                    break;
//                case SymbolFilterType.PricePercent:
//                    var pricePercentFilter = (BinanceSymbolPercentPriceFilter)filter;
//                    writer.WritePropertyName("multiplierUp");
//                    writer.WriteValue(pricePercentFilter.MultiplierUp);
//                    writer.WritePropertyName("multiplierDown");
//                    writer.WriteValue(pricePercentFilter.MultiplierDown);
//                    writer.WritePropertyName("avgPriceMins");
//                    writer.WriteValue(pricePercentFilter.AveragePriceMinutes);
//                    break;
//                case SymbolFilterType.TrailingDelta:
//                    var TrailingDelta = (BinanceSymbolTrailingDeltaFilter)filter;
//                    writer.WritePropertyName("maxTrailingAboveDelta");
//                    writer.WriteValue(TrailingDelta.MaxTrailingAboveDelta);
//                    writer.WritePropertyName("maxTrailingBelowDelta");
//                    writer.WriteValue(TrailingDelta.MaxTrailingBelowDelta);
//                    writer.WritePropertyName("minTrailingAboveDelta");
//                    writer.WriteValue(TrailingDelta.MinTrailingAboveDelta);
//                    writer.WritePropertyName("minTrailingBelowDelta");
//                    writer.WriteValue(TrailingDelta.MinTrailingBelowDelta);
//                    break;
//                case SymbolFilterType.IcebergOrders:
//                    var MaxNumIcebergOrders = (BinanceMaxNumberOfIcebergOrdersFilter)filter;
//                    writer.WritePropertyName("maxNumIcebergOrders");
//                    writer.WriteValue(MaxNumIcebergOrders.MaxNumIcebergOrders);                   
//                    break;
//                case SymbolFilterType.PercentagePriceBySide:
//                    var pricePercentSideBySideFilter = (BinanceSymbolPercentPriceBySideFilter)filter;
//                    writer.WritePropertyName("askMultiplierUp");
//                    writer.WriteValue(pricePercentSideBySideFilter.AskMultiplierUp);
//                    writer.WritePropertyName("askMultiplierDown");
//                    writer.WriteValue(pricePercentSideBySideFilter.AskMultiplierDown);
//                    writer.WritePropertyName("bidMultiplierUp");
//                    writer.WriteValue(pricePercentSideBySideFilter.BidMultiplierUp);
//                    writer.WritePropertyName("bidMultiplierDown");
//                    writer.WriteValue(pricePercentSideBySideFilter.BidMultiplierDown);
//                    writer.WritePropertyName("avgPriceMins");
//                    writer.WriteValue(pricePercentSideBySideFilter.AveragePriceMinutes);
//                    break;
//                case SymbolFilterType.Notional:
//                    var notionalFilter = (BinanceSymbolNotionalFilter)filter;
//                    writer.WritePropertyName("minNotional");
//                    writer.WriteValue(notionalFilter.MinNotional);
//                    writer.WritePropertyName("maxNotional");
//                    writer.WriteValue(notionalFilter.MaxNotional);
//                    writer.WritePropertyName("applyMinToMarketOrders");
//                    writer.WriteValue(notionalFilter.ApplyMinToMarketOrders);
//                    writer.WritePropertyName("applyMaxToMarketOrders");
//                    writer.WriteValue(notionalFilter.ApplyMaxToMarketOrders);
//                    writer.WritePropertyName("avgPriceMins");
//                    writer.WriteValue(notionalFilter.AveragePriceMinutes);
//                    break;
//                default:
//                    Trace.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss:fff} | Warning | Can't write symbol filter of type: " + filter.FilterType);
//                    break;
//            }

//            writer.WriteEndObject();
//        }
    }
}
