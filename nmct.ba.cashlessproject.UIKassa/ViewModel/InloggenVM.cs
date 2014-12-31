using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

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
       
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("Username"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }

        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        private void Login()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new ScanVM());


            //ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            //// appvm.ChangePage(new CustomerVM());   
            //ApplicationVM.token = GetToken();


            //if (!ApplicationVM.token.IsError)
            //{
            //    appvm.ChangePage(new ScanVM());
            //}
            //else
            //{
            //    Error = "Naam of LoginNummer klopt niet";
            //}
        }
        //private TokenResponse GetToken()
        //{
        //    OAuth2Client client = new OAuth2Client(new Uri("http://localhost:1817/token"));
        //    return client.RequestResourceOwnerPasswordAsync(Username, Password).Result;
        //}
    }
}
