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
    public class SaleController : ApiController
    {
        public HttpResponseMessage Put(Customers customer)
        {
            DASales.UpdateAccount(customer);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
