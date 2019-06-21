﻿using System;
using System.Collections.Generic;
using System.Text;


namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Services;
    using Xamarin.Forms;
    using Views;

    class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        #endregion

        #region Attributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;

        #endregion


        #region Properties

        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRemembered
        {
            get; set;
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }



        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Introduzca su correo",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Introduzca su password",
                    "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   connection.Message,
                   "Aceptar");
                return;

            }

            var token = await this.apiService.GetToken(
                "https://productosi220.azurewebsites.net",
                this.Email,
                this.Password);

            if(token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Error de token intentelo mas tarde",
                   "Aceptar");
                return;

            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                    token.ErrorDescription,
                   "Aceptar");
                this.Password = string.Empty;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

     


        }
        #endregion  

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();

            this.IsRemembered = true;
            this.IsEnabled = true;
       

        }
        #endregion
    }
}
