using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using nmct.ba.cashlessproject.Models;
using nmct.ba.cashlessproject.api.Helper;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class ProductController : ApiController
    {
        public List<Products> Get()
        {
            return DAProduct.GetProducts();
        }
        public HttpResponseMessage Put(Products product)
        {
            DAProduct.UpdateProduct(product);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public Products Get(int id)
        {
            return DAProduct.GetProducts(id);
        }
        public HttpResponseMessage Delete(int id)
        {
            DAProduct.DeleteProduct(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public HttpResponseMessage Post(Products product)
        {
            DAProduct.AddNewProduct(product);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
       
    }
}
