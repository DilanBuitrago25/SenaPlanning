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

            ViewBag.Id = id;
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
        public ActionResult Edit([Bind(Include = "IdMeta,MetaFecha,MetaTecnPresencial,MetaTecnVirtual,MetaTecPresencial,MetaTecVirtual,MetaETPresencial,MetaETVirtual,MetaOtros,EstadoMeta,MetaTGOApPasan,MetaTCOApPasan,MetaETApPasan,MetaOTROApPasan")] Meta meta)
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

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Instructores_ContratarITrim(int metaId)
        {
            const int HORAS_INST_PLANTA = 403;
            const int HORAS_INST_CONTRATO = 440;
            var meta = db.Meta.Find(metaId);
            int anoMeta = int.Parse(meta.MetaFecha); // Asegurarse de que MetaFecha sea un string válido

            // Obtener la fecha de inicio y fin del primer trimestre del año especificado
            DateTime inicioTrimestre = new DateTime(anoMeta, 1, 1);
            DateTime finTrimestre = new DateTime(anoMeta, 3, 31);

            var fichasByArea = (from f in db.Ficha
                                join p in db.Programa_Formacion on f.IdPrograma equals p.IdPrograma
                                join a in db.Area_Conocimiento on p.IdArea equals a.IdArea
                                join r in db.Red_Conocimiento on a.IdRed equals r.IdRed
                                // Filtrar fichas activas y dentro del trimestre especificado en la meta
                                where f.FechaInFicha < DateTime.Now && f.FechaFinFicha > DateTime.Now && // Ficha activa
                                       f.FechaInFicha <= finTrimestre // Dentro del trimestre
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
        public ActionResult Instructores_ContratarIITrim(int metaId)
        {
            const int HORAS_INST_PLANTA = 403;
            const int HORAS_INST_CONTRATO = 440;
            var meta = db.Meta.Find(metaId);
            int anoMeta = int.Parse(meta.MetaFecha); // Asegurarse de que MetaFecha sea un string válido

            // Obtener la fecha de inicio y fin del primer trimestre del año especificado
            DateTime inicioTrimestre = new DateTime(anoMeta, 4, 1);
            DateTime finTrimestre = new DateTime(anoMeta, 6, 30);

            var fichasByArea = (from f in db.Ficha
                                join p in db.Programa_Formacion on f.IdPrograma equals p.IdPrograma
                                join a in db.Area_Conocimiento on p.IdArea equals a.IdArea
                                join r in db.Red_Conocimiento on a.IdRed equals r.IdRed
                                // Filtrar fichas activas y dentro del trimestre especificado en la meta
                                where f.FechaInFicha < DateTime.Now && f.FechaFinFicha > DateTime.Now && // Ficha activa
                                       f.FechaInFicha <= finTrimestre // Dentro del trimestre
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
        public ActionResult Instructores_ContratarIIITrim(int metaId)
        {
            const int HORAS_INST_PLANTA = 403;
            const int HORAS_INST_CONTRATO = 440;
            var meta = db.Meta.Find(metaId);
            int anoMeta = int.Parse(meta.MetaFecha); // Asegurarse de que MetaFecha sea un string válido

            // Obtener la fecha de inicio y fin del primer trimestre del año especificado
            DateTime inicioTrimestre = new DateTime(anoMeta, 7, 1);
            DateTime finTrimestre = new DateTime(anoMeta, 9, 30);

            var fichasByArea = (from f in db.Ficha
                                join p in db.Programa_Formacion on f.IdPrograma equals p.IdPrograma
                                join a in db.Area_Conocimiento on p.IdArea equals a.IdArea
                                join r in db.Red_Conocimiento on a.IdRed equals r.IdRed
                                // Filtrar fichas activas y dentro del trimestre especificado en la meta
                                where f.FechaInFicha < DateTime.Now && f.FechaFinFicha > DateTime.Now && // Ficha activa
                                       f.FechaInFicha <= finTrimestre // Dentro del trimestre
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
        public ActionResult Instructores_ContratarVITrim(int metaId)
        {
            const int HORAS_INST_PLANTA = 403;
            const int HORAS_INST_CONTRATO = 440;
            var meta = db.Meta.Find(metaId);
            int anoMeta = int.Parse(meta.MetaFecha); // Asegurarse de que MetaFecha sea un string válido

            // Obtener la fecha de inicio y fin del primer trimestre del año especificado
            DateTime inicioTrimestre = new DateTime(anoMeta, 10, 1);
            DateTime finTrimestre = new DateTime(anoMeta, 12, 31);

            var fichasByArea = (from f in db.Ficha
                                join p in db.Programa_Formacion on f.IdPrograma equals p.IdPrograma
                                join a in db.Area_Conocimiento on p.IdArea equals a.IdArea
                                join r in db.Red_Conocimiento on a.IdRed equals r.IdRed
                                // Filtrar fichas activas y dentro del trimestre especificado en la meta
                                where f.FechaInFicha < DateTime.Now && f.FechaFinFicha > DateTime.Now && // Ficha activa
                                       f.FechaInFicha <= finTrimestre // Dentro del trimestre
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
