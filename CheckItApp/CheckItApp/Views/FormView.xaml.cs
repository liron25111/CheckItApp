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
            FormViewModel context = new FormViewModel(f);
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}