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
    public class MetasController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Metas
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Index()
        {
            return View(db.Meta.ToList());
        }

        // GET: Metas/Details/5
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

        // GET: Metas/Create
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Metas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMeta,MetaFecha,MetaTecnPresencial,MetaTecnVirtual,MetaTecPresencial,MetaTecVirtual,MetaETPresencial,MetaETVirtual,MetaOtros,EstadoMeta,MetaTGOApPasan,MetaTCOApPasan,MetaETApPasan,MetaOTROApPasan")] Meta meta)
        {
            if (ModelState.IsValid)
            {
                db.Meta.Add(meta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meta);
        }

        // GET: Metas/Edit/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Edit(int? id)
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

        // POST: Metas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMeta,MetaFormacion,MetaTecnologia,MetaTecnico,MetaET,MetaOtros,EstadoMeta,MetaTGOApPasan,MetaTCOApPasan,MetaETApPasan,MetaOTROApPasan")] Meta meta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meta);
        }

        // GET: Metas/Delete/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Delete(int? id)
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

        // POST: Metas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meta meta = db.Meta.Find(id);
            db.Meta.Remove(meta);
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
