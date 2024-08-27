using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
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
            ViewBag.Red = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed");
            ViewBag.Area = new SelectList(db.Area_Conocimiento, "IdArea", "NombreArea");
            ViewBag.Oferta = new SelectList(db.Oferta, "IdOferta", "EstadoOferta");
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


        public ActionResult ObtenerAreasPorRedes(int idRed)
        {
            var areas = db.Red_Conocimiento
                .Where(R => R.IdRed == idRed)
                .Select(R => new
                {
                    Areas = R.Area_Conocimiento.Select(a => new { Value = a.IdArea, Text = a.NombreArea })
                })
                .FirstOrDefault()?
                .Areas;

            return Json(areas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerProgramasPorAreas(int idArea)
        {
            var programas = db.Area_Conocimiento
                .Where(A => A.IdArea == idArea)
                .Select(A => new
                {
                    Programas = A.Programa_Formacion.Select(p => new { Value = p.IdPrograma, Text = p.DenominacionPrograma })
                })
                .FirstOrDefault()?
                .Programas;

            return Json(programas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerNivelPrograma(int idPrograma)
        {
            var nivelPrograma = db.Programa_Formacion
                .Where(p => p.IdPrograma == idPrograma)
                .Select(p => p.NivelPrograma)
                .FirstOrDefault();

            return Json(nivelPrograma, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerCodigoPrograma(int idPrograma)
        {
            var codigoPrograma = db.Programa_Formacion
                .Where(p => p.IdPrograma == idPrograma)
                .Select(p => p.CodigoPrograma)
                .FirstOrDefault();

            return Json(codigoPrograma, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerHorasPrograma(int idPrograma)
        {
            var horasPrograma = db.Programa_Formacion
                .Where(p => p.IdPrograma == idPrograma)
                .Select(p => p.HorasPrograma
                .FirstOrDefault());

            return Json(horasPrograma, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult ObtenerCompetenciasPorFicha(int idFicha)
        //{
        //    var competencias = db.Ficha
        //        .Where(f => f.Id_ficha == idFicha)
        //        .Select(f => new
        //        {
        //            Competencias = f.Programa_formacion.Competencia.Select(c => new { Value = c.Id_competencia, Text = c.Nombre_competencia })
        //        })
        //        .FirstOrDefault()?
        //        .Competencias;

        //    return Json(competencias, JsonRequestBehavior.AllowGet);
        //}

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
