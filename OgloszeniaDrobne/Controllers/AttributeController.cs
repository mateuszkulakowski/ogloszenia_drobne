using OgloszeniaDrobne.DAL;
using OgloszeniaDrobne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgloszeniaDrobne.Controllers
{
    public class AttributeController : Controller
    {

        private DbContext db = new DbContext();
        
        public String getAttributesCategory(int id)// metoda do wypisywania w tabelce atrybutów danej kategorii (do edytowania atrybutów)
        {
            String wynik = "";
            bool czy_byl_wynik = false;
            Category wybranaKategoria = db.Categories.Find(id);

            if (wybranaKategoria == null) return "";
            else
            {
                wynik = "<table class=\"table table-hover\">" +
                    "<thead>" +
                        "<th>Nazwa Atrybutu</th>" +
                        "<th>Typ</th>" +
                        "<th>Opcje</th>" +
                    "</thead>";

                //pobieranie atrybutów danej kategorii
                List<Models.Attribute> listaAtrybutówKategorii = db.Attributes.Where(a => a.CategoryID == wybranaKategoria.CategoryID).ToList();
   
                foreach(Models.Attribute atrybut in listaAtrybutówKategorii)
                {
                    czy_byl_wynik = true;
                    wynik += "<tr>";
                    wynik += "<td>"+atrybut.Nazwa+"</td>";
                    wynik += "<td>"+atrybut.Typ+"</td>";
                    wynik += "<td> <a href=\"/Admin/EdytujAtr/"+atrybut.AttributeID+ "\">Edytuj</a> | <a href=\"/Admin/UsunAtr/" + atrybut.AttributeID + "\">Usuń</a>  </td>";
                    wynik += "</tr>";
                }

                wynik += "</table>";
            }

            if (!czy_byl_wynik) wynik = "<span style=\"color:red;\">Ta kategoria nie posiada atrybutów!</span>";
            

            return wynik;
        }

    }
}