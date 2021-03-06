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

namespace CheckItApp.ViewModels
{
    class AddFileViewModel : BaseViewModel
    {
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
                    OnPropertyChanged("Error");
                }
            }
        }
        private bool showgif;
        public bool ShowGif// get eror
        {
            get
            {
                return showgif;
            }
            set
            {
                if (showgif != value)
                {
                    showgif = value;
                    OnPropertyChanged("ShowGif");
                }
            }
        }
         

        public Command AddFileCommand => new Command(() => AddFile());
        public async Task<FileResult> AddFile()
        {
            ShowGif = true;
            var customFileType =
            new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "*.xlsx" } }, // or general UTType values
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
                    if(uploadFileSuccess)
                    {
                        await App.Current.MainPage.DisplayAlert("Upload Success", "Your file has been successfully uploaded to the server!", "Ok");
                        Push?.Invoke(new CheckItApp.Views.HomePageStaff());

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Upload Failed", "There was a problem uploading your file, try again later", "Ok");

                    }
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

        public AddFileViewModel()
        {
            showgif = false;
        }
        
    }
}
