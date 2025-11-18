using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Socket;
using Binance.Net.Objects.Sockets;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Binance.Net.Clients.SpotApi
{
    internal class BinanceSocketClientSpotApiMessageConverter : DynamicJsonConverter
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BinanceExchange._serializerContext);

        protected override MessageEvaluator[] MessageEvaluators { get; } = [
            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new MessageFieldReference { Level = 1, Name = "stream", Type = typeof(string)  },
                    new MessageFieldReference { Level = 2, Name = "e", Type = typeof(string)  },
                ],
                MessageIdentifier = x => x.GetString("stream"),
                TypeIdentifier = x =>
                {
                    if (x.GetString("stream").EndsWith("arr"))
                    {
                         if(_arrayDeserializationTypeMap.TryGetValue(x.GetString("e"), out var type))
                            return type;
                    }
                    else
                    {
                         if(_deserializationTypeMap.TryGetValue(x.GetString("e"), out var type))
                            return type;
                    }

                    return null;
                }},

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new MessageFieldReference { Level = 1, Name = "stream", Type = typeof(string)  },
                    new MessageFieldReference { Level = 3, Name = "e", Type = typeof(string)  },
                ],
                MessageIdentifier = x => x.GetString("stream"),
                TypeIdentifier = x =>
                {
                    if (x.GetString("stream").EndsWith("arr"))
                    {
                         if(_arrayDeserializationTypeMap.TryGetValue(x.GetString("e"), out var type))
                            return type;
                    }
                    else
                    {
                         if(_deserializationTypeMap.TryGetValue(x.GetString("e"), out var type))
                            return type;
                    }

                    return null;
                }},

            new MessageEvaluator {
                Priority = 5,
                ForceIfFound = true,
                Fields = [
                    new MessageFieldReference { Level = 1, Name = "id", Type = typeof(int) }
                ],
                MessageIdentifier = x => x.GetInt("id").ToString(),
                TypeIdentifier = x => typeof(BinanceSocketQueryResponse)
            }
        ];

        // Streams without clear identifier:
        // Partial book stream
        // book ticker stream

        private static Dictionary<string, Type> _deserializationTypeMap = new Dictionary<string, Type>()
        {
            { "trade", typeof(BinanceCombinedStream<BinanceStreamTrade>) },
            { "aggTrade", typeof(BinanceCombinedStream<BinanceStreamAggregatedTrade>) },
            { "kline", typeof(BinanceCombinedStream<BinanceStreamKlineData>) },
            { "24hrMiniTicker", typeof(BinanceCombinedStream<BinanceStreamMiniTick>) },
            { "24hrTicker", typeof(BinanceCombinedStream<BinanceStreamTick>) },
            { "1hTicker", typeof(BinanceCombinedStream<BinanceStreamRollingWindowTick>) },
            { "4hTicker", typeof(BinanceCombinedStream<BinanceStreamRollingWindowTick>) },
            { "1dTicker", typeof(BinanceCombinedStream<BinanceStreamRollingWindowTick>) },
            { "depthUpdate", typeof(BinanceCombinedStream<BinanceEventOrderBook>) },
            { "avgPrice", typeof(BinanceCombinedStream<BinanceStreamAveragePrice>) },
        };

        private static Dictionary<string, Type> _arrayDeserializationTypeMap = new Dictionary<string, Type>()
        {
            { "24hrMiniTicker", typeof(BinanceCombinedStream<BinanceStreamMiniTick[]>) },
            { "24hrTicker", typeof(BinanceCombinedStream<BinanceStreamTick[]>) },
            { "1hTicker", typeof(BinanceCombinedStream<BinanceStreamRollingWindowTick[]>) },
            { "4hTicker", typeof(BinanceCombinedStream<BinanceStreamRollingWindowTick[]>) },
            { "1dTicker", typeof(BinanceCombinedStream<BinanceStreamRollingWindowTick[]>) },
        };

        //public override MessageInfo GetMessageInfo(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType)
        //{
        //    var reader = new Utf8JsonReader(data);
        //    string? streamId = null;
        //    bool arrayMessage = false;
        //    while (reader.Read())
        //    {
        //        if (reader.CurrentDepth == 1 && reader.TokenType == JsonTokenType.StartArray)
        //        {
        //            arrayMessage = true;
        //            continue;
        //        }

        //        if (reader.TokenType == JsonTokenType.PropertyName)
        //        {
        //            if (reader.CurrentDepth == 1 && reader.ValueTextEquals("id"))
        //            {
        //                // Query response
        //                reader.Read();
        //                return new MessageInfo { DeserializationType = typeof(BinanceSocketQueryResponse), Identifier = reader.GetInt32().ToString()! };
        //            }
        //            if (reader.CurrentDepth == 1 && reader.ValueTextEquals("stream"))
        //            {
        //                // Query response
        //                reader.Read();
        //                streamId = reader.GetString();
        //            }
        //            else if (
        //                (reader.CurrentDepth == 2 || reader.CurrentDepth == 3) // 2 for individual messages, 3 for arrays
        //                && reader.ValueTextEquals("e"))
        //            {
        //                // Event
        //                reader.Read();

        //                Type? deserializationType = null;
        //                foreach (var item in arrayMessage ? _arrayDeserializationTypeMap : _deserializationTypeMap)
        //                {
        //                    if (reader.ValueTextEquals(item.Key))
        //                    {
        //                        deserializationType = item.Value;
        //                        break;
        //                    }
        //                }

        //                return new MessageInfo { DeserializationType = deserializationType, Identifier = streamId };
        //            }
        //        }
        //    }

        //    return new MessageInfo() { Identifier = streamId };
        //}
    }
}
