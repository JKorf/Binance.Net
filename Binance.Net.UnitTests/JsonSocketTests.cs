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
