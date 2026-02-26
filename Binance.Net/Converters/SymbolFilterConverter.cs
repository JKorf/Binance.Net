using Binance.Net.Enums;
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
                        MultiplierDecimal = obj.TryGetProperty("multiplierDecimal", out var mulDec) ? (mulDec.ValueKind == JsonValueKind.String ? int.Parse(mulDec.GetString()!) : mulDec.GetInt32()) : null
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
                case SymbolFilterType.OrderAmends:
                    result = new BinanceMaxNumberOfOrderAmendsFilter
                    {
                        MaxNumOrderAmends = obj.TryGetProperty("maxNumOrderAmends", out var maxAm) ? maxAm.GetInt32() : 0
                    };
                    break;
                case SymbolFilterType.OrderLists:
                    result = new BinanceMaxNumberOfOrderListsFilter
                    {
                        MaxNumOrderLists = obj.TryGetProperty("maxNumOrderLists", out var maxLists) ? maxLists.GetInt32() : 0
                    };
                    break;
                case SymbolFilterType.PositionRiskControl:
                    result = new BinanceSymbolFilter();
                    break;
                default:
                    LibraryHelpers.StaticLogger?.LogWarning("Can't parse symbol filter of type: " + obj.GetProperty("filterType").GetString());
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
            writer.WriteStartObject();
            if (value is BinanceSymbolPriceFilter priceFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(priceFilter.FilterType));
                writer.WriteString("minPrice", priceFilter.MinPrice.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("maxPrice", priceFilter.MaxPrice.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("tickSize", priceFilter.TickSize.ToString(CultureInfo.InvariantCulture));
            }
            else if (value is BinanceSymbolPercentPriceFilter pricePercentFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(pricePercentFilter.FilterType));
                writer.WriteString("multiplierUp", pricePercentFilter.MultiplierUp.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("multiplierDown", pricePercentFilter.MultiplierDown.ToString(CultureInfo.InvariantCulture));
                if (pricePercentFilter.AveragePriceMinutes != null)
                    writer.WriteNumber("avgPriceMins", pricePercentFilter.AveragePriceMinutes.Value);
                if (pricePercentFilter.MultiplierDecimal != null)
                    writer.WriteNumber("multiplierDecimal", pricePercentFilter.MultiplierDecimal.Value);
            }
            else if (value is BinanceSymbolPercentPriceBySideFilter pricePercentSideFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(pricePercentSideFilter.FilterType));
                writer.WriteString("askMultiplierUp", pricePercentSideFilter.AskMultiplierUp.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("askMultiplierDown", pricePercentSideFilter.AskMultiplierDown.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("bidMultiplierUp", pricePercentSideFilter.BidMultiplierUp.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("bidMultiplierDown", pricePercentSideFilter.BidMultiplierDown.ToString(CultureInfo.InvariantCulture));
                writer.WriteNumber("avgPriceMins", pricePercentSideFilter.AveragePriceMinutes);
            }
            else if (value is BinanceSymbolLotSizeFilter lotSizeFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(lotSizeFilter.FilterType));
                writer.WriteString("maxQty", lotSizeFilter.MaxQuantity.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("minQty", lotSizeFilter.MinQuantity.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("stepSize", lotSizeFilter.StepSize.ToString(CultureInfo.InvariantCulture));
            }
            else if (value is BinanceSymbolMarketLotSizeFilter marketLotSizeFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(marketLotSizeFilter.FilterType));
                writer.WriteString("maxQty", marketLotSizeFilter.MaxQuantity.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("minQty", marketLotSizeFilter.MinQuantity.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("stepSize", marketLotSizeFilter.StepSize.ToString(CultureInfo.InvariantCulture));
            }
            else if (value is BinanceSymbolMinNotionalFilter minNotionalFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(minNotionalFilter.FilterType));
                writer.WriteString("minNotional", minNotionalFilter.MinNotional.ToString(CultureInfo.InvariantCulture));
                if (minNotionalFilter.ApplyToMarketOrders != null)
                    writer.WriteBoolean("applyToMarket", minNotionalFilter.ApplyToMarketOrders.Value);
                if (minNotionalFilter.AveragePriceMinutes != null)
                    writer.WriteNumber("avgPriceMins", minNotionalFilter.AveragePriceMinutes.Value);
            }
            else if (value is BinanceSymbolNotionalFilter notionalFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(notionalFilter.FilterType));
                writer.WriteString("minNotional", notionalFilter.MinNotional.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("maxNotional", notionalFilter.MaxNotional.ToString(CultureInfo.InvariantCulture));
                writer.WriteBoolean("applyMinToMarket", notionalFilter.ApplyMinToMarketOrders);
                writer.WriteBoolean("applyMaxToMarket", notionalFilter.ApplyMaxToMarketOrders);
                writer.WriteNumber("avgPriceMins", notionalFilter.AveragePriceMinutes);
            }
            else if (value is BinanceSymbolMaxOrdersFilter maxOrdersFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(maxOrdersFilter.FilterType));
                writer.WriteNumber("maxNumOrders", maxOrdersFilter.MaxNumberOrders);
            }
            else if (value is BinanceSymbolMaxAlgorithmicOrdersFilter maxAlgoOrdersFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(maxAlgoOrdersFilter.FilterType));
                writer.WriteNumber("maxNumAlgoOrders", maxAlgoOrdersFilter.MaxNumberAlgorithmicOrders);                
            }
            else if (value is BinanceSymbolIcebergPartsFilter icebergPartsFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(icebergPartsFilter.FilterType));
                writer.WriteNumber("limit", icebergPartsFilter.Limit);
            }
            else if (value is BinanceSymbolMaxPositionFilter positionFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(positionFilter.FilterType));
                writer.WriteString("maxPosition", positionFilter.MaxPosition.ToString(CultureInfo.InvariantCulture));                
            }
            else if (value is BinanceSymbolTrailingDeltaFilter trailingDeltaFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(trailingDeltaFilter.FilterType));
                writer.WriteNumber("maxTrailingBelowDelta", trailingDeltaFilter.MaxTrailingBelowDelta);
                writer.WriteNumber("maxTrailingAboveDelta", trailingDeltaFilter.MaxTrailingAboveDelta);
                writer.WriteNumber("minTrailingBelowDelta", trailingDeltaFilter.MinTrailingBelowDelta);
                writer.WriteNumber("minTrailingAboveDelta", trailingDeltaFilter.MinTrailingAboveDelta);
                
            }
            else if (value is BinanceMaxNumberOfIcebergOrdersFilter icebergOrdersFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(icebergOrdersFilter.FilterType));
                writer.WriteNumber("maxNumIcebergOrders", icebergOrdersFilter.MaxNumIcebergOrders);                
            }
            else if (value is BinanceMaxNumberOfOrderAmendsFilter amendsFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(amendsFilter.FilterType));
                writer.WriteNumber("maxNumOrderAmends", amendsFilter.MaxNumOrderAmends);               

            }
            else if (value is BinanceMaxNumberOfOrderListsFilter orderListsFilter)
            {
                writer.WriteString("filterType", EnumConverter.GetString(orderListsFilter.FilterType));
                writer.WriteNumber("maxNumOrderLists", orderListsFilter.MaxNumOrderLists);                
            }
            writer.WriteEndObject();
        }
    }
}