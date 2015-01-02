using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class KlantUIController : ApiController
    {
        public Customers Get(string kaartNummer)
        {
            return DAKlantUI.GetCustomer(kaartNummer);
        }

        public HttpResponseMessage Post(Customers kl)
        {
            DAKlantUI.AddNewCustomer(kl);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
        public HttpResponseMessage Put(Customers kl)
        {
            //ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            DAKlantUI.UpdateAccount(kl);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
