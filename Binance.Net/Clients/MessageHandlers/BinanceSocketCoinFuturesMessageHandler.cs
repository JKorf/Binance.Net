using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using System.Text.Json;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;

namespace Binance.Net.Clients.MessageHandlers
{
    internal class BinanceSocketCoinFuturesMessageHandler : JsonSocketMessageHandler
    {
        private static readonly HashSet<string?> _userEvents = new HashSet<string?>
        {
            "ACCOUNT_CONFIG_UPDATE",
            "MARGIN_CALL",
            "ACCOUNT_UPDATE",
            "ORDER_TRADE_UPDATE",
            "listenKeyExpired",
            "STRATEGY_UPDATE",
            "GRID_UPDATE"
        };

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BinanceExchange._serializerContext);

        public BinanceSocketCoinFuturesMessageHandler()
        {
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
