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
        private IRepositoryInterface _repo;
        

        public MainMenu(IRepositoryInterface repo)
        {
            _repo = repo;
            InitializeComponent();
       
        }

        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var qr = new QRScanner(_repo);
             NavigationPage.SetHasNavigationBar(qr, false);
            await Navigation.PushAsync(qr);
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert", "Do you really want to log out?", "Yes", "No");
                if (result == true)
                {
                    var login = new LoginPage();
                    NavigationPage.SetHasNavigationBar(login, false);
                    await Navigation.PushAsync(login);
                }

            });
            return true;
        }

    }
}