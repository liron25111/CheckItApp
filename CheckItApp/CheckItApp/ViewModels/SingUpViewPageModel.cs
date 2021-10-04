using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using CheckItApp.Views;
using Xamarin.Forms;
using System.Threading.Tasks;
using CheckItApp.Services;
using CheckItApp.Models;

namespace CheckItApp.ViewModels
{
    class SingUpViewPageModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
     
        #region סיסמה
        private bool showPassError;
        public bool ShowPassError
        {
            get => showPassError;
            set
            {
                showPassError = value;
                OnPropertyChanged("ShowPassError");
            }
        }

        private string pass;

        public string Pass
        {
            get => pass;
            set
            {
                pass = value;
                ValidatePass();
                OnPropertyChanged("Pass");
            }
        }

        private string passError;

        public string PassError
        {
            get => passError;
            set
            {
                passError = value;
                OnPropertyChanged("PassError");
            }
        }

        private void ValidatePass()
        {
            ShowPassError = true;
            if (string.IsNullOrEmpty(Pass))
                PassError = "סיסמה חייבת להכיל תווים";
            else if (Pass.Length < 8)
                PassError = "סיסמה חייבת להכיל 8 תווים לפחות";
            else
                ShowPassError = false;
        }
        #endregion
        #region שם
        private bool showNameError;

        public bool ShowMashovCodeError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string mashovCode;

        public string MashovCode
        {
            get => mashovCode;
            set
            {
                mashovCode = value;
                ValidateMashovCode();
                OnPropertyChanged("MashovCode");
            }
        }

        private string mashovCodeError;

        public string MashovCodeError
        {
            get => mashovCodeError;
            set
            {
                mashovCodeError = value;
                OnPropertyChanged("MashovCodeError");
            }
        }

        private void ValidateMashovCode()
        {
            this.ShowMashovCodeError = string.IsNullOrEmpty(MashovCode);
        }
        #endregion
       
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateMashovCode();
         
            ValidatePass();
            //check if any validation failed
            if (
                ShowMashovCodeError ||  ShowPassError )
                return false;
            return true;
        }
        public ICommand RegisterCommand { get; set; } // RegisterCommand
        public event Action<Page> Push;
        private string error;
        private bool ShowError;
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
                    OnPropertyChanged("Error");
                }
            }
        }
        private CheckItApi proxy;

        public SingUpViewPageModel()

        {

            this.MashovCodeError = "זהו שדה חובה";
            this.ShowMashovCodeError = false;

            PassError = "זהו שדה חובה";
            this.ShowPassError = false;





            //RegisterCommand = new Command(Register);
            proxy = CheckItApi.CreateProxy();

        }

        //private async void Register()
        //{
        //    if (ValidateForm())
        //    {
        //        Account u = new Account() { Pass = pass, MashovCode = MashovCode };
        //        bool t = await proxy.SignUpAccount(u);
        //        if (t)
        //        {
        //            ((App)App.Current).CurrentUser = u;
        //            Push?.Invoke(new CheckItApp.Views.LoginPage());
        //        }

        //    }
        //    else
        //    {
        //        Error = "Something went Wrong";
        //    }
        //}
    }

}
