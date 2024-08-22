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
    public class FichasController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Fichas
        public ActionResult Index()
        {
            var ficha = db.Ficha.Include(f => f.Oferta).Include(f => f.Programa_Formacion);
            return View(ficha.ToList());
        }

        // GET: Fichas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ficha ficha = db.Ficha.Find(id);
            if (ficha == null)
            {
                return HttpNotFound();
            }
            return View(ficha);
        }

        // GET: Fichas/Create
        public ActionResult Create()
        {
            ViewBag.IdOferta = new SelectList(db.Oferta, "IdOferta", "IdOferta");
            ViewBag.IdPrograma = new SelectList(db.Programa_Formacion, "IdPrograma", "DenominacionPrograma");
            ViewBag.Programa = new SelectList(db.Programa_Formacion, "IdPrograma", "DenominacionPrograma");
            ViewBag.Area = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed");
            ViewBag.Red = new SelectList(db.Area_Conocimiento, "IdArea", "NombreArea");
            return View();
        }

        // POST: Fichas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFicha,CodigoFicha,FechaInFicha,TrimestreFicha,JornadaFicha,IdPrograma,EstadoFicha")] Ficha ficha)
        {
            if (ModelState.IsValid)
            {
                db.Ficha.Add(ficha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdOferta = new SelectList(db.Oferta, "IdOferta", "IdOferta", ficha.IdOferta);
            ViewBag.IdPrograma = new SelectList(db.Programa_Formacion, "IdPrograma", "DenominacionPrograma", ficha.IdPrograma);
            return View(ficha);
        }

      

        // GET: Fichas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ficha ficha = db.Ficha.Find(id);
            if (ficha == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOferta = new SelectList(db.Oferta, "IdOferta", "IdOferta", ficha.IdOferta);
            ViewBag.IdPrograma = new SelectList(db.Programa_Formacion, "IdPrograma", "DenominacionPrograma", ficha.IdPrograma);
            return View(ficha);
        }

        // POST: Fichas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFicha,CodigoFicha,FechaInFicha,FechaFinFicha,NumAprenFicha,TrimestreFicha,JornadaFicha,IdPrograma,IdOferta,EstadoFicha")] Ficha ficha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ficha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdOferta = new SelectList(db.Oferta, "IdOferta", "IdOferta", ficha.IdOferta);
            ViewBag.IdPrograma = new SelectList(db.Programa_Formacion, "IdPrograma", "DenominacionPrograma", ficha.IdPrograma);
            return View(ficha);
        }

        // GET: Fichas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ficha ficha = db.Ficha.Find(id);
            if (ficha == null)
            {
                return HttpNotFound();
            }
            return View(ficha);
        }

        // POST: Fichas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ficha ficha = db.Ficha.Find(id);
            db.Ficha.Remove(ficha);
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
