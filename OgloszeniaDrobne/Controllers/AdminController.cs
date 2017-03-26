using Newtonsoft.Json;
using OgloszeniaDrobne.DAL;
using OgloszeniaDrobne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgloszeniaDrobne.Controllers
{
    public class AdminController : Controller
    {
        private DAL.DbContext db = new DAL.DbContext();


        public ActionResult ZakazaneSlowo()
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (zalogowany.Rola == "USER") return RedirectToAction("BrakOdpowiedniejRoli", "Error");

            List<BannedWord> lista = db.BannedWords.ToList();

            return View(lista);
        }


       
        public ActionResult DodajZakazaneSlowo()
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (zalogowany.Rola == "USER") return RedirectToAction("BrakOdpowiedniejRoli", "Error");

            return View();
        }

        [HttpPost]
        public ActionResult DodajZakazaneSlowo(BannedWord bw)
        {
            if(ModelState.IsValid)
            {
                db.BannedWords.Add(bw);
                db.SaveChanges();

                return RedirectToAction("ZakazaneSlowo");
            }
            else
            {
                return View(bw);
            }
        }


        public ActionResult UsunZakazaneSlowo(int? id)
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");//czy zalogowany
            if (!zalogowany.Rola.Equals("ADMIN")) return RedirectToAction("BrakOdpowiedniejRoli","Error");
            if (id == null) return RedirectToAction("BrakID", "Error");

            BannedWord bannedword = db.BannedWords.Find(id);

            if (bannedword == null) return RedirectToAction("BrakTakiegoOgloszenia", "Error");

            return View(bannedword);
        }

        [HttpPost]
        public ActionResult UsunZakazaneSlowo(int id)
        {

            BannedWord bw = db.BannedWords.Find(id);

            db.BannedWords.Remove(bw);
            db.SaveChanges();

            return RedirectToAction("ZakazaneSlowo");
        }


        public ActionResult ListaUzytkownikow()
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (zalogowany.Rola.Equals("USER")) return RedirectToAction("BrakOdpowiedniejRoli", "Error");

            var listauzytkowanikow = db.Users.Where(u => u.Rola == "USER").ToList();

            return View(listauzytkowanikow);
        }


        public ActionResult DeleteByAdmin(int? id)
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null)
            {
                return RedirectToAction("NieZalogowany", "Error");
            }

            if (zalogowany.Rola == "USER") return RedirectToAction("BrakOdpowiedniejRoli", "Error");

            if (id == null)
            {
                return RedirectToAction("BrakID", "Error");
            }
            User user = db.Users.Find(id);

            if (user == null)
            {
                return RedirectToAction("BrakUzytkownikaOPodanymID", "Error");
            }

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteByAdmin(int id)
        {
            User user = db.Users.Find(id);

            //potwierdzono zgodę usuwania usera więc usuwamy jego wszyskie ogloszenia wraz z atrybutami

            IEnumerable<Add> lista_ogloszen_uzytkownika = db.Adds.Where(a => a.UserID.Equals(user.UserID));
            DAL.DbContext db2 = new DAL.DbContext();

            foreach(Add add in lista_ogloszen_uzytkownika)
            {
                IEnumerable<AddAtrribute> lista_Atrybutow = db2.AddAttributes.Where(ad => ad.AddID == add.AddID);
                db2.AddAttributes.RemoveRange(lista_Atrybutow);
                db2.SaveChanges();
            }
            

            

            db.Adds.RemoveRange(lista_ogloszen_uzytkownika);
            db.Users.Remove(user);
            db.SaveChanges();

            return RedirectToAction("ListaUzytkownikow");
        }


        public ActionResult DodajKategorie()
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (zalogowany.Rola.Equals("USER")) return RedirectToAction("BrakOdpowiedniejRoli", "Error");

            List<Category> kategorie = db.Categories.ToList();
            List<Category> przejsciowka = new List<Category>();
            IEnumerable<Category> kategorieRdzenie;

            foreach (Category kat in kategorie)//kategorie rdzenie
            {
                if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
            }

            przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

            kategorieRdzenie = przejsciowka;

            ViewData["ParentCategoryID"] = new SelectList(kategorieRdzenie, "CategoryID", "Nazwa", "-1");
            return View();
        }

        [HttpPost]
        public ActionResult DodajKategorie(Category category)
        {
            Boolean czy_nazwa_inna = true;

            List<Category> wszystkieKategorie = db.Categories.ToList();

            foreach (Category kategoriapętla in wszystkieKategorie)
            {
                if (kategoriapętla.Nazwa.ToUpper().Equals(category.Nazwa.ToUpper()))//Podana nazwa kategorii już występuje
                {
                    czy_nazwa_inna = false;
                    break;
                }
            }

            if (ModelState.IsValid && czy_nazwa_inna)
            {
                if (category.ParentCategoryID == -1) category.ParentCategoryID = null;

                db.Categories.Add(category);
                db.SaveChanges();
            }
            else // niepoprawny formularz dodajemy znów listę kategorii do widoku
            {
                List<Category> kategorie = db.Categories.ToList();
                List<Category> przejsciowka = new List<Category>();
                IEnumerable<Category> kategorieRdzenie;

                foreach (Category kat in kategorie)//kategorie rdzenie
                {
                    if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
                }

                przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

                kategorieRdzenie = przejsciowka;

                ViewData["ParentCategoryID"] = new SelectList(kategorieRdzenie, "CategoryID", "Nazwa", "-1");

                return View(category);
            }

            return RedirectToAction("Index", "Home");
        }



        public ActionResult DeleteKategoria()
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (zalogowany.Rola.Equals("USER")) return RedirectToAction("BrakOdpowiedniejRoli", "Error");

            List<Category> kategorie = db.Categories.ToList();
            List<Category> przejsciowka = new List<Category>();
            IEnumerable<Category> kategorieRdzenie;

            foreach (Category kat in kategorie)//kategorie rdzenie
            {
                if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
            }

            przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

            kategorieRdzenie = przejsciowka;

            ViewData["CategoryID"] = new SelectList(kategorieRdzenie, "CategoryID", "Nazwa", "-1");
            return View();
        }


        [HttpPost]
        public ActionResult DeleteKategoria(Category category)
        {
            //sprawdzanie czy kategoria ma dzieci
            Boolean czy_ma_dzieci = false;
            foreach(Category aktualna in db.Categories.ToList())
            {
                if(aktualna.ParentCategoryID == category.CategoryID)
                {
                    czy_ma_dzieci = true;
                    break;
                }
            }

            if(category.CategoryID == -1 || czy_ma_dzieci)//nie wybrano kategorii lub kategoria ma dzieci
            {
                List<Category> kategorie = db.Categories.ToList();
                List<Category> przejsciowka = new List<Category>();
                IEnumerable<Category> kategorieRdzenie;

                foreach (Category kat in kategorie)//kategorie rdzenie
                {
                    if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
                }

                przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

                kategorieRdzenie = przejsciowka;

                ViewData["CategoryID"] = new SelectList(kategorieRdzenie, "CategoryID", "Nazwa", "-1");
                return View();
            }

            //wybrana kategoria jest liściem
            //usuwamy ogłoszenia w niej znajdujące się

            foreach(Add add in db.Adds.ToList())
            {
                if (add.CategoryID == category.CategoryID)//szukamy ogłoszeń danej kategorii 
                {
                    foreach(AddAtrribute addAtrribute in db.AddAttributes.ToList())//usuwanie ustawionych atrybutów
                    {
                        if (addAtrribute.AddID == add.AddID) db.AddAttributes.Remove(addAtrribute);
                    }

                    db.Adds.Remove(add);
                }
            }
            Category categoryfull = db.Categories.Find(category.CategoryID);

            db.Categories.Remove(categoryfull);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public ActionResult DodajAtrybut()
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (zalogowany.Rola.Equals("USER")) return RedirectToAction("BrakOdpowiedniejRoli", "Error");

            List<Category> kategorie = db.Categories.ToList();
            List<Category> przejsciowka = new List<Category>();
            IEnumerable<Category> kategorieRdzenie;
            
            foreach (Category kat in kategorie)//kategorie rdzenie
            {
                if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
            }

            przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

            kategorieRdzenie = przejsciowka;

            ViewData["CategoryID"] = new SelectList(kategorieRdzenie, "CategoryID", "Nazwa", "-1");
            return View(); 
        }



        [HttpPost]
        public ActionResult DodajAtrybut(Models.Attribute atrybut, FormCollection formCollection)
        {
            if(atrybut.CategoryID == -1) // zamiast kategorii wybrano pole "Wybierz kategorie..."
            {
                List<Category> kategorie = db.Categories.ToList();
                List<Category> przejsciowka = new List<Category>();
                IEnumerable<Category> kategorieRdzenie;

                foreach (Category kat in kategorie)//kategorie rdzenie
                {
                    if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
                }

                przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

                kategorieRdzenie = przejsciowka;

                ViewData["CategoryID"] = new SelectList(kategorieRdzenie, "CategoryID", "Nazwa", "-1");

                ViewBag.NieWybranoKategorii = "Nie wybrano kategorii!";
                return View(atrybut);
            }


            if(ModelState.IsValid)
            {
                // pobieranie wartości pozycji słownika które zostały wpisane 
                String sPattern = "[0-9]+";
                List<String> listaPolSlownika = new List<String>();
                String wynik = "";

                foreach (var key in formCollection.AllKeys) //pobieranie wartości wpisanych pól słonika
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(key, sPattern))
                    {
                        listaPolSlownika.Add(formCollection[key]);
                    }

                }

                db.Attributes.Add(atrybut);
                
                foreach(String opcja in listaPolSlownika)
                {
                    db.Dictionary.Add(new Dictionary { Pole = opcja, AttributeID = atrybut.AttributeID});
                }
                
                db.SaveChanges();

                return RedirectToAction("Index","Home");
            }
            else
            {
                return View(atrybut);
            }
        }



        public ActionResult EdytujAtrybut() // wyświetla tylko dropdownliste z wyborem kategorii i tabelke z atrybutami danej kat.
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (zalogowany.Rola.Equals("USER")) return RedirectToAction("BrakOdpowiedniejRoli", "Error");

            List<Category> kategorie = db.Categories.ToList();
            List<Category> przejsciowka = new List<Category>();
            IEnumerable<Category> kategorieRdzenie;

            foreach (Category kat in kategorie)//kategorie rdzenie
            {
                if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
            }

            przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

            kategorieRdzenie = przejsciowka;

            ViewData["CategoryID"] = new SelectList(kategorieRdzenie, "CategoryID", "Nazwa", "-1");
            return View();
        }


        public ActionResult EdytujAtr(int? id)
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (zalogowany.Rola.Equals("USER")) return RedirectToAction("BrakOdpowiedniejRoli", "Error");
            if (id == null) return RedirectToAction("BrakID","Error");

            Models.Attribute atrybut = db.Attributes.Find(id);

            if (atrybut == null) return RedirectToAction("NieMaTakiegoAtrybutu","Error");

            Category kategoriaAtrybutu = db.Categories.Find(atrybut.CategoryID);
            Category rodzicKategoriaAtrybutu = db.Categories.Find(kategoriaAtrybutu.ParentCategoryID);
            List<Category> wynikpomoc = new List<Category>();
            IEnumerable<Category> wynik;

            if (rodzicKategoriaAtrybutu == null) // jeżeli atrybut należy do kategorii rdzenia to wypisujemy rdzenie
            {
                foreach (Category kat in db.Categories.ToList())//kategorie rdzenie
                {
                    if (kat.ParentCategoryID == null) wynikpomoc.Add(kat);
                }
                
            }
            else // atrybut jest w kategorii innej niż rdzeń czyli np. Pojazdy,itp.
            {
                foreach (Category kat in db.Categories.ToList())
                {
                    if (kat.ParentCategoryID == rodzicKategoriaAtrybutu.CategoryID)
                    {
                        wynikpomoc.Add(kat);
                    }
                }
            }
            wynik = wynikpomoc;

            List<Dictionary> listaWyboru = db.Dictionary.Where(d => d.AttributeID == atrybut.AttributeID).ToList();

            Session["staryTypAtrybutu"] = atrybut.Typ;
            ViewData["CategoryID"] = new SelectList(wynik, "CategoryID", "Nazwa", atrybut.CategoryID);
            ViewBag.Typ = JsonConvert.SerializeObject(new Models.Attribute { Typ = atrybut.Typ });
            ViewBag.listaWyboru = listaWyboru;
            ViewBag.IloscElementow = listaWyboru.Count;

            return View(atrybut);
        }


        [HttpPost]
        public ActionResult EdytujAtr(Models.Attribute atrybut, FormCollection formCollection)
        {
            
            if(ModelState.IsValid)
            {
                String starytyp = Session["staryTypAtrybutu"] as String;

                //jeżeli atrybut miał wcześniej typ listowy usuwamy jego wszystkie pola wyboru
                if (starytyp == "list")
                {
                    db.Dictionary.RemoveRange(db.Dictionary.Where(d => d.AttributeID == atrybut.AttributeID));
                    db.SaveChanges();
                }
                Session.Remove("staryTypAtrybutu");

                // pobieranie wartości pozycji słownika które zostały wpisane 
                String sPattern = "[0-9]+";
                List<String> listaPolSlownika = new List<String>();
                String wynik = "";

                foreach (var key in formCollection.AllKeys) //pobieranie wartości wpisanych pól słonika
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(key, sPattern))
                    {
                        listaPolSlownika.Add(formCollection[key]);
                    }

                }

                if(listaPolSlownika.Count > 0)//atrybut typu list
                {

                    //dodajemy ustawione pola wyboru danego atrybutu
                    foreach(String pole in listaPolSlownika)
                    {
                        db.Dictionary.Add(new Dictionary { Pole = pole, AttributeID=atrybut.AttributeID});
                    }

                }

                db.Entry(atrybut).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("EdytujAtrybut", "Admin");
            }
            else
            {
                Category kategoriaAtrybutu = db.Categories.Find(atrybut.CategoryID);
                Category rodzicKategoriaAtrybutu = db.Categories.Find(kategoriaAtrybutu.ParentCategoryID);
                List<Category> wynikpomoc = new List<Category>();
                IEnumerable<Category> wynik;

                if (rodzicKategoriaAtrybutu == null) // jeżeli atrybut należy do kategorii rdzenia to wypisujemy rdzenie
                {
                    foreach (Category kat in db.Categories.ToList())//kategorie rdzenie
                    {
                        if (kat.ParentCategoryID == null) wynikpomoc.Add(kat);
                    }

                }
                else // atrybut jest w kategorii innej niż rdzeń czyli np. Pojazdy,itp.
                {
                    foreach (Category kat in db.Categories.ToList())
                    {
                        if (kat.ParentCategoryID == rodzicKategoriaAtrybutu.CategoryID)
                        {
                            wynikpomoc.Add(kat);
                        }
                    }
                }
                wynik = wynikpomoc;


                ViewData["CategoryID"] = new SelectList(wynik, "CategoryID", "Nazwa", atrybut.CategoryID);
                ViewBag.Typ = JsonConvert.SerializeObject(new Models.Attribute { Typ = atrybut.Typ });


                return View(atrybut);
            }
            
        }


        public ActionResult UsunAtr(int? id) // usuwanie atrybutu - tabelka ze strony /Admin/EdytujAtrybut
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (zalogowany.Rola.Equals("USER")) return RedirectToAction("BrakOdpowiedniejRoli", "Error");
            if (id == null) return RedirectToAction("BrakID", "Error");

            Models.Attribute atrybut = db.Attributes.Find(id);

            if (atrybut == null) return RedirectToAction("NieMaTakiegoAtrybutu", "Error");
            Session["atrybut"] = atrybut;

            return View(atrybut);
        }


        [HttpPost]
        public ActionResult UsunAtr()
        {
            Models.Attribute atrybut = Session["atrybut"] as Models.Attribute;
            
            
            // usuwamy także wartości podanego atrybutu już wpisane w baze
            if(atrybut.Typ == "list") // jeżeli usuwamy atybut typu listowego usuwamy także słownik z nim związany
            {
                db.Dictionary.RemoveRange(db.Dictionary.Where(d=>d.AttributeID == atrybut.AttributeID));
                db.AddAttributes.RemoveRange(db.AddAttributes.Where(ad=>ad.AttributeID == atrybut.AttributeID));
                db.Attributes.Remove(db.Attributes.Find(atrybut.AttributeID));
                Session.Remove("atrybut");

                db.SaveChanges();

                return RedirectToAction("EdytujAtrybut", "Admin");
            }
            else // usuwamy atrybut typu nie listowego
            {
                db.AddAttributes.RemoveRange(db.AddAttributes.Where(ad => ad.AttributeID == atrybut.AttributeID));
                db.Attributes.Remove(db.Attributes.Find(atrybut.AttributeID));
                Session.Remove("atrybut");

                db.SaveChanges();

                return RedirectToAction("EdytujAtrybut", "Admin");
            }

        }
    }
}