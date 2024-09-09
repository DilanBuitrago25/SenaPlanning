using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public ActionResult Index()
        {
            MetaAndOferta metaAndOferta = new MetaAndOferta
            {
                Metas = db.Meta.Where(m => m.MetaFecha == DateTime.Now.Year.ToString()).ToList(),
                Ofertas = db.Oferta.Where(o => o.Meta.MetaFecha == DateTime.Now.Year.ToString()).ToList()

            };

            return View(db.Meta.Where(m => m.MetaFecha == DateTime.Now.Year.ToString()).ToList());
        }
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meta meta = db.Meta.Find(id);
            if (meta == null)
            {
                return HttpNotFound();
            }
            return View(meta);
        }
    }
}