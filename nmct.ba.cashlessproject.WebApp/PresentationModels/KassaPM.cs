using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.WebApp.PresentationModels
{
    public class KassaPM
    {
        public RegistersManagement Kassa { get; set; }
        public Organisations Organisatie { get; set; }
        public Organisation_Register Organisatie_Register { get; set; }


    }
}