using be.belgium.eid;
using GalaSoft.MvvmLight.CommandWpf;
using nmct.ba.cashlessproject.Models;
using nmct.ba.cashlessproject.UIKlant.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ICommand OpladenCommand
        {
            get { return new RelayCommand(Opladen); }
        }

        private void Opladen()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new PageOpladenVM());
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
