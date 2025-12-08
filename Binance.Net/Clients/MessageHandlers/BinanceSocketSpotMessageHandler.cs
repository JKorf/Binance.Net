using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Binance.Net.Objects.Models;
using System.Text.Json;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;

namespace Binance.Net.Clients.MessageHandlers
{
    internal class BinanceSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        private static readonly HashSet<string?> _userEvents = new HashSet<string?>
        {
            "outboundAccountPosition",
            "balanceUpdate",
            "executionReport",
            "listStatus",
            "listenKeyExpired",
            "eventStreamTerminated",
            "externalLockUpdate",
            "MARGIN_LEVEL_STATUS_CHANGE",
            "USER_LIABILITY_CHANGE"
        };

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BinanceExchange._serializerContext);

        public BinanceSocketSpotMessageHandler()
        {
            // Stream == listen key
            AddTopicMapping<BinanceCombinedStream<BinanceStreamPositionsUpdate>>(x => x.Stream);
            AddTopicMapping<BinanceCombinedStream<BinanceStreamBalanceUpdate>>(x => x.Stream);
            AddTopicMapping<BinanceCombinedStream<BinanceStreamOrderUpdate>>(x => x.Stream);
            AddTopicMapping<BinanceCombinedStream<BinanceStreamOrderList>>(x => x.Stream);
            AddTopicMapping<BinanceCombinedStream<BinanceStreamEvent>>(x => x.Stream);
            AddTopicMapping<BinanceCombinedStream<BinanceStreamBalanceLockUpdate>>(x => x.Stream);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("e") { Depth = 2 }.WithFilterContstraint(_userEvents),
                ],
                TypeIdentifierCallback = x => x.FieldValue("e")!,
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("stream"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("stream")!,
            },

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!,
            }
        ];
    }
}
