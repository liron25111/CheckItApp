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
     class StaffGroupPage : BaseViewModel
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
        private Class group;

        public Class Class
        {
            get
            {
                return group;
            }
            set
            {
                if (group != value)
                {
                    group = value;
                    OnPropertyChanged("Class");
                }
            }
        }

        private CheckItApi proxy;
        public ObservableCollection<Class> GroupCollection { protected set; get; }

        public StaffGroupPage()
        {
            GroupCollection = new ObservableCollection<Class>();
            ShowGroupCollection();
        }
        private async void ShowGroupCollection()
        {
            proxy = CheckItApi.CreateProxy();
            int StaffId = ((App)App.Current).CurrentUser2.Id;
            List<Class> classes = await proxy.GetClass2(StaffId);
            foreach(Class c in classes)
            {
                GroupCollection.Add(c);
            }

        }

    }
}

