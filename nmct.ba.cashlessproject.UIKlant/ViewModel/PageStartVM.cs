using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.UIKlant.ViewModel
{
    class PageStartVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Startpagina"; }
        }

        public ICommand RegistrerenCommand
        {
            get { return new RelayCommand(Registreren); }
        }

        private void Registreren()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new PageRegistrerenVM());
        }
        public ICommand BekijkenCommand
        {
            get { return new RelayCommand(Bekijken); }
        }

        private void Bekijken()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new PageScanKaartVM());
        }
    }
}
