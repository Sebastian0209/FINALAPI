using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Lands.ViewModels;
using Lands.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Lands
{
    using Views;

    public partial class App : Application
    {
        #region Constructors
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Lands = new LandsViewModel();

            this.MainPage = new NavigationPage(new LoginPage());
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
