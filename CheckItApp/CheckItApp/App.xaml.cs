using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CheckItApp.Views;

namespace CheckItApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SignUpPage();
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
