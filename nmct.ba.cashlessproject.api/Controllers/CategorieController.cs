using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class CategorieController : ApiController
    {
        public List<string> Get()
        {
            //ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return DAProduct.GetCategorie();
        }
        public List<Products> Get(int id)
        {
            return DACategorie.GetProducts(id);
        }
        
    }
}
