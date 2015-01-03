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
using System.Windows;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.UIKassa.ViewModel
{
    class BestellingVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Bestelling"; }
        }
        public BestellingVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            if (appvm.ActiveUserId > 0)
            {
                GetKlant(appvm.ActiveUserId);
                ShowDrankLijst();
            }
        }
        private double _teBetalen;

        public double TeBetalen
        {
            get { return _teBetalen; }
            set { _teBetalen = value; OnPropertyChanged("TeBetalen"); }
        }
        private Customers _selectedCustomer;

        public Customers SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }
        private ObservableCollection<Products> _categorie;

        public ObservableCollection<Products> Categorie
        {
            get { return _categorie; }
            set { _categorie = value; }
        }
        private ObservableCollection<Products> _gekozenProducten;

        public ObservableCollection<Products> GekozenProducten
        {
            get { return _gekozenProducten; }
            set { _gekozenProducten = value; OnPropertyChanged("GekozenProducten"); }
        }
      
        private ObservableCollection<Products> _products;

        public ObservableCollection<Products> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged("Products"); }
        }
        private Products _selectedProduct;

        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }
        #region Terug
        public ICommand TerugCommand
        {
            get { return new RelayCommand(Terug); }
        }

        private void Terug()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new ScanVM());
        }
        #endregion  
        #region Afmelden
        public ICommand AfmeldenCommand
        {
            get { return new RelayCommand(Afmelden); }
        }

        private void Afmelden()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new InloggenVM());
        }
        #endregion
        #region klant
        private async void GetKlant(int id)
         {
             using (HttpClient client = new HttpClient())
             {
                 //client.SetBearerToken(ApplicationVM.token.AccessToken);
                 HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/klant/" + id);
                 if (response.IsSuccessStatusCode)
                 {
                     string json = await response.Content.ReadAsStringAsync();
                     SelectedCustomer = JsonConvert.DeserializeObject<Customers>(json);
                 }
             }
         }
        #endregion
        #region Listbox opvullen
        public ICommand KiesDrankCommand
        {
            get { return new RelayCommand(ShowDrankLijst); }
        }

        private async void ShowDrankLijst()
        {
            using (HttpClient client = new HttpClient())
            {
                //client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/categorie/1");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    if (Products != null )
                    {
                        Products.Clear();
                    }
                    
                    Products = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
                }
            }
        }
        public ICommand KiesSnackCommand
        {
            get { return new RelayCommand(ShowSnackLijst); }
        }
        private async void ShowSnackLijst()
        {
            using (HttpClient client = new HttpClient())
            {
                //client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/categorie/2");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    if (Products.Count > 0)
                    {
                        Products.Clear();
                    }
                    Products = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
                }
            }
        }
        #endregion
        #region Product kiezen
        public ICommand VoegToeCommand
        {
            get { return new RelayCommand(VoegToe); }
        } 
        ObservableCollection<Products> list = new ObservableCollection<Products>();
        private void VoegToe()
        {
            if (SelectedProduct != null)
            {
                Products p = new Products();
                p = SelectedProduct;
                double prijs = p.Price;
                BepaalTeBepalen(prijs);
                list.Add(p);
                GekozenProducten = list;   
            }
                         
        }
        private void BepaalTeBepalen(double prijs)
        {
            TeBetalen += prijs;
        }
        #endregion
        public ICommand Afrekenen
        {
            get { return new RelayCommand(BedragAfrekenen); }
        }
        private void BedragAfrekenen()
        {
            if (TeBetalen < SelectedCustomer.Balance)
            {
                foreach (Products p in GekozenProducten)
                {
                    Sales s = new Sales
                    {
                        Amount = 1,
                        CustomerId = SelectedCustomer.Id,
                        ProductId = p.Id,
                        RegisterId = 1,
                        Timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                        TotalPrice = p.Price
                    };
                    AddSale(s);
                    //PasStockAan(p);
                }

                double nieuwBalance = SelectedCustomer.Balance - TeBetalen;
                Customers nieuweCustomer = new Customers
                {
                    Id = SelectedCustomer.Id,
                    Address = SelectedCustomer.Address,
                    BirthDate = SelectedCustomer.BirthDate,
                    CustomerName = SelectedCustomer.CustomerName,
                    Sex = SelectedCustomer.Sex,
                    Picture = SelectedCustomer.Picture,
                    Balance = nieuwBalance
                };
                UpdateKlant(nieuweCustomer);
            }
            else
            {
                MessageBox.Show("Het toegestane bedrag is overschreden!!!", "Belangrijk!", MessageBoxButton.OK, MessageBoxImage.Error);
            }   
        }
        private async void PasStockAan(Products p)
        {
            using (HttpClient client = new HttpClient())
            {
                string input = JsonConvert.SerializeObject(p);
                //client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.PutAsync("http://localhost:1817/api/sales", new StringContent(input, Encoding.UTF8, "application/json"));
                //if (response.IsSuccessStatusCode)
                //{
                //    GetMedewerkers();
                //}
            }
        }
        public async void AddSale(Sales s)
        {
            using (HttpClient client = new HttpClient())
            {
                string input = JsonConvert.SerializeObject(s);
                //client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.PostAsync("http://localhost:1817/api/sales", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    foreach (Products p in GekozenProducten)
                    {
                        PasStockAan(p);
                    }
                }
            }
        }
        private async void UpdateKlant(Customers customer)
        {
            using (HttpClient client = new HttpClient())
            {
                Customers kl = customer;
                //client.SetBearerToken(ApplicationVM.token.AccessToken);
                string input = JsonConvert.SerializeObject(kl);
                HttpResponseMessage response = await client.PutAsync("http://localhost:1817/api/sale", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    //GetKlanten();
                }
            }
        }
    }
}
