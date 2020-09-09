using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Shared;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketStream
{
    /// <summary>
    /// Wrapper for kline information for a symbol
    /// </summary>
    public class BinanceFuturesStreamCoinKlineData : BinanceStreamEvent, IBinanceStreamKlineData
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// The data
        /// </summary>
        [JsonProperty("k")]
        [JsonConverter(typeof(InterfaceConverter<BinanceFuturesStreamCoinKline>))]
        public IBinanceStreamKline Data { get; set; } = default!;
    }

    /// <summary>
    /// The kline data
    /// </summary>
    public class BinanceFuturesStreamCoinKline : BinanceKlineBase, IBinanceStreamKline
    {
        /// <summary>
        /// The open time of this candlestick
        /// </summary>
        [JsonProperty("t"), JsonConverter(typeof(TimestampConverter))]
        public new DateTime OpenTime { get; set; }

        /// <inheritdoc />
        [JsonProperty("q")]
        public override decimal BaseVolume { get; set; }

        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public new DateTime CloseTime { get; set; }

        /// <inheritdoc />
        [JsonProperty("v")]
        public override decimal QuoteVolume { get; set; }

        /// <summary>
        /// The symbol this candlestick is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The interval of this candlestick
        /// </summary>
        [JsonProperty("i"), JsonConverter(typeof(KlineIntervalConverter))]
        public KlineInterval Interval { get; set; }
        /// <summary>
        /// The first trade id in this candlestick
        /// </summary>
        [JsonProperty("f")]
        public long FirstTrade { get; set; }
        /// <summary>
        /// The last trade id in this candlestick
        /// </summary>
        [JsonProperty("L")]
        public long LastTrade { get; set; }
        /// <summary>
        /// The open price of this candlestick
        /// </summary>
        [JsonProperty("o")]
        public new decimal Open { get; set; }
        /// <summary>
        /// The close price of this candlestick
        /// </summary>
        [JsonProperty("c")]
        public new decimal Close { get; set; }
        /// <summary>
        /// The highest price of this candlestick
        /// </summary>
        [JsonProperty("h")]
        public new decimal High { get; set; }
        /// <summary>
        /// The lowest price of this candlestick
        /// </summary>
        [JsonProperty("l")]
        public new decimal Low { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        [JsonProperty("n")]
        public new int TradeCount { get; set; }

        /// <inheritdoc />
        [JsonProperty("Q")]
        public override decimal TakerBuyBaseVolume { get; set; }
        /// <inheritdoc />
        [JsonProperty("V")]
        public override decimal TakerBuyQuoteVolume { get; set; }

        /// <summary>
        /// Boolean indicating whether this candlestick is closed
        /// </summary>
        [JsonProperty("x")]
        public bool Final { get; set; }

        /// <summary>
        /// Casts this object to a <see cref="BinanceSpotKline"/> object
        /// </summary>
        /// <returns></returns>
        public BinanceSpotKline ToKline()
        {
            return new BinanceSpotKline
            {
                Open = Open,
                Close = Close,
                BaseVolume = BaseVolume,
                CloseTime = CloseTime,
                High = High,
                Low = Low,
                OpenTime = OpenTime,
                QuoteVolume = QuoteVolume,
                TakerBuyBaseVolume = TakerBuyBaseVolume,
                TakerBuyQuoteVolume = TakerBuyQuoteVolume,
                TradeCount = TradeCount
            };
        }
    }
}
