using be.belgium.eid;
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
    class PageScanKaartVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "ScanKaart"; }
        }
        private Customers _selectedCustomer;

        public Customers SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }
        public ICommand ScanCommand
        {
            get { return new RelayCommand(Scan); }
        }

        private void Scan()
        {
            Reader();
        }
        public async void Reader()
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
                        //OnPropertyChanged("SelectedCustomer");
                        ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                        bool exists = await CheckIfCustomerExists(c.KaartNummer);

                        if (exists == false)
                        {
                            PageRegistrerenVM klantRegis = new PageRegistrerenVM();
                            appvm.ChangePage(klantRegis);
                        }
                        else
                        {
                            PageGegevensVM klantGeg = new PageGegevensVM();
                            appvm.ChangePage(klantGeg);
                        }
                    }
                }
            }
            BEID_ReaderSet.releaseSDK();
        }
        public async Task<bool> CheckIfCustomerExists(string KaartNummer)
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
                bool exists = true;
                return exists;
            }
            return false;
        }
    }
}
