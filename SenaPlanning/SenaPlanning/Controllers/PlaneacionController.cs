using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
            return View(db.Oferta.ToList());
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

            // Cargar los datos necesarios para la vista en caso de error
            ViewBag.IdMetas = new SelectList(db.Meta, "IdMeta", "MetaFecha", oferta.IdMetas);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Usuario", "NombreUsuario", oferta.IdUsuario);
            return View(oferta);
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Planeacion_Ficha(int id)
        {
            var ofertas = db.Ficha
         .Where(a => a.IdOferta == id)
         .ToList();
            ViewBag.OfertaId = id;
            ViewBag.NombreOferta = db.Oferta.Find(id)?.NombreOferta;
            return View(ofertas);
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
            ViewBag.Metas = new SelectList(db.Meta, "IdMeta", "MetaFecha", oferta.IdMetas);
            ViewBag.Usuario = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario", oferta.IdUsuario);
            return View(oferta);
        }

        // POST: Planeacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Edit([Bind(Include = "IdOferta,EstadoOferta,NombreOferta,FechaInicioOferta,MetaOferta,IdUsuario,IdMetas,IdRed")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oferta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Metas = new SelectList(db.Meta, "IdMeta", "FechaInicioOferta", oferta.IdMetas);
            ViewBag.Usuario = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario", oferta.IdUsuario);
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
        public ActionResult Instructores_ContratarITrim()
        {
            const int HORAS_INST_PLANTA = 403;
            const int HORAS_INST_CONTRATO = 440;
            DateTime inicioTrimestre = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime finTrimestre = new DateTime(DateTime.Now.Year, 3, 31);

            var fichasByArea = (from f in db.Ficha
                                join p in db.Programa_Formacion on f.IdPrograma equals p.IdPrograma
                                join a in db.Area_Conocimiento on p.IdArea equals a.IdArea
                                join r in db.Red_Conocimiento on a.IdRed equals r.IdRed
                                // Filtrar fichas activas y dentro del primer trimestre
                                where f.FechaInFicha < inicioTrimestre && f.FechaFinFicha > finTrimestre  // Dentro del trimestre
                                group new { f, p, a, r } by new { a.NombreArea } into groupedFichas
                                let instructores = db.Instructor.Where(i => i.AreaInstructor == groupedFichas.Key.NombreArea).Select(i => i.IdInstructor).Distinct()
                                select new ResumenAreaConocimiento
                                {
                                    AreaConocimiento = groupedFichas.Key.NombreArea,
                                    NumeroFichas = groupedFichas.Count(),
                                    RedConocimiento = groupedFichas.FirstOrDefault().r.NombreRed,
                                    HorasRequeridas = groupedFichas.Count() * 440,
                                    NumeroInstructoresPlanta = instructores.Count(),
                                    HorasContrato = (groupedFichas.Count() * 440) - (instructores.Count() * HORAS_INST_PLANTA),
                                    NumeroInstructoresContrato = (int)((groupedFichas.Count() * 440) - (instructores.Count() * HORAS_INST_PLANTA)) / HORAS_INST_CONTRATO
                                })
                                .ToList();

            return View(fichasByArea);
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Instructores_ContratarIITrim()
        {
            const int HORAS_INST_PLANTA = 403;
            const int HORAS_INST_CONTRATO = 440;
            DateTime inicioTrimestre = new DateTime(DateTime.Now.Year, 4, 1);
            DateTime finTrimestre = new DateTime(DateTime.Now.Year, 6, 30);


            var fichasByArea = (from f in db.Ficha
                                join p in db.Programa_Formacion on f.IdPrograma equals p.IdPrograma
                                join a in db.Area_Conocimiento on p.IdArea equals a.IdArea
                                join r in db.Red_Conocimiento on a.IdRed equals r.IdRed
                                // Filtrar fichas activas y dentro del primer trimestre
                                where f.FechaInFicha < inicioTrimestre && f.FechaFinFicha > finTrimestre   // Dentro del trimestre
                                group new { f, p, a, r } by new { a.NombreArea } into groupedFichas
                                let instructores = db.Instructor.Where(i => i.AreaInstructor == groupedFichas.Key.NombreArea).Select(i => i.IdInstructor).Distinct()
                                select new ResumenAreaConocimiento
                                {
                                    AreaConocimiento = groupedFichas.Key.NombreArea,
                                    NumeroFichas = groupedFichas.Count(),
                                    RedConocimiento = groupedFichas.FirstOrDefault().r.NombreRed,
                                    HorasRequeridas = groupedFichas.Count() * 440,
                                    NumeroInstructoresPlanta = instructores.Count(),
                                    HorasContrato = (groupedFichas.Count() * 440) - (instructores.Count() * HORAS_INST_PLANTA),
                                    NumeroInstructoresContrato = (int)((groupedFichas.Count() * 440) - (instructores.Count() * HORAS_INST_PLANTA)) / HORAS_INST_CONTRATO
                                })
                                .ToList();

            return View(fichasByArea);
        }


        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Instructores_ContratarIIITrim()
        {
            const int HORAS_INST_PLANTA = 403;
            const int HORAS_INST_CONTRATO = 440;
            DateTime inicioTrimestre = new DateTime(DateTime.Now.Year, 7, 1);
            DateTime finTrimestre = new DateTime(DateTime.Now.Year, 9, 30);


            var fichasByArea = (from f in db.Ficha
                                join p in db.Programa_Formacion on f.IdPrograma equals p.IdPrograma
                                join a in db.Area_Conocimiento on p.IdArea equals a.IdArea
                                join r in db.Red_Conocimiento on a.IdRed equals r.IdRed
                                // Filtrar fichas activas y dentro del primer trimestre
                                where f.FechaInFicha < inicioTrimestre && f.FechaFinFicha > finTrimestre // Dentro del trimestre
                                group new { f, p, a, r } by new { a.NombreArea } into groupedFichas
                                let instructores = db.Instructor.Where(i => i.AreaInstructor == groupedFichas.Key.NombreArea).Select(i => i.IdInstructor).Distinct()
                                select new ResumenAreaConocimiento
                                {
                                    AreaConocimiento = groupedFichas.Key.NombreArea,
                                    NumeroFichas = groupedFichas.Count(),
                                    RedConocimiento = groupedFichas.FirstOrDefault().r.NombreRed,
                                    HorasRequeridas = groupedFichas.Count() * 440,
                                    NumeroInstructoresPlanta = instructores.Count(),
                                    HorasContrato = (groupedFichas.Count() * 440) - (instructores.Count() * HORAS_INST_PLANTA),
                                    NumeroInstructoresContrato = (int)((groupedFichas.Count() * 440) - (instructores.Count() * HORAS_INST_PLANTA)) / HORAS_INST_CONTRATO
                                })
                                .ToList(); 

            return View(fichasByArea); 
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Instructores_ContratarVITrim()
        {
            const int HORAS_INST_PLANTA = 403;
            const int HORAS_INST_CONTRATO = 440;
            DateTime inicioTrimestre = new DateTime(DateTime.Now.Year, 10, 1);
            DateTime finTrimestre = new DateTime(DateTime.Now.Year, 12, 31);

            var fichasByArea = (from f in db.Ficha
                                join p in db.Programa_Formacion on f.IdPrograma equals p.IdPrograma
                                join a in db.Area_Conocimiento on p.IdArea equals a.IdArea
                                join r in db.Red_Conocimiento on a.IdRed equals r.IdRed
                                // Filtrar fichas activas y dentro del primer trimestre
                                where f.FechaFinFicha > finTrimestre  // Dentro del trimestre
                                group new { f, p, a, r } by new { a.NombreArea } into groupedFichas
                                let instructores = db.Instructor.Where(i => i.AreaInstructor == groupedFichas.Key.NombreArea).Select(i => i.IdInstructor).Distinct()
                                select new ResumenAreaConocimiento
                                {
                                    AreaConocimiento = groupedFichas.Key.NombreArea,
                                    NumeroFichas = groupedFichas.Count(),
                                    RedConocimiento = groupedFichas.FirstOrDefault().r.NombreRed,
                                    HorasRequeridas = groupedFichas.Count() * 440,
                                    NumeroInstructoresPlanta = instructores.Count(),
                                    HorasContrato = (groupedFichas.Count() * 440) - (instructores.Count() * HORAS_INST_PLANTA),
                                    NumeroInstructoresContrato = (int)((groupedFichas.Count() * 440) - (instructores.Count() * HORAS_INST_PLANTA)) / HORAS_INST_CONTRATO
                                })
                                .ToList();

            return View(fichasByArea);
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
