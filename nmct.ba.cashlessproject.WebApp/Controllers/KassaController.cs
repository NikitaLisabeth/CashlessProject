using nmct.ba.cashlessproject.Models;
using nmct.ba.cashlessproject.WebApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.WebApp.Controllers
{
    public class KassaController : Controller
    {
        // GET: Kassa
        public ActionResult Kassa()
        {
            List<RegistersManagement> list = KassaDA.getKassas();
            ViewBag.KassaList = list;
            return View();
        }
        public ActionResult Toevoegen()
        {
            return View();
        }

    }
}