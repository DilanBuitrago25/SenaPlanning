using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClaseModelo;

namespace SenaPlanning.Controllers
{
    public class HomeController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        public ActionResult Index()
        {
            return View(db.Meta.ToList());
        }

    }
}