using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot.Socket;
using Binance.Net.UnitTests.TestImplementations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Binance.Net.UnitTests
{
    internal class JsonSocketTests
    {
        [Test]
        public async Task ValidatAggregatedTradeUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamAggregatedTrade>(@"JsonResponses/Spot/Socket/AggregatedTradeUpdate.txt");
        }

        [Test]
        public async Task ValidateTradeUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamTrade>(@"JsonResponses/Spot/Socket/TradeUpdate.txt");
        }

        [Test]
        public async Task ValidateKlineUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamKlineData>(@"JsonResponses/Spot/Socket/KlineUpdate.txt", new List<string> { "B" });
        }

        [Test]
        public async Task ValidateMiniTickUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamMiniTick>(@"JsonResponses/Spot/Socket/MiniTickUpdate.txt", new List<string> { "B" });
        }

        [Test]
        public async Task ValidateBookPriceUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamBookPrice>(@"JsonResponses/Spot/Socket/BookPriceUpdate.txt", new List<string> { "B" });
        }

        [Test]
        public async Task ValidateTickerUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamTick>(@"JsonResponses/Spot/Socket/TickerUpdate.txt", new List<string> { "B" });
        }

        [Test]
        public async Task ValidateRollingTickerUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamRollingWindowTick>(@"JsonResponses/Spot/Socket/RollingTickerUpdate.txt", new List<string> { "B" });
        }

        [Test]
        public async Task ValidateUserUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamOrderUpdate>(@"JsonResponses/Spot/Socket/UserUpdate1.txt", new List<string> { "M" });
            await TestFileToObject<BinanceStreamOrderList>(@"JsonResponses/Spot/Socket/UserUpdate2.txt", new List<string> { "B" });
            await TestFileToObject<BinanceStreamPositionsUpdate>(@"JsonResponses/Spot/Socket/UserUpdate3.txt", new List<string> { "B" });
            await TestFileToObject<BinanceStreamBalanceUpdate>(@"JsonResponses/Spot/Socket/UserUpdate4.txt", new List<string> { "B" });
        }

        [Test]
        public async Task ValidateUsdFuturesMarginUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamMarginUpdate>(@"JsonResponses/UsdFutures/Socket/UserMarginUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesConfigUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamConfigUpdate>(@"JsonResponses/UsdFutures/Socket/UserConfigUpdate1.txt");
            await TestFileToObject<BinanceFuturesStreamConfigUpdate>(@"JsonResponses/UsdFutures/Socket/UserConfigUpdate2.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesAccountUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamAccountUpdate>(@"JsonResponses/UsdFutures/Socket/UserAccountUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesOrderUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamOrderUpdate>(@"JsonResponses/UsdFutures/Socket/UserOrderUpdate.txt", new List<string> { "pP", "si", "ss" });
        }

        [Test]
        public async Task ValidateUsdFuturesStrategyUpdateStreamJson()
        {
            await TestFileToObject<BinanceStrategyUpdate>(@"JsonResponses/UsdFutures/Socket/UserStrategyUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesGridUpdateStreamJson()
        {
            await TestFileToObject<BinanceGridUpdate>(@"JsonResponses/UsdFutures/Socket/UserGridUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesContractInfoUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamSymbolUpdate>(@"JsonResponses/UsdFutures/Socket/ContractInfoUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesOrderBookUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamOrderBookDepth>(@"JsonResponses/UsdFutures/Socket/OrderBookUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesPartialOrderBookUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamOrderBookDepth>(@"JsonResponses/UsdFutures/Socket/PartialOrderBookUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesLiquidationUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamLiquidationData>(@"JsonResponses/UsdFutures/Socket/LiquidationUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesBookTickerUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamBookPrice>(@"JsonResponses/UsdFutures/Socket/BookTickerUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesAggTradesUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamAggregatedTrade>(@"JsonResponses/UsdFutures/Socket/AggTradesUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesMarketTickerUpdateStreamJson()
        {
            await TestFileToObject<IEnumerable<BinanceStreamTick>>(@"JsonResponses/UsdFutures/Socket/MarketTickersUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesCompositeIndexUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamCompositeIndex>(@"JsonResponses/UsdFutures/Socket/CompositeIndexUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesMiniTickerUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamMiniTick>(@"JsonResponses/UsdFutures/Socket/MiniTickersUpdate.txt");
        }

        [Test]
        public async Task ValidateUsdFuturesContinuousKlineUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamContinuousKlineData>(@"JsonResponses/UsdFutures/Socket/ContractKlinesUpdate.txt", new List<string> { "B" });
        }

        [Test]
        public async Task ValidateUsdFuturesMarkPriceUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesUsdtStreamMarkPrice>(@"JsonResponses/UsdFutures/Socket/MarkPriceUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesKlineUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamCoinKlineData>(@"JsonResponses/CoinFutures/Socket/KlineUpdate.txt", new List<string> { "B" });
        }

        [Test]
        public async Task ValidateCoinFuturesIndexPriceUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamIndexPrice>(@"JsonResponses/CoinFutures/Socket/IndexPriceUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesMarkPriceUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesCoinStreamMarkPrice>(@"JsonResponses/CoinFutures/Socket/MarkPriceUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesMiniTickerUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamCoinMiniTick>(@"JsonResponses/CoinFutures/Socket/MiniTickerUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesTickerUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamCoinTick>(@"JsonResponses/CoinFutures/Socket/TickerUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesAggTradeUpdateStreamJson()
        {
            await TestFileToObject<BinanceStreamAggregatedTrade>(@"JsonResponses/CoinFutures/Socket/AggTradeUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesBookTickerUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamBookPrice>(@"JsonResponses/CoinFutures/Socket/BookTickerUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesLiquidationOrderUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamLiquidationData>(@"JsonResponses/CoinFutures/Socket/LiquidationOrderUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesPartialBookUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamOrderBookDepth>(@"JsonResponses/CoinFutures/Socket/PartialBookUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesOrderBookUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamOrderBookDepth>(@"JsonResponses/CoinFutures/Socket/OrderBookUpdate.txt");
        }

        [Test]
        public async Task ValidateCoinFuturesContractInfoUpdateStreamJson()
        {
            await TestFileToObject<BinanceFuturesStreamSymbolUpdate>(@"JsonResponses/CoinFutures/Socket/ContractInfoUpdate.txt");
        }

        private static async Task TestFileToObject<T>(string filePath, List<string> ignoreProperties = null)
        {
            var listener = new EnumValueTraceListener();
            Trace.Listeners.Add(listener);
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string json;
            try
            {
                var file = File.OpenRead(Path.Combine(path, filePath));
                using var reader = new StreamReader(file);
                json = await reader.ReadToEndAsync();
            }
            catch (FileNotFoundException)
            {
                throw;
            }

            var result = JsonConvert.DeserializeObject<T>(json);
            JsonToObjectComparer<IBinanceSocketClient>.ProcessData("", result, json, ignoreProperties: new Dictionary<string, List<string>>
            {
                { "", ignoreProperties ?? new List<string>() }
            });
            Trace.Listeners.Remove(listener);
        }
    }

    internal class EnumValueTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            if (message.Contains("Cannot map"))
                throw new Exception("Enum value error: " + message);
        }

        public override void WriteLine(string message)
        {
            if (message.Contains("Cannot map"))
                throw new Exception("Enum value error: " + message);
        }
    }
}
