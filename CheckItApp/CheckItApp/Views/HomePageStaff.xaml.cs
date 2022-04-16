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
    public partial class HomePageStaff : ContentPage
    {
        public HomePageStaff()
        {
            InitializeComponent();
            HomePageStaffViewModel Lvm = new HomePageStaffViewModel();
            BindingContext = Lvm;
            Lvm.Push += (p) => Navigation.PushAsync(p);
        }

        protected override void OnAppearing()
        {
            HomePageStaffViewModel vm = (HomePageStaffViewModel)BindingContext;
            vm.Refresh();
        }
    }
}