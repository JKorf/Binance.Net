using System.Collections.ObjectModel;
using System.Linq;
using Binance.Net.ClientWPF.MVVM;

namespace Binance.Net.ClientWPF.ViewModels
{
    public class BinanceSymbolViewModel: ObservableObject
    {
        private string symbol;
        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                RaisePropertyChangedEvent("Symbol");
            }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChangedEvent("Price");
            }
        }

        private double priceChangePercent;
        public double PriceChangePercent
        {
            get { return priceChangePercent; }
            set
            {
                priceChangePercent = value;
                RaisePropertyChangedEvent("PriceChangePercent");
            }
        }

        private double highPrice;
        public double HighPrice
        {
            get { return highPrice; }
            set
            {
                highPrice = value;
                RaisePropertyChangedEvent("HighPrice");
            }
        }

        private double lowPrice;
        public double LowPrice
        {
            get { return lowPrice; }
            set
            {
                lowPrice = value;
                RaisePropertyChangedEvent("LowPrice");
            }
        }

        private double volume;
        public double Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                RaisePropertyChangedEvent("Volume");
            }
        }

        private double tradeAmount;
        public double TradeAmount
        {
            get { return tradeAmount; }
            set
            {
                tradeAmount = value;
                RaisePropertyChangedEvent("TradeAmount");
            }
        }

        private double tradePrice;
        public double TradePrice
        {
            get { return tradePrice; }
            set
            {
                tradePrice = value;
                RaisePropertyChangedEvent("TradePrice");
            }
        }
        
        private ObservableCollection<OrderViewModel> orders;
        public ObservableCollection<OrderViewModel> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                RaisePropertyChangedEvent("Orders");
            }
        }

        public BinanceSymbolViewModel(string symbol, double price)
        {
            this.symbol = symbol;
            this.price = price;
        }

        public void AddOrder(OrderViewModel order)
        {
            Orders.Add(order);
            Orders.OrderByDescending(o => o.Time);
            RaisePropertyChangedEvent("Orders");
        }
    }
}
