using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using System.Text.Json;
using Binance.Net.Objects.Models;

namespace Binance.Net.Clients.MessageHandlers
{
    internal class BinanceSocketUsdFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BinanceExchange._serializerContext);

        public BinanceSocketUsdFuturesMessageHandler()
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

            // Asset index stream send array of events which means `e` field is one deeper
            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("e") { Depth = 3 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("e")!,
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
