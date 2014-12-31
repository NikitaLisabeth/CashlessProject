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
    public class ProductController : ApiController
    {
        public List<Products> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;

            return DAProduct.GetProducts(p.Claims);
        }
     
        public HttpResponseMessage Put(Products product)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;

            DAProduct.UpdateProduct(product, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public Products Get(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;

            return DAProduct.GetProducts(id, p.Claims);
        }
        public HttpResponseMessage Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;

            DAProduct.DeleteProduct(id, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public HttpResponseMessage Post(Products product)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;

            DAProduct.AddNewProduct(product, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
       
    }
}
