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
                    new PropertyFieldReference("stream"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("stream"),
            },

            new MessageEvaluator {
                Priority = 2,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id"),
            }
        ];
    }
}
