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
using System.Collections.Generic;
using System;
namespace CheckItApp.ViewModels
{
    internal class FormStaffMemberViewModel : BaseViewModel
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
        private int numsigned;
        public int Numsigned
        {
            get => numsigned;
            set
            {
                if (numsigned != value)
                {
                    numsigned = value;
                    OnPropertyChanged();
                }
            }
        }
        private string formBody;
        public string FormBody
        {
            get => formBody;
            set => SetValue(ref formBody, value);
        }
        private int formId;
        public int FormId
        {
            get => formId;
            set => SetValue(ref formId, value);
        }
       
        private string sentBy;
        public string SentBy
        {
            get => sentBy;
            set => SetValue(ref sentBy, value);
        }

        public ObservableCollection<Class> FormGroups { get; set; }

        public event Action<Page> Push;

       
        private CheckItApi proxy;
        public FormStaffMemberViewModel(Form f)
        {
            proxy = CheckItApi.CreateProxy();
            currentForm = f;
            formTitle = f.Topic;
            formType = f.FormType;
            formBody = f.MassageBody;
            formId = f.FormId;
            numsigned = 0;
            FormGroups = new ObservableCollection<Class>();
            GetGroups();
            GetPostedByName();
            SignedEvent += NumSigned;
        }
        public async void SignedEventFunction()
        {
            await SignedEvent();
        }
        public ICommand LoginCommand { get; set; }// login Command
        public delegate Task<int> SignedDelegate();
        public event SignedDelegate SignedEvent;
        public async Task<int> NumSigned()
        {
            int Numsigned = await proxy.GetSigns(formId);
            return Numsigned;
        }
        public async void GetGroups()
        {
            List<Class> groups = await proxy.GetFormGroups(currentForm.FormId);
            foreach (Class c in groups)
                FormGroups.Add(c);
        }
        public async void GetNumSigned()
        {
            int a = currentForm.FormId;
            //int signs = await proxy.GetSigns(a);
        }
        public void GetPostedByName()
        {

        }
    }
}
