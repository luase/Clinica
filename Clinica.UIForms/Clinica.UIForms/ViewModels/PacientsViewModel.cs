using Clinica.Common.Models;
using Clinica.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Clinica.UIForms.ViewModels
{
    public class PacientsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private ObservableCollection<Pacients> pacients;
        public ObservableCollection<Pacients> Pacients 
        { 
            get { return this.pacients; } 
            set { this.SetValue(ref this.pacients, value); } 
        }

        public PacientsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPacients();
        }

        private async void LoadPacients()
        {
            var response = await this.apiService.GetListAsync<Pacients>(
                "https://webclinica.azurewebsites.net",
                "/api",
                "/Pacients");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var myPacients = (List<Pacients>)response.Result;
            this.Pacients = new ObservableCollection<Pacients>(myPacients);
        }
    }
}
