using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Logging;

namespace Binance.Net.Clients.SpotApi
{
    public class BinanceSocketClientSpotApiAccount
    {
        private const string executionUpdateEvent = "executionReport";
        private const string ocoOrderUpdateEvent = "listStatus";
        private const string accountPositionUpdateEvent = "outboundAccountPosition";
        private const string balanceUpdateEvent = "balanceUpdate";

        private readonly BinanceSocketClientSpotApi _client;

        private const string _baseAddressWebsocketApi = "wss://ws-api.binance.com:443/ws-api/v3";

        private readonly Log _log;

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClientSpot with default options
        /// </summary>
        public BinanceSocketClientSpotApiAccount(Log log, BinanceSocketClientSpotApi client)
        {
            _client = client;
            _log = log;
        }
        #endregion

        #region Streams


        #region User Data Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage,
            Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate, 
            CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var handler = new Action<DataEvent<string>>(data =>
            {
                var combinedToken = JToken.Parse(data.Data);
                var token = combinedToken["data"];
                if (token == null)
                    return;

                var evnt = token["e"]?.ToString();
                if (evnt == null)
                    return;

                switch (evnt)
                {
                    case executionUpdateEvent:
                        {
                            var result = _client.DeserializeInternal<BinanceStreamOrderUpdate>(token);
                            if (result)
                            {
                                result.Data.ListenKey = combinedToken["stream"]!.Value<string>()!;
                                onOrderUpdateMessage?.Invoke(data.As(result.Data, result.Data.Id.ToString()));
                            }
                            else
                                _log.Write(LogLevel.Warning,
                                    "Couldn't deserialize data received from order stream: " + result.Error);
                            break;
                        }
                    case ocoOrderUpdateEvent:
                        {
                            var result = _client.DeserializeInternal<BinanceStreamOrderList>(token);
                            if (result)
                            {
                                result.Data.ListenKey = combinedToken["stream"]!.Value<string>()!;
                                onOcoOrderUpdateMessage?.Invoke(data.As(result.Data, result.Data.Id.ToString()));
                            }
                            else
                                _log.Write(LogLevel.Warning,
                                    "Couldn't deserialize data received from oco order stream: " + result.Error);
                            break;
                        }
                    case accountPositionUpdateEvent:
                        {
                            var result = _client.DeserializeInternal<BinanceStreamPositionsUpdate>(token);
                            if (result)
                            {
                                result.Data.ListenKey = combinedToken["stream"]!.Value<string>()!;
                                onAccountPositionMessage?.Invoke(data.As(result.Data));
                            }
                            else
                                _log.Write(LogLevel.Warning,
                                    "Couldn't deserialize data received from account position stream: " + result.Error);
                            break;
                        }
                    case balanceUpdateEvent:
                        {
                            var result = _client.DeserializeInternal<BinanceStreamBalanceUpdate>(token);
                            if (result)
                            {
                                result.Data.ListenKey = combinedToken["stream"]!.Value<string>()!;
                                onAccountBalanceUpdate?.Invoke(data.As(result.Data, result.Data.Asset));
                            }
                            else
                                _log.Write(LogLevel.Warning,
                                    "Couldn't deserialize data received from account position stream: " + result.Error);
                            break;
                        }
                    default:
                        _log.Write(LogLevel.Warning, $"Received unknown user data event {evnt}: " + data);
                        break;
                }
            });

            return await _client.SubscribeAsync(_client.Options.BaseAddress, new[] { listenKey }, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion
    }
}
