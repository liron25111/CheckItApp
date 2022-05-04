using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckItApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPageStaff : ContentPage
    {
        public GroupPageStaff()
        {
            InitializeComponent();
            StaffGroupPage Lvm = new StaffGroupPage();
            BindingContext = Lvm;
            Lvm.Push += (p) => Navigation.PushAsync(p);
        }
    }
}