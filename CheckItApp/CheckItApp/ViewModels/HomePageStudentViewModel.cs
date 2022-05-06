using CheckItApp.Models;
using CheckItApp.Services;
using CheckItApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CheckItApp.ViewModels
{
    class HomePageStudentViewModel :BaseViewModel
    {
        public event Action<Page> Push;
        public event Action PopToRoot;

        private Account account;

        public Account Account // get Email
        {
            get
            {
                return account;
            }
            set
            {
                if (account != value)
                {
                    account = value;
                    OnPropertyChanged();
                }
            }
        }
        public Command AccountButtonCommand { protected set; get; }
        public Command CreateFormCommand { protected set; get; }
        public Command SendToFormsPageCommand { protected set; get; }
        public Command SendToGroupCommand { protected set; get; }

        public Command LogOutButtonCommand { protected set; get; }


        public ObservableCollection<Form> FormCollection { protected set; get; }
        private CheckItApi proxy;
        public HomePageStudentViewModel()
        {
            Account = ((App)App.Current).CurrentUser;
            AccountButtonCommand = new Command(AccountButton);
            CreateFormCommand = new Command(CreateForm);
            SendToFormsPageCommand = new Command(AllFormsPage);
            SendToGroupCommand = new Command(GroupButtonCommand);
            FormCollection = new ObservableCollection<Form>();

            proxy = CheckItApi.CreateProxy();
            LogOutButtonCommand = new Command(LogOut);

            FillForms();
        }

        public Command<Form> GoToFormView => new Command<Form>((f) => Push?.Invoke(new FormView(f)));
        private async void FillForms()
        {
            FormCollection.Clear();
            List<Form> f = await proxy.GetForms(((App)App.Current).CurrentUser.Id);
            foreach(Form form in f)
            {
                FormCollection.Add(form);
            }
        }
        public void Refresh()
        {
            this.FillForms();
        }
        // פעולות
        private async void LogOut()
        {
            bool b = await proxy.LogOut();
            if (b)
                PopToRoot?.Invoke();
            else
            {

            }
        }
        private void CreateForm()
        {
            Push?.Invoke(new CheckItApp.Views.CreateForm());
        }
        private void AccountButton()
        {
            Push?.Invoke(new CheckItApp.Views.ProfilePage());
        }
        private void GroupButtonCommand()
        {
            Push?.Invoke(new CheckItApp.Views.GroupPage());
        }
        private void AllFormsPage()
        {
            Push?.Invoke(new CheckItApp.Views.AllFormsPage());
        }

        private async void FillFormCollection()
        {
            int clientId = ((App)App.Current).CurrentUser.Id;
            CheckItApi api = CheckItApi.CreateProxy();
            //List<Form> forms = await api.GetForms(clientId);
            List<Form> forms = FillFormsDemo();
            foreach (Form f in forms)
            {
                FormCollection.Add(f);
            }
        }

        private List<Form> FillFormsDemo()
        {
            List<Form> forms = new List<Form>();

            forms.Add(new Form()
            {
                FormType = "Trip",
                Topic = "Checkpoint Trip",
                MassageBody = "Please sign this",
                TripDate = DateTime.Now
            });

            forms.Add(new Form()
            {
                FormType = "Hike",
                Topic = "Ramat HaGolan",
                MassageBody = "Please sign this",
                TripDate = DateTime.Now
            }); 

            forms.Add(new Form()
            {
                FormType = "Fun",
                Topic = "Magic Kass",
                MassageBody = "Please sign this",
                TripDate = DateTime.Now
            });

            return forms;
        }

        private Form form;

        public Form Form
        {
            get
            {
                return form;
            }
            set
            {
                if (form != value)
                {
                    form = value;
                    OnPropertyChanged();
                }
            }

        }
    }
}
