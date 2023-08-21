using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;

namespace Binance.Net.UnitTests.TestImplementations
{
    public class TestHelpers
    {
        [ExcludeFromCodeCoverage]
        public static bool AreEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                var type = self.GetType();
                var ignoreList = new List<string>(ignore);
                foreach (var pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (ignoreList.Contains(pi.Name))
                    {
                        continue;
                    }

                    var selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                    var toValue = type.GetProperty(pi.Name).GetValue(to, null);

                    if (pi.PropertyType.IsClass && !pi.PropertyType.Module.ScopeName.Equals("System.Private.CoreLib.dll"))
                    {
                        // Check of "CommonLanguageRuntimeLibrary" is needed because string is also a class
                        if (AreEqual(selfValue, toValue, ignore))
                        {
                            continue;
                        }

                        return false;
                    }

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        return false;
                    }
                }

                return true;
            }

            return self == to;
        }

        public static IBinanceSocketClient CreateSocketClient(IWebsocket socket, Action<BinanceSocketOptions> options = null)
        {
            BinanceSocketClient client;
            client = options != null ? new BinanceSocketClient(options) : new BinanceSocketClient();
            client.SpotApi.SocketFactory = Mock.Of<IWebsocketFactory>();
            client.UsdFuturesApi.SocketFactory = Mock.Of<IWebsocketFactory>();
            client.CoinFuturesApi.SocketFactory = Mock.Of<IWebsocketFactory>();
            Mock.Get(client.SpotApi.SocketFactory).Setup(f => f.CreateWebsocket(It.IsAny<ILogger>(), It.IsAny<WebSocketParameters>())).Returns(socket);
            Mock.Get(client.UsdFuturesApi.SocketFactory).Setup(f => f.CreateWebsocket(It.IsAny<ILogger>(), It.IsAny<WebSocketParameters>())).Returns(socket);
            Mock.Get(client.CoinFuturesApi.SocketFactory).Setup(f => f.CreateWebsocket(It.IsAny<ILogger>(), It.IsAny<WebSocketParameters>())).Returns(socket);
            return client;
        }

        public static IBinanceRestClient CreateClient(Action<BinanceRestOptions> options = null)
        {
            IBinanceRestClient client;
            client = options != null ? new BinanceRestClient(options) : new BinanceRestClient();
            client.SpotApi.RequestFactory = Mock.Of<IRequestFactory>();
            client.GeneralApi.RequestFactory = Mock.Of<IRequestFactory>();
            client.CoinFuturesApi.RequestFactory = Mock.Of<IRequestFactory>();
            client.UsdFuturesApi.RequestFactory = Mock.Of<IRequestFactory>();
            return client;
        }

        public static IBinanceRestClient CreateResponseClient(string response, Action<BinanceRestOptions> options = null)
        {
            var client = (BinanceRestClient)CreateClient(options);
            SetResponse(client, response);
            return client;
        }

        public static IBinanceRestClient CreateResponseClient<T>(T response, Action<BinanceRestOptions> options = null)
        {
            var client = (BinanceRestClient)CreateClient(options);
            SetResponse(client, JsonConvert.SerializeObject(response));
            return client;
        }

        public static void SetResponse(BinanceRestClient client, string responseData)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<IResponse>();
            response.Setup(c => c.IsSuccessStatusCode).Returns(true);
            response.Setup(c => c.GetResponseStreamAsync()).Returns(Task.FromResult((Stream)responseStream));

            var request = new Mock<IRequest>();
            request.Setup(c => c.Uri).Returns(new Uri("http://www.test.com"));
            request.Setup(c => c.GetHeaders()).Returns(new Dictionary<string, IEnumerable<string>>());
            request.Setup(c => c.GetResponseAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(response.Object));

            var factory = Mock.Get(client.SpotApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);

            factory = Mock.Get(client.UsdFuturesApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);

            factory = Mock.Get(client.CoinFuturesApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);

            factory = Mock.Get(client.GeneralApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);
        }

        public static void SetErrorWithResponse(IBinanceRestClient client, string responseData, HttpStatusCode code)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<IResponse>();
            response.Setup(c => c.IsSuccessStatusCode).Returns(false);
            response.Setup(c => c.GetResponseStreamAsync()).Returns(Task.FromResult((Stream)responseStream));

            var request = new Mock<IRequest>();
            request.Setup(c => c.Uri).Returns(new Uri("http://www.test.com"));
            request.Setup(c => c.GetHeaders()).Returns(new Dictionary<string, IEnumerable<string>>());
            request.Setup(c => c.GetResponseAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(response.Object));

            var factory = Mock.Get(client.SpotApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);

            factory = Mock.Get(client.UsdFuturesApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);

            factory = Mock.Get(client.CoinFuturesApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);

            factory = Mock.Get(client.GeneralApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);
        }

        public static object GetTestValue(Type type, int i)
        {
            if (type == typeof(bool))
                return true;

            if (type == typeof(bool?))
                return (bool?)true;

            if (type == typeof(decimal))
                return i / 100m;

            if (type == typeof(decimal?))
                return (decimal?)(i / 100m);

            if (type == typeof(int))
                return i;

            if (type == typeof(int?))
                return (int?)i;

            if (type == typeof(long))
                return (long)i;

            if (type == typeof(long?))
                return (long?)i;

            if (type == typeof(DateTime))
                return new DateTime(2019, 1, Math.Max(i, 1));

            if (type == typeof(DateTime?))
                return (DateTime?)new DateTime(2019, 1, Math.Max(i, 1));

            if (type == typeof(string))
                return "string" + i;

            if (type == typeof(IEnumerable<string>))
                return new[] { "string" + i };

            if (type == typeof(IEnumerable<BinanceFuturesBatchEditOrder>))
                return new BinanceFuturesBatchEditOrder[1]
                {
                    new BinanceFuturesBatchEditOrder
                    {
                        OrderId = 1,
                        Price = 1,
                        Quantity = 1,
                        Side = Enums.OrderSide.Sell,
                        Symbol = "1"
                    }
                };

            if (type.IsEnum)
            {
                return Activator.CreateInstance(type);
            }

            if (type.IsArray)
            {
                var elementType = type.GetElementType()!;
                var result = Array.CreateInstance(elementType, 2);
                result.SetValue(GetTestValue(elementType, 0), 0);
                result.SetValue(GetTestValue(elementType, 1), 1);
                return result;
            }

            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>)))
            {
                var result = (IList)Activator.CreateInstance(type)!;
                result.Add(GetTestValue(type.GetGenericArguments()[0], 0));
                result.Add(GetTestValue(type.GetGenericArguments()[0], 1));
                return result;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                var result = (IDictionary)Activator.CreateInstance(type)!;
                result.Add(GetTestValue(type.GetGenericArguments()[0], 0)!, GetTestValue(type.GetGenericArguments()[1], 0));
                result.Add(GetTestValue(type.GetGenericArguments()[0], 1)!, GetTestValue(type.GetGenericArguments()[1], 1));
                return Convert.ChangeType(result, type);
            }

            return null;
        }

        public static async Task<object> InvokeAsync(MethodInfo @this, object obj, params object[] parameters)
        {
            var task = (Task)@this.Invoke(obj, parameters);
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty.GetValue(task);
        }
    }
}
