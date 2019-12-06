
namespace Clinica.UIForms.ViewModels
{
    using Clinica.Common.Models;
    using Clinica.Common.Services;
    using Clinica.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.Email = "a.lonso@yahoo.com";
            this.Password = "12345";
        }

        private async void Login()
        {
            if(string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email",
                    "Accept"
                    );
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password",
                    "Accept"
                    );
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email or password incorrect.", "Accept");
                return;
            }

            var token = (TokenResponse)response.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Pacients = new PacientsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PacientsPage());


            //if(!this.Email.Equals("a.lonso@yahoo.com") || !this.Password.Equals("12345"))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        "Error",
            //        "Incorrect password or email",
            //        "Accept"
            //        );
            //    return;
            //}

            ////await Application.Current.MainPage.DisplayAlert(
            ////    "Ok",
            ////    "Fuck yeah",
            ////    "Accept"
            ////    );
            ////return;
            //MainViewModel.GetInstance().Pacients = new PacientsViewModel();
            //await Application.Current.MainPage.Navigation.PushAsync(new PacientsPage());
        }
    }
}
