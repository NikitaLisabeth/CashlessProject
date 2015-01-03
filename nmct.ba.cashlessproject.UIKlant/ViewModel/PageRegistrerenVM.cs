using be.belgium.eid;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.Models;
using nmct.ba.cashlessproject.UIKlant.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.UIKlant.ViewModel
{
    class PageRegistrerenVM : ObservableObject, IPage
    {
        public PageRegistrerenVM()
        {
        }
        public string Name
        {
            get { return "Registreren"; }
        }

        private Customers _selectedCustomer;

        public Customers SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }
        public ICommand OpslaanCommand
        {
            get { return new RelayCommand(Opslaan); }
        }

        private async void Opslaan()
        {
            Customers newKlant = SelectedCustomer;
            using (HttpClient client = new HttpClient())
            {
                //client.SetBearerToken(ApplicationVM.token.AccessToken);
                string kl = JsonConvert.SerializeObject(newKlant);
                HttpResponseMessage response = await client.PostAsync("http://localhost:1817/api/klantui", new StringContent(kl, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    getKlantId(SelectedCustomer.KaartNummer);
                    //ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;                    
                    //appvm.ActiveUserId = SelectedCustomer.Id;
                    //appvm.ChangePage(new PageOpladenVM());
                }
            }

        }

        public async void getKlantId(string KaartNummer)
        {
            var client = new System.Net.Http.HttpClient();
            //string natnr = Convert.ToString(nationalNumber);
            //client.SetBearerToken(token);
            HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/klantui?KaartNummer=" + KaartNummer);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                //bool exists = JsonConvert.DeserializeObject<bool>(json);
                Customers c = JsonConvert.DeserializeObject<Customers>(json);
                ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                appvm.ActiveUserId = c.Id;
                appvm.ChangePage(new PageOpladenVM());
            }
        }

        public ICommand AnnulerenCommand
        {
            get { return new RelayCommand(Annuleren); }
        }

        private void Annuleren()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new PageStartVM());
        }
        public ICommand LeesKaartCommand
        {
            get { return new RelayCommand(Reader); }
        }
        public void Reader()
        {
            BEID_ReaderSet.initSDK();
            // access the eID card here
            if (BEID_ReaderSet.instance().readerCount() > 0)
            {
                BEID_ReaderContext readerContext = readerContext = BEID_ReaderSet.instance().getReader();
                if (readerContext != null)
                {
                    if (readerContext.getCardType() == BEID_CardType.BEID_CARDTYPE_EID)
                    {
                        Customers c = new Customers();
                        BEID_EIDCard card = readerContext.getEIDCard();
                        BEID_Picture picture;
                        picture = card.getPicture();
                        byte[] bytearray;
                        bytearray = picture.getData().GetBytes();
                        c.Picture = bytearray;
                        //
                        c.KaartNummer = card.getID().getNationalNumber();
                        c.Address = card.getID().getStreet() + " " + card.getID().getZipCode();
                        c.CustomerName = card.getID().getFirstName() + " " + card.getID().getSurname();
                        c.BirthDate = Convert.ToDateTime(card.getID().getDateOfBirth());
                        c.Sex = card.getID().getGender();
                        SelectedCustomer = c;
                        OnPropertyChanged("SelectedCustomer");
                    }
                }
            }
            BEID_ReaderSet.releaseSDK();
        }
        
    }
}
