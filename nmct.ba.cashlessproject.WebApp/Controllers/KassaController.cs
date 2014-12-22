using nmct.ba.cashlessproject.Models;
using nmct.ba.cashlessproject.WebApp.DataAccess;
using nmct.ba.cashlessproject.WebApp.PresentationModels;
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
        [Authorize]
        [HttpGet]
        public ActionResult Kassa()
        {
            List<Organisations> listorganisations = VerenigingDA.getVerenigingen();
            ViewBag.VerenigingList = listorganisations;
            List<KassaPM> list = KassaDA.getKassas();
            ViewBag.KassaList = list;
            return View();
        }
        [Authorize]
        public ActionResult Toevoegen()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Toevoegen(string RegisterName, string Device, DateTime PurchaseDate, DateTime ExpiresDate)
        {
            if (RegisterName != null && Device != null && PurchaseDate != null && ExpiresDate != null)
            {
                RegistersManagement register = new RegistersManagement();
                register.Device = Device;
                register.RegisterName = RegisterName;
                register.PurchaseDate = PurchaseDate;
                register.ExpiresDate = ExpiresDate;

                KassaDA.AddRegister(register);
            }
            return RedirectToAction("Kassa");
        }

    }
}