using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.Models;
using System.Security.Claims;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class KlantController : ApiController
    {
        public List<Customers> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return DAKlant.GetKlanten(p.Claims);
        }

        public HttpResponseMessage Put(Customers kl)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            DAKlant.UpdateAccount(kl,p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(Customers kl)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            DAKlant.AddNewCustomer(kl, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.Created);
        } 
    }
}
