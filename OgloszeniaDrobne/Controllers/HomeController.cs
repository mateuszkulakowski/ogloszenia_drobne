using OgloszeniaDrobne.DAL;
using OgloszeniaDrobne.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OgloszeniaDrobne.Controllers
{
    public class HomeController : Controller
    {
        private DbContext db = new DbContext();

        public ActionResult Index(int? page)
        {
            var zalogowany = Session["zalogowany"] as User;

            Session["requestfrom"] = "Index";
            var listaOgloszen = db.Adds.ToList();

            if(zalogowany == null)
            {
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(listaOgloszen.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                int pageSize = (zalogowany.IloscOgloszenStrona ?? 10);
                int pageNumber = (page ?? 1);
                return View(listaOgloszen.ToPagedList(pageNumber, pageSize));
            }
        }

        




    }
}