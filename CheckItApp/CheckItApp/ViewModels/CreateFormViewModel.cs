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
        public CreateFormViewModel()
        {
            rest = new List<Class>();
            Classes = new ObservableCollection<Class>();
            Text = string.Empty;
            this.onStart += async (s, e) =>
            {
                CheckItApi api = CheckItApi.CreateProxy();
                rest = await api.GetClassesAsync();
            };
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
                    OnPropertyChanged();
                    textChanged();
                }
            }
        }
        private List<Class> rest;
        private ObservableCollection<Class> classes;
        public ObservableCollection<Class> Classes
        {
            get
            {
                return classes;
            }
            set
            {
                if(classes != value)
                {
                    classes = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        #region functions
        private void textChanged()
        {
            if(text == string.Empty)
            {
                foreach(Class c in Classes)
                {
                    Classes.Remove(c);
                    rest.Add(c);
                }
            }
            else
            {
                if(lastText == string.Empty)
                {
                    foreach(Class c in rest)
                    {
                        if (c.ClassName.StartsWith(text))
                        {
                            Classes.Add(c);
                            rest.Remove(c);
                        }
                    }
                }
                else
                {
                    if(text.Length > lastText.Length)
                    {
                        foreach (Class c in Classes)
                        {
                            if (!c.ClassName.StartsWith(text))
                            {
                                rest.Add(c);
                                Classes.Remove(c);
                            }
                        }
                    }
                    else
                    {
                        foreach(Class c in rest)
                        {
                            if (c.ClassName.StartsWith(text))
                            {
                                Classes.Add(c);
                                rest.Remove(c);
                            }
                        }
                    }
                }
            }
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
