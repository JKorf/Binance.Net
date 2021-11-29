using Binance.Net.Clients.Rest.Spot;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.Rest.UsdFutures;
using Binance.Net.Objects;
using Binance.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients.Rest.CoinFutures;
using Binance.Net.Interfaces.Clients.Rest.Spot;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IBinanceClientSpot> _comparer = new JsonToObjectComparer<IBinanceClientSpot>((json) => TestHelpers.CreateResponseClient(json, new BinanceClientSpotOptions()
        { ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"), AutoTimestamp = false, RateLimiters = new List<IRateLimiter>()}));

        private JsonToObjectComparer<IBinanceClientCoinFutures> _comparerCoin = new JsonToObjectComparer<IBinanceClientCoinFutures>((json) => TestHelpers.CreateResponseClientCoin(json, new BinanceClientCoinFuturesOptions()
        { ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"), AutoTimestamp = false, RateLimiters = new List<IRateLimiter>() }));

        private JsonToObjectComparer<IBinanceClientUsdFuturesMarket> _comparerUsd = new JsonToObjectComparer<IBinanceClientUsdFuturesMarket>((json) => TestHelpers.CreateResponseClientUsd(json, new BinanceClientUsdFuturesOptions()
        { ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"), AutoTimestamp = false, RateLimiters = new List<IRateLimiter>() }));


        [Test]
        public async Task ValidateSpotAccountCalls()
        {   
            await _comparer.ProcessSubject(
                "Spot/Account",
                c => c.Account,
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
                c => c.ExchangeData,
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
                c => c.Trading,
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
                c => c.SubAccount,

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
                c => c.Brokerage);
        }

        [Test]
        public async Task ValidateSpotMiningCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/Mining",
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
        public async Task ValidateSpotFuturesCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/Futures",
                c => c.Futures,
                new [] { "collateralQuantity" });
        }


        [Test]
        public async Task ValidateSpotLiquidSwapCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/LiquidSwap",
                c => c.LiquidSwap);
        }

        [Test]
        public async Task ValidateSpotLeveragedTokensCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/LeveragedTokens",
                c => c.LeveragedTokens);
        }

        [Test]
        public async Task ValidateSpotLendingCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/Lending",
                c => c.Lending);
        }

        [Test]
        public async Task ValidateCoinFuturesTradingCalls()
        {
            await _comparerCoin.ProcessSubject(
                "CoinFutures/Trading",
                c => c.Trading,
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
            await _comparerCoin.ProcessSubject(
                "CoinFutures/Account",
                c => c.Account,
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
            await _comparerCoin.ProcessSubject(
                "CoinFutures/ExchangeData",
                c => c.ExchangeData,
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
            await _comparerUsd.ProcessSubject(
                "UsdFutures/Account",
                c => c.Account,
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
            await _comparerUsd.ProcessSubject(
                "UsdFutures/ExchangeData",
                c => c.ExchangeData,
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
            await _comparerUsd.ProcessSubject(
                "UsdFutures/Trading",
                c => c.Trading,
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
