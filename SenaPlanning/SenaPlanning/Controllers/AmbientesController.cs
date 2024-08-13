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
    public class AmbientesController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Ambientes
        public ActionResult Index()
        {
            return View(db.Ambiente.ToList());
        }

        // GET: Ambientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ambiente ambiente = db.Ambiente.Find(id);
            if (ambiente == null)
            {
                return HttpNotFound();
            }
            return View(ambiente);
        }

        // GET: Ambientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ambientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAmbiente,NombreAmbiente,EstadoAmbiente")] Ambiente ambiente)
        {
            if (ModelState.IsValid)
            {
                db.Ambiente.Add(ambiente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ambiente);
        }

        // GET: Ambientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ambiente ambiente = db.Ambiente.Find(id);
            if (ambiente == null)
            {
                return HttpNotFound();
            }
            return View(ambiente);
        }

        // POST: Ambientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAmbiente,NombreAmbiente,EstadoAmbiente")] Ambiente ambiente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ambiente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ambiente);
        }

        // GET: Ambientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ambiente ambiente = db.Ambiente.Find(id);
            if (ambiente == null)
            {
                return HttpNotFound();
            }
            return View(ambiente);
        }

        // POST: Ambientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ambiente ambiente = db.Ambiente.Find(id);
            db.Ambiente.Remove(ambiente);
            db.SaveChanges();
            return RedirectToAction("Index");
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
