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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            ProfilePageViewModel vm = new ProfilePageViewModel(); ;
            vm.Push += (p) => Navigation.PushAsync(p);
            BindingContext = vm;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}