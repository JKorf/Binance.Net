using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    public class BinanceSpotUserDataSubscription : Subscription
    {
        /// <inheritdoc />
        public override List<string> Identifiers => _identifiers;

        private readonly Action<DataEvent<BinanceStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceStreamOrderList>>? _orderListHandler;
        private readonly Action<DataEvent<BinanceStreamPositionsUpdate>>? _positionHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceUpdate>>? _balanceHandler;
        private readonly List<string> _identifiers;
        private int _subId;
        private int _unsubId;

        /// <inheritdoc />
        public BinanceSpotUserDataSubscription(
            ILogger logger,
            List<string> topics,
            Action<DataEvent<BinanceStreamOrderUpdate>>? orderHandler,
            Action<DataEvent<BinanceStreamOrderList>>? orderListHandler,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? positionHandler,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? balanceHandler,
            bool auth) : base(logger, auth)
        {
            _orderHandler = orderHandler;
            _orderListHandler = orderListHandler;
            _positionHandler = positionHandler;
            _balanceHandler = balanceHandler;
            _identifiers = topics;
        }

        /// <inheritdoc />
        public override object? GetSubRequest()
        {
            _subId = ExchangeHelpers.NextId();
            return new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = _identifiers.ToArray(),
                Id = _subId
            };
        }

        /// <inheritdoc />
        public override object? GetUnsubRequest()
        {
            _unsubId = ExchangeHelpers.NextId();
            return new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = _identifiers.ToArray(),
                Id = _unsubId
            };
        }

        /// <inheritdoc />
        public override bool MessageMatchesSubRequest(ParsedMessage message)
        {
            if (message.Data is not BinanceSocketQueryResponse response)
                return false;

            if (response.Id != _subId)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool MessageMatchesUnsubRequest(ParsedMessage message)
        {
            if (message.Data is not BinanceSocketQueryResponse response)
                return false;

            if (response.Id != _unsubId)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override Task HandleEventAsync(DataEvent<ParsedMessage> message)
        {
            var data = message.Data.Data;
            if (data is BinanceCombinedStream<BinanceStreamOrderUpdate> orderUpdate)
            {
                orderUpdate.Data.ListenKey = orderUpdate.Stream;
                _orderHandler?.Invoke(message.As(orderUpdate.Data, orderUpdate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceStreamOrderList> orderListUpdate)
            {
                orderListUpdate.Data.ListenKey = orderListUpdate.Stream;
                _orderListHandler?.Invoke(message.As(orderListUpdate.Data, orderListUpdate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceStreamPositionsUpdate> positionUpdate)
            {
                positionUpdate.Data.ListenKey = positionUpdate.Stream;
                _positionHandler?.Invoke(message.As(positionUpdate.Data, positionUpdate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceStreamBalanceUpdate> balanceUpdate)
            {
                balanceUpdate.Data.ListenKey = balanceUpdate.Stream;
                _balanceHandler?.Invoke(message.As(balanceUpdate.Data, balanceUpdate.Stream, SocketUpdateType.Update));
            }

            return Task.CompletedTask;
        }

        public override CallResult HandleSubResponse(ParsedMessage message) => new CallResult(null);
        public override CallResult HandleUnsubResponse(ParsedMessage message) => new CallResult(null);
    }
}
