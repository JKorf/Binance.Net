using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/Spot/Account", "https://api.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountInfoAsync(), "GetAccountInfo", ignoreProperties: new List<string> { "commissionRates" });
            await tester.ValidateAsync(client => client.SpotApi.Account.GetFiatPaymentHistoryAsync(Enums.OrderSide.Buy), "GetFiatPaymentHistory", "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetFiatDepositWithdrawHistoryAsync(Enums.TransactionType.Withdrawal), "GetFiatDepositWithdrawHistory", "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("ETH", "123", 1), "Withdraw");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalHistoryAsync(), "GetWithdrawalHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalAddressesAsync(), "GetWithdrawalAddresses");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressAsync("ETH"), "GetDepositAddress");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositHistoryAsync(), "GetDepositHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDailySpotAccountSnapshotAsync(), "GetDailySpotAccountSnapshot", "snapshotVos");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDailyMarginAccountSnapshotAsync(), "GetDailyMarginAccountSnapshot", "snapshotVos");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDailyFutureAccountSnapshotAsync(), "GetDailyFutureAccountSnapshot", "snapshotVos");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountStatusAsync(), "GetAccountStatus");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetFundingWalletAsync(), "GetFundingWallet");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAPIKeyPermissionsAsync(), "GetAPIKeyPermissions");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUserAssetsAsync(), "GetUserAssets");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBalancesAsync(), "GetBalances");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWalletBalancesAsync(), "GetWalletBalances");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAssetDividendRecordsAsync(), "GetAssetDividendRecords");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDustLogAsync(), "GetDustLog");
            await tester.ValidateAsync(client => client.SpotApi.Account.DustTransferAsync(new[] {"ETH"}), "DustTransfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAssetsForDustTransferAsync(), "GetAssetsForDustTransfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBnbBurnStatusAsync(), "GetBnbBurnStatus");
            await tester.ValidateAsync(client => client.SpotApi.Account.SetBnbBurnStatusAsync(true), "SetBnbBurnStatus");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferAsync(Enums.UniversalTransferType.CoinFuturesToFunding, "ETH", 1), "Transfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTransfersAsync(Enums.UniversalTransferType.CoinFuturesToFunding), "GetTransfers");

        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetServerTimeAsync(), "GetServerTime", "serverTime");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSystemStatusAsync(), "GetSystemStatus");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetDetailsAsync(), "GetAssetDetails");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT"), "GetAggregatedTradeHistory");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneSecond), "GetKlines");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetUiKlinesAsync("ETHUSDT", Enums.KlineInterval.OneSecond), "GetUiKlines");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetCurrentAvgPriceAsync("ETHUSDT"), "GetAvgPrice");
            await tester.ValidateAsync<IBinanceTick, Binance24HPrice>(client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradingDayTickerAsync("ETHUSDT"), "GetTradingDayTicker");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetRollingWindowTickerAsync("ETHUSDT"), "GetRollingWindowTicker");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetPriceAsync("ETHUSDT"), "GetPrice");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetBookPriceAsync("ETHUSDT"), "GetBookPrice");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetMarginAssetsAsync("ETHUSDT"), "GetMarginAssets");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetMarginSymbolsAsync("ETHUSDT"), "GetMarginSymbols");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetMarginPriceIndexAsync("ETHUSDT"), "GetMarginPriceIndex");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetIsolatedMarginSymbolsAsync("ETHUSDT"), "GetIsolatedMarginSymbols");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetLeveragedTokenInfoAsync(), "GetLeveragedTokenInfo");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetCrossMarginCollateralRatioAsync(), "GetCrossMarginCollateralRatio");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetFutureHourlyInterestRateAsync(new[] { "ETHUSDT" }, false), "GetFutureHourlyInterestRate");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetMarginDelistScheduleAsync(), "GetMarginDelistSchedule");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetConvertListAllPairsAsync(), "GetConvertListAllPairs");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetConvertQuantityPrecisionPerAssetAsync(), "GetConvertQuantityPrecisionPerAsset");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetDelistScheduleAsync(), "GetDelistSchedule");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl?.Contains("signature") == true || result.RequestBody?.Contains("signature=") == true;
        }
    }
}
