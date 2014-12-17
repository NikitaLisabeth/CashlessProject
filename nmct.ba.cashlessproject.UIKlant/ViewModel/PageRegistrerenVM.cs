using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.UIKlant.ViewModel
{
    class PageRegistrerenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Registreren"; }
        }
        private string _registreren;

        public string Registreren
        {
            get { return _registreren; }
            set { _registreren = value; OnPropertyChanged("Registreren"); }

        }
        
    }
}
