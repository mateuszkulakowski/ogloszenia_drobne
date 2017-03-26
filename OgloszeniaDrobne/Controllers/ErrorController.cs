using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgloszeniaDrobne.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NieZalogowany()
        {
            return View();
        }

        public ActionResult NieJestesWlascicielem()
        {
            return View();
        }

        public ActionResult BrakTakiegoOgloszenia()
        {
            return View();
        }

        public ActionResult BrakID()
        {
            return View();
        }
        public ActionResult BrakOgłoszeńWKategorii()
        {
            return View();
        }


        public ActionResult BrakOdpowiedniejRoli()
        {
            return View();
        }

        public ActionResult NieMaTakiegoAtrybutu()
        {
            return View();
        }

        public ActionResult BrakOgłoszeńOPodanymSłowieKluczowym()
        {
            return View();
        }

        public ActionResult BrakOgłoszeńZaawansowane()
        {
            return View();
        }
    }
}