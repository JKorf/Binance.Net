using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    public abstract class RestIntergrationTest<TClient>
    {
        public abstract TClient GetClient(ILoggerFactory loggerFactory);
        public virtual bool Run { get; set; }
        public bool Authenticated { get; set; }

        protected TClient CreateClient()
        {
            var fact = new LoggerFactory();
            fact.AddProvider(new TraceLoggerProvider());
            return GetClient(fact);
        }

        protected bool ShouldRun()
        {
            var integrationTests = Environment.GetEnvironmentVariable("INTEGRATION");
            if (!Run && integrationTests != "1")
                return false;

            return true;
        }

        public async Task RunAndCheckResult<T>(Expression<Func<TClient, Task<WebCallResult<T>>>> expre, bool authRequest)
        {
            if (!ShouldRun())
                return;

            var client = CreateClient();

            var expressionBody = (MethodCallExpression)expre.Body;
            if (authRequest && !Authenticated)
            {
                Debug.WriteLine($"Skipping {expressionBody.Method.Name}, not authenticated");
                return;
            }

            var listener = new EnumValueTraceListener();
            Trace.Listeners.Add(listener);

            WebCallResult<T> result;
            try
            {
                result = await expre.Compile().Invoke(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Method {expressionBody.Method.Name} threw an exception: " + ex.ToLogString());
            }
            finally
            {
                Trace.Listeners.Remove(listener);
            }

            if (!result.Success)
                throw new Exception($"Method {expressionBody.Method.Name} returned error: " + result.Error);

            Debug.WriteLine($"{expressionBody.Method.Name} {result}");
        }
    }
}
