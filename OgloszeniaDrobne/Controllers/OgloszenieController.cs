using OgloszeniaDrobne.DAL;
using OgloszeniaDrobne.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OgloszeniaDrobne.Controllers
{
    public class OgloszenieController : Controller
    {
        private OgloszeniaDrobne.DAL.DbContext db = new OgloszeniaDrobne.DAL.DbContext();
        
        public List<Category> getKorzenie()
        {
            return db.Categories.Where(c => c.ParentCategoryID == null).ToList();
        }
        
        private String getAttributesCategory(int CategoryID,String wynik, int? AddID)
        {
            
            List<Models.Attribute> atrybutydanejKategorii = db.Attributes.Where(a => a.CategoryID == CategoryID).ToList();

            foreach (Models.Attribute atrybut in atrybutydanejKategorii)
            {
                wynik += "<div class=\"form-group\">";

                //pobieranie danej wartości atrybutu z tabeli AddAtributes które to zawiera nasze ogłoszenie AddID..
                AddAtrribute addAtrribute = null;

                if (AddID != null) // wchodzi gdy podamy AddID do metody (czyli chcemy mieć pola ze zaznaczonymi selectami i wypełnionymi inputami
                {
                    try
                    {
                        addAtrribute = db.AddAttributes.Where(a => (a.AttributeID == atrybut.AttributeID) && (a.AddID == AddID)).First();
                    }
                    catch (Exception e) // wyrzucany gdy administrator dodał nowy atrybut
                    {
                        addAtrribute = null;
                    }
                }
                

                if (atrybut.Typ == "list") // jeżeli atrybut jest typu słownikowego
                {
                    wynik += "<label class=\"control-label col-md-2\" for=\""+atrybut.AttributeID+"\">"+atrybut.Nazwa+"</label>" +
                            "<div class=\"col-md-10\">" +
                                "<select class=\"form-control\" id=\"" + atrybut.AttributeID + "\" name=\"" + atrybut.AttributeID + "\" required>";

                    List<Dictionary> listawyboru = db.Dictionary.Where(d => d.AttributeID == atrybut.AttributeID).ToList();

                    foreach(Dictionary pojedynczywybór in listawyboru)
                    {
                        if(addAtrribute != null) // tutaj wchodzi z metody EDIT
                        {
                            if (pojedynczywybór.Pole.Equals(addAtrribute.Value))//to ta wartość w dictionary którą wybrał user -EDIT
                            {
                                wynik += "<option value=\"" + pojedynczywybór.Pole + "\" selected>" + pojedynczywybór.Pole + "</option>";
                            }
                            else // to ta wartość która nie była wybrana przez usera ale jest nadal do wyboru - EDIT
                            {
                                wynik += "<option value=\"" + pojedynczywybór.Pole + "\">" + pojedynczywybór.Pole + "</option>";
                            }
                        }
                        else // tutaj wchodzi metoda ADD bo addAtrribute == null
                        {
                            wynik += "<option value=\"" + pojedynczywybór.Pole + "\">" + pojedynczywybór.Pole + "</option>";
                        }

                    }

                    wynik += "</select>";
                    wynik += "</div>";
                }
                else if(atrybut.Typ == "text" || atrybut.Typ == "number") // jeżeli atrybut jest polem do wpisania (text,number)
                {
                    if(addAtrribute != null)
                    wynik += "<label class=\"control-label col-md-2\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                        "<div class=\"col-md-10\">" +
                             "<input value=\""+addAtrribute.Value+"\" class=\"form-control text-box single-line\" type=\"" + atrybut.Typ + "\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" required></input>" +
                        "</div>";

                    else
                    {
                        wynik += "<label class=\"control-label col-md-2\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                        "<div class=\"col-md-10\">" +
                             "<input class=\"form-control text-box single-line\" type=\"" + atrybut.Typ + "\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" required></input>" +
                        "</div>";
                    }
                }
                else if(atrybut.Typ == "textarea")
                {
                    if (addAtrribute != null)
                        wynik += "<label class=\"control-label col-md-2\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                            "<div class=\"col-md-10\">" +
                                 "<textarea class=\"form-control text-box single-line\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" required>"+addAtrribute.Value+"</textarea>" +
                            "</div>";

                    else
                    {
                        wynik += "<label class=\"control-label col-md-2\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                        "<div class=\"col-md-10\">" +
                             "<textarea class=\"form-control text-box single-line\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" required></textarea>" +
                        "</div>";
                    }
                }
                else if(atrybut.Typ == "double")
                {
                    if (addAtrribute != null)
                        wynik += "<label class=\"control-label col-md-2\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                            "<div class=\"col-md-10\">" +
                                 "<input value=\"" + addAtrribute.Value + "\" class=\"form-control text-box single-line\" type=\"number\" step=\"0.01\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" required></input>" +
                            "</div>";

                    else
                    {
                        wynik += "<label class=\"control-label col-md-2\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                        "<div class=\"col-md-10\">" +
                             "<input class=\"form-control text-box single-line\" type=\"number\" step=\"0.01\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" required></input>" +
                        "</div>";
                    }

                }
                else if (atrybut.Typ == "checkbox") // jeżeli atrybut jest polem do wpisania (text,number)
                {
                    if (addAtrribute != null)
                    {
                        wynik += "<label class=\"control-label col-md-2\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                            "<div class=\"col-md-10\">";
                        wynik += "<input class=\"form-control text-box single-line\" type=\"" + atrybut.Typ + "\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" ></input>";
                        wynik += "</div>";
                    }

                    else
                    {
                        wynik += "<label class=\"control-label col-md-2\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                        "<div class=\"col-md-10\">" +
                             "<input class=\"form-control text-box single-line\" type=\"" + atrybut.Typ + "\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" ></input>" +
                        "</div>";
                    }
                }

                wynik += "</div>";
            }

            return wynik;
        }


        public ActionResult getExtra(int id)//generowanie atrybutów bez wartości pól - ADD - przyjmuję ID Kategorii
        {
            Category wybranaKategoria = null;
            String wynik = "";
            int flaga = 1;

            do
            {
                if (flaga == 1) wybranaKategoria = db.Categories.Find(id);
                else wybranaKategoria = db.Categories.Find(wybranaKategoria.ParentCategoryID);
                wynik = getAttributesCategory(wybranaKategoria.CategoryID, wynik, null);
                flaga++;

            } while (wybranaKategoria.ParentCategoryID != null);
            

            return Content(wynik, "text/html");
        }


        public ActionResult getExtraWithValues(int id)//generowanie atrybutów z wartościami pól - EDIT - przyjmuję ID Ogłoszenia
        {

            Add add = db.Adds.Find(id);

            int AddID = add.AddID;
            int CatID = add.CategoryID;

            Category wybranaKategoria = null;
            String wynik = "";
            int flaga = 1;

            do
            {
                if (flaga == 1) wybranaKategoria = db.Categories.Find(CatID);
                else wybranaKategoria = db.Categories.Find(wybranaKategoria.ParentCategoryID);
                wynik = getAttributesCategory(wybranaKategoria.CategoryID, wynik, id);
                flaga++;

            } while (wybranaKategoria.ParentCategoryID != null);


            return Content(wynik, "text/html");
        }


        private String getAttributesZaawansowane(int CategoryID, String wynik)
        {
            List<Models.Attribute> atrybutydanejKategorii = db.Attributes.Where(a => a.CategoryID == CategoryID).ToList();

            foreach (Models.Attribute atrybut in atrybutydanejKategorii)
            {
                wynik += "<div class=\"form-group\">";

                if (atrybut.Typ == "list") // jeżeli atrybut jest typu słownikowego
                {
                    wynik += "<label class=\"control-label \" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                                "<select class=\"form-control\" id=\"" + atrybut.AttributeID + "\" name=\"" + atrybut.AttributeID + "\" required>";

                    List<Dictionary> listawyboru = db.Dictionary.Where(d => d.AttributeID == atrybut.AttributeID).ToList();

                    wynik += "<option value=\"-1\">Nie wybrano...</option>";

                    foreach (Dictionary pojedynczywybór in listawyboru)
                    {
                            wynik += "<option value=\"" + pojedynczywybór.Pole + "\">" + pojedynczywybór.Pole + "</option>";
                    }

                    wynik += "</select>";
                    wynik += "</div>";
                }
                else if (atrybut.Typ == "text") // jeżeli atrybut jest polem do wpisania (text,number)
                {
                        wynik += "<label class=\"control-label\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                             "<input class=\"form-control\" type=\"" + atrybut.Typ + "\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" ></input>" +
                        "</div>";
                    
                }
                else if(atrybut.Typ == "number")
                {
                    wynik += "<label class=\"control-label\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                             "<input class=\"form-control\" type=\"" + atrybut.Typ + "\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" ></input>" +
                             "<div class=\"radio\">" +
                                "<label><input type=\"radio\" id=\"" + atrybut.AttributeID + "opcja\" name=\"" + atrybut.AttributeID + "opcja\" value=\"wieksze\" checked>Wartości większe</label>" +
                            "</div>" +
                            "<div class=\"radio\">" +
                                "<label><input type=\"radio\" id=\"" + atrybut.AttributeID + "opcja\" name=\"" + atrybut.AttributeID + "opcja\" value=\"mniejsze\">Wartości mniejsze</label>" +
                            "<br/><br/></div>" +
                        "</div>";
                }
                else if (atrybut.Typ == "textarea")
                {
                    
                        wynik += "<label class=\"control-label\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                             "<textarea class=\"form-control\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" ></textarea>" +
                        "</div>";
                    
                }
                else if (atrybut.Typ == "double")
                {
                    
                        wynik += "<label class=\"control-label\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                             "<input class=\"form-control\" type=\"number\" step=\"0.01\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" ></input>" +
                            "<div class=\"radio\">"+
                                "<label><input type=\"radio\" id=\"" + atrybut.AttributeID + "opcja\" name=\"" + atrybut.AttributeID+"opcja\" value=\"wieksze\" checked>Wartości większe</label>"+
                            "</div>"+
                            "<div class=\"radio\">"+
                                "<label><input type=\"radio\" id=\"" + atrybut.AttributeID + "opcja\" name=\"" + atrybut.AttributeID+"opcja\" value=\"mniejsze\">Wartości mniejsze</label>"+
                            "<br/><br/></div>"+
                           "</div>";
                    

                }
                else if (atrybut.Typ == "checkbox") // jeżeli atrybut jest polem do wpisania (text,number)
                {
                        wynik += "<label class=\"control-label\" for=\"" + atrybut.AttributeID + "\">" + atrybut.Nazwa + "</label>" +
                             "<input class=\"form-control\" type=\"" + atrybut.Typ + "\" name=\"" + atrybut.AttributeID + "\" id=\"" + atrybut.AttributeID + "\" ></input>"+
                        "</div>";
                }

                wynik += "</div>";
            }

            return wynik;
        }



        public ActionResult getExtraZaawansowane(int id)
        {
            Category wybranaKategoria = null;
            String wynik = "";
            int flaga = 1;

            do
            {
                if (flaga == 1) wybranaKategoria = db.Categories.Find(id);
                else wybranaKategoria = db.Categories.Find(wybranaKategoria.ParentCategoryID);
                wynik = getAttributesZaawansowane(wybranaKategoria.CategoryID, wynik);
                flaga++;

            } while (wybranaKategoria.ParentCategoryID != null);


            return Content(wynik, "text/html");
        }

        public ActionResult getChildrenCategory(int id)//bierze id wybranej kategorii i zwraca dzieci tej kategorii
        {
            
            String wynik = "";
            Category wybranaKategoria = db.Categories.Find(id);
            List<Category> listaKategorii = db.Categories.ToList();
            Boolean czy_foreach_coś_zwrócił = false;

            wynik += "<option value=\"-1\" name=\"-1\" selected>Wybierz podkategorię..</option>";

            foreach (Category c in listaKategorii)
            {
                if(c.ParentCategoryID == wybranaKategoria.CategoryID)
                {
                    wynik += "<option value=\""+c.CategoryID+ "\" name=\"" + c.CategoryID + "\">"+c.Nazwa+"</option>";
                    czy_foreach_coś_zwrócił = true;
                }
            }

            if (czy_foreach_coś_zwrócił) return Content(wynik);
            else return Content(null);
        }


        public ActionResult previousCategories(int id)//zwraca null gdy już nie da sie cofnąć lub listę kategorii poprzednich czyli jak jest wybrane np. Opel zwróci np. (pojazdy,traktory,motocykle...)
        {
            String wynik = "";

            Category dzieckoKategoria = db.Categories.Find(id);


            if (dzieckoKategoria.ParentCategoryID == null)//dziecko jest już korzeniem zwracamy null 
            {
                return Content(null);
            }
            else
            {
                Category rodzicKategoria = db.Categories.Find(dzieckoKategoria.ParentCategoryID);

                if(rodzicKategoria.ParentCategoryID == null)//nasz rodzic jest korzeniem więc wypisujemy wszystkie korzenie drzewa kategorii gdy jest to kategoria np. Pojazd i musi zwrócić(motoryzzacja,elektronika,...) -korzenie
                {
                    wynik += "<option value=\"-1\" name=\"-1\" selected>Wybierz kategorię..</option>";

                    List<Category> kategorie = db.Categories.ToList();

                    foreach(Category category in kategorie)
                    {
                        if(category.ParentCategoryID == null)
                        {
                            wynik += "<option value=\"" + category.CategoryID + "\" name=\"" + category.CategoryID + "\">" + category.Nazwa + "</option>";
                        }
                    }

                    return Content(wynik);
                }
                else // gdy dziecko było kategorią np.Opel musimy zwrócić (osobowe,traktory itp.)
                {
                    Category rodzicrodzicaKategoria = db.Categories.Find(rodzicKategoria.ParentCategoryID);//musimy zwrócić dzieci danej kategorii więc skorzystamy już z napisanej funkcji getChildrenCategory.

                    List<Category> listaKategorii = db.Categories.ToList();
                    Boolean czy_foreach_coś_zwrócił = false;

                    if(rodzicrodzicaKategoria.ParentCategoryID == null)
                    wynik += "<option value=\"-1\" name=\"-1\" selected>Wybierz kategorię..</option>";
                    else wynik += "<option value=\"-1\" name=\"-1\" selected>Wybierz podkategorię..</option>";

                    foreach (Category c in listaKategorii)
                    {
                        if (c.ParentCategoryID == rodzicrodzicaKategoria.CategoryID)
                        {
                            wynik += "<option value=\"" + c.CategoryID + "\" name=\"" + c.CategoryID + "\">" + c.Nazwa + "</option>";
                            czy_foreach_coś_zwrócił = true;
                        }
                    }

                    if (czy_foreach_coś_zwrócił) return Content(wynik);
                    else return Content(null);
                }
            }
            
        }


        public ActionResult Dodaj()
        {
            if (Session["zalogowany"] == null) return RedirectToAction("NieZalogowany", "Error");
            else
            {
                List<Category> kategorie = db.Categories.ToList();

                Boolean flaga = true;

                List<Category> przejsciowka = new List<Category>();
                IEnumerable<Category> kategorieRdzenie;
                

                foreach (Category kat in kategorie)
                {
                    if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
                }

                przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

                kategorieRdzenie = przejsciowka;

                ViewData["CategoryID"] = new SelectList(kategorieRdzenie, "CategoryID", "Nazwa", "-1");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Dodaj([Bind(Include = "AddID,Title,Content,TelephoneNumber,Price,CategoryID")] Add add, FormCollection formCollection)//(FormCollection formCollection)
        {
            // sprwdzenie czy kategoria dana nie ma dzieci !
            Category wybranakategoria = db.Categories.Find(add.CategoryID);
            Boolean czy_ma_dzieci = false;
            foreach(Category category in db.Categories.ToList())
            {
                try
                {
                    if (wybranakategoria.CategoryID == category.ParentCategoryID)
                    {
                        czy_ma_dzieci = true;
                        break;
                    }
                }catch(NullReferenceException e)//występuje gdy kategoria nie jest wybrana jest zaznaczone (Wybierz kategorię..)
                {
                    ViewBag.komunikatBrakKategorii = "Nie wybrałeś kategorii!\n";
                    break;
                }
            }


            if (ModelState.IsValid && add.CategoryID!=-1 && !czy_ma_dzieci)//jeżeli wybrano etykietę o id=-1 to nie wchodzimy
            {

                // pobieranie wartości atrybutów dodatkowych
                String sPattern = "[0-9]+";
                Dictionary<int, string> listaAtrybutów = new Dictionary<int, string>();

                foreach (var key in formCollection.AllKeys) //pobieranie wartości wpisanych atrybutów
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(key, sPattern))
                    {
                        listaAtrybutów.Add(int.Parse(key), formCollection[key]);
                    }

                }

                List<BannedWord> lista = db.BannedWords.ToList();
                Boolean flaga = false;

                String title = add.Title;
                String content = add.Content;


                foreach (BannedWord banned in lista)
                {
                    if (title.ToUpper().Contains(banned.Word.ToUpper())) flaga = true;
                    if (content.ToUpper().Contains(banned.Word.ToUpper())) flaga = true;
                }



                if (!flaga)
                {
                    add.Data = System.DateTime.Now;
                    var zalogowany = Session["zalogowany"] as User;
                    add.UserID = zalogowany.UserID; //ustawianie autora ogłoszenia

                    db.Adds.Add(add);

                    for (int i = 0; i < listaAtrybutów.Count; i++)
                    {
                        int atrybutID = listaAtrybutów.ElementAt(i).Key;
                        string value = listaAtrybutów.ElementAt(i).Value;

                        Models.Attribute atrybut = db.Attributes.Find(atrybutID);
                        AddAtrribute addattr = null;

                        if (atrybut.Typ == "checkbox")
                        {
                            if (value == "on")
                            {
                                addattr = new AddAtrribute
                                {
                                    AddID = add.AddID,
                                    AttributeID = atrybutID,
                                    Value = "Tak"
                                };
                            }
                            else
                            {
                                addattr = new AddAtrribute
                                {
                                    AddID = add.AddID,
                                    AttributeID = atrybutID,
                                    Value = "Nie"
                                };
                            }
                        }
                        else
                        {
                            addattr = new AddAtrribute
                            {
                                AddID = add.AddID,
                                AttributeID = atrybutID,
                                Value = value
                            };
                        }

                        db.AddAttributes.Add(addattr);
                    }


                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else//słowo zawiera zakazane
                {
                    ViewBag.komunikatSłowoZakazane = "Ogłoszenie zawiera słowo zakazane!\n";
                    List<Category> kategorie = db.Categories.ToList();

                    Boolean flaga1 = true;

                    List<Category> przejsciowka = new List<Category>();
                    IEnumerable<Category> kategorieKorzenie;

                    foreach (Category kat in kategorie)
                    {
                        if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
                    }

                    przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

                    kategorieKorzenie = przejsciowka;

                    ViewData["CategoryID"] = new SelectList(kategorieKorzenie, "CategoryID", "Nazwa", "-1");

                    return View(add);
                }
            }
            else//niepoprawny formularz
            {
                if(czy_ma_dzieci) ViewBag.komunikatMaDzieci = "Podana kategoria nie może być wybrana!\n";

                List<Category> kategorie = db.Categories.ToList();

                Boolean flaga1 = true;

                List<Category> przejsciowka = new List<Category>();
                IEnumerable<Category> kategorieKorzenie;

                foreach (Category kat in kategorie)
                {
                    if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
                }

                przejsciowka.Add(new Category { CategoryID = -1, Nazwa = "Wybierz kategorię..." });

                kategorieKorzenie = przejsciowka;

                ViewData["CategoryID"] = new SelectList(kategorieKorzenie, "CategoryID", "Nazwa", "-1");

                return View(add);

            }
        }


        public ActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("BrakID", "Error");
            
            Add add = db.Adds.Find(id);
            add.LiczbaOdsłon = add.LiczbaOdsłon + 1;
            db.Entry(add).State = EntityState.Modified;
            db.SaveChanges();

            if (add == null)return RedirectToAction("BrakTakiegoOgloszenia", "Error");
            
            ViewBag.ListaWartosciAtrybutow = db.AddAttributes.Where(ad => ad.AddID == add.AddID).ToList();

            return View(add);
        }


        public ActionResult Edit(int? id)
        {
            var zalogowany = Session["zalogowany"] as User;
            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");
            if (id == null) return RedirectToAction("BrakID", "Error");

            Add add = db.Adds.Find(id);

            if (add == null)return RedirectToAction("BrakTakiegoOgloszenia", "Error");
            if (!add.UserID.Equals(zalogowany.UserID)) return RedirectToAction("NieJestesWlascicielem", "Error");

            //bierzemy rodzica kategorii ogłoszenia żeby wyświetlić jego wszystkie dzieci
            Category kategoriaOgłoszenia = db.Categories.Find(add.CategoryID);
            Category rodzicKategoriiOgłoszenia;

            List<Category> kategorie = db.Categories.ToList();
            List<Category> przejsciowka = new List<Category>();
            IEnumerable<Category> kategorie_bez_dzieci;

            try
            {
                rodzicKategoriiOgłoszenia = db.Categories.Where(c => c.CategoryID == kategoriaOgłoszenia.ParentCategoryID).First();
            }
            catch(InvalidOperationException ex) // wchodzi gdy kategoria nie ma dzieci czyli wypisujemy korzenie
            {
                foreach (Category kat in kategorie)
                {
                    if (kat.ParentCategoryID == null) przejsciowka.Add(kat);
                }

                kategorie_bez_dzieci = przejsciowka;

                ViewData["CategoryID"] = new SelectList(kategorie_bez_dzieci, "CategoryID", "Nazwa");
                ViewBag.AddID = add.AddID;
                ViewBag.CatID = add.CategoryID;
                return View(add);

            }
            
            // wybieramy dzieci kategorii rodzica czyli wszystkie kategorie na równi z kategoriąogłoszenia np. jak Seat to: Skoda,Toyota itp...
            foreach (Category kat in kategorie)
                {
                    if (kat.ParentCategoryID == rodzicKategoriiOgłoszenia.CategoryID) przejsciowka.Add(kat);
                }

            kategorie_bez_dzieci = przejsciowka;

            ViewData["CategoryID"] = new SelectList(kategorie_bez_dzieci, "CategoryID", "Nazwa");
            ViewBag.AddID = add.AddID;
            ViewBag.CatID = add.CategoryID;
            return View(add);
            

        }
        



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddID,Title,Content,TelephoneNumber,Price,CategoryID")] Add add, FormCollection formCollection)//(FormCollection formCollection)
         {
            // sprwdzenie czy kategoria dana nie ma dzieci !
            Category wybranakategoria = db.Categories.Find(add.CategoryID);
            Boolean czy_ma_dzieci = false;
            foreach (Category category in db.Categories.ToList())
            {
                try
                {
                    if (wybranakategoria.CategoryID == category.ParentCategoryID)
                    {
                        czy_ma_dzieci = true;
                        break;
                    }
                }
                catch (NullReferenceException e)//występuje gdy kategoria nie jest wybrana jest zaznaczone (Wybierz kategorię..)
                {
                    ViewBag.komunikatBrakKategorii = "Nie wybrałeś kategorii!\n";
                    break;
                }
            }

            //sprawdzanie czy zawiera słowo zakazane

            List<BannedWord> lista = db.BannedWords.ToList();
            Boolean czy_zawiera_słowo_zakazane = false;

            String title = add.Title;
            String content = add.Content;


            foreach (BannedWord banned in lista)
            {
                if (title.ToUpper().Contains(banned.Word.ToUpper())) czy_zawiera_słowo_zakazane = true;
                if (content.ToUpper().Contains(banned.Word.ToUpper())) czy_zawiera_słowo_zakazane = true;
            }



            if (ModelState.IsValid && add.CategoryID!=-1 && !czy_ma_dzieci && !czy_zawiera_słowo_zakazane)
            {
                // pobieranie wartości atrybutów dodatkowych
                String sPattern = "[0-9]+";
                Dictionary<int, string> listaAtrybutów = new Dictionary<int, string>();

                foreach (var key in formCollection.AllKeys) //pobieranie wartości wpisanych atrybutów
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(key, sPattern))
                    {
                        listaAtrybutów.Add(int.Parse(key), formCollection[key]);
                    }

                }


                //na początku należy usunąć wcześniej ustawione wartości atrybutów
                List<AddAtrribute> listaWartościAtrybutów = db.AddAttributes.Where(a => a.AddID == add.AddID).ToList();

                foreach(AddAtrribute addAttribute in listaWartościAtrybutów)
                {
                    db.AddAttributes.Remove(addAttribute);
                }

                add.Data = DateTime.Now;
                var zalogowany = Session["zalogowany"] as User;
                add.UserID = zalogowany.UserID;

                DAL.DbContext db2 = new DAL.DbContext();

                Add staryAdd = db2.Adds.Find(add.AddID);
                add.LiczbaOdsłon = staryAdd.LiczbaOdsłon;

                db.Entry(add).State = EntityState.Modified;

                //dodajemy wartości atrybutów nowo ustalonych
                for (int i = 0; i < listaAtrybutów.Count; i++)
                {
                    int atrybutID = listaAtrybutów.ElementAt(i).Key;
                    string value = listaAtrybutów.ElementAt(i).Value;

                    Models.Attribute atrybut = db.Attributes.Find(atrybutID);
                    AddAtrribute addattr = null;

                    if (atrybut.Typ == "checkbox")
                    {
                        if (value == "on")
                        {
                            addattr = new AddAtrribute
                            {
                                AddID = add.AddID,
                                AttributeID = atrybutID,
                                Value = "Tak"
                            };
                        }
                        else
                        {
                            addattr = new AddAtrribute
                            {
                                AddID = add.AddID,
                                AttributeID = atrybutID,
                                Value = "Nie"
                            };
                        }
                    }
                    else
                    {
                        addattr = new AddAtrribute
                        {
                            AddID = add.AddID,
                            AttributeID = atrybutID,
                            Value = value
                        };
                    }

                    db.AddAttributes.Add(addattr);
                }
                db.SaveChanges();

                return RedirectToAction("MojeOgloszenia","Ogloszenie");
            }
            else // formularz jest nie poprawny
            {
                    if(czy_ma_dzieci) ViewBag.komunikatMaDzieci = "Podana kategoria nie może być wybrana!\n";
                    if(czy_zawiera_słowo_zakazane) ViewBag.komunikatSłowoZakazane = "Ogłoszenie zawiera słowo zakazane!\n";
                //pobieranie z bazy ustawionych wartości ogłoszenia

                Add addBaza = db.Adds.Find(add.AddID);

                    Category kategoriaOgłoszenia = db.Categories.Find(addBaza.CategoryID);
                    Category rodzicKategoriiOgłoszenia = db.Categories.Where(c => c.CategoryID == kategoriaOgłoszenia.ParentCategoryID).First();
                    List<Category> kategorie = db.Categories.ToList();
                    List<Category> przejsciowka = new List<Category>();
                    IEnumerable<Category> kategorie_bez_dzieci;

                    foreach (Category kat in kategorie)
                    {
                        if (kat.ParentCategoryID == rodzicKategoriiOgłoszenia.CategoryID) przejsciowka.Add(kat);
                    }

                    kategorie_bez_dzieci = przejsciowka;

                    ViewData["CategoryID"] = new SelectList(kategorie_bez_dzieci, "CategoryID", "Nazwa");
                    ViewBag.AddID = addBaza.AddID;
                    ViewBag.CatID = addBaza.CategoryID;
                    return View(add);
                
                
            }
            
        }

        public ActionResult Delete(int? id)
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");//czy zalogowany
            if (id == null)return RedirectToAction("BrakID", "Error");

            Add add = db.Adds.Find(id);
            
            if (add == null) return RedirectToAction("BrakTakiegoOgloszenia","Error");
            if (!add.UserID.Equals(zalogowany.UserID) && zalogowany.Rola.Equals("USER")) return RedirectToAction("NieJestesWlascicielem", "Error");

            return View(add);
        }
        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var zalogowany = Session["zalogowany"] as User;
            

            Add add = db.Adds.Find(id);

            List<AddAtrribute> listaAtrybutów = db.AddAttributes.Where(a => a.AddID == add.AddID).ToList();

            foreach(AddAtrribute addAtrribute in listaAtrybutów)
            {
                db.AddAttributes.Remove(addAtrribute);
            }

            
            db.Adds.Remove(add);
            db.SaveChanges();

            if (zalogowany.Rola.Equals("USER"))
                return RedirectToAction("MojeOgloszenia"); // jeżeli to user to wracamy do jego ogłoszeń
            else return RedirectToAction("Index", "Home"); // jeżeli to admin to do strony z listą wszystkich ogłoszen wracamy
        }


        [HttpPost]
        public ActionResult SzukajOgłoszeniaKategoria(FormCollection formCollection)
        {
            try
            {
                int catID = int.Parse(formCollection["wybranaKategoria"]);

                Boolean czy_liść = true;
                List<Add> listaWynikowa = new List<Add>();

                foreach(Category category in db.Categories.ToList())
                {
                    czy_liść = true;

                    foreach(Category następnicyCategory in db.Categories.ToList())//sprawdzenie czy category jest liściem
                    {
                        if(category.CategoryID == następnicyCategory.ParentCategoryID)
                        {
                            czy_liść = false;
                            break;
                        }
                    }

                    if (czy_liść)//wchodzi gdy category nie jest liściem
                    {
                        if (category.ParentCategoryID == null && category.CategoryID == catID)//liść jest korzeniem czyli wypisujemy jego ogłoszenia do wyniku
                        {
                            var listaOgłoszeń = db.Adds.Where(a => a.CategoryID == category.CategoryID).ToList();

                            if (listaOgłoszeń.Count == 0) return RedirectToAction("BrakOgłoszeńWKategorii", "Error");
                            return View(listaOgłoszeń);
                        }

                        if (category.CategoryID == catID)// szukana kategoria jest liściem więc tylko ogłoszenia liścia zwracamy
                        {
                            var listaOgłoszeń = db.Adds.Where(a => a.CategoryID == category.CategoryID).ToList();

                            if (listaOgłoszeń.Count == 0) return RedirectToAction("BrakOgłoszeńWKategorii", "Error");
                            return View(listaOgłoszeń);
                        }
                        //dalej sprawdzamy poprzedników danego liścia category to liść
                        Category pop = db.Categories.Find(category.ParentCategoryID);//bierzemy poprzednika
                        

                        do // idziemy w kierunku korzenia szukając czy szukana kategoria nie wystąpiła, jak wystąpiła dodajemy ogłoszenia aktualnego liścia do listyWynikowej
                        {
                            try
                            {
                                if (pop.CategoryID == catID)//gdy natrafimy na kategorię którą szukamy to dodajemy liść do listy
                                {
                                    List<Add> listaPomocnicza = db.Adds.Where(a => a.CategoryID == category.CategoryID).ToList();

                                    foreach (Add add in listaPomocnicza)
                                    {
                                        listaWynikowa.Add(add);
                                    }
                                    break;
                                }

                            
                                pop = db.Categories.Find(pop.ParentCategoryID);
                            }
                            catch(NullReferenceException ex)
                            {
                                break;
                            }

                        }
                        while(true);
                        
                    }
  
                }

                if (listaWynikowa.Count == 0) return RedirectToAction("BrakOgłoszeńWKategorii", "Error");
                return View(listaWynikowa);
                

            }
            catch(ArgumentNullException ex)//gdy wywołamy metodę /Ogloszenia/SzukajOgłoszeniaKategoria bez uzupełnienia formularza
            {
                return RedirectToAction("BrakOgłoszeńWKategorii", "Error");
            }

            
        }

        [HttpPost]
        public ActionResult SzukajOgloszeniaSlowoKlucz(FormCollection formCollection)
        {
            String opcja = formCollection["opcja"];
            String slowaklucze = formCollection["slowaklucze"];

            string[] splits = slowaklucze.Split(' ');
            List<Add> listaWynikowa = new List<Add>();

            if (opcja == "and")
            {
                bool flaga = false;
                foreach (String slowoklucz in splits)
                {
                    if (!flaga)
                    {
                        listaWynikowa = db.Adds.Where(a => a.Title.ToUpper().Contains(slowoklucz.ToUpper()))
                            .ToList();

                        if (listaWynikowa.Count == 0) return RedirectToAction("BrakOgłoszeńOPodanymSłowieKluczowym", "Error");
                        flaga = true;

                    }
                    else
                    {
                        List<int> addIDdoUsuniecia = new List<int>();

                        for (int i = 0; i < listaWynikowa.Count; i++)
                        {
                            if (!listaWynikowa[i].Title.ToUpper().Contains(slowoklucz.ToUpper()))
                            {
                                addIDdoUsuniecia.Add(listaWynikowa[i].AddID);
                            }
                        }

                        foreach (int addid in addIDdoUsuniecia)
                        {
                            listaWynikowa.RemoveAll(p => p.AddID == addid);
                        }


                        if (listaWynikowa.Count == 0) return RedirectToAction("BrakOgłoszeńOPodanymSłowieKluczowym", "Error");
                    }

                }

                if (listaWynikowa.Count == 0) return RedirectToAction("BrakOgłoszeńOPodanymSłowieKluczowym", "Error");
                return View(listaWynikowa);

            }
            else if (opcja == "or")
            {
                List<Add> listaPomocnicza = new List<Add>();

                foreach (String slowoklucz in splits)
                {
                    listaPomocnicza = db.Adds.Where(a => (a.Title.ToUpper().Contains(slowoklucz.ToUpper()))).ToList();

                    if (listaWynikowa.Count == 0)
                    {
                        listaWynikowa = listaPomocnicza;
                    }
                    else // sprawdzamy żeby sie ogłoszenia nie powtarzały
                    {
                        foreach (Add addPomocnicza in listaPomocnicza)
                        {
                            bool czy_już_jest = false;

                            foreach (Add addwynikowa in listaWynikowa)
                            {
                                if (addwynikowa.AddID == addPomocnicza.AddID)
                                {
                                    czy_już_jest = true;
                                    break;
                                }
                            }

                            if (!czy_już_jest)//ogłoszenia nie ma w naszej liście wynikowej to dodajemy
                            {
                                listaWynikowa.Add(addPomocnicza);
                            }

                        }

                    }

                }

                if (listaWynikowa.Count == 0) return RedirectToAction("BrakOgłoszeńOPodanymSłowieKluczowym", "Error");
                return View(listaWynikowa);
            }
            else // opcja "not"
            {
                bool flaga = false;
                foreach (String slowoklucz in splits)
                {
                    if (!flaga)
                    {
                        listaWynikowa = db.Adds.Where(a => !a.Title.ToUpper().Contains(slowoklucz.ToUpper()))
                            .ToList();
                        
                        if (listaWynikowa.Count == 0) return RedirectToAction("BrakOgłoszeńOPodanymSłowieKluczowym", "Error");
                        flaga = true;

                    }
                    else // tu wchodzi słowo 2,3, ... do n i usuwa z listy ogłoszenia które nie pasują
                    {
                        List<int> addIDdoUsuniecia = new List<int>();

                        for (int i = 0; i < listaWynikowa.Count; i++)
                        {
                            if (listaWynikowa[i].Title.ToUpper().Contains(slowoklucz.ToUpper()))
                            {
                                addIDdoUsuniecia.Add(listaWynikowa[i].AddID);
                            }
                        }

                        foreach (int addid in addIDdoUsuniecia)
                        {
                            listaWynikowa.RemoveAll(p => p.AddID == addid);
                        }

                        if (listaWynikowa.Count == 0) return RedirectToAction("BrakOgłoszeńOPodanymSłowieKluczowym", "Error");
                    }

                }

                if (listaWynikowa.Count == 0) return RedirectToAction("BrakOgłoszeńOPodanymSłowieKluczowym", "Error");
                return View(listaWynikowa);
            }

        }

        [HttpPost]
        public ActionResult SzukajOgloszeniaZaawansowane(FormCollection formCollection)
        {
            String wzorPola = "\\d+$";
            String wzorOpcje = "\\d+opcja";
            int WybranaKategoriaID = int.Parse(formCollection["wybranaZaawansowane"]);
            Dictionary<int, string> listaPolWpisanych = new Dictionary<int, string>();
            Dictionary<string, string> listaOpcji = new Dictionary<string, string>();

            foreach (var key in formCollection.AllKeys) //pobieranie wartości wpisanych atrybutów
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(key, wzorPola))
                {
                    listaPolWpisanych.Add(int.Parse(key), formCollection[key]);
                }

                if (System.Text.RegularExpressions.Regex.IsMatch(key, wzorOpcje))
                {
                    listaOpcji.Add(key, formCollection[key]);
                }

            }

            Category wybranaKategoria = db.Categories.Find(WybranaKategoriaID);
            List<Category> kategorieLiście = new List<Category>();
            Boolean wybrana_kategoria_to_liść = false;

            OgloszeniaDrobne.DAL.DbContext db2 = new OgloszeniaDrobne.DAL.DbContext();
            OgloszeniaDrobne.DAL.DbContext db3 = new OgloszeniaDrobne.DAL.DbContext();


            foreach (Category kategoria in db2.Categories)// uzupełnianie kategorieLiście 
            {
                bool flaga = false;

                foreach (Category kategoriaDzieci in db3.Categories)
                {
                    if(kategoria.CategoryID == kategoriaDzieci.ParentCategoryID)
                    {
                        flaga = true;
                    }
                }

                if(!flaga)// czyli podana kategoria jest liściem
                {
                    if (kategoria.CategoryID == WybranaKategoriaID) wybrana_kategoria_to_liść = true;
                    kategorieLiście.Add(kategoria);
                }
            }

            List<Add> listaWynikowa = new List<Add>();


            if (wybrana_kategoria_to_liść)// bierzemy ogłoszenia tylko z tego liścia
            {
                listaWynikowa = db.Adds.Where(c => c.CategoryID == WybranaKategoriaID).ToList();
            }
            else // sprawdzamy czy liść należy do wybranej kategorii
            {
                foreach(Category liść in kategorieLiście)
                {
                    Category pomocnicza = null;
                    List<Add> pomocniczaAdd = db.Adds.Where(a => a.CategoryID == liść.CategoryID).ToList();
                    bool czy_pierwszy_raz = true;

                    do // sprawdzanie czy ten liść należy do kategorii
                    {
                        if(czy_pierwszy_raz)
                        {
                            pomocnicza = db.Categories.Find(liść.CategoryID);
                            czy_pierwszy_raz = false;
                        }
                        else
                        {
                            pomocnicza = db.Categories.Find(pomocnicza.ParentCategoryID);
                        }

                        if(pomocnicza.CategoryID == WybranaKategoriaID) // ten liść należy do naszej kategorii - dodajemy ogłoszenia z niego
                        {
                            foreach(Add add in pomocniczaAdd)
                            {
                                listaWynikowa.Add(add);
                            }
                            break;
                        }
                        

                    } while (pomocnicza.ParentCategoryID != null);
                }

            }
            String wynik = "";


            for (int i = 0; i < listaPolWpisanych.Count; i++)
            {
                int atrybutID = listaPolWpisanych.ElementAt(i).Key;
                string value = listaPolWpisanych.ElementAt(i).Value;

                Models.Attribute atrybut = db.Attributes.Find(atrybutID);

                if(atrybut.Typ == "number" || atrybut.Typ == "double") 
                {
                    if(value != "") // w polu coś sie znajduje
                    {
                        String pole_opcja = atrybut.AttributeID + "opcja";
                        String wieksze_mniejsze = listaOpcji.Where(lo=>lo.Key == pole_opcja).Select(s=>s.Value).First();    

                            List<int> pozycjeDoUsuniecia = new List<int>();

                        for (int j = 0; j < listaWynikowa.Count; j++)
                        {
                            try
                            {
                                AddAtrribute addAtributeOgloszenia = listaWynikowa[j].ListaDodatkowychAtr.Where(lda => lda.AttributeID == atrybut.AttributeID).First();

                                if (addAtributeOgloszenia == null) pozycjeDoUsuniecia.Add(listaWynikowa[j].AddID);
                                else if (wieksze_mniejsze == "wieksze") // wybrano opcję większe niż podana wartość
                                {
                                    if(atrybut.Typ == "double")
                                    {
                                        if (Double.Parse(addAtributeOgloszenia.Value.Replace('.',',')) < Double.Parse(value.Replace('.', ',')))
                                        {
                                            pozycjeDoUsuniecia.Add(listaWynikowa[j].AddID);
                                        }
                                    }
                                    else
                                    {
                                        if (int.Parse(addAtributeOgloszenia.Value) < int.Parse(value))
                                        {
                                            pozycjeDoUsuniecia.Add(listaWynikowa[j].AddID);
                                        }
                                    }
                                }
                                else// wybrano opcję mniejsze niż podana wartość
                                {
                                    if (atrybut.Typ == "double")
                                    {
                                        if (Double.Parse(addAtributeOgloszenia.Value.Replace('.', ',')) >= Double.Parse(value.Replace('.', ',')))
                                        {
                                            pozycjeDoUsuniecia.Add(listaWynikowa[j].AddID);
                                        }
                                    }
                                    else
                                    {
                                        if (int.Parse(addAtributeOgloszenia.Value) >= int.Parse(value))
                                        {
                                            pozycjeDoUsuniecia.Add(listaWynikowa[j].AddID);
                                        }
                                    }

                                }
                            }catch(InvalidOperationException ex) { pozycjeDoUsuniecia.Add(listaWynikowa[j].AddID); }
                        }

                        foreach (int ogloszenieIDdousuniecia in pozycjeDoUsuniecia)
                        {
                            if (listaWynikowa.Count == 1)
                            {
                                return RedirectToAction("BrakOgłoszeńZaawansowane", "Error");
                            }
                            listaWynikowa.RemoveAll(d => d.AddID == ogloszenieIDdousuniecia);
                        }


                    }
                    else //pole jest puste więc nie bierzemy go pod uwagę
                    {
                        continue;
                    }

                }
                else // wchodzą tu pola list,textarea bool itp.
                {

                    if (value != "") // w polu coś sie znajduje
                    {
                        if (atrybut.Typ=="list" && value == "-1")//jeżeli w liście nie wybraliśmy opcji robimy continue;
                        {
                            continue;
                        }


                        List<int> pozycjeDoUsuniecia = new List<int>();

                        for (int j = 0; j < listaWynikowa.Count; j++)
                        {
                            try
                            {
                                AddAtrribute addAtributeOgloszenia = listaWynikowa[j].ListaDodatkowychAtr.Where(lda => lda.AttributeID == atrybut.AttributeID).First();

                                if (addAtributeOgloszenia == null)
                                {
                                    pozycjeDoUsuniecia.Add(listaWynikowa[j].AddID);
                                }
                                else if (addAtributeOgloszenia.Value.ToUpper() != value.ToUpper()) // usuwamy z listy te ogłoszenia które nie równają sie wpisanej wartosci
                                {
                                    pozycjeDoUsuniecia.Add(listaWynikowa[j].AddID);
                                }

                            }
                            catch (InvalidOperationException ex) { pozycjeDoUsuniecia.Add(listaWynikowa[j].AddID); }
                            
                        }

                        foreach(int ogloszenieIDdousuniecia in pozycjeDoUsuniecia)
                        {
                            if (listaWynikowa.Count == 1)
                            {
                                return RedirectToAction("BrakOgłoszeńZaawansowane", "Error");
                            }
                            listaWynikowa.RemoveAll(d=>d.AddID == ogloszenieIDdousuniecia);
                        }
                    }
                    else //pole jest puste więc nie bierzemy go pod uwagę
                    {
                        continue;
                    }
                        
                }
                

            }
            var zalogowany = Session["zalogowany"] as User;

            if (listaWynikowa.Count == 0) return RedirectToAction("BrakOgłoszeńZaawansowane", "Error");
            return View(listaWynikowa);
            
        }





        public ActionResult MojeOgloszenia(int? page)
        {
            var zalogowany = Session["zalogowany"] as User;

            if (zalogowany == null) return RedirectToAction("NieZalogowany", "Error");

            Session["requestfrom"] = "MojeOgloszenia";
            
            var listaMoichOgloszen = db.Adds.Where(a => a.UserID == zalogowany.UserID).ToList();

            int pageSize = (zalogowany.IloscOgloszenStrona ?? 10);
            int pageNumber = (page ?? 1);
            return View(listaMoichOgloszen.ToPagedList(pageNumber, pageSize));
            
        }


    }
}