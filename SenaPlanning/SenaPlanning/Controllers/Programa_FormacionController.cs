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
    public class Programa_FormacionController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Programa_Formacion
        public ActionResult Index()
        {
            var programa_Formacion = db.Programa_Formacion.Include(p => p.Area_Conocimiento);
            return View(programa_Formacion.ToList());
        }

        // GET: Programa_Formacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programa_Formacion programa_Formacion = db.Programa_Formacion.Find(id);
            if (programa_Formacion == null)
            {
                return HttpNotFound();
            }
            return View(programa_Formacion);
        }

        // GET: Programa_Formacion/Create
        public ActionResult Create()
        {
            ViewBag.IdArea = new SelectList(db.Area_Conocimiento, "IdArea", "NombreArea");
            return View();
        }

        // POST: Programa_Formacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrograma,DenominacionPrograma,VersionPrograma,NivelPrograma,IdArea,EstadoPrograma")] Programa_Formacion programa_Formacion)
        {
            if (ModelState.IsValid)
            {
                db.Programa_Formacion.Add(programa_Formacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArea = new SelectList(db.Area_Conocimiento, "IdArea", "NombreArea", programa_Formacion.IdArea);
            return View(programa_Formacion);
        }

        // GET: Programa_Formacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programa_Formacion programa_Formacion = db.Programa_Formacion.Find(id);
            if (programa_Formacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArea = new SelectList(db.Area_Conocimiento, "IdArea", "NombreArea", programa_Formacion.IdArea);
            return View(programa_Formacion);
        }

        // POST: Programa_Formacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrograma,DenominacionPrograma,VersionPrograma,NivelPrograma,IdArea,EstadoPrograma")] Programa_Formacion programa_Formacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programa_Formacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArea = new SelectList(db.Area_Conocimiento, "IdArea", "NombreArea", programa_Formacion.IdArea);
            return View(programa_Formacion);
        }

        // GET: Programa_Formacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programa_Formacion programa_Formacion = db.Programa_Formacion.Find(id);
            if (programa_Formacion == null)
            {
                return HttpNotFound();
            }
            return View(programa_Formacion);
        }

        // POST: Programa_Formacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Programa_Formacion programa_Formacion = db.Programa_Formacion.Find(id);
            db.Programa_Formacion.Remove(programa_Formacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Ficha_Programa(int id)
        {
            var areas = db.Ficha
         .Where(a => a.IdPrograma == id)
         .ToList();

            ViewBag.ProgramaId = id;
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
