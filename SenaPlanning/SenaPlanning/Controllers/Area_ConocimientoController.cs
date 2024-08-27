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
    public class Area_ConocimientoController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Area_Conocimiento
        public ActionResult Index()
        {
            var area_Conocimiento = db.Area_Conocimiento.Include(a => a.Red_Conocimiento);
            return View(area_Conocimiento.ToList());
        }

        // GET: Area_Conocimiento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area_Conocimiento area_Conocimiento = db.Area_Conocimiento.Find(id);
            if (area_Conocimiento == null)
            {
                return HttpNotFound();
            }
            return View(area_Conocimiento);
        }

        // GET: Area_Conocimiento/Create
        public ActionResult Create()
        {
            ViewBag.IdRed = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed");
            return View();
        }

        // POST: Area_Conocimiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArea,CodigoArea,NombreArea,IdRed,EstadoArea")] Area_Conocimiento area_Conocimiento)
        {
            if (ModelState.IsValid)
            {
                db.Area_Conocimiento.Add(area_Conocimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRed = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed", area_Conocimiento.IdRed);
            return View(area_Conocimiento);
        }

        // GET: Area_Conocimiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area_Conocimiento area_Conocimiento = db.Area_Conocimiento.Find(id);
            if (area_Conocimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRed = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed", area_Conocimiento.IdRed);
            return View(area_Conocimiento);
        }

        // POST: Area_Conocimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArea,CodigoArea,NombreArea,IdRed,EstadoArea")] Area_Conocimiento area_Conocimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(area_Conocimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRed = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed", area_Conocimiento.IdRed);
            return View(area_Conocimiento);
        }

        // GET: Area_Conocimiento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area_Conocimiento area_Conocimiento = db.Area_Conocimiento.Find(id);
            if (area_Conocimiento == null)
            {
                return HttpNotFound();
            }
            return View(area_Conocimiento);
        }

        // POST: Area_Conocimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Area_Conocimiento area_Conocimiento = db.Area_Conocimiento.Find(id);
            db.Area_Conocimiento.Remove(area_Conocimiento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Programa_Area(int id)
        {
            var areas = db.Programa_Formacion
         .Where(a => a.IdArea == id)
         .ToList();

            ViewBag.AreaId = id;
            ViewBag.NombreArea = db.Area_Conocimiento.Find(id)?.NombreArea;
            return View(areas);
        }

        public ActionResult Ficha_Programa_Area(int idArea, int id)
        {
            var areas = db.Ficha
         .Where(a => a.Programa_Formacion.IdArea == idArea && a.IdPrograma == id)
         .ToList();

            ViewBag.AreaId = idArea;
            ViewBag.ProgramaId = id;
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
