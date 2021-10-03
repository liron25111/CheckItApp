using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CheckItApp.ViewModels;

namespace CheckItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            this.BindingContext = new SingUpViewPageModel();
            SingUpViewPageModel Rgp = new SingUpViewPageModel();
            BindingContext = Rgp;
            Rgp.Push += (p) => Navigation.PushAsync(p);
        }

       
    }
}

