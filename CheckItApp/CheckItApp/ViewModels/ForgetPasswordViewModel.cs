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
    class ForgetPasswordViewModel : BaseViewModel
    {
        private string email;

        public string Email // get Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }
        public Command ForgotPasswordCommand { protected set; get; }

        public ForgetPasswordViewModel()
        {
            ForgotPasswordCommand = new Command(ForgotPassword);

        }
        public event Action<Page> Push;

        private async void ForgotPassword()
        {
            CheckItApi proxy = CheckItApi.CreateProxy();

            bool b = await proxy.ForgotPassword(Email);
            Push?.Invoke(new CheckItApp.Views.LoginPage());

        }

    }
}
