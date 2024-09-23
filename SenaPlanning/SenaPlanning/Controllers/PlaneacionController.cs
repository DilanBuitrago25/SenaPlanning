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
        public ActionResult Instructores_ContratarITrim()
        {
            ViewBag.Metas = db.Meta.Where(m => m.MetaFecha == DateTime.Now.Year.ToString()).ToList().ToString();

            


            return View();   
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Instructores_ContratarIITrim()
        {
            const int HORAS_INST_PLANTA = 403;
            const int HORAS_INST_CONTRATO = 440;

            var fichasByArea = (from f in db.Ficha
                                join p in db.Programa_Formacion on f.IdPrograma equals p.IdPrograma
                                join a in db.Area_Conocimiento on p.IdArea equals a.IdArea
                                join r in db.Red_Conocimiento on a.IdRed equals r.IdRed
                                group new { f, p, a, r } by new { a.NombreArea } into groupedFichas
                                select new ResumenAreaConocimiento
                                {
                                    AreaConocimiento = groupedFichas.Key.NombreArea,
                                    NumeroFichas = groupedFichas.Count(),
                                    RedConocimiento = groupedFichas.FirstOrDefault().r.NombreRed,
                                    HorasRequeridas = groupedFichas.Count() * 440,
                                    NumeroInstructoresPlanta = groupedFichas.Sum(g => db.Instructor.Count(i => i.AreaInstructor == g.a.NombreArea && i.EstadoInstructor)),
                                    HorasContrato = groupedFichas.Sum(g => db.Instructor.Count(i => i.AreaInstructor == g.a.NombreArea && i.EstadoInstructor)) * HORAS_INST_PLANTA - groupedFichas.Count() * 440,
                                    NumeroInstructoresContrato = (int)(groupedFichas.Sum(g => db.Instructor.Count(i => i.AreaInstructor == g.a.NombreArea && i.EstadoInstructor)) * HORAS_INST_PLANTA - groupedFichas.Count() * 440) / HORAS_INST_CONTRATO
                                })
                                 .ToList();

            return View(fichasByArea);
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Instructores_ContratarIIITrim()
        {
            return View();
        }

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Instructores_ContratarVITrim()
        {
            return View();
        }

        //public ActionResult ResumenPorAño(int año)
        //{
        //    var añoActual = DateTime.Now.Year;
        //    var totalTrimestres = 4;

        //    var trimestres = new List<ResumenTrimestre>();
        //    for (int trimestre = 1; trimestre <= totalTrimestres; trimestre++)
        //    {
        //        var rangoFechas = ObtenerRangoFechasPorTrimestre(trimestre, añoActual);

        //        var resumenTrimestre = db.Ficha
        //            .Where(f => f.Oferta.Meta.MetaFecha >= rangoFechas.Item1 && f.Oferta.Meta.MetaFecha <= rangoFechas.Item2 && !EsTrimestreEP(f.Oferta.Meta.MetaFecha))
        //            .GroupBy(f => new { f.Programa.AreaConocimiento.NombreArea, f.Programa.AreaConocimiento.RedConocimiento })
        //            .Select(g => new ResumenAreaConocimiento
        //            {
        //                RedConocimiento = g.Key.RedConocimiento,
        //                AreaConocimiento = g.Key.NombreArea,
        //                NumeroFichas = g.Count(),
        //                // ... otros cálculos
        //            })
        //            .ToList();

        //        trimestres.Add(new ResumenTrimestre { Trimestre = trimestre, Datos = resumenTrimestre });
        //    }

        //    return View(trimestres);
        //}

        //private bool EsTrimestreEP(DateTime fecha)
        //{
        //    // Lógica para determinar si la fecha corresponde al trimestre EP
        //    // Por ejemplo, si "EP" representa el último trimestre del año:
        //    return fecha.Month >= 10;
        //}

        //// Método para obtener el rango de fechas por trimestre (ya definido anteriormente)
        //public static Tuple<DateTime, DateTime> ObtenerRangoFechasPorTrimestre(int trimestre, int año)
        //{
        //    // ... implementación del método ...
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
