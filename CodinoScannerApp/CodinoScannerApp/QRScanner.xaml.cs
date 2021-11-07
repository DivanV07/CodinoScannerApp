using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodinoScannerApp.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace CodinoScannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanner : ContentPage
    {
        bool bScanned = true;
        private IRepositoryInterface _repo;


        public QRScanner(IRepositoryInterface repo)
        {
            InitializeComponent();
            var grid = new Grid();
            var row = new RowDefinition() { Height = new GridLength(120) };
            var row2 = new RowDefinition() { Height = new GridLength(60) };
            var row3 = new RowDefinition() { Height = new GridLength(80) };

            bScanned = true;
            scanView.IsAnalyzing = true;
            _repo = repo;
        }


        private void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (bScanned)
                {
                    bScanned = false;
                    scanView.IsAnalyzing = false;
                    if (!String.IsNullOrWhiteSpace(result.Text))
                    {
                        bool validScan = false;
                        Document doc = null;
                        try
                        {
                            doc = await _repo.GetDocument(result.Text);
                            if (doc == null)
                            {
                                throw new Exception("doc not found");
                            }
                            validScan = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            await DisplayAlert("Error!", "Document not found, please try again.", "Ok");
                            bScanned = true;
                            scanView.IsAnalyzing = true;
                        }

                        if (validScan)
                        {
                            if (doc != null)
                            {
                                var update = new UpdatePage(_repo, result.Text);
                                update.PrepareWindow(doc);
                                NavigationPage.SetHasNavigationBar(update, false);
                                await Navigation.PushAsync(update);
                            }
                        }
                    }
                }
            });
        }


        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}