using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.UIKlant.ViewModel
{
    class PageOpladenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Mijn kaart opladen"; }
        }
        private string _opladen;

        public string Opladen
        {
            get { return _opladen; }
            set { _opladen = value; OnPropertyChanged("Opladen"); }
        }

        
    }
}
