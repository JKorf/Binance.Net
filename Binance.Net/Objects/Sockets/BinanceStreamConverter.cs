using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Sockets
{
    public class BinanceStreamConverter : SocketConverter
    {
        public override string[] SubscriptionIdFields => new[] { "stream" }; 
        public override string[] TypeIdFields => new[] { "id", "data:e" }; 

        public override Type? GetDeserializationType(Dictionary<string, string> idValues, List<MessageListener> listeners)
        {
            if (idValues["id"] != null)
                return typeof(BinanceSocketQueryResponse);

            var eventType = idValues["data:e"];
            switch (eventType)
            {
                case "trade": return typeof(BinanceCombinedStream<BinanceStreamTrade>);
                case "kline": return typeof(BinanceCombinedStream<BinanceStreamKlineData>);
                default: return null;
            }
        }
    }
}
