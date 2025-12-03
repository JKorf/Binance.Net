using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Binance.Net.Objects.Models;
using System.Text.Json;
using Binance.Net.Objects.Models.Spot.Socket;

namespace Binance.Net.Clients.MessageHandlers
{
    internal class BinanceSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        private static readonly HashSet<string> _userEvents = new HashSet<string>
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

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("e") { Constraint = x => _userEvents.Contains(x!), Depth = 2 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("e")!,
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("stream"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("stream")!,
            },

            new MessageEvaluator {
                Priority = 3,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!,
            }
        ];
    }
}
