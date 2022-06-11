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
using CheckItApp.DTO;

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
        private int numsigned;
        public int Numsigned
        {
            get => numsigned;
            set
            {
                if(numsigned != value)
                {
                    numsigned = value;
                    OnPropertyChanged("Numsigned");
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
        private string signedStudents;
        public string SignedStudents
        {
            get { return signedStudents; }
            set
            {
                if(signedStudents != value)
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

        public Command ConfirmSignature => new Command(async () => {if (signed == true)
            {
                int clientId = ((App)App.Current).CurrentUser.Id;
                bool IsSinged = await proxy.IsSinged(FormId, clientId);
                Push?.Invoke(new CheckItApp.Views.HomePageStudent());
            }
        });
        private CheckItApi proxy;
        public FormViewModel(Form f)
        {
            proxy = CheckItApi.CreateProxy();
            currentForm = f;
            formTitle = f.Topic;
            formType = f.FormType;
            formBody = f.MassageBody;
            signed = false;
            formId = f.FormId;
            FormGroups = new ObservableCollection<Class>();
            GetGroups();
            GetPostedByName();
            SignedStudentsFunc();
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

            foreach (StudentSignDTO studentSign in signedPeople)
            {
                if (studentSign.signed)
                {
                    SignedStudents += $"{studentSign.s.Name},";
                    signedStudens++;
                }
                else
                {
                    UnsignedStudents += $"{studentSign.s.Name},";
                    unsignedStudens++;
                }
            }
            if(signedStudens == 0)
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
