using nmct.ba.cashlessproject.api.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class CategorieController : ApiController
    {
        public List<string> Get()
        {
            return DAProduct.GetCategorie();
        }
        
    }
}
