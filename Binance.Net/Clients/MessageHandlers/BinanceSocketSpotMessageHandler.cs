using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Binance.Net.Objects.Models;
using System.Text.Json;

namespace Binance.Net.Clients.MessageHandlers
{
    internal class BinanceSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BinanceExchange._serializerContext);

        public BinanceSocketSpotMessageHandler()
        {
            AddTopicMapping<BinanceCombinedStream>(x => x.Stream);
        }

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("e") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("e")!,
            },

            // Book ticker doesn't push `e` field..
            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("stream") { Constraint = x => x!.EndsWith("@bookTicker") },
                ],
                StaticIdentifier = "bookTicker"
            },

            // Partial book doesn't push `e` field..
            new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("stream") 
                    { 
                        Constraint = x => x!.EndsWith("depth5@100ms") || x!.EndsWith("depth10@100ms") || x!.EndsWith("depth20@100ms")
                                        || x!.EndsWith("depth5") || x!.EndsWith("depth10") || x!.EndsWith("depth20")
                    },
                ],
                StaticIdentifier = "depthUpdate"
            },

            new MessageEvaluator {
                Priority = 4,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!,
            }
        ];
    }
}
