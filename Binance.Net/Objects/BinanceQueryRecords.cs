namespace Binance.Net.Objects
{
    internal class BinanceQueryRecords<T>
    {
        /// <summary>
        /// The list records
        /// </summary>
        public T[] Rows { get; set; }
        /// <summary>
        /// The total count of the records
        /// </summary>
        public int Total { get; set; }
    }
}
