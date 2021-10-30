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
    class ResetPasswordViewModel : BaseViewModel
    {
        private string Newpassword;
        public event Action<Page> Push;
        private string error;

        public string Error// get eror
        {
            get
            {
                return error;
            }
            set
            {
                if (error != value)
                {
                    error = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password // GetPassword
        {
            get
            {
                return Newpassword;
            }
            set
            {
                if (Newpassword != value)
                {
                    Newpassword = value;
                    OnPropertyChanged();
                }
            }
        }
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
        public ICommand ResetPassword { get; set; }// login Command
            
        public ResetPasswordViewModel()
        {
            Account = ((App)App.Current).CurrentUser;
            ResetPassword = new Command(ResetPass);
        }
        private async void ResetPass()
        {
            CheckItApi proxy = CheckItApi.CreateProxy();

            try
            {
                Account u = await proxy.ResetPassAsync(Newpassword);
                if (u != null)
                {
                    ((App)App.Current).CurrentUser = u;
                    Push?.Invoke(new CheckItApp.Views.LoginPage());
                }
                else
                {
                    Error = "This Password Doesn't correctly Please try again";

                }
            }

            catch (Exception)
            {
                Error = "Something went Wrong";
            }
        }
    }
}

