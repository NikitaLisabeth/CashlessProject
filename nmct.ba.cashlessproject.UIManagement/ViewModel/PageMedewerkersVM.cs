using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.UIManagement.ViewModel
{
    class PageMedewerkersVM : ObservableObject, IPage
    {
         public PageMedewerkersVM()
        {
            if (ApplicationVM.token != null)
            {
                GetMedewerkers();
            }
            
        }
        public string Name
        {
            get { return "Medewerker"; }
        }

        private ObservableCollection<Employee> _medewerkers;

        public ObservableCollection<Employee> Medewerkers
        {
            get { return _medewerkers; }
            set { _medewerkers = value; OnPropertyChanged("Medewerkers"); }
        }

        private async void GetMedewerkers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/medewerker");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Medewerkers = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                }
            }
        }

        private Employee _selectedMedewerker;
        public Employee SelectedMedewerker
        {
            get { return _selectedMedewerker; }
            set { _selectedMedewerker = value; OnPropertyChanged("SelectedMedewerker"); }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }

        private async void UpdateMedewerker()
        {
            using (HttpClient client = new HttpClient())
            {
                Employee emp = SelectedMedewerker;
                string input = JsonConvert.SerializeObject(emp);
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.PutAsync("http://localhost:1817/api/medewerker", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    GetMedewerkers();
                }
            }
        }

        public async void AddMedewerker()
        {
            Employee newEmployee = SelectedMedewerker;
            using (HttpClient client = new HttpClient())
            {
                string emp = JsonConvert.SerializeObject(newEmployee);
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.PostAsync("http://localhost:1817/api/medewerker", new StringContent(emp, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    GetMedewerkers();
                }
            }
        }

        public async void DeleteMedewerker()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:1817/api/medewerker/"+ SelectedMedewerker.Id);
                if (response.IsSuccessStatusCode)
                {
                    GetMedewerkers();
                }
            }
        }

        public void SetStatusToUpdate()
        {
            Status = "Update";
        }

        public ICommand SetStatusToUpdateCommand
        {
            get { return new RelayCommand(SetStatusToUpdate); }
        }
        public ICommand DeleteEmployeeCommand
        {
            get { return new RelayCommand(DeleteMedewerker); }
        }
        public void SetStatusToAdd()
        {
            Employee emp = new Employee();
            Medewerkers.Add(emp);
            SelectedMedewerker = emp;

            Status = "Add";
        }

        public ICommand SetStatusToAddCommand
        {
            get { return new RelayCommand(SetStatusToAdd); }
        }

        public void ClickSave()
        {
            if (Status == "Update")
            {
                UpdateMedewerker();
            }

            else if (Status == "Add")
            {
                AddMedewerker();
            }
        }

        public ICommand ClickSaveCommand
        {
            get { return new RelayCommand(ClickSave); }
        }
    }
}
