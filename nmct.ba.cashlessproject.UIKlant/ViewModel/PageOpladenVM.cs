using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.UIKlant.ViewModel
{
    class PageOpladenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Opladen"; }
        }
        #region properties
        private Customers _selectedCustomer;

        public Customers SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        } 
        private string _opladen;

        public string Opladen
        {
            get { return _opladen; }
            set { _opladen = value; OnPropertyChanged("Opladen"); }
        }
        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }
        private double _totaalBedrag;

        public double TotaalBedrag
        {
            get { return _totaalBedrag; }
            set { _totaalBedrag = value; OnPropertyChanged("TotaalBedrag"); }
        }
        
        #endregion
        
        public PageOpladenVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            if (appvm.ActiveUserId > 0)
            {
                GetKlant(appvm.ActiveUserId);
            }
        }
       
        private async void GetKlant(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/klant/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    SelectedCustomer = JsonConvert.DeserializeObject<Customers>(json);
                }
            }
        }
        private void Controle()
        {
            int opladenGetal = Convert.ToInt32(Opladen);
            TotaalBedrag = SelectedCustomer.Balance + opladenGetal;
            if (TotaalBedrag > 100)
            {
                Error = "Je kan maximum opladen tot € 100!";
            }
            else
            {
                Error = "";
            }
        }
        #region OpladenButtons
        public ICommand NulCommand
        {
            get { return new RelayCommand(Nul); }
        }

        private void Nul()
        {
            Opladen += "0";
            Controle();
        }
        public ICommand EenCommand
        {
            get { return new RelayCommand(Een); }
        }

        private void Een()
        {
            Opladen += "1";
            Controle();
        }
        public ICommand TweeCommand
        {
            get { return new RelayCommand(Twee); }
        }

        private void Twee()
        {
            Opladen += "2";
            Controle();
        }
        public ICommand DrieCommand
        {
            get { return new RelayCommand(Drie); }
        }

        private void Drie()
        {
            Opladen += "3";
            Controle();
        }
        public ICommand VierCommand
        {
            get { return new RelayCommand(Vier); }
        }

        private void Vier()
        {
            Opladen += "4";
            Controle();
        }
        public ICommand VijfCommand
        {
            get { return new RelayCommand(Vijf); }
        }

        private void Vijf()
        {
            Opladen += "5";
            Controle();
        }
        public ICommand ZesCommand
        {
            get { return new RelayCommand(Zes); }
        }

        private void Zes()
        {
            Opladen += "6";
            Controle();
        }
        public ICommand ZevenCommand
        {
            get { return new RelayCommand(Zeven); }
        }

        private void Zeven()
        {
            Opladen += "7";
            Controle();
        }
        public ICommand AchtCommand
        {
            get { return new RelayCommand(Acht); }
        }

        private void Acht()
        {
            Opladen += "8";
            Controle();
        }
        public ICommand NegenCommand
        {
            get { return new RelayCommand(Negen); }
        }

        private void Negen()
        {
            Opladen += "9";
            Controle();
        }
        public ICommand ResetCommand
        {
            get { return new RelayCommand(Reset); }
        }

        private void Reset()
        {
            Opladen = null;
            Controle();
        }

        #endregion

        #region Buttons
        public ICommand AnnulerenCommand
        {
            get { return new RelayCommand(Annuleren); }
        }

        private void Annuleren()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new PageStartVM());
        }
        public ICommand BevestigCommand
        {
            get { return new RelayCommand(Bevestig); }
        }

        private void Bevestig()
        {
            if (TotaalBedrag <= 100)
            {
                UpdateKlant();
            }
        }
        #endregion

        private async void UpdateKlant()
        {
            using (HttpClient client = new HttpClient())
            {
                Customers kl = SelectedCustomer;
                kl.Balance = TotaalBedrag;
                string input = JsonConvert.SerializeObject(kl);
                HttpResponseMessage response = await client.PutAsync("http://localhost:1817/api/klantui", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                    appvm.ChangePage(new PageStartVM());
                }
            }
        }
    }
}
