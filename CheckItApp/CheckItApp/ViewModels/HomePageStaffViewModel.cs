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
    class HomePageStaffViewModel : BaseViewModel
    {
        public event Action<Page> Push;

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

        public ObservableCollection<Form> FormCollection { protected set; get; }

        public HomePageStaffViewModel()
        {
            AccountButtonCommand = new Command(AccountButton);
            CreateFormCommand = new Command(CreateForm);
            AddFilePageCommand = new Command(AddFilePage);
            FormCollection = new ObservableCollection<Form>();
            FillFormCollection();
        }

        // פעולות
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

        private async void FillFormCollection()
        {
            int clientId = ((App)App.Current).CurrentUser2.Id;
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
                Time = DateTime.Now.TimeOfDay
            });

            forms.Add(new Form()
            {
                FormType = "Hike",
                Topic = "Ramat HaGolan",
                MassageBody = "Please sign this",
                Time = DateTime.Now.TimeOfDay
            });

            forms.Add(new Form()
            {
                FormType = "Fun",
                Topic = "Magic Kass",
                MassageBody = "Please sign this",
                Time = DateTime.Now.TimeOfDay
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
