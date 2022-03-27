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
    class ProfilePageStaffViewModel : BaseViewModel

    {
        public event Action<Page> Push;

        private StaffMember account;

        public StaffMember Account // get Email
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
        public ProfilePageStaffViewModel()
        {
            Account = ((App)App.Current).CurrentUser2;
            ProfileToMainApp = new Command(PushToApp);
            ChangePass = new Command(ResetPass);
        }

        StaffMember u = ((App)App.Current).CurrentUser2;


        public void PushToApp()
        {
                Push?.Invoke(new CheckItApp.Views.HomePageStaff());
        }


        public void ResetPass()
        {
            Push?.Invoke(new CheckItApp.Views.ResetPasswordPage());

        }
    }
}
