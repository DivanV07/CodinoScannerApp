using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodinoScannerApp.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodinoScannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        //todo setup this stuff properly later
        
        public MainMenu()
        {
            InitializeComponent();
       
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
        IRepositoryInterface repo = new MongoRepository("localhost");
        var qr = new QRScanner(repo);
            NavigationPage.SetHasNavigationBar(qr, false);
            await Navigation.PushAsync(qr);
        }
    }
}