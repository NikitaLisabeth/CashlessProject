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
            //GetCategorien();
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

        //private async void GetCategorien()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/categorie");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string json = await response.Content.ReadAsStringAsync();
        //            Categorie = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
        //        }
        //    }
        //}

        public ICommand TerugCommand
        {
            get { return new RelayCommand(Terug); }
        }

        private void Terug()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new ScanVM());
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
    }
}
