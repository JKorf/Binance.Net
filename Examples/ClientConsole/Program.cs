using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;

namespace BinanceAPI.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("APIKEY", "APISECRET"),
                LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }
            });
            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials("APIKEY", "APISECRET"),
                LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }
            });

            using (var client = new BinanceClient())
            {
                // Spot.Market | Spot market info endpoints
                client.Spot.Market.GetBookPrice("BTCUSDT");
                // Spot.Order | Spot order info endpoints
                client.Spot.Order.GetAllOrders("BTCUSDT");
                // Spot.System | Spot system endpoints
                client.Spot.System.GetExchangeInfo();
                // Spot.UserStream | Spot user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.Spot.UserStream.StartUserStream();
                // Spot.Futures | Transfer to/from spot from/to the futures account + cross-collateral endpoints
                client.Spot.Futures.TransferFuturesAccount("ASSET", 1, FuturesTransferType.FromSpotToUsdtFutures);

                // FuturesCoin | Coin-M general endpoints
                client.FuturesCoin.GetPositionInformation();
                // FuturesCoin.Market | Coin-M futures market endpoints
                client.FuturesCoin.Market.GetBookPrices("BTCUSD");
                // FuturesCoin.Order | Coin-M futures order endpoints
                client.FuturesCoin.Order.GetMyTrades();
                // FuturesCoin.Account | Coin-M account info
                client.FuturesCoin.Account.GetAccountInfo();
                // FuturesCoin.System | Coin-M system endpoints
                client.FuturesCoin.System.GetExchangeInfo();
                // FuturesCoin.UserStream | Coin-M user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.FuturesCoin.UserStream.StartUserStream();

                // FuturesUsdt | USDT-M general endpoints
                client.FuturesUsdt.GetPositionInformation();
                // FuturesUsdt.Market | USDT-M futures market endpoints
                client.FuturesUsdt.Market.GetBookPrices("BTCUSDT");
                // FuturesUsdt.Order | USDT-M futures order endpoints
                client.FuturesUsdt.Order.GetMyTrades("BTCUSDT");
                // FuturesUsdt.Account | USDT-M account info
                client.FuturesUsdt.Account.GetAccountInfo();
                // FuturesUsdt.System | USDT-M system endpoints
                client.FuturesUsdt.System.GetExchangeInfo();
                // FuturesUsdt.UserStream | USDT-M user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.FuturesUsdt.UserStream.StartUserStream();

                // General | General/account endpoints
                client.General.GetAccountInfo();

                // Lending | Lending endpoints
                client.Lending.GetFlexibleProductList();

                // Margin | Margin general/account info
                client.Margin.GetMarginAccountInfo();
                // Margin.Market | Margin market endpoints
                client.Margin.Market.GetMarginPairs();
                // Margin.Order | Margin order endpoints
                client.Margin.Order.GetAllMarginAccountOrders("BTCUSDT");
                // Margin.UserStream | Margin user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.Margin.UserStream.StartUserStream();
                // Margin.IsolatedUserStream | Isolated margin user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.Margin.IsolatedUserStream.StartIsolatedMarginUserStream("BTCUSDT");

                // Mining | Mining endpoints
                client.Mining.GetMiningCoinList();

                // SubAccount | Sub account management
                client.SubAccount.TransferSubAccount("fromEmail", "toEmail", "asset", 1);

                // Brokerage | Brokerage management
                client.Brokerage.CreateSubAccountAsync();

                // WithdrawDeposit | Withdraw and deposit endpoints
                client.WithdrawDeposit.GetWithdrawalHistory();
            }

            var socketClient = new BinanceSocketClient();
            // Spot | Spot market and user subscription methods
            socketClient.Spot.SubscribeToAllBookTickerUpdates(data =>
            {
                // Handle data
            });

            // FuturesCoin | Coin-M futures market and user subscription methods
            socketClient.FuturesCoin.SubscribeToAllBookTickerUpdates(data =>
            {
                // Handle data
            });

            // FuturesUsdt | USDT-M futures market and user subscription methods
            socketClient.FuturesUsdt.SubscribeToAllBookTickerUpdates(data =>
            {
                // Handle data
            });
            
            // Unsubscribe
            socketClient.UnsubscribeAll();

            Console.ReadLine();
        }

        private void SubscribeToSpotUserStream()
        {
            var socketClient = new BinanceSocketClient();
            // Subscribe to a user stream
            var restClient = new BinanceClient();
            var listenKeyResult = restClient.Spot.UserStream.StartUserStream();
            if (!listenKeyResult.Success)
                throw new Exception("Failed to start user stream: " + listenKeyResult.Error);

            var successAccount = socketClient.Spot.SubscribeToUserDataUpdates(listenKeyResult.Data,
                data =>
                {
                    // Handle account info data
                    // Deprecated, will be removed in the future
                },
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
