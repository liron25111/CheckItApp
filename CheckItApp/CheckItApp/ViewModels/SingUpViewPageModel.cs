using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;

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
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateName();
            ValidateSchoolCode();

            //check if any validation failed
            if (
                ShowNameError || ShowSchoolCodeError)
                return false;
            return true;
        }
        public SingUpViewPageModel()

        {
            this.NameError = "זהו שדה חובה";
            this.ShowNameError = false;
            this.SchoolCodeError = "זהו שדה חובה";
            this.ShowSchoolCodeError = false;
            this.SaveDataCommand = new Command(() => SaveData());

        }

        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            if (ValidateForm())
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", "הנתונים נבדקו ונשמרו", "אישור", FlowDirection.RightToLeft);
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);
        }
    }
}
