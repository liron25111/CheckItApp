using CheckItApp.Models;
using CheckItApp.Services;
using CheckItApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CheckItApp.ViewModels
{
     class StudentGroupPageViewModel : BaseViewModel
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
        private CheckItApi proxy;
        public ObservableCollection<Form> GroupCollection { protected set; get; }

        public StudentGroupPageViewModel()
        {
            GroupCollection = new ObservableCollection<Form>();
            ShowGroupCollection();
        }
        private async void ShowGroupCollection()
        {
            proxy = CheckItApi.CreateProxy();
            int clientId = ((App)App.Current).CurrentUser.Id;
           
        }

    }
}
