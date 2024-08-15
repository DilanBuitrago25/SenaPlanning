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
    public class Red_ConocimientoController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Red_Conocimiento
        public ActionResult Index()
        {
            return View(db.Red_Conocimiento.ToList());
        }

        // GET: Red_Conocimiento/Details/5
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

        public ActionResult Area_Red(int id)
        {
            var areas = db.Area_Conocimiento
         .Where(a => a.IdRed == id) // Filtrar por IdRed
         .ToList();

            ViewBag.RedId = id;
            ViewBag.NombreRed = db.Red_Conocimiento.Find(id)?.NombreRed;
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
