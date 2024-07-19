using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
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
            await tester.ValidateAsync(client => client.SpotApi.Account.StartUserStreamAsync(), "StartUserStream", "listenKey");
            await tester.ValidateAsync(client => client.SpotApi.Account.KeepAliveUserStreamAsync("123"), "KeepAliveUserStream");
            await tester.ValidateAsync(client => client.SpotApi.Account.StopUserStreamAsync("123"), "StopUserStream");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginLevelInformationAsync(), "GetMarginLevelInformation");
            await tester.ValidateAsync(client => client.SpotApi.Account.MarginBorrowAsync("ETH", 1), "MarginBorrow");
            await tester.ValidateAsync(client => client.SpotApi.Account.MarginRepayAsync("ETH", 1), "MarginRepay");
            await tester.ValidateAsync(client => client.SpotApi.Account.CrossMarginAdjustMaxLeverageAsync(1), "CrossMarginAdjustMaxLeverage");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginTransferHistoryAsync(Enums.TransferDirection.RollOut), "GetMarginTransferHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginLoansAsync("ETH"), "GetMarginLoans");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginRepaysAsync("ETH"), "GetMarginRepays");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetInterestMarginDataAsync("ETH"), "GetInterestMarginData");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginInterestHistoryAsync("ETH"), "GetMarginInterestHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginInterestRateHistoryAsync("ETH"), "GetMarginInterestRateHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginForcedLiquidationHistoryAsync(), "GetMarginForcedLiquidationHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetIsolatedMarginTierDataAsync("ETHUSDT"), "GetIsolatedMarginTierData");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginAccountInfoAsync(), "GetMarginAccountInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginMaxBorrowAmountAsync("ETH"), "GetMarginMaxBorrowAmount");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginMaxTransferAmountAsync("ETH"), "GetMarginMaxTransferAmount", "amount");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetIsolatedMarginAccountAsync(), "GetIsolatedMarginAccount");
            await tester.ValidateAsync(client => client.SpotApi.Account.DisableIsolatedMarginAccountAsync("ETHUSDT"), "DisableIsolatedMarginAccount");
            await tester.ValidateAsync(client => client.SpotApi.Account.EnableIsolatedMarginAccountAsync("ETHUSDT"), "EnableIsolatedMarginAccount");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetEnabledIsolatedMarginAccountLimitAsync(), "GetEnabledIsolatedMarginAccountLimit");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginOrderRateLimitStatusAsync(), "GetMarginOrderRateLimitStatus");
            await tester.ValidateAsync(client => client.SpotApi.Account.StartMarginUserStreamAsync(), "StartMarginUserStream", "listenKey");
            await tester.ValidateAsync(client => client.SpotApi.Account.KeepAliveMarginUserStreamAsync("123"), "KeepAliveMarginUserStream");
            await tester.ValidateAsync(client => client.SpotApi.Account.StopMarginUserStreamAsync("123"), "StopMarginUserStream");
            await tester.ValidateAsync(client => client.SpotApi.Account.StartIsolatedMarginUserStreamAsync("ETHUSDT"), "StartIsolatedMarginUserStream", "listenKey");
            await tester.ValidateAsync(client => client.SpotApi.Account.KeepAliveIsolatedMarginUserStreamAsync("ETHUSDT", "123"), "KeepAliveIsolatedMarginUserStream");
            await tester.ValidateAsync(client => client.SpotApi.Account.CloseIsolatedMarginUserStreamAsync("ETHUSDT", "123"), "StopIsolatedMarginUserStream");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTradingStatusAsync(), "GetTradingStatus", "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetOrderRateLimitStatusAsync(), "GetOrderRateLimitStatus");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetRebateHistoryAsync(), "GetRebateHistory", "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetLeveragedTokensUserLimitAsync(), "GetLeveragedTokensUserLimit");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetPortfolioMarginAccountInfoAsync(), "GetPortfolioMarginAccountInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetPortfolioMarginBankruptcyLoanAsync(), "GetPortfolioMarginBankruptcyLoan");
            await tester.ValidateAsync(client => client.SpotApi.Account.PortfolioMarginBankruptcyLoanRepayAsync(), "PortfolioMarginBankruptcyLoanRepay");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAutoConvertStableCoinConfigAsync(), "GetAutoConvertStableCoinConfig");
            await tester.ValidateAsync(client => client.SpotApi.Account.SetAutoConvertStableCoinConfigAsync("ETH", true), "SetAutoConvertStableCoinConfig");
            await tester.ValidateAsync(client => client.SpotApi.Account.ConvertBusdAsync("1", "ETH", 1, "USDT"), "ConvertBusd");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBusdConvertHistoryAsync(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow), "GetBusdConvertHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCloudMiningHistoryAsync(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow), "GetCloudMiningHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetIsolatedMarginFeeDataAsync(), "GetIsolatedMarginFeeData");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginSmallLiabilityExchangeAssetsAsync(), "GetCrossMarginSmallLiabilityExchangeAssets");
            await tester.ValidateAsync(client => client.SpotApi.Account.CrossMarginSmallLiabilityExchangeAsync(new[] { "ETH" }), "CrossMarginSmallLiabilityExchange");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginSmallLiabilityExchangeHistoryAsync(), "GetCrossMarginSmallLiabilityExchangeHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTradeFeeAsync(), "GetTradeFee");
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

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/Spot/Trading", "https://api.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceTestOrderAsync("ETHUSDT", Enums.OrderSide.Sell, Enums.SpotOrderType.Market, 1), "PlaceTestOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Sell, Enums.SpotOrderType.Market, 1), "PlaceOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync("ETHUSDT", 123), "CancelOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllOrdersAsync("ETHUSDT"), "CancelAllOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.ReplaceOrderAsync("ETHUSDT", Enums.OrderSide.Sell, Enums.SpotOrderType.Limit, Enums.CancelReplaceMode.AllowFailure, 123, quantity: 1), "ReplaceOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync("ETHUSDT", 123), "GetOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT"), "GetOpenOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrdersAsync("ETHUSDT"), "GetOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOcoOrderListAsync("ETHUSDT", Enums.OrderSide.Buy, 1, Enums.SpotOrderType.Limit, Enums.SpotOrderType.Limit), "PlaceOcoOrderList");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOcoOrderAsync("ETHUSDT", 123), "CancelOcoOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOcoOrderAsync(123), "GetOcoOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOcoOrdersAsync(), "GetOcoOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOcoOrdersAsync(), "GetOpenOcoOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceMarginOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.SpotOrderType.Market, 1), "PlaceMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelMarginOrderAsync("ETHUSDT", 123), "CancelMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllMarginOrdersAsync("ETHUSDT"), "CancelAllMarginOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetMarginOrderAsync("ETHUSDT", 123), "GetMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenMarginOrdersAsync("ETHUSDT"), "GetOpenMarginOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetMarginOrdersAsync("ETHUSDT"), "GetMarginOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetMarginUserTradesAsync("ETHUSDT"), "GetMarginUserTrades");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceMarginOCOOrderAsync("ETHUSDT", Enums.OrderSide.Buy, 1, 1, 1), "PlaceMarginOCOOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelMarginOcoOrderAsync("ETHUSDT", false, 123), "CancelMarginOcoOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetMarginOcoOrderAsync("ETHUSDT", false, 123), "GetMarginOcoOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetMarginOcoOrdersAsync("ETHUSDT", false, 123), "GetMarginOcoOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetMarginOpenOcoOrdersAsync("ETHUSDT", false, 123), "GetMarginOpenOcoOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.SubscribeLeveragedTokenAsync("ETHUSDT", 1), "SubscribeLeveragedToken");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetLeveragedTokensSubscriptionRecordsAsync("ETHUSDT", 1), "GetLeveragedTokensSubscriptionRecords");
            await tester.ValidateAsync(client => client.SpotApi.Trading.RedeemLeveragedTokenAsync("ETHUSDT", 1), "RedeemLeveragedToken");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetLeveragedTokensRedemptionRecordsAsync("ETHUSDT", 1), "GetLeveragedTokensRedemptionRecords");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetC2CTradeHistoryAsync(Enums.OrderSide.Buy), "GetC2CTradeHistory", "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetPayTradeHistoryAsync(), "GetPayTradeHistory", "data", new List<string>{ "walletTypes", "walletAssetCost" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetPayTradeHistoryAsync(), "GetPayTradeHistory2", "data", new List<string>{ "walletTypes", "walletAssetCost" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.ConvertQuoteRequestAsync("USDT", "ETH", 1), "ConvertQuoteRequest");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetConvertOrderStatusAsync("123"), "GetConvertOrderStatus");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetConvertTradeHistoryAsync(DateTime.UtcNow, DateTime.UtcNow), "GetConvertTradeHistory");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetPreventedTradesAsync("ETHUSDT"), "GetPreventedTrades");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceTimeWeightedAveragePriceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, 1, 1), "PlaceTimeWeightedAveragePriceOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAlgoOrderAsync(123), "CancelAlgoOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenAlgoOrdersAsync(), "GetOpenAlgoOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetClosedAlgoOrdersAsync(), "GetClosedAlgoOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetAlgoSubOrdersAsync(123), "GetAlgoSubOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOtocoOrderListAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.SpotOrderType.Limit, 1, 1, 1, Enums.OrderSide.Sell, Enums.SpotOrderType.LimitMaker, Enums.SpotOrderType.LimitMaker), "PlaceOtocoOrderList");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOtoOrderListAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.SpotOrderType.Limit, 1, 1, 1, Enums.OrderSide.Sell, Enums.SpotOrderType.LimitMaker), "PlaceOtoOrderList");
        }

        [Test]
        public async Task ValidateUsdFuturesAccountCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.RateLimiterEnabled = false;
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/UsdFutures/Account", "https://fapi.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.ModifyPositionModeAsync(true), "ModifyPositionMode");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.ChangeInitialLeverageAsync("ETHUSDT", 1), "ChangeInitialLeverage");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.ChangeMarginTypeAsync("ETHUSDT", Enums.FuturesMarginType.Isolated), "ChangeMarginType");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.ModifyPositionMarginAsync("ETHUSDT", 1, Enums.FuturesMarginChangeDirectionType.Add), "ModifyPositionMargin");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetMarginChangeHistoryAsync("ETHUSDT"), "GetMarginChangeHistory");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetIncomeHistoryAsync("ETHUSDT"), "GetIncomeHistory");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetBracketsAsync("ETHUSDT"), "GetBrackets");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetPositionAdlQuantileEstimationAsync(), "GetPositionAdlQuantileEstimation");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.StartUserStreamAsync(), "StartUserStream", "listenKey");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.KeepAliveUserStreamAsync("123"), "KeepAliveUserStream");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.StopUserStreamAsync("123"), "StopUserStream");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetAccountInfoAsync(), "GetAccountInfo", ignoreProperties: new List<string> { "bidNotional", "askNotional" });
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetBalancesAsync(), "GetBalances");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetMultiAssetsModeAsync(), "GetMultiAssetsMode");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.SetMultiAssetsModeAsync(true), "SetMultiAssetsMode");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetPositionInformationAsync(), "GetPositionInformation", ignoreProperties: new List<string> { "unRealizedProfit" });
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetTradingStatusAsync(), "GetTradingStatus");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetUserCommissionRateAsync("ETHUSDT"), "GetUserCommissionRate");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetDownloadIdForTransactionHistoryAsync(DateTime.UtcNow, DateTime.UtcNow), "GetDownloadIdForTransactionHistory");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetDownloadLinkForTransactionHistoryAsync("123"), "GetDownloadLinkForTransactionHistory", ignoreProperties: new List<string> { "notified" });
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetDownloadIdForOrderHistoryAsync(DateTime.UtcNow, DateTime.UtcNow), "GetDownloadIdForOrderHistory");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetDownloadLinkForOrderHistoryAsync("123"), "GetDownloadLinkForOrderHistory", ignoreProperties: new List<string> { "notified" });
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetDownloadIdForTradeHistoryAsync(DateTime.UtcNow, DateTime.UtcNow), "GetDownloadIdForTradeHistory");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetDownloadLinkForTradeHistoryAsync("123"), "GetDownloadLinkForTradeHistory", ignoreProperties: new List<string> { "notified" });
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetOrderRateLimitAsync(), "GetOrderRateLimit");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetBnbBurnStatusAsync(), "GetBnbBurnStatus");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.SetBnbBurnStatusAsync(true), "SetBnbBurnStatus");
        }

        [Test]
        public async Task ValidateUsdFuturesExchangeDataCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.RateLimiterEnabled = false;
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/UsdFutures/ExchangeData", "https://fapi.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetServerTimeAsync(), "GetServerTime", "serverTime");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT"), "GetAggregatedTradeHistory");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetFundingInfoAsync(), "GetFundingInfo", ignoreProperties: new List<string> { "disclaimer" });
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetFundingRatesAsync("ETHUSDT"), "GetFundingRates");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetTopLongShortAccountRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay), "GetTopLongShortAccountRatio");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetTopLongShortPositionRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay), "GetTopLongShortPositionRatio");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetGlobalLongShortAccountRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay), "GetGlobalLongShortAccountRatio");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneSecond), "GetMarkPriceKlines");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetMarkPriceAsync("ETHUSDT"), "GetMarkPrice");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetBookPriceAsync("ETHUSDT"), "GetBookPrice");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSDT"), "GetOpenInterest");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetOpenInterestHistoryAsync("ETHUSDT", Enums.PeriodInterval.OneDay), "GetOpenInterestHistory");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetTakerBuySellVolumeRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay), "GetTakerBuySellVolumeRatio");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetCompositeIndexInfoAsync("ETHUSDT"), "GetCompositeIndexInfo");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetPriceAsync("ETHUSDT"), "GetPrice");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetContinuousContractKlinesAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.KlineInterval.OneSecond), "GetContinuousContractKlines");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneSecond), "GetIndexPriceKlines");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetAssetIndexesAsync(), "GetAssetIndexes");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetBasisAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.PeriodInterval.ThirtyMinutes), "GetBasis");
        }

        [Test]
        public async Task ValidateUsdFuturesTradingCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.RateLimiterEnabled = false;
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/UsdFutures/Trading", "https://fapi.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.FuturesOrderType.Market, 1), "PlaceOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.PlaceMultipleOrdersAsync(new[] { new BinanceFuturesBatchOrder { } }), "PlaceMultipleOrders", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetOrderAsync("ETHUSDT", 123), "GetOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetOrderEditHistoryAsync("ETHUSDT", 123), "GetOrderEditHistory");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.CancelOrderAsync("ETHUSDT", 123), "CancelOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.CancelAllOrdersAsync("ETHUSDT"), "CancelAllOrders");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.EditOrderAsync("ETHUSDT", Enums.OrderSide.Buy, 1, 1, 123), "EditOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.EditMultipleOrdersAsync(new[] { new BinanceFuturesBatchEditOrder() }), "EditMultipleOrders", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.CancelAllOrdersAfterTimeoutAsync("ETHUSDT", TimeSpan.Zero), "CancelAllOrdersAfterTimeout");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.CancelMultipleOrdersAsync("ETHUSDT", new List<long> { 123 }), "CancelMultipleOrders", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetOpenOrderAsync("ETHUSDT", 123), "GetOpenOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetOrdersAsync("ETHUSDT"), "GetOrders");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetForcedOrdersAsync("ETHUSDT"), "GetForcedOrders");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades");
           
        }

        [Test]
        public async Task ValidateUsdFuturesTradingAlgoCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.RateLimiterEnabled = false;
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/UsdFutures/Trading", "https://api.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.PlaceVolumeParticipationOrderAsync("ETHUSDT", Enums.OrderSide.Buy, 1, Enums.OrderUrgency.Medium), "PlaceVolumeParticipationOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.PlaceTimeWeightedAveragePriceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, 1, 1), "PlaceTimeWeightedAveragePriceOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.CancelAlgoOrderAsync(123), "CancelAlgoOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetOpenAlgoOrdersAsync(), "GetOpenAlgoOrders");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetClosedAlgoOrdersAsync(), "GetClosedAlgoOrders");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetAlgoSubOrdersAsync(123), "GetAlgoSubOrders");

        }

        [Test]
        public async Task ValidateCoinFuturesAccountCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.RateLimiterEnabled = false;
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/CoinFutures/Account", "https://dapi.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.ModifyPositionModeAsync(true), "ModifyPositionMode");
            //await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetPositionModeAsync(), "GetPositionMode");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.ChangeInitialLeverageAsync("ETHUSDT", 1), "ChangeInitialLeverage");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.ChangeMarginTypeAsync("ETHUSDT", Enums.FuturesMarginType.Isolated), "ChangeMarginType");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.ModifyPositionMarginAsync("ETHUSDT", 1, Enums.FuturesMarginChangeDirectionType.Add), "ModifyPositionMargin");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetMarginChangeHistoryAsync("ETHUSDT"), "GetMarginChangeHistory");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetIncomeHistoryAsync("ETHUSDT"), "GetIncomeHistory");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetBracketsAsync("ETHUSDT"), "GetBrackets", ignoreProperties: new List<string> { "qtyCap", "qtylFloor" });
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetPositionAdlQuantileEstimationAsync("ETHUSDT"), "GetPositionAdlQuantileEstimation");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.StartUserStreamAsync(), "StartUserStream", "listenKey");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.KeepAliveUserStreamAsync("123"), "KeepAliveUserStream");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.StopUserStreamAsync("123"), "StopUserStream");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetAccountInfoAsync(), "GetAccountInfo");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetBalancesAsync(), "GetBalances");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetPositionInformationAsync(), "GetPositionInformation", ignoreProperties: new List<string> { "unRealizedProfit" });
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetUserCommissionRateAsync("ETHUSDT"), "GetUserCommissionRate");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetDownloadIdForTransactionHistoryAsync(DateTime.UtcNow, DateTime.UtcNow), "GetDownloadIdForTransactionHistory");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Account.GetDownloadLinkForTransactionHistoryAsync("123"), "GetDownloadLinkForTransactionHistory", ignoreProperties: new List<string> { "notified" });
        }

        [Test]
        public async Task ValidateCoinFuturesExchangeDataCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.RateLimiterEnabled = false;
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/CoinFutures/ExchangeData", "https://dapi.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetServerTimeAsync(), "GetServerTime", "serverTime");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT"), "GetAggregatedTradeHistory");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetFundingRatesAsync("ETHUSDT"), "GetFundingRates");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetFundingInfoAsync(), "GetFundingInfo", ignoreProperties: new List<string> { "disclaimer" });
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetTopLongShortAccountRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay), "GetTopLongShortAccountRatio");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetTopLongShortPositionRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay), "GetTopLongShortPositionRatio", ignoreProperties: new List<string> { "longPosition", "shortPosition" });
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetGlobalLongShortAccountRatioAsync("ETHUSDT", Enums.PeriodInterval.OneDay), "GetGlobalLongShortAccountRatio");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneSecond), "GetMarkPriceKlines");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetMarkPricesAsync("ETHUSDT"), "GetMarkPrices");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneSecond), "GetKlines");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetContinuousContractKlinesAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.KlineInterval.OneSecond), "GetContinuousContractKlines");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneSecond), "GetIndexPriceKlines");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetTickersAsync("ETHUSDT"), "GetTickers");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetBookPricesAsync("ETHUSDT"), "GetBookPrices");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSDT"), "GetOpenInterest");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetOpenInterestHistoryAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.PeriodInterval.ThirtyMinutes), "GetOpenInterestHistory");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetTakerBuySellVolumeRatioAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.PeriodInterval.ThirtyMinutes), "GetTakerBuySellVolumeRatio");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetBasisAsync("ETHUSDT", Enums.ContractType.Perpetual, Enums.PeriodInterval.ThirtyMinutes), "GetBasis");
            await tester.ValidateAsync(client => client.CoinFuturesApi.ExchangeData.GetPricesAsync("ETHUSDT"), "GetPrices");
        }

        [Test]
        public async Task ValidateCoinFuturesTradingCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.RateLimiterEnabled = false;
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/CoinFutures/Trading", "https://dapi.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.FuturesOrderType.Market, 1, 1), "PlaceOrder");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.PlaceMultipleOrdersAsync(new[] { new BinanceFuturesBatchOrder() }), "PlaceMultipleOrders", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.GetOrderAsync("ETHUSDT", 123), "GetOrder");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.CancelOrderAsync("ETHUSDT", 123), "CancelOrder");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.CancelAllOrdersAsync("ETHUSDT"), "CancelAllOrders");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.CancelAllOrdersAfterTimeoutAsync("ETHUSDT", TimeSpan.Zero), "CancelAllOrdersAfterTimeout");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.CancelMultipleOrdersAsync("ETHUSDT", new List<long> { 123L }), "CancelMultipleOrders", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.GetOpenOrderAsync("ETHUSDT", 123), "GetOpenOrder");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.GetOpenOrdersAsync("ETHUSDT"), "GetOpenOrders");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.GetOrdersAsync("ETHUSDT"), "GetOrders");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.GetForcedOrdersAsync("ETHUSDT"), "GetForcedOrders");
            await tester.ValidateAsync(client => client.CoinFuturesApi.Trading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades");
        }

        [Test]
        public async Task ValidateGeneralBrokerageCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.RateLimiterEnabled = false;
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/General/Brokerage", "https://api.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.GeneralApi.Brokerage.CreateSubAccountAsync(), "CreateSubAccount");
            await tester.ValidateAsync(client => client.GeneralApi.Brokerage.GetSubAccountsAsync(), "GetSubAccounts");
            await tester.ValidateAsync(client => client.GeneralApi.Brokerage.CreateApiKeyForSubAccountAsync("123", true), "CreateApiKeyForSubAccount");
            await tester.ValidateAsync(client => client.GeneralApi.Brokerage.DeleteSubAccountApiKeyAsync("123", "123"), "DeleteSubAccountApiKey");
            await tester.ValidateAsync(client => client.GeneralApi.Brokerage.GetSubAccountApiKeyAsync("123", "123"), "GetSubAccountApiKey");
            await tester.ValidateAsync(client => client.GeneralApi.Brokerage.ChangeSubAccountApiKeyPermissionAsync("123", "123", true, true, true), "ChangeSubAccountApiKeyPermission");
            // TODO add other endpoints
        }

        [Test]
        public async Task ValidateGeneralCryptoLoansCalls()
        {
            var client = new BinanceRestClient(opts =>
            {
                opts.RateLimiterEnabled = false;
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BinanceRestClient>(client, "Endpoints/General/CryptoLoans", "https://api.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.GetIncomeHistoryAsync("ETH"), "GetIncomeHistory");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.BorrowAsync("ETH", "USDT", 1), "Borrow");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.GetBorrowHistoryAsync(), "GetBorrowHistory");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.GetOpenBorrowOrdersAsync(), "GetOpenBorrowOrders");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.RepayAsync(123, 1), "Repay");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.GetRepayHistoryAsync(), "GetRepayHistory");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.AdjustLTVAsync(123, 1, true), "AdjustLTV");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.GetLtvAdjustHistoryAsync(123), "GetLtvAdjustHistory");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.GetLoanableAssetsAsync(), "GetLoanableAssets");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.GetCollateralAssetsAsync(), "GetCollateralAssets");
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.GetCollateralRepayRateAsync("ETH", "USDT", 1), "GetCollateralRepayRate", ignoreProperties: new List<string> { "loanlCoin" });
            await tester.ValidateAsync(client => client.GeneralApi.CryptoLoans.CustomizeMarginCallAsync(123), "CustomizeMarginCall");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl?.Contains("signature") == true || result.RequestBody?.Contains("signature=") == true;
        }
    }
}
