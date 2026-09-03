using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Caching;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceRestClientSpotSharedApi :
        SharedApiBase,
        IBinanceRestClientSpotApiShared,
        IBinanceRestClientSpotSharedApi
    {
        private readonly BinanceRestClientSpotApi _api;

        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceSpot";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BinanceExchange.Metadata, this);

        private static HashSet<string> _exchangeSupportedFiatCurrencies = ["AED", "ARS", "BRL", "COP", "EUR", "JPY", "KZT", "MXN", "UAH", "ZAR", "TRY", "IDR"];

        public BinanceRestClientSpotSharedApi(BinanceRestClientSpotApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  new[] { TradingMode.Spot },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetSpotSymbolsOptions,
                GetAllSpotTickersOptions,
                GetSpotTickerOptions,
                GetBookTickerOptions,
                GetRecentTradesOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotOrderTradesOptions,
                GetSpotUserTradeHistoryOptions,
                CancelSpotOrderOptions,
                GetSpotOrderByClientOrderIdOptions,
                CancelSpotOrderByClientOrderIdOptions,
                GetOrderBookOptions,
                GetTradeHistoryOptions,
                GetBalancesOptions,
                GetFeeOptions,
                PlaceSpotTriggerOrderOptions,
                GetSpotTriggerOrderOptions,
                CancelSpotTriggerOrderOptions,
                GetAssetOptions,
                GetAllAssetsOptions,
                GetDepositAddressesOptions,
                GetDepositHistoryOptions,
                GetWithdrawalHistoryOptions,
                WithdrawOptions,
                TransferOptions
                );
        }
    }
}
