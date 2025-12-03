using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using System.Text.Json;
using Binance.Net.Objects.Models;

namespace Binance.Net.Clients.MessageHandlers
{
    internal class BinanceSocketCoinFuturesMessageHandler : JsonSocketMessageHandler
    {
        private static readonly HashSet<string> _userEvents = new HashSet<string>
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
