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
        private bool isRefreshing;

        public ObservableCollection<Pacients> Pacients
        {
            get { return this.pacients; }
            set { this.SetValue(ref this.pacients, value); }
        }

        public bool IsRefreshing 
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public PacientsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPacients();
        }

        private async void LoadPacients()
        {
            this.IsRefreshing = true;

            var response = await this.apiService.GetListAsync<Pacients>(
                "https://webclinica.azurewebsites.net",
                "/api",
                "/Pacients");

            this.IsRefreshing = false;


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
