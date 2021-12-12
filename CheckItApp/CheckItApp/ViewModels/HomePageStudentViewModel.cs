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
    class HomePageStudentViewModel :BaseViewModel
    {
        public event Action<Page> Push;

        private Account account;

        public Account Account // get Email
        {
            get
            {
                return account;
            }
            set
            {
                if (account != value)
                {
                    account = value;
                    OnPropertyChanged();
                }
            }
        }
        public Command AccountButtonCommand { protected set; get; }
        public Command CreateFormCommand { protected set; get; }
        public Command SendToFormsPageCommand { protected set; get; }


        public HomePageStudentViewModel()
        {
            AccountButtonCommand = new Command(AccountButton);
            CreateFormCommand = new Command(CreateForm);
            SendToFormsPageCommand = new Command(AllFormsPage);


        }
        // פעולות
        private void CreateForm()
        {
            Push?.Invoke(new CheckItApp.Views.CreateForm());
        }
        private void AccountButton()
        {
            Push?.Invoke(new CheckItApp.Views.ProfilePage());
        }
        private void AllFormsPage()
        {
            Push?.Invoke(new CheckItApp.Views.AllFormsPage());
        }


        private Form form;

        public Form Form
        {
            get
            {
                return form;
            }
            set
            {
                if (form != value)
                {
                    form = value;
                    OnPropertyChanged();
                }
            }

        }
    }
}
