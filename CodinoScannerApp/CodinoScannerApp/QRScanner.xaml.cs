using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodinoScannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanner : ContentPage
    {

        bool bScanned= false;

        public QRScanner()
        {
            InitializeComponent();
            var grid = new Grid();
            var row = new RowDefinition() { Height = new GridLength(120)};
            var row2 = new RowDefinition() { Height = new GridLength(60)};
            var row3 = new RowDefinition() { Height = new GridLength(80)};         
        }

        private void scanView_OnScanResult(ZXing.Result result)
        {
            if (!bScanned)
            {
                bScanned = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string action = await DisplayActionSheet(result.Text, "Cancel", "Update");
                    ResetScanner();
                    if (action == "Update")
                    {
                        string choice = await DisplayActionSheet("You clicked update", "OK", "Close");                       
                    }
                });
            }
            
        }

        private void ResetScanner() 
        {
            Task.Factory.StartNew(() => Thread.Sleep(2 * 1000))
                        .ContinueWith((t) => {
                            bScanned = false;
                        }, TaskScheduler.FromCurrentSynchronizationContext());
        }

    }
}