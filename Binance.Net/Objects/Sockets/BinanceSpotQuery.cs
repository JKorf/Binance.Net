using Binance.Net.Objects.Internal;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSpotQuery<T> : Query<T> where T : BinanceResponse
    {
        private readonly SocketApiClient _client;

        public BinanceSpotQuery(SocketApiClient client, BinanceSocketQuery request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _client = client;
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<T>(request.Id.ToString(), HandleMessage);
            MessageMatcher = MessageMatcher.Create<T>(request.Id.ToString(), HandleMessage);
        }

        public CallResult<T> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, T message)
        {
            if (message.Status != 200)
            {
                if (message.Status == 418 || message.Status == 429)
                {
                    // Rate limit error 
                    return new CallResult<T>(new BinanceRateLimitError(message.Error!.Code, message.Error!.Message)
                    {
                        RetryAfter = message.Error.Data!.RetryAfter
                    }, originalData);
                }

                return new CallResult<T>(new ServerError(message.Error!.Code.ToString(), _client.GetErrorInfo(message.Error!.Code, message.Error!.Message)), originalData);
            }

            return new CallResult<T>(message, originalData, null);
        }
    }
}
