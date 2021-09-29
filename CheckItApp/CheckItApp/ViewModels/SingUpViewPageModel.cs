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
        #region אימייל
        private bool showEmailError;
        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private async void ValidateEmail()
        {
            bool? exists = await proxy.EmailExists(Email);
            if (exists == true)
            {
                this.ShowEmailError = true;
                this.EmailError = "Email address already exists";
            }
            else if (exists == null)
            {
                ShowError = true;
                Error = "Unknown error occured. Try again";
            }
            else
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(Email);
                    this.ShowEmailError = addr.Address != Email;
                }
                catch
                {
                    EmailError = "Invalid email address";
                    this.ShowEmailError = true;
                }
            }
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

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion
        #region קוד בית ספר
        private bool showSchoolCodeError;

        public bool ShowSchoolCodeError
        {
            get => showSchoolCodeError;
            set
            {
                showSchoolCodeError = value;
                OnPropertyChanged("ShowSchoolCodeError");
            }
        }

        private string schoolCode;

        public string SchoolCode
        {
            get => schoolCode;
            set
            {
                schoolCode = value;
                ValidateSchoolCode();
                OnPropertyChanged("SchoolCode");
            }
        }

        private string schoolCodeError;

        public string SchoolCodeError
        {
            get => schoolCodeError;
            set
            {
                schoolCodeError = value;
                OnPropertyChanged("SchoolCodeError");
            }
        }

        private void ValidateSchoolCode()
        {
            this.ShowSchoolCodeError = string.IsNullOrEmpty(schoolCode);
        }
        #endregion
        #region קוד כיתה
        private bool showclassIdError;
        public bool ShowClassIdError
        {
            get => showclassIdError;
            set
            {
                showclassIdError = value;
                OnPropertyChanged("ShowClassIdError");
            }
        }

        private string classid;

        public string ClassId
        {
            get => classid;
            set
            {
                classid = value;
                ValidateClassId();
                OnPropertyChanged("ClassId");
            }
        }

        private string classidError;

        public string ClassIdError
        {
            get => classidError;
            set
            {
                classidError = value;
                OnPropertyChanged("ClassIdError");
            }
        }

        private void ValidateClassId()
        {
            this.ShowClassIdError = string.IsNullOrEmpty(classid);
        }
        #endregion
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateName();
            ValidateSchoolCode();
            ValidateClassId();
            ValidateEmail();
            ValidatePass();
            //check if any validation failed
            if (
                ShowNameError || ShowSchoolCodeError || ShowClassIdError || ShowPassError || ShowEmailError)
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


            this.NameError = "זהו שדה חובה";
            this.ShowNameError = false;

            PassError = "זהו שדה חובה";
            this.ShowPassError = false;

            this.ClassIdError = "זהו שדה חובה";
            this.ShowClassIdError = false;

            this.SchoolCodeError = "זהו שדה חובה";
            this.ShowSchoolCodeError = false;

            this.EmailError = "כתובת אימייל זו אינה תקינה";
            this.ShowEmailError = false;


            RegisterCommand = new Command(Register);
            proxy = CheckItApi.CreateProxy();

        }

        private async void Register()
        {
            if (ValidateForm())
            {
                Account account = await proxy.SignUpAccount(Email, Pass, Name, ClassId, SchoolCode);
                ((App)App.Current).CurrentUser = account;
                Push?.Invoke(new CheckItApp.Views.LoginPage());

            }
             else
            {
                Error = "Something went Wrong";
            }
        }
    }

}
