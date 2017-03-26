using OgloszeniaDrobne.DAL;
using OgloszeniaDrobne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgloszeniaDrobne.Controllers
{
    public class UserController : Controller
    {
        private DbContext db = new DbContext();
      
        public ActionResult LogOff()
        {
            Session.Remove("zalogowany");

            return View();
        }

        public ActionResult Settings()
        {
            var zalogowany = Session["zalogowany"] as User;
            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");

            ViewBag.LiczbaOgloszen = zalogowany.IloscOgloszenStrona;
            return View();
        }

        public ActionResult ZmianaWyswietlanieOgloszen()
        {
            var zalogowany = Session["zalogowany"] as User;
            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");

            return View();
        }

        [HttpPost]
        public ActionResult ZmianaWyswietlanieOgloszen(FormCollection fc)
        {
            var zalogowany = Session["zalogowany"] as User;
            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            int nowa_liczba = int.Parse(fc["liczbaogloszen"]);

            if(ModelState.IsValid && nowa_liczba > 0)
            {
                User ten_użytkownik = db.Users.Find(zalogowany.UserID);
                ten_użytkownik.IloscOgloszenStrona = nowa_liczba;

                db.Entry(ten_użytkownik).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                Session["zalogowany"] = ten_użytkownik;

                return RedirectToAction("Settings");
            }
            else
            {
                return View(fc);
            }
        }


        public ActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registry(User ur)
        {
            bool czy = false;
            
            List<User> list = db.Users.Where(u => u.Login == ur.Login).ToList();

            if (list.Count() > 0) czy = true;
            

            if(ModelState.IsValid && !czy)
            {
                ur.Rola = "USER";
                ur.IloscOgloszenStrona = 5;
                db.Users.Add(ur);
                db.SaveChanges();

                return RedirectToAction("LogIn");
            }
            else
            {
                return View(ur);
            }

            
        }

        // GET
        public ActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LogIn(LogInModel lim)
        {
            bool czybrak = false;
            bool czyhaslo = false;
            User zalogowany = new User();
            //sprawdzanie czy login juz wystąpił
            try
            {

                List<User> lista = db.Users.Where(u => u.Login == lim.Login).ToList();

                zalogowany = lista.First();

                czyhaslo = (zalogowany.Haslo == lim.Password);
            }
            catch(Exception e)
            {
                czybrak = true;
            }


            if(ModelState.IsValid && !czybrak && czyhaslo)
            {
                Session["zalogowany"] = zalogowany;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(lim);
            }

            
        }










    }
}