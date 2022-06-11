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
        private string newpassword;
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

        public string NewPassword // GetPassword
        {
            get
            {
                return newpassword;
            }
            set
            {
                if (newpassword != value)
                {
                    newpassword = value;
                    OnPropertyChanged("NewPassword");
                }
            }
        }
       
        public ICommand ResetPassword { get; set; }// login Command
            
        public ResetPasswordViewModel()
        {
            ResetPassword = new Command(ResetPass);
        }
        private async void ResetPass()
        {
            CheckItApi proxy = CheckItApi.CreateProxy();

            try
            {
                if (((App)App.Current).CurrentUser != null)
                {
                    Account u = await proxy.ResetPassAsync(NewPassword, (((App)App.Current).CurrentUser).Email);
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

                
                else
                {
                    StaffMember u = await proxy.ResetPassStaffMemberAsync(NewPassword, (((App)App.Current).CurrentUser2).Email);
                    if (u != null)
                    {
                        ((App)App.Current).CurrentUser2 = u;
                        Push?.Invoke(new CheckItApp.Views.LoginPage());
                    }
                    else
                    {
                        Error = "This Password Doesn't correctly Please try again";

                    }
                }

            }

            catch (Exception)
            {
                Error = "Something went Wrong";
            }
        }
    }
}

