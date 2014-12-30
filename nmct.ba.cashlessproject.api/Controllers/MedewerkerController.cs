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
    public class MedewerkerController : ApiController
    {
        public List<Employee> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return DAMedewerker.GetEmployee(p.Claims);
        }
        public Employee Get(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return DAMedewerker.GetEmployee(id, p.Claims);
        }
        public HttpResponseMessage Put(Employee emp)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            DAMedewerker.UpdateEmployee(emp, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(Employee emp)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            DAMedewerker.AddNewEmployee(emp,p.Claims);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
        public HttpResponseMessage Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            DAMedewerker.DeleteEmployee(id, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
