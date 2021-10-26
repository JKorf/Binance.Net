using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Binance.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IBinanceClient> _comparer = new JsonToObjectComparer<IBinanceClient>((json) => TestHelpers.CreateResponseClient(json, new BinanceClientOptions()
        { ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"), AutoTimestamp = false }));

        [Test]
        public async Task ValidateSpotCalls()
        {   
            await _comparer.ProcessSubject(
                "Spot",
                c => c.Spot,
                useNestedJsonPropertyForCompare: new Dictionary<string, string> {
                    { "GetTradingStatusAsync", "data" }
                });
        }

        [Test]
        public async Task ValidateSpotMarketCalls()
        {
            await _comparer.ProcessSubject(
                "SpotMarket",
                c => c.Spot.Market, 
                new[] { "limit" });
        }

        [Test]
        public async Task ValidateSpotFutureCalls()
        {
            await _comparer.ProcessSubject(
                "SpotFutures",
                c => c.Spot.Futures,
                new [] { "collateralQuantity" });
        }

        [Test]
        public async Task ValidateSpotOrderCalls()
        {
            await _comparer.ProcessSubject(
                "SpotOrder",
                c => c.Spot.Order,
                new[] { "quoteQuantity", "fromId" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "PlaceOrderAsync", new List<string> { "price" } }
                });
        }

        [Test]
        public async Task ValidateSpotSystemCalls()
        {
            await _comparer.ProcessSubject(
                "SpotSystem",
                c => c.Spot.System);
        }

        [Test]
        public async Task ValidateMarginCalls()
        {
            await _comparer.ProcessSubject(
                "Margin",
                c => c.Margin);
        }

        [Test]
        public async Task ValidateMarginMarketCalls()
        {
            await _comparer.ProcessSubject(
                "MarginMarket",
                c => c.Margin.Market);
        }

        [Test]
        public async Task ValidateMarginOrderCalls()
        {
            await _comparer.ProcessSubject(
                "MarginOrder",
                c => c.Margin.Order,
                new string[] { "quoteQuantity", "fromId" },
                ignoreProperties: new Dictionary<string, List<string>>
                { 
                    { "GetMarginAccountOrdersAsync", new List<string> { "price" } }
                });
        }

        [Test]
        public async Task ValidateBrokerageCalls()
        {
            await _comparer.ProcessSubject(
                "Brokerage",
                c => c.Brokerage);
        }

        [Test]
        public async Task ValidateFiatCalls()
        {
            await _comparer.ProcessSubject(
                "Fiat",
                c => c.Fiat,
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetFiatPaymentHistoryAsync", "data" },
                    { "GetFiatDepositWithdrawHistoryAsync", "data" }
                });
        }

        [Test]
        public async Task ValidateGeneralCalls()
        {
            await _comparer.ProcessSubject(
                "General",
                c => c.General,
                parametersToSetNull: new [] { "limit" },
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetDailySpotAccountSnapshotAsync", "snapshotVos" },
                    { "GetDailyMarginAccountSnapshotAsync", "snapshotVos" },
                    { "GetDailyFutureAccountSnapshotAsync", "snapshotVos" },
                    { "GetTradingStatusAsync", "data" }
                });
        }

        [Test]
        public async Task ValidateLendingCalls()
        {
            await _comparer.ProcessSubject(
                "Lending",
                c => c.Lending);
        }

        [Test]
        public async Task ValidateBlvtCalls()
        {
            await _comparer.ProcessSubject(
                "LeveragedTokens",
                c => c.Blvt);
        }

        [Test]
        public async Task ValidateLiquidSwapCalls()
        {
            await _comparer.ProcessSubject(
                "LiquidSwap",
                c => c.BSwap);
        }

        [Test]
        public async Task ValidateMiningCalls()
        {
            await _comparer.ProcessSubject(
                "Mining",
                c => c.Mining,
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetMiningCoinListAsync", "data" },
                    { "GetMiningAlgorithmListAsync", "data" },
                    { "GetMiningRevenueListAsync", "data" },
                    { "GetMiningOtherRevenueListAsync", "data" },
                    { "GetMiningStatisticsAsync", "data" },
                    { "GetMiningAccountListAsync", "data" },
                    { "GetHashrateResaleListAsync", "data" },
                    { "GetHashrateResaleDetailsAsync", "data" },
                    { "PlaceHashrateResaleRequestAsync", "data" },
                    { "CancelHashrateResaleRequestAsync", "data" },
                    { "GetMinerDetailsAsync", "data" },
                    { "GetMinerListAsync", "data" },
                });
        }

        [Test]
        public async Task ValidateSubAccountCalls()
        {
            await _comparer.ProcessSubject(
                "SubAccounts",
                c => c.SubAccount,
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetSubAccountsAsync", "subAccounts" },
                    { "GetSubAccountAssetsAsync", "balances" },
                });
        }

        [Test]
        public async Task ValidateWithdrawDepositCalls()
        {
            await _comparer.ProcessSubject(
                "WithdrawDeposit",
                c => c.WithdrawDeposit);
        }

        [Test]
        public async Task ValidateFuturesUsdCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesUsd",
                c => c.FuturesUsdt,
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetPositionInformationAsync", new List<string>{ "unRealizedProfit"  } }
                });
        }


        [Test]
        public async Task ValidateFuturesUsdAccountCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesUsdAccount",
                c => c.FuturesUsdt.Account);
        }

        [Test]
        public async Task ValidateFuturesUsdMarketCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesUsdMarket",
                c => c.FuturesUsdt.Market,
                new string[] { "limit" });
        }

        [Test]
        public async Task ValidateFuturesUsdOrderCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesUsdOrder",
                c => c.FuturesUsdt.Order);
        }

        [Test]
        public async Task ValidateFuturesUsdSystemCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesUsdSystem",
                c => c.FuturesUsdt.System);
        }

        [Test]
        public async Task ValidateFuturesCoinCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesCoin",
                c => c.FuturesCoin,
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetPositionInformationAsync", new List<string> { "unRealizedProfit" } },
                    { "GetBracketsAsync", new List<string> { "pair", "qtyCap", "qtylFloor" } },
                });
        }


        [Test]
        public async Task ValidateFuturesCoinAccountCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesCoinAccount",
                c => c.FuturesCoin.Account);
        }

        [Test]
        public async Task ValidateFuturesCoinMarketCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesCoinMarket",
                c => c.FuturesCoin.Market,
                new string[] { "limit" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetTopLongShortPositionRatioAsync", new List<string> { "shortPosition", "longPosition" } },
                    //{ "GetOrderBookAsync", new List<string> { "symbol" } },
                });
        }

        [Test]
        public async Task ValidateFuturesCoinOrderCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesCoinOrder",
                c => c.FuturesCoin.Order);
        }

        [Test]
        public async Task ValidateFuturesCoinSystemCalls()
        {
            await _comparer.ProcessSubject(
                "FuturesCoinSystem",
                c => c.FuturesCoin.System);
        }

       
    }
}
