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
    public partial class FormStaffMember : ContentPage
    {
        public FormStaffMember(Form f)
        {
            FormStaffMemberViewModel context = new FormStaffMemberViewModel(f);
            this.BindingContext = context;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            FormStaffMemberViewModel vm = (FormStaffMemberViewModel)BindingContext;
            vm.SignedEventFunction();
        }
    }
}