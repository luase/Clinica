using Clinica.Common.Models;

namespace Clinica.UIForms.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;

        public TokenResponse Token { get; set; }

        public LoginViewModel Login { get; set; }

        public PacientsViewModel Pacients { get; set; }

        public MainViewModel()
        {
            instance = this;
        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
    }
}
