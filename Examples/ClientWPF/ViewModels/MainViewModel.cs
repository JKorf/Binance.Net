using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Binance.Net.Objects;
using Binance.Net.ClientWPF.MVVM;
using Binance.Net.ClientWPF.ViewModels;
using Binance.Net.ClientWPF.UserControls;
using Binance.Net.ClientWPF.MessageBox;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.UserStream;
using CryptoExchange.Net.Authentication;

namespace Binance.Net.ClientWPF
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<BinanceSymbolViewModel> allPrices;
        public ObservableCollection<BinanceSymbolViewModel> AllPrices
        {
            get { return allPrices; }
            set
            {
                allPrices = value;
                RaisePropertyChangedEvent("AllPrices");
            }
        }

        private BinanceSymbolViewModel selectedSymbol;
        public BinanceSymbolViewModel SelectedSymbol
        {
            get { return selectedSymbol; }
            set
            {
                selectedSymbol = value;
                RaisePropertyChangedEvent("SymbolIsSelected");
                RaisePropertyChangedEvent("SelectedSymbol");
                ChangeSymbol();
            }
        }
        public bool SymbolIsSelected
        {
            get { return SelectedSymbol != null; }
        }

        private ObservableCollection<AssetViewModel> assets;
        public ObservableCollection<AssetViewModel> Assets
        {
            get { return assets; }
            set
            {
                assets = value;
                RaisePropertyChangedEvent("Assets");
            }
        }

        private bool settingsOpen = true;
        public bool SettingsOpen
        {
            get { return settingsOpen; }
            set
            {
                settingsOpen = value;
                RaisePropertyChangedEvent("SettingsOpen");
            }
        }

        private string apiKey;
        public string ApiKey
        {
            get { return apiKey; }
            set
            {
                apiKey = value;
                RaisePropertyChangedEvent("ApiKey");
                //if (value != null && apiSecret != null)
                //    BinanceDefaults.SetDefaultApiCredentials();
            }
        }

        private string apiSecret;
        public string ApiSecret
        {
            get { return apiSecret; }
            set
            {
                apiSecret = value;
                RaisePropertyChangedEvent("ApiSecret");

                //if (value != null && apiKey != null)
                //    BinanceDefaults.SetDefaultApiCredentials(apiKey, value);
            }
        }

        public ICommand BuyCommand { get; set; }
        public ICommand SellCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand SettingsCommand { get; set; }
        public ICommand CloseSettingsCommand { get; set; }

        private IMessageBoxService messageBoxService;
        private SettingsWindow settings;
        private object orderLock;
        private BinanceSocketClient socketClient;

        public MainViewModel()
        {
            // Should be done with DI
            messageBoxService = new MessageBoxService();
            orderLock = new object();

            BuyCommand = new DelegateCommand(Buy);
            SellCommand = new DelegateCommand(Sell);
            CancelCommand = new DelegateCommand(Cancel);
            SettingsCommand = new DelegateCommand(Settings);
            CloseSettingsCommand = new DelegateCommand(CloseSettings);

            Task.Run(() => GetAllSymbols());
        }

        public void Cancel(object o)
        {
            var order = (OrderViewModel)o;
            using (var client = new BinanceClient())
            {
                var result = client.Spot.Order.CancelOrder(SelectedSymbol.Symbol, order.Id);
                if (result.Success)
                    messageBoxService.ShowMessage("Order canceled!", "Sucess", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                else
                    messageBoxService.ShowMessage($"Order canceling failed: {result.Error.Message}", "Failed", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public void Buy(object o)
        {
            using (var client = new BinanceClient())
            {
                var result = client.Spot.Order.PlaceOrder(SelectedSymbol.Symbol, OrderSide.Buy, OrderType.Limit, SelectedSymbol.TradeAmount, price: SelectedSymbol.TradePrice, timeInForce: TimeInForce.GoodTillCancel);
                if (result.Success)
                    messageBoxService.ShowMessage("Order placed!", "Sucess", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                else
                    messageBoxService.ShowMessage($"Order placing failed: {result.Error.Message}", "Failed", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public void Sell(object o)
        {
            using (var client = new BinanceClient())
            {
                var result = client.Spot.Order.PlaceOrder(SelectedSymbol.Symbol, OrderSide.Sell, OrderType.Limit, SelectedSymbol.TradeAmount, price: SelectedSymbol.TradePrice, timeInForce: TimeInForce.GoodTillCancel);
                if (result.Success)
                    messageBoxService.ShowMessage("Order placed!", "Sucess", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                else
                    messageBoxService.ShowMessage($"Order placing failed: {result.Error.Message}", "Failed", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void Settings(object o)
        {
            settings = new SettingsWindow(this);
            settings.ShowDialog();
        }

        private void CloseSettings(object o)
        {
            settings?.Close();
            settings = null;

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(apiSecret))
                BinanceClient.SetDefaultOptions(new BinanceClientOptions() { ApiCredentials = new ApiCredentials(apiKey, apiSecret) });

            SubscribeUserStream();
        }

        private async Task GetAllSymbols()
        {
            using (var client = new BinanceClient())
            {
                var result = await client.Spot.Market.GetAllPricesAsync();
                if (result.Success)
                    AllPrices = new ObservableCollection<BinanceSymbolViewModel>(result.Data.Select(r => new BinanceSymbolViewModel(r.Symbol, r.Price)).ToList());
                else
                    messageBoxService.ShowMessage($"Error requesting data: {result.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            socketClient = new BinanceSocketClient();
            socketClient.Spot.SubscribeToAllSymbolTickerUpdates(data => {
                foreach (var ud in data) {
                    var symbol = AllPrices.SingleOrDefault(p => p.Symbol == ud.Symbol);
                    if (symbol != null)
                        symbol.Price = ud.LastPrice;
                }
            });             
        }

        private void Get24HourStats()
        {
            using (var client = new BinanceClient())
            {
                var result = client.Spot.Market.Get24HPrice(SelectedSymbol.Symbol);
                if (result.Success)
                {
                    SelectedSymbol.HighPrice = result.Data.HighPrice;
                    SelectedSymbol.LowPrice = result.Data.LowPrice;
                    SelectedSymbol.Volume = result.Data.BaseVolume;
                    SelectedSymbol.PriceChangePercent = result.Data.PriceChangePercent;
                }
                else
                    messageBoxService.ShowMessage($"Error requesting data: {result.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetOrders()
        {
            using (var client = new BinanceClient())
            {
                var result = client.Spot.Order.GetAllOrders(SelectedSymbol.Symbol);
                if (result.Success)
                {
                    SelectedSymbol.Orders = new ObservableCollection<OrderViewModel>(result.Data.OrderByDescending(d => d.CreateTime).Select(o => new OrderViewModel()
                    {
                        Id = o.OrderId,
                        ExecutedQuantity = o.QuantityFilled,
                        OriginalQuantity = o.Quantity,
                        Price = o.Price,
                        Side = o.Side,
                        Status = o.Status,
                        Symbol = o.Symbol,
                        Time = o.CreateTime,
                        Type = o.Type
                    }));
                }
                else
                    messageBoxService.ShowMessage($"Error requesting data: {result.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SubscribeUserStream()
        {
            if (ApiKey == null || ApiSecret == null)
                return;

            Task.Run(() =>
            {
                using (var client = new BinanceClient())
                {
                    var startOkay = client.Spot.UserStream.StartUserStream();
                    if (!startOkay.Success)
                    {
                        messageBoxService.ShowMessage($"Error starting user stream: {startOkay.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var subOkay = socketClient.Spot.SubscribeToUserDataUpdates(startOkay.Data, OnAccountUpdate, OnOrderUpdate, null, null, null);
                    if (!subOkay.Success)
                    {
                        messageBoxService.ShowMessage($"Error subscribing to user stream: {subOkay.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var accountResult = client.General.GetAccountInfo();
                    if (accountResult.Success)
                        Assets = new ObservableCollection<AssetViewModel>(accountResult.Data.Balances.Where(b => b.Free != 0 || b.Locked != 0).Select(b => new AssetViewModel() { Asset = b.Asset, Free = b.Free, Locked = b.Locked }).ToList());
                    else
                        messageBoxService.ShowMessage($"Error requesting account info: {accountResult.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void ChangeSymbol()
        {
            if (SelectedSymbol != null)
            {
                selectedSymbol.TradeAmount = 0;
                selectedSymbol.TradePrice = selectedSymbol.Price;
                Task.Run(() =>
                {
                    GetOrders();
                    Get24HourStats();
                });
            }

        }

        private void OnAccountUpdate(BinanceStreamAccountInfo data)
        {
            Assets = new ObservableCollection<AssetViewModel>(data.Balances.Where(b => b.Free != 0 || b.Locked != 0).Select(b => new AssetViewModel() { Asset = b.Asset, Free = b.Free, Locked = b.Locked }).ToList());
        }

        private void OnOrderUpdate(BinanceStreamOrderUpdate data)
        {
            var symbol = AllPrices.SingleOrDefault(a => a.Symbol == data.Symbol);
            if (symbol == null)
                return;

            lock (orderLock)
            {
                var order = symbol.Orders.SingleOrDefault(o => o.Id == data.OrderId);
                if (order == null)
                {
                    if (data.RejectReason != OrderRejectReason.None || data.ExecutionType != ExecutionType.New)
                        // Order got rejected, no need to show
                        return;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        symbol.AddOrder(new OrderViewModel()
                        {
                            ExecutedQuantity = data.QuoteQuantityFilled,
                            Id = data.OrderId,
                            OriginalQuantity = data.Quantity,
                            Price = data.Price,
                            Side = data.Side,
                            Status = data.Status,
                            Symbol = data.Symbol,
                            Time = data.CreateTime,
                            Type = data.Type
                        });
                    });
                }
                else
                {
                    order.ExecutedQuantity = data.QuoteQuantityFilled;
                    order.Status = data.Status;
                }
            }
        }
    }
}
