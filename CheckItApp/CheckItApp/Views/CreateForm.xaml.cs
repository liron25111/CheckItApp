using CheckItApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateForm : ContentPage
    {
        public CreateForm()
        {
            InitializeComponent();
            this.BindingContext = new CreateFormViewModel();
        }
        protected virtual void OnAppearing()
        {
            ((CreateFormViewModel)this.BindingContext).OnStart(EventArgs.Empty);
        }


    }
}