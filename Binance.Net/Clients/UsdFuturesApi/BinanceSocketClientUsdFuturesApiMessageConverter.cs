using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using System.Text.Json;

namespace Binance.Net.Clients.SpotApi
{
    internal class BinanceSocketClientUsdFuturesApiMessageConverter : DynamicJsonConverter
    {
        private static readonly HashSet<string> _userEvents = new HashSet<string>
        {
            "ACCOUNT_CONFIG_UPDATE",
            "MARGIN_CALL",
            "ACCOUNT_UPDATE",
            "ORDER_TRADE_UPDATE",
            "TRADE_LITE",
            "listenKeyExpired",
            "STRATEGY_UPDATE",
            "GRID_UPDATE",
            "CONDITIONAL_ORDER_TRIGGER_REJECT",
            "ALGO_UPDATE"
        };

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BinanceExchange._serializerContext);

        protected override MessageEvaluator[] MessageEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("stream"),
                    new PropertyFieldReference("e") { Depth = 2, Constraint = x => _userEvents.Contains(x!) },
                ],
                IdentifyMessageCallback = x => x.FieldValue("stream") + x.FieldValue("e"),
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("stream"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("stream"),
            },

            new MessageEvaluator {
                Priority = 3,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id"),
            }
        ];
    }
}
