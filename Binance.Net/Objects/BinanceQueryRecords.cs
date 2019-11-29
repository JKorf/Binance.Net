using System.Collections.Generic;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Query results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinanceQueryRecords<T>
    {
        /// <summary>
        /// The list records
        /// </summary>
        public IEnumerable<T> Rows { get; set; } = new List<T>();
        /// <summary>
        /// The total count of the records
        /// </summary>
        public int Total { get; set; }
    }
}
