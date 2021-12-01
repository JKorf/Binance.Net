using Binance.Net.Objects;
using Binance.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using Binance.Net.Interfaces.Clients;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IBinanceClient> _comparer = new JsonToObjectComparer<IBinanceClient>((json) => TestHelpers.CreateResponseClient(json, new BinanceClientOptions()
        { 
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"), 
            AutoTimestamp = false, 
            SpotApiOptions = new BinanceApiClientOptions
            {
                RateLimiters = new List<IRateLimiter>()
            },
            UsdFuturesApiOptions = new BinanceApiClientOptions
            {
                RateLimiters = new List<IRateLimiter>()
            },
            CoinFuturesApiOptions = new BinanceApiClientOptions
            {
                RateLimiters = new List<IRateLimiter>()
            }
        }));

        [Test]
        public async Task ValidateSpotAccountCalls()
        {   
            await _comparer.ProcessSubject(
                "Spot/Account",
                c => c.SpotApi.Account,
                useNestedJsonPropertyForCompare: new Dictionary<string, string> {
                    { "GetTradingStatusAsync", "data" },
                    { "GetDailySpotAccountSnapshotAsync", "snapshotVos" },
                    { "GetDailyMarginAccountSnapshotAsync", "snapshotVos" },
                    { "GetDailyFutureAccountSnapshotAsync", "snapshotVos" },
                    { "GetFiatPaymentHistoryAsync", "data" },
                    { "GetFiatDepositWithdrawHistoryAsync", "data" }
                },
                parametersToSetNull: new[] { "limit" });
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/ExchangeData",
                c => c.SpotApi.ExchangeData,
                useNestedJsonPropertyForCompare: new Dictionary<string, string> {
                    { "GetTradingStatusAsync", "data" }
                },
                parametersToSetNull: new[] { "limit" });
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/Trading",
                c => c.SpotApi.Trading,
                useNestedJsonPropertyForCompare: new Dictionary<string, string> {

                },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "PlaceOrderAsync", new List<string> { "price" } },
                    { "PlaceMarginOrderAsync", new List<string> { "price" } },
                    { "GetMarginOrdersAsync", new List<string> { "price" } },
                },
                parametersToSetNull: new[] { "limit", "quoteQuantity", "fromId" });
        }

        [Test]
        public async Task ValidateSpotSubAccountCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/SubAccount",
                c => c.GeneralApi.SubAccount,

                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetSubAccountsAsync", "subAccounts" },
                    { "GetSubAccountAssetsAsync", "balances" },
                });
        }

        [Test]
        public async Task ValidateSpotBrokerageCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/Brokerage",
                c => c.GeneralApi.Brokerage);
        }

        [Test]
        public async Task ValidateSpotMiningCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/Mining",
                c => c.GeneralApi.Mining,
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
        public async Task ValidateSpotFuturesCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/Futures",
                c => c.GeneralApi.Futures,
                new [] { "collateralQuantity" });
        }

        [Test]
        public async Task ValidateSpotLendingCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/Lending",
                c => c.GeneralApi.Lending);
        }

        [Test]
        public async Task ValidateCoinFuturesTradingCalls()
        {
            await _comparer.ProcessSubject(
                "CoinFutures/Trading",
                c => c.CoinFuturesApi.Trading,
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {

                },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                },
                parametersToSetNull: new string[] {  });
        }

        [Test]
        public async Task ValidateCoinFuturesAccountCalls()
        {
            await _comparer.ProcessSubject(
                "CoinFutures/Account",
                c => c.CoinFuturesApi.Account,
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {

                },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetPositionInformationAsync", new List<string>{ "unRealizedProfit"  } },
                    { "GetBracketsAsync", new List<string> { "pair", "qtyCap", "qtylFloor" } }
                },
                parametersToSetNull: new string[] { });
        }

        [Test]
        public async Task ValidateCoinFuturesExchangeDataCalls()
        {
            await _comparer.ProcessSubject(
                "CoinFutures/ExchangeData",
                c => c.CoinFuturesApi.ExchangeData,
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {

                },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetTopLongShortPositionRatioAsync", new List<string> { "shortPosition", "longPosition" } },
                },
                parametersToSetNull: new string[] {
                    "limit" 
                });
        }

        [Test]
        public async Task ValidateUsdFuturesAccountCalls()
        {
            await _comparer.ProcessSubject(
                "UsdFutures/Account",
                c => c.UsdFuturesApi.Account,
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetPositionInformationAsync", new List<string>{ "unRealizedProfit"  } }
                },
                parametersToSetNull: new string[] {
                    "limit"
                });
        }

        [Test]
        public async Task ValidateUsdFuturesExchangeDataCalls()
        {
            await _comparer.ProcessSubject(
                "UsdFutures/ExchangeData",
                c => c.UsdFuturesApi.ExchangeData,
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                },
                parametersToSetNull: new string[] {
                    "limit"
                });
        }

        [Test]
        public async Task ValidateUsdFuturesTradingCalls()
        {
            await _comparer.ProcessSubject(
                "UsdFutures/Trading",
                c => c.UsdFuturesApi.Trading,
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                },
                parametersToSetNull: new string[] {
                    "limit"
                });
        }

        //[Test]
        //public async Task ValidateSpotFutureCalls()
        //{
        //    await _comparer.ProcessSubject(
        //        "SpotFutures",
        //        c => c.Spot.Futures,
        //        new [] { "collateralQuantity" });
        //}

        //[Test]
        //public async Task ValidateBrokerageCalls()
        //{
        //    await _comparer.ProcessSubject(
        //        "Brokerage",
        //        c => c.Brokerage);
        //}

        //[Test]
        //public async Task ValidateLendingCalls()
        //{
        //    await _comparer.ProcessSubject(
        //        "Lending",
        //        c => c.Lending);
        //}

        //[Test]
        //public async Task ValidateBlvtCalls()
        //{
        //    await _comparer.ProcessSubject(
        //        "LeveragedTokens",
        //        c => c.Blvt);
        //}

        //[Test]
        //public async Task ValidateLiquidSwapCalls()
        //{
        //    await _comparer.ProcessSubject(
        //        "LiquidSwap",
        //        c => c.BSwap);
        //}
    }
}
