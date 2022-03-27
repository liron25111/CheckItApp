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
    class LoginPageViewModel : BaseViewModel
    {
        private string email;
        private string password;
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
        private bool isteacher;
        public bool IsTeacher// get eror
        {
            get
            {
                return isteacher;
            }
            set
            {
                if (isteacher != value)
                {
                    isteacher = value;
                    OnPropertyChanged("IsTeacher");
                }
            }
        }


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
        public string Password // GetPassword
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SignUpCommand { get; set; }// login Command
        public ICommand LoginCommand { get; set; }// login Command
        public ICommand ForgotPassCommand { get; set; }// login Command


        public event Action<Page> Push;
        public LoginPageViewModel()
        {
            Error = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            LoginCommand = new Command(Login);
            SignUpCommand = new Command(CreateAccount);
            ForgotPassCommand = new Command(ForgotPass);


        }
        // פעולות
        private void CreateAccount()
        {
            Push?.Invoke(new CheckItApp.Views.SignUpPage());
        }
        private void ForgotPass()
        {
            Push?.Invoke(new CheckItApp.Views.ForgetPassword1());
        }

        private async void Login()
        {
            CheckItApi proxy = CheckItApi.CreateProxy();
            if (isteacher == false)
            {
                try
                {
                    Account u = await proxy.LoginAsync(Email, Password);

                    if (u != null)
                    {
                        ((App)App.Current).CurrentUser = u;
                        Push?.Invoke(new CheckItApp.Views.HomePageStudent());
                    }
                    else
                    {
                        Error = "This Account Doesn't Exist";

                    }
                }

                catch (Exception)
                {
                    Error = "Something went Wrong";
                }
            }
            else
            {
                    try
                    {
                        StaffMember u = await proxy.Login(Email, Password);
                        if (u != null)
                        {
                            ((App)App.Current).CurrentUser2 = u;
                            Push?.Invoke(new CheckItApp.Views.HomePageStaff());
                        }
                        else
                        {
                            Error = "This Account Doesn't Exist";

                        }
                    }

                    catch (Exception)
                    {
                        Error = "Something went Wrong";
                    }
                }

          
        }
    }
}
   

