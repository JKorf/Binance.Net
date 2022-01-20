using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using Microsoft.Extensions.Logging;

namespace BinanceAPI.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("APIKEY", "APISECRET"),
                LogLevel = LogLevel.Debug,
                LogWriters = new List<ILogger> { new ConsoleLogger() }
            });
            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials("APIKEY", "APISECRET"),
                LogLevel = LogLevel.Debug,
                LogWriters = new List<ILogger> { new ConsoleLogger() }
            });

            using (var client = new BinanceClient())
            {
                // Spot.Market | Spot market info endpoints
                client.Spot.Market.GetBookPriceAsync("BTCUSDT");
                // Spot.Order | Spot order info endpoints
                client.Spot.Order.GetOrdersAsync("BTCUSDT");
                // Spot.System | Spot system endpoints
                client.Spot.System.GetExchangeInfoAsync();
                // Spot.UserStream | Spot user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.Spot.UserStream.StartUserStreamAsync();
                // Spot.Futures | Transfer to/from spot from/to the futures account + cross-collateral endpoints
                client.Spot.Futures.TransferFuturesAccountAsync("ASSET", 1, FuturesTransferType.FromSpotToUsdtFutures);

                // FuturesCoin | Coin-M general endpoints
                client.FuturesCoin.GetPositionInformationAsync();
                // FuturesCoin.Market | Coin-M futures market endpoints
                client.FuturesCoin.Market.GetBookPricesAsync("BTCUSD");
                // FuturesCoin.Order | Coin-M futures order endpoints
                client.FuturesCoin.Order.GetUserTradesAsync();
                // FuturesCoin.Account | Coin-M account info
                client.FuturesCoin.Account.GetAccountInfoAsync();
                // FuturesCoin.System | Coin-M system endpoints
                client.FuturesCoin.System.GetExchangeInfoAsync();
                // FuturesCoin.UserStream | Coin-M user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.FuturesCoin.UserStream.StartUserStreamAsync();

                // FuturesUsdt | USDT-M general endpoints
                client.FuturesUsdt.GetPositionInformationAsync();
                // FuturesUsdt.Market | USDT-M futures market endpoints
                client.FuturesUsdt.Market.GetBookPricesAsync("BTCUSDT");
                // FuturesUsdt.Order | USDT-M futures order endpoints
                client.FuturesUsdt.Order.GetUserTradesAsync("BTCUSDT");
                // FuturesUsdt.Account | USDT-M account info
                client.FuturesUsdt.Account.GetAccountInfoAsync();
                // FuturesUsdt.System | USDT-M system endpoints
                client.FuturesUsdt.System.GetExchangeInfoAsync();
                // FuturesUsdt.UserStream | USDT-M user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.FuturesUsdt.UserStream.StartUserStreamAsync();

                // General | General/account endpoints
                client.General.GetAccountInfoAsync();

                // Lending | Lending endpoints
                client.Lending.GetFlexibleProductListAsync();

                // Margin | Margin general/account info
                client.Margin.GetMarginAccountInfoAsync();
                // Margin.Market | Margin market endpoints
                client.Margin.Market.GetMarginPairsAsync();
                // Margin.Order | Margin order endpoints
                client.Margin.Order.GetMarginAccountOrdersAsync("BTCUSDT");
                // Margin.UserStream | Margin user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.Margin.UserStream.StartUserStreamAsync();
                // Margin.IsolatedUserStream | Isolated margin user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.Margin.IsolatedUserStream.StartIsolatedMarginUserStreamAsync("BTCUSDT");

                // Mining | Mining endpoints
                client.Mining.GetMiningCoinListAsync();

                // SubAccount | Sub account management
                client.SubAccount.TransferSubAccountAsync("fromEmail", "toEmail", "asset", 1);

                // Brokerage | Brokerage management
                client.Brokerage.CreateSubAccountAsync();

                // WithdrawDeposit | Withdraw and deposit endpoints
                client.WithdrawDeposit.GetWithdrawalHistoryAsync();
            }

            var socketClient = new BinanceSocketClient();
            // Spot | Spot market and user subscription methods
            socketClient.Spot.SubscribeToAllBookTickerUpdatesAsync(data =>
            {
                // Handle data
            });

            // FuturesCoin | Coin-M futures market and user subscription methods
            socketClient.FuturesCoin.SubscribeToAllBookTickerUpdatesAsync(data =>
            {
                // Handle data
            });

            // FuturesUsdt | USDT-M futures market and user subscription methods
            socketClient.FuturesUsdt.SubscribeToAllBookTickerUpdatesAsync(data =>
            {
                // Handle data
            });
            
            // Unsubscribe
            socketClient.UnsubscribeAllAsync();

            Console.ReadLine();
        }

        private async Task SubscribeToSpotUserStream()
        {
            var socketClient = new BinanceSocketClient();
            // Subscribe to a user stream
            var restClient = new BinanceClient();
            var listenKeyResult = await restClient.Spot.UserStream.StartUserStreamAsync();
            if (!listenKeyResult.Success)
                throw new Exception("Failed to start user stream: " + listenKeyResult.Error);

            var successAccount = socketClient.Spot.SubscribeToUserDataUpdatesAsync(listenKeyResult.Data,                
                data =>
                {
                    // Handle order update info data
                },
                null, // Handler for OCO updates
                null, // Handler for position updates
                null); // Handler for account balance updates (withdrawals/deposits)
            Console.ReadLine();
        }
    }
}
