using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.Models;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class KlantController : ApiController
    {
        public List<Customers> Get()
        {
            return DAKlant.GetKlanten();
        }

        public HttpResponseMessage Put(Customers kl)
        {
            DAKlant.UpdateAccount(kl);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(Customers kl)
        {
            DAKlant.AddNewCustomer(kl);
            return new HttpResponseMessage(HttpStatusCode.Created);
        } 
    }
}
