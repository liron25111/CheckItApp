using CheckItApp.Models;
using CheckItApp.Services;
using CheckItApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace CheckItApp.ViewModels
{
    class HomePageStaffViewModel : BaseViewModel
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
        public Command AddFilePageCommand { protected set; get; }
        public Command SendToGroupCommand { protected set; get; }
        public Command LogOutButtonCommand { protected set; get; }

        public Command<Form> GoToFormView => new Command<Form>((f) => Push?.Invoke(new FormStaffMember(f)));
        public ObservableCollection<Form> FormCollection { protected set; get; }

        public HomePageStaffViewModel()
        {
            proxy = CheckItApi.CreateProxy();
            AccountButtonCommand = new Command(AccountButton);
            CreateFormCommand = new Command(CreateForm);
            AddFilePageCommand = new Command(AddFilePage);
            FormCollection = new ObservableCollection<Form>();
            SendToGroupCommand = new Command(GroupButtonCommand);
            LogOutButtonCommand = new Command(LogOut);
            FillFormCollection();
        }

        // פעולות
        private async void LogOut()
        {
            bool b = await proxy.LogOut();
            if (b)
                PopAllTo(new LoginPage());
            else
            {

            }
        }
        public async void PopAllTo(Page page) // clear navigation stack and goes to page
        {
            App.Current.MainPage.Navigation.InsertPageBefore(page, App.Current.MainPage.Navigation.NavigationStack.First());
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }
        private void CreateForm()
        {
            Push?.Invoke(new CheckItApp.Views.CreateForm());
        }
        private void AccountButton()
        {
            Push?.Invoke(new CheckItApp.Views.ProfilePageStaff());
        }
        private void AddFilePage()
        {
            Push?.Invoke(new CheckItApp.Views.AddFile());
        }
        private void GroupButtonCommand()
        {
            Push?.Invoke(new CheckItApp.Views.GroupPageStaff());
        }
        private CheckItApi proxy;
        private async void FillFormCollection()
        {
            proxy = CheckItApi.CreateProxy();
            int clientId = ((App)App.Current).CurrentUser2.Id;
            //List<Form> forms = FillFormsDemo();
            //foreach(Form form in forms)
            //    FormCollection.Add(form);
        }

        private async void FillForms()
        {
            FormCollection.Clear();
            List<Form> teachersForms = await proxy.GetForms(((App)App.Current).CurrentUser2.Id);
            if(teachersForms != null && teachersForms.Count > 0)
            {
                foreach (Form f in teachersForms)
                {
                    FormCollection.Add(f);
                }
            }
            
        }
        public void Refresh()
        {
            FillForms();
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
