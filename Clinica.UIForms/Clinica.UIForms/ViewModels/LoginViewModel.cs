
namespace Clinica.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
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
            
            if(!this.Email.Equals("a.lonso@yahoo.com") || !this.Password.Equals("12345"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Incorrect password or email",
                    "Accept"
                    );
                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                "Fuck yeah",
                "Accept"
                );
            return;
        }
    }
}
