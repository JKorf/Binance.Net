using Binance.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [NonParallelizable]
    internal class BinanceRestIntegrationTests : RestIntergrationTest<BinanceRestClient>
    {
        public override bool Run { get; set; } = false;

        public override BinanceRestClient GetClient()
        {
            var fact = new LoggerFactory();
            fact.AddProvider(new TraceLoggerProvider());

            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            return new BinanceRestClient(null, fact, opts =>
            {
                opts.OutputOriginalData = true;
                opts.ApiCredentials = key != null && sec != null ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null;
            });
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.PingAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSystemStatusAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetDetailsAsync(5000, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetProductsAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", 1, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", 1, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT", 1, 1, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", null, null, null, 10, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneHour, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 1, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetUiKlinesAsync("ETHUSDT", Enums.KlineInterval.OneHour, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 1, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetCurrentAvgPriceAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickersAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTradingDayTickerAsync("ETHUSDT", "0", CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetRollingWindowTickerAsync("ETHUSDT", TimeSpan.FromHours(1), CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetPriceAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetPricesAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetBookPriceAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetMarginAssetsAsync(null, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetMarginSymbolsAsync(null, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetMarginPriceIndexAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetIsolatedMarginSymbolsAsync("ETHUSDT", 5000, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetLeveragedTokenInfoAsync(5000, CancellationToken.None));
            
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetLeveragedTokensHistoricalKlinesAsync("ETHUP", Enums.KlineInterval.OneHour, null, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetCrossMarginCollateralRatioAsync(null, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetFutureHourlyInterestRateAsync(new[] { "ETH" }, false, null, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetMarginDelistScheduleAsync(null, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetConvertListAllPairsAsync(null, null, CancellationToken.None));
            //await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetConvertQuantityPrecisionPerAssetAsync(null, CancellationToken.None));
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetDelistScheduleAsync(null, CancellationToken.None));
        }

        [Test]
        public async Task TestUsdFuturesExchangeData()
        {
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.PingAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetServerTimeAsync(false, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT", 5, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", 1, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT", 1, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", null, null, null, 10, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetFundingInfoAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetFundingRatesAsync("ETHUSDT", null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetTopLongShortAccountRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetTopLongShortPositionRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetGlobalLongShortAccountRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneHour, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 1, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetTickerAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetTickersAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetPriceAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetPricesAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetBookPriceAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetOpenInterestHistoryAsync("ETHUSDT", Enums.PeriodInterval.FifteenMinutes, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetTakerBuySellVolumeRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetCompositeIndexInfoAsync(null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetPriceAsync("ETHUSDT", CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetContinuousContractKlinesAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetAssetIndexesAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetBasisAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
        }

        [Test]
        public async Task TestCoinFuturesExchangeData()
        {
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.PingAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetServerTimeAsync(false, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetExchangeInfoAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSD_PERP", 5, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSD_PERP", 1, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSD_PERP", 1, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSD_PERP", null, null, null, 10, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetKlinesAsync("ETHUSD_PERP", Enums.KlineInterval.OneHour, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 1, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetTickersAsync(null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetPricesAsync(null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetBasisAsync("ETHUSD", Enums.ContractType.Perpetual, Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetTakerBuySellVolumeRatioAsync("ETHUSD", Enums.ContractType.Perpetual, Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetOpenInterestHistoryAsync("ETHUSD", Enums.ContractType.Perpetual, Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSD_PERP", CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetBookPricesAsync(null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSD", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetContinuousContractKlinesAsync("ETHUSD", Enums.ContractType.Perpetual, Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetKlinesAsync("ETHUSD_PERP", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetMarkPricesAsync(null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSD_PERP", Enums.KlineInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetGlobalLongShortAccountRatioAsync("ETHUSD", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetTopLongShortPositionRatioAsync("ETHUSD", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetTopLongShortAccountRatioAsync("ETHUSD", Enums.PeriodInterval.OneDay, null, null, null, CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetFundingInfoAsync(CancellationToken.None));
            await RunAndCheckResult(client => client.CoinFuturesApi.ExchangeData.GetFundingRatesAsync("ETHUSD_PERP", null, null, null, CancellationToken.None));
        }
    }
}
