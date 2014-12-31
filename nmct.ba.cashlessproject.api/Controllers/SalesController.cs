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
    public class SalesController : ApiController
    {
        public HttpResponseMessage Put(Products product)
        {
            DASales.UpdateProduct(product);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
       
        public HttpResponseMessage Post(Sales s)
        {
            //ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            DASales.AddNewSale(s);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
