using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    public class TestHelpers
    {
        [ExcludeFromCodeCoverage]
        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
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

                    if (type.GetProperty(pi.Name).GetMethod == null)
                        continue;

                    var selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                    var toValue = type.GetProperty(pi.Name).GetValue(to, null);

                    if (pi.PropertyType.IsClass && !pi.PropertyType.Module.ScopeName.Equals("System.Private.CoreLib.dll"))
                    {
                        // Check of "CommonLanguageRuntimeLibrary" is needed because string is also a class
                        if (PublicInstancePropertiesEqual(selfValue, toValue, ignore))
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

        public static MockObjects<T> PrepareClient<T>(Func<T> construct, string responseData) where T : ExchangeClient, new()
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<IResponse>();
            response.Setup(c => c.GetResponseStream()).Returns(responseStream);

            var requestStream = new Mock<Stream>();

            var request = new Mock<IRequest>();
            request.Setup(c => c.GetRequestStream()).Returns(Task.FromResult(requestStream.Object));
            request.Setup(c => c.Headers).Returns(new WebHeaderCollection());
            request.Setup(c => c.Uri).Returns(new Uri("http://www.test.com"));
            request.Setup(c => c.GetResponse()).Returns(Task.FromResult(response.Object));

            var factory = new Mock<IRequestFactory>();
            factory.Setup(c => c.Create(It.IsAny<string>()))
                .Returns(request.Object);

            var client = construct();
            client.RequestFactory = factory.Object;
            var log = (Log)typeof(T).GetField("log", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(client);
            log.Level = LogVerbosity.Debug;
            return new MockObjects<T>()
            {
                Client = client,
                Request = request,
                RequestStream = requestStream,
                Response = response,
                RequestFactory = factory
            };
        }

        public static T PrepareExceptionClient<T>(string responseData, string exceptionMessage, int statusCode) where T : ExchangeClient, new()
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var we = new WebException();
            var r = Activator.CreateInstance<HttpWebResponse>();
            var re = new HttpResponseMessage();

            typeof(HttpResponseMessage).GetField("_statusCode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(re, (HttpStatusCode)statusCode);
            typeof(HttpWebResponse).GetField("_httpResponseMessage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(r, re);
            typeof(WebException).GetField("_message", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(we, exceptionMessage);
            typeof(WebException).GetField("_response", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(we, r);

            var response = new Mock<IResponse>();
            response.Setup(c => c.GetResponseStream()).Throws(we);

            var request = new Mock<IRequest>();
            request.Setup(c => c.Headers).Returns(new WebHeaderCollection());
            request.Setup(c => c.GetResponse()).Returns(Task.FromResult(response.Object));

            var factory = new Mock<IRequestFactory>();
            factory.Setup(c => c.Create(It.IsAny<string>()))
                .Returns(request.Object);

            var client = new T
            {
                RequestFactory = factory.Object
            };
            var log = (Log)typeof(T).GetField("log", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(client);
            log.Level = LogVerbosity.Debug;
            return client;
        }

        public static (Mock<IWebsocket>, T) PrepareSocketClient<T>(Func<T> construct) where T : ExchangeClient, new()
        {
            List<IWebsocket> sockets = new List<IWebsocket>();
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close()).Returns(Task.FromResult(true));
            socket.Setup(s => s.Connect()).Returns(Task.FromResult(true));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);

            var client = construct();
            var log = (Log)typeof(T).GetField("log", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(client);
            if (log.Level == LogVerbosity.Info)
                log.Level = LogVerbosity.None;
            typeof(T).GetProperty("SocketFactory").SetValue(client, factory.Object);
            return (socket, client);
        }

        public static void InvokeWebsocket(Mock<IWebsocket> socket, string data)
        {
            socket.Raise(r => r.OnMessage += null, data);
        }

        public class MockObjects<T> where T : ExchangeClient
        {
            public T Client { get; set; }
            public Mock<IRequest> Request { get; set; }
            public Mock<Stream> RequestStream { get; set; }
            public Mock<IResponse> Response { get; set; }
            public Mock<IRequestFactory> RequestFactory { get; set; }

        }
    }
}
