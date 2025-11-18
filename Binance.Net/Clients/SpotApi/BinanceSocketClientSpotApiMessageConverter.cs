using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using System.Text.Json;

namespace Binance.Net.Clients.SpotApi
{
    internal class BinanceSocketClientSpotApiMessageConverter : DynamicJsonConverter
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BinanceExchange._serializerContext);

        protected override MessageEvaluator[] MessageEvaluators { get; } = [
            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new MessageFieldReference { Depth = 1, PropertyName = "stream", ValueType = typeof(string)  },
                    new MessageFieldReference { MaxDepth = 3, PropertyName = "e", ValueType = typeof(string)  }, // Can be either depth 2 or 3
                ],
                MessageIdentifier = x => x["stream"],
            },

            new MessageEvaluator {
                Priority = 2,
                ForceIfFound = true,
                Fields = [
                    new MessageFieldReference { Depth = 1, PropertyName = "id", ValueType = typeof(int) }
                ],
                MessageIdentifier = x => x["id"]
            }
        ];
    }
}
