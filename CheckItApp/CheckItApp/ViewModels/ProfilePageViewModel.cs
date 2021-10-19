using CheckItApp.Models;
using CheckItApp.Services;
using CheckItApp.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CheckItApp.ViewModels
{
    class ProfilePageViewModel : BaseViewModel

    {
        public event Action<Page> Push;

        private Account account;

        public Account Account // get Email
        {
            get
            {
                return account;
            }
            set
            {
                if (account != value)
                {
                    account = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ProfileToMainApp { get; set; }// login Command
        public ICommand ChangePass { get; set; }
        public ProfilePageViewModel()
        {
            Account = ((App)App.Current).CurrentUser;
            ProfileToMainApp = new Command(PushToApp);
            ChangePass = new Command(ResetPass);
        }
        public void PushToApp()
        {
            Push?.Invoke(new CheckItApp.Views.HomePage());

        }
        public void ResetPass()
        {
            Push?.Invoke(new CheckItApp.Views.ResetPasswordPage());

        }
    }
}
