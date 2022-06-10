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

        Account a = ((App) App.Current).CurrentUser;
        StaffMember u = ((App) App.Current).CurrentUser2;

       
       public void PushToApp()
        {
            if(a!=null)
            {
                Push?.Invoke(new CheckItApp.Views.HomePageStudent());
            }
            else if(u!= null)
            {
                Push?.Invoke(new CheckItApp.Views.HomePageStaff());

            }


        }
    
        
        public void ResetPass()
        {
            Push?.Invoke(new CheckItApp.Views.ResetPasswordPage());

        }
    }
}
