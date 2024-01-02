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
    public class BinanceSpotUserDataSubscription : Subscription<BinanceSocketQueryResponse>
    {
        /// <inheritdoc />
        public override List<string> StreamIdentifiers { get; set; }

        private readonly Action<DataEvent<BinanceStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceStreamOrderList>>? _orderListHandler;
        private readonly Action<DataEvent<BinanceStreamPositionsUpdate>>? _positionHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceUpdate>>? _balanceHandler;

        /// <inheritdoc />
        public override Dictionary<string, Type> TypeMapping { get; } = new Dictionary<string, Type>
        {
            { "outboundAccountPosition", typeof(BinanceCombinedStream<BinanceStreamPositionsUpdate>) },
            { "balanceUpdate", typeof(BinanceCombinedStream<BinanceStreamBalanceUpdate>) },
            { "executionReport", typeof(BinanceCombinedStream<BinanceStreamOrderUpdate>) },
            { "listStatus", typeof(BinanceCombinedStream<BinanceStreamOrderList>) },
        };

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
            StreamIdentifiers = topics;
        }

        /// <inheritdoc />
        public override BaseQuery? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = StreamIdentifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public override BaseQuery? GetUnsubQuery()
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = StreamIdentifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public override Task<CallResult> DoHandleMessageAsync(SocketConnection connection, DataEvent<BaseParsedMessage> message)
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

            return Task.FromResult(new CallResult(null)); // TODO error not mapped
        }
    }
}
