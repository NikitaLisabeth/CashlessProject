using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.temp.ViewModel
{
    class ShowGegevensVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "First Page"; }
        }
        private string _demo;

        public string Demo
        {
            get { return _demo; }
            set { _demo = value; OnPropertyChanged("Demo"); }
        }
    }
}
