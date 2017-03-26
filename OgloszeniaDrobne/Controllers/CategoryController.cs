using OgloszeniaDrobne.DAL;
using OgloszeniaDrobne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgloszeniaDrobne.Controllers
{
    public class CategoryController : Controller
    {
        private DbContext db = new DbContext();


        public String getNawigacja(int? id)
        {
            String wynik = "";
            List<String> kolejnoscNawigacja = new List<String>();

            Category wybranaKategoria = null;
            bool czy_pierwsza_iteracja = true;
            String ostatnia;

            do // dodawanie kategorii ogłoszenia
            {
                if (czy_pierwsza_iteracja)
                {
                    wybranaKategoria = db.Categories.Find(id);
                    kolejnoscNawigacja.Add("<li class=\"active\">" + wybranaKategoria.Nazwa + "</li>");
                    czy_pierwsza_iteracja = false;
                }
                else
                {
                    wybranaKategoria = db.Categories.Find(wybranaKategoria.ParentCategoryID);
                    kolejnoscNawigacja.Add("<li><a href=\"#\">" + wybranaKategoria.Nazwa + "</a></li>");
                }
                

            } while (wybranaKategoria.ParentCategoryID != null);
            

            kolejnoscNawigacja.Reverse();

            foreach(String wartosc in kolejnoscNawigacja)
            {
                wynik += wartosc;
            }


            return wynik;
        }






        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
    }
}