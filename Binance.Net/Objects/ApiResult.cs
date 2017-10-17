namespace Binance.Net.Objects
{
    public class ApiResult<T>
    {
        public bool Success { get; internal set; }
        public T Data { get; internal set; }
        public BinanceError Error { get; internal set; }
    }
}
