
namespace Lands.ViewModels
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Services;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    class LandsViewModel: BaseViewModel
    {
        #region Services

        private ApiService apiService;
        #endregion


        #region Attributes

        private ObservableCollection<Land> land;
        private bool isRefreshing;
        #endregion


        #region Properties
        public ObservableCollection<Land> Land
        {
            get { return this.land; }
            set { SetValue(ref this.land, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion



        #region Constructors

        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }

        #endregion


        #region Methods
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                   "Error", connection.Message,
                   "Aceptar");
                return;
                await Application.Current.MainPage.Navigation.PopAsync();
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error", response.Message,
                    "Aceptar");
                return;
            }
            var list = (List<Land>)response.Result;
            this.Land = new ObservableCollection<Land>(list);
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);

            }
        }
        #endregion
    }
}

