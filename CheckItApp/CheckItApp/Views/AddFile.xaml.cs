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
    public partial class AddFile : ContentPage
    {
        public AddFile()
        {


            InitializeComponent();
            AddFileViewModel context = new AddFileViewModel();
            this.BindingContext = context;
            context.Push += (p) => Navigation.PushAsync(p);
        }
    }
}