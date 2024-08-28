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
    public class Red_ConocimientoController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Red_Conocimiento
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Index()
        {
            return View(db.Red_Conocimiento.ToList());
        }

        // GET: Red_Conocimiento/Details/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Red_Conocimiento red_Conocimiento = db.Red_Conocimiento.Find(id);
            if (red_Conocimiento == null)
            {
                return HttpNotFound();
            }
            return View(red_Conocimiento);
        }

        // GET: Red_Conocimiento/Create
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Red_Conocimiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRed,NombreRed,CodigoRed,EstadoRed")] Red_Conocimiento red_Conocimiento)
        {
            if (ModelState.IsValid)
            {
                db.Red_Conocimiento.Add(red_Conocimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(red_Conocimiento);
        }

        // GET: Red_Conocimiento/Edit/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Red_Conocimiento red_Conocimiento = db.Red_Conocimiento.Find(id);
            if (red_Conocimiento == null)
            {
                return HttpNotFound();
            }
            return View(red_Conocimiento);
        }

        // POST: Red_Conocimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRed,NombreRed,CodigoRed")] Red_Conocimiento red_Conocimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(red_Conocimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(red_Conocimiento);
        }

        // GET: Red_Conocimiento/Delete/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Red_Conocimiento red_Conocimiento = db.Red_Conocimiento.Find(id);
            if (red_Conocimiento == null)
            {
                return HttpNotFound();
            }
            return View(red_Conocimiento);
        }

        // POST: Red_Conocimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Red_Conocimiento red_Conocimiento = db.Red_Conocimiento.Find(id);
            db.Red_Conocimiento.Remove(red_Conocimiento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Area_Red(int id)
        {
            var areas = db.Area_Conocimiento
         .Where(a => a.IdRed == id) // Filtrar por IdRed
         .ToList();

            ViewBag.RedId = id;
            ViewBag.NombreRed = db.Red_Conocimiento.Find(id)?.NombreRed;
            return View(areas);
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Programa_Area_Red(int idRed, int id)
        {
            var areas = db.Programa_Formacion
         .Where(a => a.Area_Conocimiento.IdRed == idRed && a.IdArea == id) // Filtrar por IdRed
         .ToList();

            ViewBag.RedId = idRed;
            ViewBag.AreaId = id;
            ViewBag.NombreRed = db.Red_Conocimiento.Find(idRed)?.NombreRed;
            ViewBag.NombreArea = db.Area_Conocimiento.Find(id)?.NombreArea;
            return View(areas);
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Ficha_Programa_Area_Red(int idRed, int idArea, int id)
        {
            var areas = db.Ficha
         .Where(a => a.Programa_Formacion.Area_Conocimiento.IdRed == idRed && a.Programa_Formacion.IdArea == idArea && a.IdPrograma == id) // Filtrar por IdRed
         .ToList();

            ViewBag.RedId = idRed;
            ViewBag.AreaId = idArea;
            ViewBag.ProgramaId = id;
            ViewBag.NombreRed = db.Red_Conocimiento.Find(idRed)?.NombreRed;
            ViewBag.NombreArea = db.Area_Conocimiento.Find(id)?.NombreArea;
            ViewBag.NombrePrograma = db.Programa_Formacion.Find(id)?.DenominacionPrograma;
            return View(areas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
