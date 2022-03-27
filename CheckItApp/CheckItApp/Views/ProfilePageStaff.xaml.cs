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
    public partial class ProfilePageStaff : ContentPage
    {
        public ProfilePageStaff()
        {
            InitializeComponent();
            ProfilePageStaffViewModel vm = new ProfilePageStaffViewModel(); ;
            vm.Push += (p) => Navigation.PushAsync(p);
            BindingContext = vm;
        }
    }
}