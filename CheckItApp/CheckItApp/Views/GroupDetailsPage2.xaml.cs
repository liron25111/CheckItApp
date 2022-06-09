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
    public partial class GroupDetailsPage2 : ContentPage
    {
        public GroupDetailsPage2(Class c)
        {
            InitializeComponent();
            GroupDetailsPgaeViewModel2 Lvm = new GroupDetailsPgaeViewModel2(c);
            BindingContext = Lvm;
            Lvm.Push += (p) => Navigation.PushAsync(p);
        }
    }
}