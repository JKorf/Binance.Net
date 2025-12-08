namespace Binance.Net.Converters
{
    internal class ClientOrderIdReplaceConverter : ReplaceConverter
    {
        public ClientOrderIdReplaceConverter(): base(
            $"{LibraryHelpers.GetClientReference(() => null, "Binance", "Spot")}{LibraryHelpers.ClientOrderIdSeparator}->",
            $"{LibraryHelpers.GetClientReference(() => null, "Binance", "Futures")}{LibraryHelpers.ClientOrderIdSeparator}->")
        {
        }
    }
}
