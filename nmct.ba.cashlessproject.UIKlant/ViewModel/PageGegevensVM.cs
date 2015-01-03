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
    class PageGegevensVM : ObservableObject, IPage
    {
        public PageGegevensVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            if (appvm.ActiveUserId > 0)
            {
                GetKlant(appvm.ActiveUserId);
            }
        } 
        public string Name
        {
            get { return "Gegevens"; }
        }
        private Customers _selectedCustomer;

        public Customers SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }
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
        public ICommand OpladenCommand
        {
            get { return new RelayCommand(Opladen); }
        }

        private void Opladen()
        {
            if (SelectedCustomer != null)
            {
                ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                appvm.ChangePage(new PageOpladenVM());
                appvm.ActiveUserId = SelectedCustomer.Id;
            }

        }
    }
}
