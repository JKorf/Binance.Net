using System.IO;

namespace Binance.Net.Interfaces
{
    public interface IResponse
    {
        Stream GetResponseStream();
    }
}
