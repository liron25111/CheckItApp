using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using CheckItApp.Views;
using Xamarin.Forms;
using System.Threading.Tasks;
using CheckItApp.Services;
using CheckItApp.Models;
using Xamarin.Essentials;
using System.Collections.ObjectModel;

namespace CheckItApp.ViewModels
{
    internal class FormViewModel : BaseViewModel
    {

        private Form currentForm;
        private string formTitle;
        public string FormTitle
        {
            get => formTitle;
            set => SetValue(ref formTitle, value);
        }
        private string formType;
        public string FormType
        {
            get => formType;
            set => SetValue(ref formType, value);
        }
        private string formBody;
        public string FormBody
        {
            get => formBody;
            set => SetValue(ref formBody, value);
        }
        private bool signed;
        public bool Signed
        {
            get => signed;
            set => SetValue(ref signed, value);
        }
        private string sentBy;
        public string SentBy
        {
            get => sentBy;
            set => SetValue(ref sentBy, value);
        }
        
        public ObservableCollection<Class> FormGroups { get; set; }
        public Command ConfirmSignature => new Command(() => { });

        public FormViewModel(Form f)
        {
            currentForm = f;
            formTitle = f.Topic;
            formType = f.FormType;
            formBody = f.MassageBody;
            signed = false;
            FormGroups = new ObservableCollection<Class>();
            GetGroups();
            GetPostedByName();
        }

        public void GetGroups()
        {

        }
        public void GetPostedByName()
        {

        }
    }
}
