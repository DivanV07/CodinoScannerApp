using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CodinoScannerApp.Domain;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodinoScannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private IRepositoryInterface _repo;
        private List<Employee> _employees;
        

        public LoginPage()
        {
            InitializeComponent();
            tbxPassword.Text = "";
            tbxUsername.Text = "";
            this.BindingContext = this;
            this.IsBusy = false;
            
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            this.IsBusy = true;

            var ip = Preferences.Get("server_ip", "192.168.2.181");
            _repo = new MongoRepository(ip);

            try
            {
                _employees = await _repo.GetEmployees();
            }
            catch (Exception)
            {
                this.IsBusy = false;
                var newIP = await DisplayPromptAsync("Error connecting to Server", "Please enter server IP");
                Preferences.Set("server_ip", newIP);
                return;
            }

            try
            {
                ///fetch employee from _employees by finding on the email.Text to lowercase
                var username = tbxUsername.Text.ToLower();
                var employee = _employees.Find(emp => emp.EmployeeId == username);
                var password = tbxPassword.Text;


                if (employee != null)
                {
                    var result = BCrypt.Net.BCrypt.Verify(password, employee.Password);

                    ///if login is successful
                    if (result)
                    {
                        tbxPassword.Text = "";
                        tbxUsername.Text = "";
                        _repo.SetLoggedInUser(employee);
                        var mainmenu = new MainMenu(_repo);
                        NavigationPage.SetHasNavigationBar(mainmenu, false);
                        await Navigation.PushAsync(mainmenu);
                        this.IsBusy = false;
                    }

                }
                else
                {
                    throw new Exception("Invalid Username");
                    this.IsBusy = false;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Login Failed!", "Invalid Username or Password", "Try again");
                this.IsBusy = false;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert", "Do you want to exit?", "Yes", "No");
                if (result == true)
                {
                    System.Environment.Exit(0);
                }

            });
            return true;
        }
    }
}