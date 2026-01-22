using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net
{
    public enum UpdateSource
    {
        /// <summary>
        /// Polling result
        /// </summary>
        Poll,
        /// <summary>
        /// Websocket push
        /// </summary>
        Push
    }

    public class UserDataUpdate<T>
    {
        public UpdateSource Source { get; set; }
        public T Data { get; set; }
    }

    public class UserDataTracker
    {
        private ConcurrentDictionary<string, SharedBalance> _balanceStore = new ConcurrentDictionary<string, SharedBalance>();
        private ConcurrentDictionary<string, SharedSpotOrder> _orderStore = new ConcurrentDictionary<string, SharedSpotOrder>();
        private ConcurrentDictionary<string, SharedUserTrade> _tradeStore = new ConcurrentDictionary<string, SharedUserTrade>();


        private readonly IBinanceRestClient _restClient;
        private readonly IBinanceSocketClient _socketClient;
        private readonly ILogger _logger;

        private AsyncResetEvent _pollWaitEvent = new AsyncResetEvent(false, true);
        private Task? _pollTask;
        private CancellationTokenSource? _cts;
        private object _symbolLock = new object();

        private List<string> _symbols = new List<string>();
        private TimeSpan _pollInterval;

        public event Action<UserDataUpdate<SharedBalance[]>> OnBalanceUpdate;
        public event Action<UserDataUpdate<SharedSpotOrder[]>> OnOrderUpdate;
        public event Action<UserDataUpdate<SharedUserTrade[]>> OnTradeUpdate;

        public SharedBalance[] Balances => _balanceStore.Values.ToArray();
        public SharedSpotOrder[] Orders => _orderStore.Values.ToArray();

        public UserDataTracker(
            ILogger<UserDataTracker> logger,
            IBinanceRestClient restClient,
            IBinanceSocketClient socketClient)
        {
            _logger = logger;
            _restClient = restClient;
            _socketClient = socketClient;
        }
    
        public async Task<CallResult> StartAsync(
            IEnumerable<string>? symbols = null,
            TimeSpan? pollInterval = null)
        {
            _pollInterval = pollInterval ?? TimeSpan.Zero;
            _symbols = symbols?.ToList() ?? [];

            _logger.LogDebug("Starting UserDataTracker");
            _cts = new CancellationTokenSource();

            var lkResult = await _restClient.SpotApi.SharedClient.StartListenKeyAsync(new StartListenKeyRequest()).ConfigureAwait(false);
            if (!lkResult)
                return lkResult;

            var subBalanceResult = await _socketClient.SpotApi.SharedClient.SubscribeToBalanceUpdatesAsync(new SubscribeBalancesRequest(lkResult.Data), x => HandleBalanceUpdate(UpdateSource.Push, x.Data), ct: _cts.Token).ConfigureAwait(false);
            if (!subBalanceResult)
                return subBalanceResult;

            var subOrderResult = await _socketClient.SpotApi.SharedClient.SubscribeToSpotOrderUpdatesAsync(new SubscribeSpotOrderRequest(lkResult.Data), x => HandleOrderUpdate(UpdateSource.Push, x.Data), ct: _cts.Token).ConfigureAwait(false);
            if (!subOrderResult)
                return subOrderResult;

            subBalanceResult.Data.SubscriptionStatusChanged += SubscriptionStatusChanged;
            subOrderResult.Data.SubscriptionStatusChanged += SubscriptionStatusChanged;

            _pollTask = PollAsync();
            _logger.LogDebug("Started UserDataTracker");
            return CallResult.SuccessResult;
        }

        private void UpdateSymbolsList(IEnumerable<string> symbols)
        {
            lock (_symbolLock)
            {
                foreach (var symbol in symbols.Distinct())
                {
                    if (!_symbols.Contains(symbol))
                    {
                        _symbols.Add(symbol);
                        _logger.LogDebug("Adding {Symbol} to symbol tracking list", symbol);
                    }
                }
            }

        }

        private void HandleOrderUpdate(UpdateSource source, SharedSpotOrder[] @event)
        {
            UpdateSymbolsList(@event.Select(x => x.Symbol));

            // Update local store
            var updatedIds = @event.Select(x => x.OrderId).ToList();

            foreach (var item in @event)
            {
                _logger.LogDebug("Updating spot order {Symbol}.{Id}", item.Symbol, item.OrderId);

                _orderStore.AddOrUpdate(item.OrderId, item, (id, existing) =>
                {
                    var updated = UpdateSpotOrder(existing, item);
                    if (!updated)
                    {
                        _logger.LogDebug("Skipping update spot order {Symbol}.{Id}, deemed out of sync", item.Symbol, item.OrderId);
                        updatedIds.Remove(id);
                    }
                    return existing;
                });



                //if (item.LastTrade != null)
                //{
                //    _tradeStore[item.LastTrade.Id] = item.LastTrade;
                //}
            }

            if (updatedIds.Count > 0)
            {
                OnOrderUpdate?.Invoke(
                    new UserDataUpdate<SharedSpotOrder[]>
                    {
                        Source = source,
                        Data = _orderStore.Where(x => updatedIds.Contains(x.Key)).Select(x => x.Value).ToArray()
                    });
            }

            //var trades = @event.Data.Where(x => x.LastTrade != null).Select(x => x.LastTrade!).ToArray();
            //if (trades.Length != 0)
            //    OnTradeUpdate?.Invoke(new UserDataUpdate<SharedUserTrade[]> { Source = UpdateSource.Push, Data = trades });
        }

        private void HandleBalanceUpdate(UpdateSource source, SharedBalance[] @event)
        {
            // Update local store
            var updatedAssets = @event.Select(x => x.Asset).ToList();

            foreach (var item in @event)
            {
                _logger.LogDebug("Updating balance for {Asset}", item.Asset);

                _balanceStore.AddOrUpdate(item.Asset, item, (asset, existing) =>
                {
                    var updated = UpdateBalance(existing, item);
                    if (!updated)
                        updatedAssets.Remove(asset);

                    return existing;
                });
            }

            if (updatedAssets.Count > 0)
            {
                OnBalanceUpdate?.Invoke(
                new UserDataUpdate<SharedBalance[]>
                {
                    Source = source,
                    Data = _balanceStore.Where(x => updatedAssets.Contains(x.Key)).Select(x => x.Value).ToArray()
                });
            }
        }

        private void SubscriptionStatusChanged(SubscriptionStatus newState)
        {
            if (newState == SubscriptionStatus.Subscribed)
            {
                // Trigger REST polling since we weren't connected
                _pollWaitEvent.Set();
            }
        }

        private bool UpdateBalance(SharedBalance existingBalance, SharedBalance newBalance)
        {
            // Some other way to way to determine sequence? Maybe timestamp?
            var changed = false;
            if (existingBalance.Total != newBalance.Total)
            {
                existingBalance.Total = newBalance.Total;
                changed = true;
            }

            if (existingBalance.Available != newBalance.Available)
            {
                existingBalance.Available = newBalance.Available;
                changed = true;
            }

            return changed;
        }

        private bool UpdateSpotOrder(SharedSpotOrder existingOrder, SharedSpotOrder newOrder)
        {
            if (CheckIfUpdateIsNewer(existingOrder, newOrder) == false)
                // Update is older than the existing data, ignore
                return false;

            var changed = false;
            if (newOrder.AveragePrice != null && newOrder.AveragePrice != existingOrder.AveragePrice)
            {
                existingOrder.AveragePrice = newOrder.AveragePrice;
                changed = true;
            }

            if (newOrder.OrderPrice != null && newOrder.OrderPrice != existingOrder.OrderPrice)
            {
                existingOrder.OrderPrice = newOrder.OrderPrice;
                changed = true;
            }

            if (newOrder.Fee != null && newOrder.Fee != existingOrder.Fee)
            {
                existingOrder.Fee = newOrder.Fee;
                changed = true;
            }

            if (newOrder.FeeAsset != null && newOrder.FeeAsset != existingOrder.FeeAsset)
            {
                existingOrder.FeeAsset = newOrder.FeeAsset;
                changed = true;
            }

            if (newOrder.OrderQuantity != null && newOrder.OrderQuantity != existingOrder.OrderQuantity)
            {
                existingOrder.OrderQuantity = newOrder.OrderQuantity;
                changed = true;
            }

            if (newOrder.QuantityFilled != null && newOrder.QuantityFilled != existingOrder.QuantityFilled)
            {
                existingOrder.QuantityFilled = newOrder.QuantityFilled;
                changed = true;
            }

            if (newOrder.Status != existingOrder.Status)
            {
                existingOrder.Status = newOrder.Status;
                changed = true;
            }

            if (newOrder.UpdateTime != null && newOrder.UpdateTime != existingOrder.UpdateTime)
            {
                existingOrder.UpdateTime = newOrder.UpdateTime;
                changed = true;
            }

            return changed;
        }

        private bool? CheckIfUpdateIsNewer(SharedSpotOrder existingOrder, SharedSpotOrder newOrder)
        {
            if (existingOrder.Status == SharedOrderStatus.Open && newOrder.Status != SharedOrderStatus.Open)
                // status changed from open to not open
                return true;

            if (existingOrder.Status != SharedOrderStatus.Open && newOrder.Status == SharedOrderStatus.Open)
                // status changed from not open to open; stale
                return false;

            if (existingOrder.UpdateTime != null && newOrder.UpdateTime != null)
            {
                // If both have an update time base of that
                if (existingOrder.UpdateTime < newOrder.UpdateTime)
                    return true;

                if (existingOrder.UpdateTime > newOrder.UpdateTime)
                    return false;
            }

            if (existingOrder.QuantityFilled != null && newOrder.QuantityFilled != null)
            {
                if (existingOrder.QuantityFilled.QuantityInBaseAsset != null && newOrder.QuantityFilled.QuantityInBaseAsset != null)
                {
                    // If base quantity is not null we can base it on that
                    if (existingOrder.QuantityFilled.QuantityInBaseAsset < newOrder.QuantityFilled.QuantityInBaseAsset)
                        return true;

                    else if (existingOrder.QuantityFilled.QuantityInBaseAsset > newOrder.QuantityFilled.QuantityInBaseAsset)
                        return false;
                }

                if (existingOrder.QuantityFilled.QuantityInQuoteAsset != null && newOrder.QuantityFilled.QuantityInQuoteAsset != null)
                {
                    // If quote quantity is not null we can base it on that
                    if (existingOrder.QuantityFilled.QuantityInQuoteAsset < newOrder.QuantityFilled.QuantityInQuoteAsset)
                        return true;

                    else if (existingOrder.QuantityFilled.QuantityInQuoteAsset > newOrder.QuantityFilled.QuantityInQuoteAsset)
                        return false;
                }
            }

            if (existingOrder.Fee != null && newOrder.Fee != null)
            {
                // Higher fee means later processing
                if (existingOrder.Fee < newOrder.Fee)
                    return true;

                if (existingOrder.Fee > newOrder.Fee)
                    return false;
            }

            return null;
        }

        private async Task PollAsync()
        {
            while (!_cts!.IsCancellationRequested)
            {
                try
                {
                    await _pollWaitEvent.WaitAsync(_pollInterval, _cts.Token).ConfigureAwait(false); // Config interval
                }
                catch { }

                if (_cts.IsCancellationRequested)
                    break;

                _logger.LogDebug("Starting user data polling");
                var balances = await _restClient.SpotApi.SharedClient.GetBalancesAsync(new GetBalancesRequest()).ConfigureAwait(false);
                if (!balances.Success)
                {
                    // .. ?
                }
                else
                {
                    HandleBalanceUpdate(UpdateSource.Poll, balances.Data);
                }

                var openOrders = await _restClient.SpotApi.SharedClient.GetOpenSpotOrdersAsync(new GetOpenOrdersRequest()).ConfigureAwait(false);
                if (!openOrders.Success)
                {
                    // .. ?
                }
                else
                {
                    HandleOrderUpdate(UpdateSource.Poll, openOrders.Data);
                }

                foreach (var symbol in _symbols)
                {
                    var sharedSymbol = new SharedSymbol(TradingMode.Spot, "-", "-", symbol);
                    var closedOrders = await _restClient.SpotApi.SharedClient.GetClosedSpotOrdersAsync(new GetClosedOrdersRequest(sharedSymbol)).ConfigureAwait(false);
                    if (!closedOrders.Success)
                    {
                        // .. ?
                    }
                    else
                    {
                        HandleOrderUpdate(UpdateSource.Poll, closedOrders.Data);
                    }

                    var trades = await _restClient.SpotApi.SharedClient.GetSpotUserTradesAsync(new GetUserTradesRequest(sharedSymbol)).ConfigureAwait(false);
                    if (!trades.Success)
                    {
                        // .. ?
                    }
                    else
                    {
                        OnTradeUpdate?.Invoke(new UserDataUpdate<SharedUserTrade[]> { Source = UpdateSource.Poll, Data = trades.Data });
                    }
                }

                _logger.LogDebug("User data polling completed");
            }
        }

        public async Task StopAsync()
        {
            _logger.LogDebug("Stopping UserDataTracker");
            _cts?.Cancel();

            if (_pollTask != null)
                await _pollTask.ConfigureAwait(false);

            _logger.LogDebug("Stopped UserDataTracker");
        }
    }
}
