using nmct.ba.cashlessproject.Models;
using nmct.ba.cashlessproject.WebApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.WebApp.Controllers
{
    public class VerenigingController : Controller
    {
        // GET: Vereniging
        [Authorize]
        [HttpGet]
        public ActionResult Vereniging()
        {
            List<Organisations> list = VerenigingDA.getVerenigingen();
            ViewBag.listVerenigingen = list;
            return View();
        }
        [Authorize]
        public ActionResult Toevoegen()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Toevoegen(string login, string password, string DbName, string DbLogin, string DbPassword, string organisationName, string address, string email, string phone)
        {
            if (login != null && password != null && DbName != null && DbLogin !=null && DbPassword != null && organisationName != null && address != null && email != null && phone != null)
            {
                Organisations o = new Organisations();
                o.Address = address;
                o.DbLogin = DbLogin;
                o.DbName = DbName;
                o.DbPassword = DbPassword;
                o.Email = email;
                o.Login = login;
                o.OrganisationName = organisationName;
                o.Password = password;
                o.Phone = phone;
                VerenigingDA.AddOrganisation(o);
            }
            return RedirectToAction("Vereniging");
        }

        public ActionResult Bewerk(int id)
        {
            Organisations o = VerenigingDA.getVerenigingenMetId(id);
            return View(o);
        }
        public ActionResult BewerkenOpslaan(int id,string login, string password, string DbName, string DbLogin, string DbPassword, string organisationName, string address, string email, string phone)
        {
            if (id>0 &&login != null && password != null && DbName != null && DbLogin != null && DbPassword != null && organisationName != null && address != null && email != null && phone != null)
            {
                Organisations o = new Organisations();
                o.Id = id;
                o.Address = address;
                o.DbLogin = DbLogin;
                o.DbName = DbName;
                o.DbPassword = DbPassword;
                o.Email = email;
                o.Login = login;
                o.OrganisationName = organisationName;
                o.Password = password;
                o.Phone = phone;
                VerenigingDA.UpdateVereniging(o);
            }
            return RedirectToAction("Vereniging");
        }
    }
}