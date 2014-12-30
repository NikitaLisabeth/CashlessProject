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
    class PageKlantenVM : ObservableObject, IPage
    {
        public PageKlantenVM()
        {
            if (ApplicationVM.token != null)
            {
                GetKlanten();
            }

        }
        public string Name
        {
            get { return "Klanten"; }
        }
        private ObservableCollection<Customers> _klant;

        public ObservableCollection<Customers> Klant
        {
            get { return _klant; }
            set { _klant = value; OnPropertyChanged("Klant"); }
        }
        
        private async void GetKlanten()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/klant");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Klant = JsonConvert.DeserializeObject<ObservableCollection<Customers>>(json);
                }
            }
        }

        private Customers _selectedKlant;
        public Customers SelectedKlant
        {
            get { return _selectedKlant; }
            set { _selectedKlant = value; OnPropertyChanged("SelectedKlant"); }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }

        private async void UpdateKlant()
        {
            using (HttpClient client = new HttpClient())
            {
                Customers kl = SelectedKlant;
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string input = JsonConvert.SerializeObject(kl);
                HttpResponseMessage response = await client.PutAsync("http://localhost:1817/api/klant", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    GetKlanten();
                }
            }
        }

        public async void AddKlant()
        {
            Customers newKlant = SelectedKlant;
            
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string emp = JsonConvert.SerializeObject(newKlant);
                HttpResponseMessage response = await client.PostAsync("http://localhost:1817/api/klant", new StringContent(emp, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    GetKlanten();
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

        public void SetStatusToAdd()
        {
            Customers kl = new Customers();
            Klant.Add(kl);
            SelectedKlant = kl;

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
                UpdateKlant();
            }

            else if (Status == "Add")
            {
                AddKlant();
            }
        }

        public ICommand ClickSaveCommand
        {
            get { return new RelayCommand(ClickSave); }
        }
    }
}
