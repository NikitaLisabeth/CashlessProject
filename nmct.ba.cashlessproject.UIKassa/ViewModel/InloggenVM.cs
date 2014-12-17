using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.UIKassa.ViewModel
{
    class InloggenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Inloggen"; }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }


        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        private void Login()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new ScanVM());


            //ApplicationVM.token = GetToken();

            //if (!ApplicationVM.token.IsError)
            //{
            //    appvm.ChangePage(new CustomerVM());
            //}
            //else
            //{
            //    Error = "Gebruikersnaam of paswoord kloppen niet";
            //}
        }
    }
}
