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
    public class PlaneacionController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Planeacion
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Index()
        {
            var ficha = db.Ficha.Where(m => m.Oferta.Meta.MetaFecha == DateTime.Now.Year.ToString() || m.Oferta.Meta.MetaFecha == (DateTime.Now.Year - 1).ToString() || m.Oferta.Meta.MetaFecha == (DateTime.Now.Year - 2).ToString());
            return View(ficha.ToList());
        }

        // GET: Planeacion/Details/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oferta oferta = db.Oferta.Find(id);
            if (oferta == null)
            {
                return HttpNotFound();
            }
            return View(oferta);
        }

        // GET: Planeacion/Create
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Create()
        {
            ViewBag.IdRed = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed");
            ViewBag.IdMetas = new SelectList(db.Meta, "IdMeta", "MetaFecha");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: Planeacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOferta,EstadoOferta,NombreOferta,FechaInicioOferta,MetaOferta,IdUsuario,IdMetas,IdRed")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                db.Oferta.Add(oferta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IdRed = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed", oferta.IdRed);
            ViewBag.IdMetas = new SelectList(db.Meta, "IdMeta", "MetaFecha", oferta.IdMetas);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Usuario", "NombreUsuario", oferta.IdUsuario);
            return View(oferta);
        }

        // GET: Planeacion/Edit/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oferta oferta = db.Oferta.Find(id);
            if (oferta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMetas = new SelectList(db.Meta, "IdMeta", "IdMeta", oferta.IdMetas);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "TipoDocumentoUsuario", oferta.IdUsuario);
            return View(oferta);
        }

        // POST: Planeacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOferta,CodigoOferta,HoraReqTrimIOferta,HoraReqTrimIIOferta,HoraReqTrimIIIOferta,HoraReqTrimIVOferta,CanInstPlantaOferta,HorasContTrimIOferta,HorasContTrimIIOferta,HorasContTrimIIIOferta,HorasContTrimIVOferta,CantidadInstContratoTrimIOferta,CantidadInstContratoTrimIIOferta,CantidadInstContratoTrimIIIOferta,CantidadInstContratoTrimIVOferta,TrimestreProgramadosOferta,TotalAprendicesOferta,TotalCursosNuevosOferta,TotalCursosEPtrimestreOferta,TotalCursosCursosNuevosOferta,TotalCursosOferta,CantidadTrimProgramadosOferta,CantidadTrimEPOferta,TotalInstaContratarOferta,AprenPasanOferta,AprenProgOferta,CursoOferta,IdUsuario,IdMetas,EstadoOferta")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oferta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMetas = new SelectList(db.Meta, "IdMeta", "IdMeta", oferta.IdMetas);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "TipoDocumentoUsuario", oferta.IdUsuario);
            return View(oferta);
        }

        // GET: Planeacion/Delete/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oferta oferta = db.Oferta.Find(id);
            if (oferta == null)
            {
                return HttpNotFound();
            }
            return View(oferta);
        }

        // POST: Planeacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oferta oferta = db.Oferta.Find(id);
            db.Oferta.Remove(oferta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Instructores_Contratar(int año)
        {
            ViewBag.AñoActual = año;
            return View(año);   
        }

        //public static Tuple<DateTime, DateTime> ObtenerRangoFechasPorTrimestre(int trimestre, int año)
        //{
        //    DateTime inicioTrimestre;
        //    DateTime finTrimestre;

        //    switch (trimestre)
        //    {
        //        case 1:
        //            inicioTrimestre = new DateTime(año, 1, 1);
        //            finTrimestre = new DateTime(año, 3, 31);
        //            break;
        //        case 2:
        //            inicioTrimestre = new DateTime(año, 4, 1);
        //            finTrimestre = new DateTime(año, 6, 30);
        //            break;
        //        case 3:
        //            inicioTrimestre = new DateTime(año, 7, 1);
        //            finTrimestre = new DateTime(año, 9, 30);
        //            break;
        //        case 4:
        //            inicioTrimestre = new DateTime(año, 10, 1);
        //            finTrimestre = new DateTime(año, 12, 31);
        //            break;
                
        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(trimestre), "El trimestre debe estar entre 1 y 4");
        //    }

        //    return new Tuple<DateTime, DateTime>(inicioTrimestre, finTrimestre);
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
