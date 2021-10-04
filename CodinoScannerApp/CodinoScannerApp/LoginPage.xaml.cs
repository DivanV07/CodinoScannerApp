using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CodinoScannerApp.Domain;
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
            //Todo Need to add a way for users to set the IP here --- Shared preferences access / cfg file / i have no idea
            _repo = new MongoRepository("localhost");
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                _employees = await _repo.GetEmployees();
            }
            catch (Exception)
            {
                //Todo -- Add server IP method change here.
                // //Dialog host for changing server IP is displayed here
                // DialogHost.Close("LoginDialog", null);
                // //Wait for user to finish setting new IP
                // dhIpChange.IsOpen = true;
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
                        _repo.SetLoggedInUser(employee);
                        var mainmenu = new MainMenu(_repo);
                        NavigationPage.SetHasNavigationBar(mainmenu, false);
                        await Navigation.PushAsync(mainmenu);
                    }

                }
                else
                {
                    throw new Exception("Invalid Username");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Login Failed!", "Invalid Username or Password", "Try again");
            }
        }
    }
}