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
    public class KassaController : ApiController
    {
        public List<RegistersKlant> Get()
        {
            return DAKassaManagement.GetKassasManagement();
        }
        public HttpResponseMessage Put(RegistersKlant kl)
        {
            DAKassaManagement.UpdateAccount(kl);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
