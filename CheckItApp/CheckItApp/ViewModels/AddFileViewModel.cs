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
    class AddFileViewModel : BaseViewModel
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
        public ICommand AddFileCommand { get; set; }// login Command
        public void AddFile()
        {
         
                        
        }
        public AddFileViewModel()
        {
            AddFileCommand = new Command(AddFile);

        }
    }
}
