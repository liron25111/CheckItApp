using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CheckItApp.Services;
using CheckItApp.Models;
using System.Threading.Tasks;
using CheckItApp.Views;

namespace CheckItApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv { get; internal set; }
        public Account CurrentUser { get; set; }

        public App()
        {
            IsDevEnv = true;

            InitializeComponent();

            Sharpnado.CollectionView.Initializer.Initialize(true, false);

            MainPage = new NavigationPage(new LoginPage());
            CurrentUser = null;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
