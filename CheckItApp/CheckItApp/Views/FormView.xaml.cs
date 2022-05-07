using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckItApp.Models;
using CheckItApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormView : ContentPage
    {
        public FormView(Form f)
        {
            InitializeComponent();

            FormViewModel context = new FormViewModel(f);
            this.BindingContext = context;
            context.Push += (p) => Navigation.PushAsync(p);

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            FormViewModel vm = (FormViewModel)BindingContext;
            vm.SignedEventFunction();
        }
    }
}