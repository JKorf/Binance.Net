using Binance.Net.Enums;
using System.Diagnostics;
using Binance.Net.Objects.Models.Spot;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;

namespace Binance.Net.Converters
{
    internal class SymbolFilterConverterImp<T> : JsonConverter<T>
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var obj = JsonDocument.ParseValue(ref reader).RootElement;
            var type = obj.GetProperty("filterType").Deserialize((JsonTypeInfo<SymbolFilterType>)options.GetTypeInfo(typeof(SymbolFilterType)));
            BinanceSymbolFilter result;
            switch (type)
            {
                case SymbolFilterType.LotSize:
                    result = new BinanceSymbolLotSizeFilter
                    {
                        MaxQuantity = decimal.Parse(obj.GetProperty("maxQty").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MinQuantity = decimal.Parse(obj.GetProperty("minQty").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        StepSize = decimal.Parse(obj.GetProperty("stepSize").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                    break;
                case SymbolFilterType.MarketLotSize:
                    result = new BinanceSymbolMarketLotSizeFilter
                    {
                        MaxQuantity = decimal.Parse(obj.GetProperty("maxQty").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MinQuantity = decimal.Parse(obj.GetProperty("minQty").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        StepSize = decimal.Parse(obj.GetProperty("stepSize").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                    break;
                case SymbolFilterType.MinNotional:
                    result = new BinanceSymbolMinNotionalFilter
                    {
                        MinNotional = decimal.Parse(obj.TryGetProperty("minNotional", out var minNotional) ? minNotional.GetString()! : obj.GetProperty("notional").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        ApplyToMarketOrders = obj.TryGetProperty("applyToMarket", out var applyToMarket) ? applyToMarket.GetBoolean() : null,
                        AveragePriceMinutes = obj.TryGetProperty("avgPriceMins", out var avgPrice) ? avgPrice.GetInt32() : null
                    };
                    break;
                case SymbolFilterType.Notional:
                    result = new BinanceSymbolNotionalFilter
                    {
                        MinNotional = decimal.Parse(obj.GetProperty("minNotional").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MaxNotional = decimal.Parse(obj.GetProperty("maxNotional").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        ApplyMinToMarketOrders = obj.GetProperty("applyMinToMarket").GetBoolean(),
                        ApplyMaxToMarketOrders = obj.GetProperty("applyMaxToMarket").GetBoolean(),
                        AveragePriceMinutes = obj.GetProperty("avgPriceMins").GetInt32()
                    };
                    break;
                case SymbolFilterType.Price:
                    result = new BinanceSymbolPriceFilter
                    {
                        MaxPrice = decimal.Parse(obj.GetProperty("maxPrice").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MinPrice = decimal.Parse(obj.GetProperty("minPrice").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        TickSize = decimal.Parse(obj.GetProperty("tickSize").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                    };
                    break;
                case SymbolFilterType.MaxNumberAlgorithmicOrders:
                    result = new BinanceSymbolMaxAlgorithmicOrdersFilter
                    {
                        MaxNumberAlgorithmicOrders = obj.TryGetProperty("maxNumAlgoOrders", out var algoOrderEl) ? algoOrderEl.GetInt32() : obj.GetProperty("limit").GetInt32()
                    };
                    break;
                case SymbolFilterType.MaxNumberOrders:
                    result = new BinanceSymbolMaxOrdersFilter
                    {
                        MaxNumberOrders = obj.TryGetProperty("maxNumOrders", out var orderEl) ? orderEl.GetInt32() : obj.GetProperty("limit").GetInt32()
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
                        MultiplierUp = decimal.Parse(obj.GetProperty("multiplierUp").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MultiplierDown = decimal.Parse(obj.GetProperty("multiplierDown").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        AveragePriceMinutes = obj.TryGetProperty("avgPriceMins", out var avgPriceMins) ? avgPriceMins.GetInt32() : null,
                        MultiplierDecimal = obj.TryGetProperty("multiplierDecimal", out var mulDec) ? mulDec.GetInt32() : null
                    };
                    break;
                case SymbolFilterType.MaxPosition:
                    result = new BinanceSymbolMaxPositionFilter
                    {
                        MaxPosition = obj.TryGetProperty("maxPosition", out var el) ? decimal.Parse(el.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture) : 0
                    };
                    break;
                case SymbolFilterType.PercentagePriceBySide:
                    result = new BinanceSymbolPercentPriceBySideFilter
                    {
                        AskMultiplierUp = decimal.Parse(obj.GetProperty("askMultiplierUp").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        AskMultiplierDown = decimal.Parse(obj.GetProperty("askMultiplierDown").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        BidMultiplierUp = decimal.Parse(obj.GetProperty("bidMultiplierUp").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        BidMultiplierDown = decimal.Parse(obj.GetProperty("bidMultiplierDown").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
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
            return (T)(object)result;
        }

#if NET5_0_OR_GREATER
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL3050:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2026:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
#endif
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<T>(writer, value, SerializerOptions.WithConverters(BinanceExchange.SerializerContext));
        }
    }
}