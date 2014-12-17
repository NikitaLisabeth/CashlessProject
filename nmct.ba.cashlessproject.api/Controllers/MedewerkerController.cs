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
    public class MedewerkerController : ApiController
    {
        public List<Employee> Get()
        {
            return DAMedewerker.GetEmployee();
        }
        public Employee Get(int id)
        {
            return DAMedewerker.GetEmployee(id);
        }
        public HttpResponseMessage Put(Employee emp)
        {
            DAMedewerker.UpdateEmployee(emp);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(Employee emp)
        {
            DAMedewerker.AddNewEmployee(emp);
            return new HttpResponseMessage(HttpStatusCode.Created);
        
        }
        public HttpResponseMessage Delete(int id)
        {
            DAMedewerker.DeleteEmployee(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
