using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using Binance.Net.SymbolOrderBooks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [NonParallelizable]
    internal class BinanceRestIntegrationTests : RestIntegrationTest<BinanceRestClient>
    {
        public override bool Run { get; set; } = true;

        public BinanceRestIntegrationTests()
        {
            BinanceExchange.RateLimiter.RateLimitTriggered += (x) => Debug.WriteLine(x);
        }

        public override BinanceRestClient GetClient(ILoggerFactory loggerFactory, bool newDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new BinanceRestClient(null, loggerFactory, Options.Create(new BinanceRestOptions
            {
                AutoTimestamp = false,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestErrorResponseParsing(bool newDeserialization)
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient(newDeserialization).SpotApi.ExchangeData.GetTickerAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("-1121"));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotAccount(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetAccountInfoAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetFiatPaymentHistoryAsync(Enums.OrderSide.Buy, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetFiatDepositWithdrawHistoryAsync(Enums.TransactionType.Withdrawal, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetWithdrawalHistoryAsync(default, default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetWithdrawalAddressesAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetDepositHistoryAsync(default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetDailySpotAccountSnapshotAsync(default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetDailyFutureAccountSnapshotAsync(default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetAccountStatusAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetFundingWalletAsync(default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetAPIKeyPermissionsAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetUserAssetsAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetBalancesAsync(default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetWalletBalancesAsync("BTC", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetAssetDividendRecordsAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetDustLogAsync(default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetBnbBurnStatusAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetTransfersAsync(Enums.UniversalTransferType.FundingToUsdFutures, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginLevelInformationAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetInterestMarginDataAsync(default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginInterestRateHistoryAsync("ETH", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetIsolatedMarginTierDataAsync("ETHUSDT", default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginAccountInfoAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetIsolatedMarginAccountAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetEnabledIsolatedMarginAccountLimitAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetTradingStatusAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetOrderRateLimitStatusAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetRebateHistoryAsync(default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetPortfolioMarginCollateralRateAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetPortfolioMarginBankruptcyLoanAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetBusdConvertHistoryAsync(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetCloudMiningHistoryAsync(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetIsolatedMarginFeeDataAsync(default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetTradeFeeAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetAccountVipLevelAndStatusAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetCommissionRatesAsync("ETHUSDT", default, default), true);

            // Not available without margin account
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginTransferHistoryAsync(Enums.TransferDirection.RollOut, default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginLoansAsync("ETH", default,  default, default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginRepaysAsync("ETH", default, default, default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginInterestHistoryAsync(default, default, default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginForcedLiquidationHistoryAsync(default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginMaxBorrowAmountAsync("ETH", default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginMaxTransferAmountAsync("ETH", default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetMarginOrderRateLimitStatusAsync(default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetPortfolioMarginAccountInfoAsync(default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetCrossMarginSmallLiabilityExchangeAssetsAsync(default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetCrossMarginSmallLiabilityExchangeHistoryAsync(default, default, default, default, default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotExchangeData(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.PingAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetServerTimeAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(false, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetSystemStatusAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetAssetDetailsAsync(5000, CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetProductsAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT", 1, 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", null, null, null, 10, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneHour, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetUiKlinesAsync("ETHUSDT", Enums.KlineInterval.OneHour, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetCurrentAvgPriceAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetTickersAsync(null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetTradingDayTickerAsync("ETHUSDT", "0", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetRollingWindowTickerAsync("ETHUSDT", TimeSpan.FromHours(1), CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetPriceAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetPricesAsync(null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetBookPriceAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetMarginAssetsAsync(null, CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetMarginSymbolsAsync(null, CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetMarginPriceIndexAsync("ETHUSDT", CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetIsolatedMarginSymbolsAsync("ETHUSDT", 5000, CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetCrossMarginCollateralRatioAsync(null, CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetFutureHourlyInterestRateAsync(new[] { "ETH" }, false, null, CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetMarginDelistScheduleAsync(null, CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetConvertListAllPairsAsync(null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetDelistScheduleAsync(null, CancellationToken.None), true);

            // Needs more permissions
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetConvertQuantityPrecisionPerAssetAsync(null, CancellationToken.None), false);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotTrading(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetOrdersAsync("ETHUSDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetOcoOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetOpenOcoOrdersAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetC2CTradeHistoryAsync(Enums.OrderSide.Buy, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetPayTradeHistoryAsync(default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetConvertTradeHistoryAsync(DateTime.UtcNow.AddHours(-1), DateTime.UtcNow, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetOpenAlgoOrdersAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetClosedAlgoOrdersAsync(default, default, default, default, default, default, default, default), true);

            // Not available without margin account
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetOpenMarginOrdersAsync(default, default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetMarginOrdersAsync("ETHUSDT", default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetMarginUserTradesAsync(default, default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetMarginOcoOrdersAsync(default, default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetMarginOpenOcoOrdersAsync(default, default, default, default), true);

        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestUsdFuturesAccount(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetPositionModeAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetMarginChangeHistoryAsync("ETHUSDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetIncomeHistoryAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetBracketsAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetPositionAdlQuantileEstimationAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetAccountInfoV2Async(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetAccountInfoV3Async(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetBalancesAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetMultiAssetsModeAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetPositionInformationAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetTradingStatusAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetUserCommissionRateAsync("ETHUSDT", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetOrderRateLimitAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetBnbBurnStatusAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetSymbolConfigurationAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Account.GetAccountConfigurationAsync(default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestUsdFuturesExchangeData(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.PingAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetServerTimeAsync(false, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT", 5, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT", 1, null, CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", null, null, null, 10, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetFundingInfoAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetFundingRatesAsync("ETHUSDT", null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetTopLongShortAccountRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetTopLongShortPositionRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetGlobalLongShortAccountRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneHour, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetPremiumIndexKlinesAsync("ETHUSDT", Enums.KlineInterval.OneHour, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetTickerAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetTickersAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetPriceAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetPricesAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetBookPriceAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetOpenInterestHistoryAsync("ETHUSDT", Enums.PeriodInterval.FifteenMinutes, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetTakerBuySellVolumeRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetCompositeIndexInfoAsync(null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetPriceAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetContinuousContractKlinesAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetAssetIndexesAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetBasisAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.ExchangeData.GetIndexPriceConstituentsAsync("ETHUSDT", CancellationToken.None), false);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestUsdFuturesTrading(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Trading.GetOrderEditHistoryAsync("ETHUSDT", default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Trading.GetOpenOrdersAsync("ETHUSDT", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Trading.GetOrdersAsync("ETHUSDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Trading.GetForcedOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Trading.GetOpenAlgoOrdersAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Trading.GetClosedAlgoOrdersAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.UsdFuturesApi.Trading.GetPositionsAsync(default, default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestCoinFuturesAccount(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Account.GetPositionModeAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Account.GetMarginChangeHistoryAsync("ETHUSD_PERP", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Account.GetIncomeHistoryAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Account.GetBracketsAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Account.GetPositionAdlQuantileEstimationAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Account.GetAccountInfoAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Account.GetBalancesAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Account.GetPositionInformationAsync(default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Account.GetUserCommissionRateAsync("ETHUSD_PERP", default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestCoinFuturesExchangeData(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.PingAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetServerTimeAsync(false, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetExchangeInfoAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSD_PERP", 5, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSD_PERP", 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSD_PERP", 1, null, CancellationToken.None), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSD_PERP", null, null, null, 10, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetKlinesAsync("ETHUSD_PERP", Enums.KlineInterval.OneHour, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 1, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetTickersAsync(null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetPricesAsync(null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetBasisAsync("ETHUSD", Enums.ContractType.Perpetual, Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetTakerBuySellVolumeRatioAsync("ETHUSD", Enums.ContractType.Perpetual, Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetOpenInterestHistoryAsync("ETHUSD", Enums.ContractType.Perpetual, Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSD_PERP", CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetBookPricesAsync(null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSD", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetContinuousContractKlinesAsync("ETHUSD", Enums.ContractType.Perpetual, Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetKlinesAsync("ETHUSD_PERP", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetPremiumIndexKlinesAsync("ETHUSD_PERP", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetMarkPricesAsync(null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSD_PERP", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetGlobalLongShortAccountRatioAsync("ETHUSD", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetTopLongShortPositionRatioAsync("ETHUSD", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetTopLongShortAccountRatioAsync("ETHUSD", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetFundingInfoAsync(CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetFundingRatesAsync("ETHUSD_PERP", null, null, null, CancellationToken.None), false);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.ExchangeData.GetIndexPriceConstituentsAsync("ETHUSD", CancellationToken.None), false);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestCoinFuturesTrading(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Trading.GetOpenOrdersAsync("ETHUSD_PERP", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Trading.GetOrdersAsync("ETHUSD_PERP", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Trading.GetForcedOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.CoinFuturesApi.Trading.GetUserTradesAsync("ETHUSD_PERP", default, default, default, default, default, default, default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestOrderBooks(bool newDeserialization)
        {
            await TestOrderBook(new BinanceSpotSymbolOrderBook("ETHUSDT"));
            await TestOrderBook(new BinanceFuturesUsdtSymbolOrderBook("ETHUSDT"));
            await TestOrderBook(new BinanceFuturesCoinSymbolOrderBook("ETHUSD_PERP"));
        }
    }
}
