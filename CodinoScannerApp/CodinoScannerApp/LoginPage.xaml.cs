﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodinoScannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "Log in complete", "Continue");
            var mainmenu = new MainMenu();
            NavigationPage.SetHasNavigationBar(mainmenu, false);
            await Navigation.PushAsync(mainmenu);
        }
    }
}