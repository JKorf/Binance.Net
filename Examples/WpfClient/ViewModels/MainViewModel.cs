using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Binance.Net.Objects;
using Binance.Net.Enums;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Sockets;
using WpfClient.MVVM;
using WpfClient.MessageBox;
using WpfClient.UserControls;
using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot.Socket;

namespace WpfClient.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private bool _shownCredentailsMessage = false;

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
                RaisePropertyChangedEvent("CredentialsEntered");
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
                RaisePropertyChangedEvent("CredentialsEntered");
            }
        }

        public bool CredentialsEntered => !string.IsNullOrEmpty(ApiKey);

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

            BuyCommand = new DelegateCommand(async (o) => await Buy(o));
            SellCommand = new DelegateCommand(async (o) => await Sell(o));
            CancelCommand = new DelegateCommand(async (o) => await Cancel(o));
            SettingsCommand = new DelegateCommand(Settings);
            CloseSettingsCommand = new DelegateCommand(async (o) => await CloseSettings(o));

            Task.Run(() => GetAllSymbols());
        }

        public async Task Cancel(object o)
        {
            var order = (OrderViewModel)o;
            using (var client = new BinanceClient())
            {
                var result = await client.SpotApi.Trading.CancelOrderAsync(SelectedSymbol.Symbol, order.Id);
                if (result.Success)
                    messageBoxService.ShowMessage("Order canceled!", "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    messageBoxService.ShowMessage($"Order canceling failed: {result.Error.Message}", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task Buy(object o)
        {
            using (var client = new BinanceClient())
            {
                var result = await client.SpotApi.Trading.PlaceOrderAsync(SelectedSymbol.Symbol, OrderSide.Buy, SpotOrderType.Limit, SelectedSymbol.TradeAmount, price: SelectedSymbol.TradePrice, timeInForce: TimeInForce.GoodTillCanceled);
                if (result.Success)
                    messageBoxService.ShowMessage("Order placed!", "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    messageBoxService.ShowMessage($"Order placing failed: {result.Error.Message}", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task Sell(object o)
        {
            using (var client = new BinanceClient())
            {
                var result = await client.SpotApi.Trading.PlaceOrderAsync(SelectedSymbol.Symbol, OrderSide.Sell, SpotOrderType.Limit, SelectedSymbol.TradeAmount, price: SelectedSymbol.TradePrice, timeInForce: TimeInForce.GoodTillCanceled);
                if (result.Success)
                    messageBoxService.ShowMessage("Order placed!", "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    messageBoxService.ShowMessage($"Order placing failed: {result.Error.Message}", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Settings(object o)
        {
            settings = new SettingsWindow(this);
            settings.ShowDialog();
        }

        private async Task CloseSettings(object o)
        {
            settings?.Close();
            settings = null;

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(apiSecret))
                BinanceClient.SetDefaultOptions(new BinanceClientOptions() { ApiCredentials = new ApiCredentials(apiKey, apiSecret) });

            await GetOrders();
            await SubscribeUserStream();
        }

        private async Task GetAllSymbols()
        {
            using (var client = new BinanceClient())
            {
                var result = await client.SpotApi.ExchangeData.GetPricesAsync();
                if (result.Success)
                    AllPrices = new ObservableCollection<BinanceSymbolViewModel>(result.Data.Select(r => new BinanceSymbolViewModel(r.Symbol, r.Price)).ToList());
                else
                    messageBoxService.ShowMessage($"Error requesting data: {result.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            socketClient = new BinanceSocketClient();
            var subscribeResult = await socketClient.SpotStreams.SubscribeToAllTickerUpdatesAsync(data =>
            {
                foreach (var ud in data.Data)
                {
                    var symbol = AllPrices.SingleOrDefault(p => p.Symbol == ud.Symbol);
                    if (symbol != null)
                        symbol.Price = ud.LastPrice;
                }
            });

            if (!subscribeResult.Success)
                messageBoxService.ShowMessage($"Failed to subscribe to price updates: {subscribeResult.Error}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async Task Get24HourStats()
        {
            using (var client = new BinanceClient())
            {
                var result = await client.SpotApi.ExchangeData.GetTickerAsync(SelectedSymbol.Symbol);
                if (result.Success)
                {
                    SelectedSymbol.HighPrice = result.Data.HighPrice;
                    SelectedSymbol.LowPrice = result.Data.LowPrice;
                    SelectedSymbol.Volume = result.Data.Volume;
                    SelectedSymbol.PriceChangePercent = result.Data.PriceChangePercent;
                }
                else
                    messageBoxService.ShowMessage($"Error requesting data: {result.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task GetOrders()
        {
            if (SelectedSymbol == null)
                return;

            if (apiKey == null)
            {
                if(!_shownCredentailsMessage)
                {
                    _shownCredentailsMessage = true;
                    messageBoxService.ShowMessage($"To retrieve and manage orders enter your API credentials via the settings on the top right", "Credentials", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                return;
            }

            using (var client = new BinanceClient())
            {
                var result = await client.SpotApi.Trading.GetOrdersAsync(SelectedSymbol.Symbol);
                if (result.Success)
                {
                    SelectedSymbol.Orders = new ObservableCollection<OrderViewModel>(result.Data.OrderByDescending(d => d.CreateTime).Select(o => new OrderViewModel()
                    {
                        Id = o.Id,
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

        private async Task SubscribeUserStream()
        {
            if (ApiKey == null || ApiSecret == null)
                return;
            
            using (var client = new BinanceClient())
            {
                var startOkay = await client.SpotApi.Account.StartUserStreamAsync();
                if (!startOkay.Success)
                {
                    messageBoxService.ShowMessage($"Error starting user stream: {startOkay.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var subOkay = await socketClient.SpotStreams.SubscribeToUserDataUpdatesAsync(startOkay.Data, OnOrderUpdate, null, OnAccountUpdate, null);
                if (!subOkay.Success)
                {
                    messageBoxService.ShowMessage($"Error subscribing to user stream: {subOkay.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var accountResult = await client.SpotApi.Account.GetAccountInfoAsync();
                if (accountResult.Success)
                    Assets = new ObservableCollection<AssetViewModel>(accountResult.Data.Balances.Where(b => b.Available != 0 || b.Locked != 0).Select(b => new AssetViewModel() { Asset = b.Asset, Free = b.Available, Locked = b.Locked }).ToList());
                else
                    messageBoxService.ShowMessage($"Error requesting account info: {accountResult.Error.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeSymbol()
        {
            if (SelectedSymbol != null)
            {
                selectedSymbol.TradeAmount = 0;
                selectedSymbol.TradePrice = selectedSymbol.Price;
                Task.Run(async () => await Task.WhenAll(GetOrders(), Get24HourStats()));
            }

        }

        private void OnAccountUpdate(DataEvent<BinanceStreamPositionsUpdate> data)
        {
            Assets = new ObservableCollection<AssetViewModel>(data.Data.Balances.Where(b => b.Available != 0 || b.Locked != 0).Select(b => new AssetViewModel() { Asset = b.Asset, Free = b.Available, Locked = b.Locked }).ToList());
        }

        private void OnOrderUpdate(DataEvent<BinanceStreamOrderUpdate> data)
        {
            var orderUpdate = data.Data;

            var symbol = AllPrices.SingleOrDefault(a => a.Symbol == orderUpdate.Symbol);
            if (symbol == null)
                return;

            lock (orderLock)
            {
                var order = symbol.Orders.SingleOrDefault(o => o.Id == orderUpdate.Id);
                if (order == null)
                {
                    if (orderUpdate.RejectReason != OrderRejectReason.None || orderUpdate.ExecutionType != ExecutionType.New)
                        // Order got rejected, no need to show
                        return;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        symbol.AddOrder(new OrderViewModel()
                        {
                            ExecutedQuantity = orderUpdate.QuoteQuantityFilled,
                            Id = orderUpdate.Id,
                            OriginalQuantity = orderUpdate.Quantity,
                            Price = orderUpdate.Price,
                            Side = orderUpdate.Side,
                            Status = orderUpdate.Status,
                            Symbol = orderUpdate.Symbol,
                            Time = orderUpdate.CreateTime,
                            Type = orderUpdate.Type
                        });
                    });
                }
                else
                {
                    order.ExecutedQuantity = orderUpdate.QuantityFilled;
                    order.Status = orderUpdate.Status;
                }
            }
        }
    }
}
