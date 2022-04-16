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
    public partial class HomePageStudent : ContentPage
    {
        public HomePageStudent()
        {
          
            HomePageStudentViewModel Lvm = new HomePageStudentViewModel();
            BindingContext = Lvm;
            Lvm.Push += (p) => Navigation.PushAsync(p);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            HomePageStudentViewModel vm = (HomePageStudentViewModel)BindingContext;
            vm.Refresh();
        }
    }
}