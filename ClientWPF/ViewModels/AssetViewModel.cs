using Binance.Net.ClientWPF.MVVM;

namespace Binance.Net.ClientWPF.ViewModels
{
    public class AssetViewModel: ObservableObject
    {
        private string asset;
        public string Asset
        {
            get { return asset; }
            set
            {
                asset = value;
                RaisePropertyChangedEvent("Asset");
            }
        }

        private double free;
        public double Free
        {
            get { return free; }
            set
            {
                free = value;
                RaisePropertyChangedEvent("Free");
            }
        }

        private double locked;
        public double Locked
        {
            get { return locked; }
            set
            {
                locked = value;
                RaisePropertyChangedEvent("Locked");
            }
        }
    }
}
