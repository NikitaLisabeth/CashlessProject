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

            List<RegistersManagement> listRegisterWithoutOrg = KassaDA.getKassasZonderVereniging();
            ViewBag.KassaListZonderOrg = listRegisterWithoutOrg;

            List<KassaPM> list = KassaDA.getKassasMetVereniging();
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
        [Authorize]
        [HttpGet]
        public ActionResult PerVereniging(int vereniging)
        {
            List<KassaPM> list = KassaDA.getKassasMetId(vereniging);
            ViewBag.KassaList = list;
            return View("Kassa");
        }
        [Authorize]
        [HttpGet]
        public ActionResult Overzicht(int vereniging)
        {
            if (vereniging > 0)
            {
                List<KassaPM> list = KassaDA.getKassasMetId(vereniging);
                ViewBag.KassaList = list;

                Organisations o = VerenigingDA.getVerenigingenMetId(vereniging);
                ViewBag.Vereniging = o; 
                return View();
            }
            return RedirectToAction("Kassa");
        }
        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            List<Organisations> listorganisatie = VerenigingDA.getVerenigingen();
            ViewBag.listorganisatie = listorganisatie;
            List<RegistersManagement> listkassa = KassaDA.getKassasZonderVereniging();
            ViewBag.listkassa = listkassa;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int kassa, int vereniging, DateTime vanaf, DateTime tot)
        {
            List<KassaPM> toekenning = new List<KassaPM>();
            toekenning = KassaDA.getKassasMetId(kassa);

            if (toekenning.Count == 0) //kassa nog niet toegekend aan vereniging
            {
                int rowsaffected = KassaDA.VoegOrganisatieToeAanKassa(kassa, vereniging,vanaf,tot);
            }
            else if (toekenning.Count == 1)//kassa al toegekend aan vereniging => kassa wijzigen van vereniging
            {
                int rowsaffected = KassaDA.UpdateOrganisationRegister(vereniging, kassa,vanaf,tot);
            }

            return RedirectToAction("Kassa", "Kassa");
        }

    }
}