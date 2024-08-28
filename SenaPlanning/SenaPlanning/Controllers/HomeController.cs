using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClaseModelo;
using static SenaPlanning.Controllers.LoginController;

namespace SenaPlanning.Controllers
{
    public class HomeController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Index()
        {
            return View(db.Meta.ToList());
        }

      
    }
}