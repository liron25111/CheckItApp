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
using Xamarin.Essentials;
using System.Collections.ObjectModel;

namespace CheckItApp.ViewModels
{
    class CreateFormViewModel : BaseViewModel
    {
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
                    OnPropertyChanged("Error");
                }
            }
        }
        public event Action<Page> Push;

        public Command AddFileCommand => new Command(() => AddFile());
        public async Task<FileResult> AddFile()
        {
            var customFileType =
            new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "*.docx" } }, // or general UTType values
                { DevicePlatform.Android, new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };

            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    CheckItApi proxy = CheckItApi.CreateProxy();
                    bool uploadFileSuccess = await proxy.UploadFile(result.FullPath, result.FileName);
                    //if (uploadFileSuccess)
                    //Account.ProfilePicture = $"{Account.AccountId}.jpg";
                    //Text = $"File Name: {result.FileName}";
                    //if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    //    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    var stream = await result.OpenReadAsync();
                    //    Image = ImageSource.FromStream(() => stream);
                    //}
                }

                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }

        public Command ClassSelectedCommand => new Command(ClassSelected);
        private void ClassSelected()
        {
            if (SelectedClass != null)
            {
                SelectedClasses.Add(SelectedClass);
                Classes.Remove(SelectedClass);
                SelectedClass = null;
            }
            
        }

        private Class selectedClass;
        public Class SelectedClass
        {
            get { return selectedClass; }
            set
            {
                selectedClass = value;
                Text = String.Empty;
                OnPropertyChanged("SelectedClass");
            }
        }

        private DateTime tripdate;
        public DateTime TripDate
        {
            get { return tripdate; }
            set
            {
                tripdate = value;
                OnPropertyChanged("TripDate");
            }
        }

        private string formtype;
        public string FormType // get FormType
        {
            get
            {
                return formtype;
            }
            set
            {
                if (formtype != value)
                {
                    formtype = value;
                    OnPropertyChanged("FormType");
                }
            }
        }

        private string formsubject;

        public string FormSubject // get FormType
        {
            get
            {
                return formsubject;
            }
            set
            {
                if (formsubject != value)
                {
                    formsubject = value;
                    OnPropertyChanged("FormSubject");
                }
            }
        }

        private string formtext;

        public string FormText // get FormType
        {
            get
            {
                return formtext;
            }
            set
            {
                if (formtext != value)
                {
                    formtext = value;
                    OnPropertyChanged("FormText");
                }
            }
        }

        private bool collectionViewIsVisibleToggle;
        public bool CollectionViewIsVisibleToggle
        {
            get { return collectionViewIsVisibleToggle; }
            set
            {
                collectionViewIsVisibleToggle = value;
                OnPropertyChanged("CollectionViewIsVisibleToggle");
            }
        }
        private CheckItApi proxy;
        public CreateFormViewModel()
        {
            CollectionViewIsVisibleToggle = false;
            proxy = CheckItApi.CreateProxy();
            rest = new List<Class>();
            Classes = new ObservableCollection<Class>();
            SelectedClasses = new ObservableCollection<Class>();
            Text = string.Empty;

            TripDate = DateTime.Now;
            LoadClasses();
        }
        private async void LoadClasses()
        {
            CheckItApi api = CheckItApi.CreateProxy();
            rest = await api.GetClassesAsync();
        }

        public Command ConfirmForm => new Command(SendForm);
        private async void SendForm()
        {
            bool isValid = true;
            if(FormText.Length == 0)
            {
                isValid = false;
                await App.Current.MainPage.DisplayAlert("Invalid Form", "The form has to contain a body", "OK");
            }
            if(SelectedClasses.Count == 0)
            {
                isValid=false;
                await App.Current.MainPage.DisplayAlert("Invalid Form", "The form has to have at least one recipient", "OK");
            }
            if(FormSubject == "")
            {
                isValid = false;
                await App.Current.MainPage.DisplayAlert("Invalid Form", "The form has to have a subject", "OK");
            }
            if(FormType == "")
            {
                isValid = false;
                await App.Current.MainPage.DisplayAlert("Invalid Form", "The form has to be of some type", "OK");
            }


            if (isValid)
            {
                List<int> classes = new List<int>();
                Form f = new Form()
                {
                    FormType = this.FormType,
                    MassageBody = this.FormText,
                    Topic = this.FormSubject,
                    TripDate = this.TripDate,
                    StatusOfTheMessage = 0,
                    SentByStaffMemebr = ((App)App.Current).CurrentUser2.Id
                };
                foreach (Class c in SelectedClasses)
                {
                    
                    classes.Add(c.GroupId);
                }
                bool result = await proxy.PostForms(f, classes);
                if (result)
                {
                    await App.Current.MainPage.DisplayAlert("Form Sent!", "The form has been sent to the selected recipients", "OK");
                    Push?.Invoke(new CheckItApp.Views.HomePageStaff());
                }
            }
                
        }
        
        #region recipents
        #region properties
        private string lastText;
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    lastText = text;
                    text = value;
                    if (text.Length > 0)
                        CollectionViewIsVisibleToggle = true;
                    else
                        CollectionViewIsVisibleToggle = false;
                    OnPropertyChanged("Text");
                    textChanged();
                }
            }
        }
        private List<Class> rest;
        public ObservableCollection<Class> Classes { get; set; }
        public ObservableCollection<Class> SelectedClasses { get; set; }
        #endregion
        #region functions
        private void textChanged()
        {
            if(text == string.Empty)
            {
                foreach(Class c in Classes)
                {
                    rest.Add(c);
                }
                foreach (Class c in rest)
                    Classes.Remove(c);
            }
            else
            {
                if(lastText == string.Empty)
                {
                    foreach(Class c in rest)
                    {
                        if (c.ClassName.ToLower().StartsWith(text.ToLower()))
                        {
                            Classes.Add(c);
                        }
                    }
                    foreach (Class c in Classes)
                        rest.Remove(c);
                }
                else
                {
                    if(text.Length > lastText.Length)
                    {
                        foreach (Class c in Classes)
                        {
                            if (!c.ClassName.ToLower().StartsWith(text.ToLower()))
                            {
                                rest.Add(c);

                            }
                        }
                        foreach (Class c in rest)
                            Classes.Remove(c);
                    }
                    else
                    {
                        foreach(Class c in rest)
                        {
                            if (c.ClassName.ToLower().StartsWith(text.ToLower()))
                            {
                                Classes.Add(c);
                            }
                        }
                        foreach (Class c in Classes)
                            rest.Remove(c);
                    }
                }
            }
            
        }
        public Command<Class> DeleteCommand => new Command<Class>(DeleteCommandMethod);
        private void DeleteCommandMethod(Class c)
        {
            SelectedClasses.Remove(c);
            rest.Add(c);
            textChanged();
        }
        public virtual void OnStart(EventArgs e)
        {
            onStart?.Invoke(this, e);
        }
        #endregion
        #region events
        private event EventHandler onStart;
        #endregion
        #endregion
    }
}
