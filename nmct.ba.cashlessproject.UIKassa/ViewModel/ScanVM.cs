using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.UIKassa.ViewModel
{
    class ScanVM : ObservableObject, IPage
    {
         public string Name
        {
            get { return "Scan"; }
        }
         private int _userId;

         public int UserID
         {
             get { return _userId; }
             set { _userId = value; OnPropertyChanged("UserID"); }
         }

         private Customers _selectedCustomer;

         public Customers SelectedCustomer
         {
             get { return _selectedCustomer; }
             set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
         }


         public ICommand BestellingCommand
         {
             get { return new RelayCommand(Bestelling); }
         }

         private void Bestelling()
         {
             if (SelectedCustomer.Balance > 0)
             {
                ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                appvm.ActiveUserId = SelectedCustomer.Id;
                appvm.ChangePage(new BestellingVM());
             }
             
         }
         public ICommand AfmeldenCommand
         {
             get { return new RelayCommand(Afmelden); }
         }

         private void Afmelden()
         {
             ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
             appvm.ChangePage(new InloggenVM());
         }

         public ICommand kiesCommand
         {
             get { return new RelayCommand(KiesKlant); }
         }

         private void KiesKlant()
         {
             if (UserID > 0)
             {
                 GetKlant();
             }
         }
         private async void GetKlant()
         {
             using (HttpClient client = new HttpClient())
             {
                 //client.SetBearerToken(ApplicationVM.token.AccessToken);
                 HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/klant/" + UserID);
                 if (response.IsSuccessStatusCode)
                 {
                     string json = await response.Content.ReadAsStringAsync();
                     SelectedCustomer = JsonConvert.DeserializeObject<Customers>(json);
                 }
             }
         }
    }
}
