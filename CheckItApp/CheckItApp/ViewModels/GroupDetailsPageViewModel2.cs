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
    class GroupDetailsPgaeViewModel2 : BaseViewModel
    {
        public event Action<Page> Push;
        private CheckItApi proxy;
        private Class group;
        public ObservableCollection<Student> GroupCollection { protected set; get; }
        public GroupDetailsPgaeViewModel2(Class c)
        {
            group = c;
            GroupCollection = new ObservableCollection<Student>();
            ShowGroupCollection();
        }
        private async void ShowGroupCollection()
        {
            proxy = CheckItApi.CreateProxy();
            int StaffId = ((App)App.Current).CurrentUser2.Id;
            List<Student> students = await proxy.GetStudentsInGroup(group.GroupId);
            foreach (Student s in students)
            {
                GroupCollection.Add(s);
            }

        }

    }
}
