using System;
using Binance.Net.Enums;
using WpfClient.MVVM;

namespace WpfClient.ViewModels
{
    public class OrderViewModel : ObservableObject
    {
        private long id;
        public long Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChangedEvent("Id");
            }
        }

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

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChangedEvent("Price");
            }
        }

        private decimal originalQuantity;
        public decimal OriginalQuantity
        {
            get { return originalQuantity; }
            set
            {
                originalQuantity = value;
                RaisePropertyChangedEvent("OriginalQuantity");
            }
        }

        private decimal executedQuantity;
        public decimal ExecutedQuantity
        {
            get { return executedQuantity; }
            set
            {
                executedQuantity = value;
                RaisePropertyChangedEvent("ExecutedQuantity");
                RaisePropertyChangedEvent("Fullfilled");
            }
        }

        public string FullFilled
        {
            get { return ExecutedQuantity + "/" + OriginalQuantity; }
        }

        private OrderStatus status;
        public OrderStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChangedEvent("Status");
                RaisePropertyChangedEvent("CanCancel");
            }
        }

        private OrderSide side;
        public OrderSide Side
        {
            get { return side; }
            set
            {
                side = value;
                RaisePropertyChangedEvent("Side");
            }
        }

        private SpotOrderType type;
        public SpotOrderType Type
        {
            get { return type; }
            set
            {
                type = value;
                RaisePropertyChangedEvent("Type");
            }
        }

        private DateTime time;
        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                RaisePropertyChangedEvent("Time");
            }
        }

        public bool CanCancel
        {
            get { return Status == OrderStatus.New || Status == OrderStatus.PartiallyFilled; }
        }
    }
}
