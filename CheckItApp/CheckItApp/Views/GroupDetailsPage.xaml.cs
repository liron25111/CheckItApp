using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckItApp.ViewModels;
using CheckItApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupDetailsPage : ContentPage
    {
        public GroupDetailsPage(Class c)
        {
            InitializeComponent();
            GroupDetailsPgaeViewModel Lvm = new GroupDetailsPgaeViewModel(c);
            BindingContext = Lvm;
            Lvm.Push += (p) => Navigation.PushAsync(p);
        }
    }
}