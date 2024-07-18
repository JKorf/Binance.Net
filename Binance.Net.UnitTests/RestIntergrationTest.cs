using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
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
        public abstract TClient GetClient();
        public virtual bool Run { get; set; }

        public async Task RunAndCheckResult<T>(Expression<Func<TClient, Task<WebCallResult<T>>>> expre)
        {
            var integrationTests = Environment.GetEnvironmentVariable("INTEGRATION");
            if (!Run && integrationTests != "1")
                return;

            var client = GetClient();
            WebCallResult<T> result;
            var expressionBody = (MethodCallExpression)expre.Body;
            try
            {
                result = await expre.Compile().Invoke(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Method {expressionBody.Method.DeclaringType.Name}.{expressionBody.Method.Name} threw an exception: " + ex.ToLogString());
            }

            if (!result.Success)
                throw new Exception($"Method {expressionBody.Method.DeclaringType.Name}.{expressionBody.Method.Name} returned error: " + result.Error);

            Debug.WriteLine($"{expressionBody.Method.DeclaringType.Name}.{expressionBody.Method.Name} {result}");
        }
    }
}
