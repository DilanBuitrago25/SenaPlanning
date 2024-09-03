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
    public class InstructorsController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Instructors
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Index()
        {
            return View(db.Instructor.ToList());
        }

        // GET: Instructors/Details/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Create()
        {
            ViewBag.IdRed = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed");
            ViewBag.IdArea = new SelectList(db.Area_Conocimiento, "IdArea", "NombreArea");
            return View();
        }

        // POST: Instructors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdInstructor,DocumentoInstructor,NombreCompletoInstructor,CodRegionalInstructor,RegionalInstructor,CodCCOS,DependenciaInstructor,CargoInstructor,TipoCargoInstructor,CorreoSENAInstructor,RedInstructor,AreaInstructor,RutaInstructor,CodMunicipioInstructor,MunicipioInstructor,FechaNacimientoInstructor,FechaIngreso,SexoInstructor,EstadoInstructor")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Instructor.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRed = new SelectList(db.Red_Conocimiento, "IdRed", "NombreRed", instructor.RedInstructor);
            ViewBag.IdArea = new SelectList(db.Red_Conocimiento, "IdArea", "NombreArea", instructor.AreaInstructor);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdInstructor,DocumentoInstructor,NombreCompletoInstructor,CodRegionalInstructor,RegionalInstructor,CodCCOS,DependenciaInstructor,CargoInstructor,TipoCargoInstructor,CorreoSENAInstructor,RedInstructor,AreaInstructor,RutaInstructor,CodMunicipioInstructor,MunicipioInstructor,FechaNacimientoInstructor,FechaIngreso,SexoInstructor,EstadoInstructor")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructor.Find(id);
            db.Instructor.Remove(instructor);
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
        [HttpPost]
        public ActionResult instructorRegister([Bind(Include = "IdInstructor,DocumentoInstructor,NombreCompletoInstructor,CodRegionalInstructor,RegionalInstructor,CodCCOS,DependenciaInstructor,CargoInstructor,TipoCargoInstructor,CorreoSENAInstructor,RedInstructor,AreaInstructor,RutaInstructor,CodMunicipioInstructor,MunicipioInstructor,FechaNacimientoInstructor,FechaIngreso,SexoInstructor,EstadoInstructor")] Instructor instructor)
        {

            var consultaInstructor = db.Instructor.Where(p => p.DocumentoInstructor == instructor.DocumentoInstructor).ToList();

            if (consultaInstructor.Count() == 0)
            {
                var codigoArea = db.Instructor.Where(r => r.RedInstructor == instructor.RedInstructor).ToList();

                db.Instructor.Add(instructor);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Json(new { code = 200 });
                }


            }
            return Json(new { code = 500 });
        }
    }
}
