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
        [HttpGet]
        public ActionResult Vereniging()
        {
            List<Organisations> list = VerenigingDA.getVerenigingen();
            ViewBag.listVerenigingen = list;
            return View();
        }
        public ActionResult Toevoegen()
        {
            return View();
        }
    }
}