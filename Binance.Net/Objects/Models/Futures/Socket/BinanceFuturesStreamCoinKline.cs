using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Wrapper for kline information for a symbol
    /// </summary>
    public record BinanceFuturesStreamCoinKlineData : BinanceStreamEvent, IBinanceStreamKlineData
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The data
        /// </summary>
        [JsonPropertyName("k")]
        [JsonConverter(typeof(InterfaceConverter<BinanceFuturesStreamCoinKline, IBinanceStreamKline>))]
        public IBinanceStreamKline Data { get; set; } = default!;
    }

    /// <summary>
    /// The kline data
    /// </summary>
    public record BinanceFuturesStreamCoinKline : BinanceKlineBase, IBinanceStreamKline
    {
        /// <summary>
        /// The open time of this candlestick
        /// </summary>
        [JsonPropertyName("t"), JsonConverter(typeof(DateTimeConverter))]
        public new DateTime OpenTime { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("q")]
        public override decimal Volume { get; set; }

        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public new DateTime CloseTime { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("v")]
        public override decimal QuoteVolume { get; set; }

        /// <summary>
        /// The symbol this candlestick is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The interval of this candlestick
        /// </summary>
        [JsonPropertyName("i")]
        public KlineInterval Interval { get; set; }
        /// <summary>
        /// The first trade id in this candlestick
        /// </summary>
        [JsonPropertyName("f")]
        public long FirstTrade { get; set; }
        /// <summary>
        /// The last trade id in this candlestick
        /// </summary>
        [JsonPropertyName("L")]
        public long LastTrade { get; set; }
        /// <summary>
        /// The open price of this candlestick
        /// </summary>
        [JsonPropertyName("o")]
        public new decimal OpenPrice { get; set; }
        /// <summary>
        /// The close price of this candlestick
        /// </summary>
        [JsonPropertyName("c")]
        public new decimal ClosePrice { get; set; }
        /// <summary>
        /// The highest price of this candlestick
        /// </summary>
        [JsonPropertyName("h")]
        public new decimal HighPrice { get; set; }
        /// <summary>
        /// The lowest price of this candlestick
        /// </summary>
        [JsonPropertyName("l")]
        public new decimal LowPrice { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        [JsonPropertyName("n")]
        public new int TradeCount { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("Q")]
        public override decimal TakerBuyBaseVolume { get; set; }
        /// <inheritdoc />
        [JsonPropertyName("V")]
        public override decimal TakerBuyQuoteVolume { get; set; }

        /// <summary>
        /// Boolean indicating whether this candlestick is closed
        /// </summary>
        [JsonPropertyName("x")]
        public bool Final { get; set; }

        /// <summary>
        /// Casts this object to a <see cref="BinanceSpotKline"/> object
        /// </summary>
        /// <returns></returns>
        public BinanceSpotKline ToKline()
        {
            return new BinanceSpotKline
            {
                OpenPrice = OpenPrice,
                ClosePrice = ClosePrice,
                Volume = Volume,
                CloseTime = CloseTime,
                HighPrice = HighPrice,
                LowPrice = LowPrice,
                OpenTime = OpenTime,
                QuoteVolume = QuoteVolume,
                TakerBuyBaseVolume = TakerBuyBaseVolume,
                TakerBuyQuoteVolume = TakerBuyQuoteVolume,
                TradeCount = TradeCount
            };
        }
    }
}
