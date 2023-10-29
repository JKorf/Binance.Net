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
        public override string[] IdFields => new[] { "e" }; 

        public override Type? GetDeserializationType(Dictionary<string, string> idValues, List<MessageListener> listeners)
        {
            var eventType = idValues["e"];
            switch (eventType)
            {
                case "trade": return typeof(BinanceStreamTrade);
                default: return null;
            }
        }
    }
}
