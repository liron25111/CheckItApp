using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using CheckItApp.Views;
using Xamarin.Forms;
using System.Threading.Tasks;
using CheckItApp.Services;
using CheckItApp.DTO;
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
        private string signedStudents;
        public string SignedStudents
        {
            get { return signedStudents; }
            set
            {
                if (signedStudents != value)
                {
                    signedStudents = value;
                    OnPropertyChanged();
                }
            }
        }
        private string unsignedStudents;
        public string UnsignedStudents
        {
            get { return unsignedStudents; }
            set
            {
                if (unsignedStudents != value)
                {
                    unsignedStudents = value;
                    OnPropertyChanged();
                }
            }
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
            FormGroups = new ObservableCollection<Class>();
            GetGroups();
            GetPostedByName();
            SignedStudentsFunc();
            SignedEvent += NumSigned;
            Alert = new Command(ALertFunc);
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
            Numsigned = await proxy.GetSigns(formId);
            return Numsigned;
        }
        public async void SignedStudentsFunc()
        {
            List<StudentSignDTO> signedPeople = await proxy.GetSignedStudentsInForm(formId);
            SignedStudents = "Signed Students:";
            UnsignedStudents = "Unsigned Students:";
            int signedStudens = 0;
            int unsignedStudens = 0;

            foreach (StudentSignDTO s in signedPeople)
            {
                if (s.signed)
                {
                    SignedStudents += $"{s.s.Name},";
                    signedStudens++;
                }
                else
                {
                    UnsignedStudents += $"{s.s.Name},";
                    unsignedStudens++;
                }
            }
            if (signedStudens == 0)
            {
                SignedStudents += "None";
            }
            else
            {
                SignedStudents = SignedStudents.Substring(0, SignedStudents.Length - 1);
            }
            if (unsignedStudens == 0)
            {
                UnsignedStudents += "None";
            }
            else
            {
                UnsignedStudents = UnsignedStudents.Substring(0, UnsignedStudents.Length - 1);
            }
        }
        public async void GetGroups()
        {
            List<Class> groups = await proxy.GetFormGroups(currentForm.FormId);
            foreach (Class c in groups)
                FormGroups.Add(c);
        }
        //public async void GetNumSigned()
        //{
        //    int a = currentForm.FormId;
        //    //int signs = await proxy.GetSigns(a);
        //}
        public ICommand Alert { get; set; }

        private async void ALertFunc()
        {
            await proxy.AlertAsync(formId);
        }

        public void GetPostedByName()
        {

        }
    }
}
