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
    public class PlaneacionController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Planeacion
        public ActionResult Index()
        {
            var oferta = db.Oferta.Include(o => o.Meta).Include(o => o.Usuario);
            return View(oferta.ToList());
        }

        // GET: Planeacion/Details/5
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
        public ActionResult Create()
        {
            ViewBag.IdMetas = new SelectList(db.Meta, "IdMeta", "IdMeta");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "TipoDocumentoUsuario");
            return View();
        }

        // POST: Planeacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOferta,CodigoOferta,HoraReqTrimIOferta,HoraReqTrimIIOferta,HoraReqTrimIIIOferta,HoraReqTrimIVOferta,CanInstPlantaOferta,HorasContTrimIOferta,HorasContTrimIIOferta,HorasContTrimIIIOferta,HorasContTrimIVOferta,CantidadInstContratoTrimIOferta,CantidadInstContratoTrimIIOferta,CantidadInstContratoTrimIIIOferta,CantidadInstContratoTrimIVOferta,TrimestreProgramadosOferta,TotalAprendicesOferta,TotalCursosNuevosOferta,TotalCursosEPtrimestreOferta,TotalCursosCursosNuevosOferta,TotalCursosOferta,CantidadTrimProgramadosOferta,CantidadTrimEPOferta,TotalInstaContratarOferta,AprenPasanOferta,AprenProgOferta,CursoOferta,IdUsuario,IdMetas,EstadoOferta")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                db.Oferta.Add(oferta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMetas = new SelectList(db.Meta, "\r\n<br />\r\n\r\n<div class=\"page-header\">\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-6\">\r\n            <h3>Registrar Asistencia</h3>\r\n            <ol class=\"breadcrumb\">\r\n                <li class=\"breadcrumb-item\"><a href=\"@Url.Action(\"Index\",\"Instructor\")\">Home</a></li>\r\n                <li class=\"breadcrumb-item\">Asistencia                                     </li>\r\n                <li class=\"breadcrumb-item active\">Registrar</li>\r\n            </ol>\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n          \r\n        </div>\r\n    </div>\r\n</div>\r\n          </div>\r\n<!-- Container-fluid starts-->\r\n@using (Html.BeginForm())\r\n{\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row\">\r\n            <div class=\"col-sm-12\">\r\n                <div class=\"card\">\r\n                    @Html.AntiForgeryToken()\r\n                    <div class=\"card-body\">\r\n                        <div class=\"form theme-form\">\r\n                            <div class=\"row\">\r\n                                <div class=\"col\">\r\n                                    <div class=\"mb-3\">\r\n                                        <label>Ficha</label>\r\n                                        <div class=\"input-group\">\r\n                                            @Html.DropDownList(\"Id_ficha\", null, \"Selecciona una Ficha\", new { @class = \"js-example-basic-single col-sm-12 form-control\", onchange = \"cargarCompetencias()\" })\r\n\r\n                                        </div>\r\n                                        @Html.ValidationMessageFor(model => model.Id_ficha, \"\", new { @class = \"text-danger\" })\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col\">\r\n                                    <div class=\"mb-3\">\r\n                                        <label>Competencia</label>\r\n                                        <div class=\"input-group\">\r\n                                            @Html.DropDownList(\"Id_competencia\",null,\"Selecciona una Competencia\" ,htmlAttributes: new { @class = \"js-example-basic-single col-sm-12 form-control\" })\r\n                                        </div>\r\n                                        @Html.ValidationMessageFor(model => model.Id_competencia, \"\", new { @class = \"text-danger\" })\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-sm-4\">\r\n                                    <div class=\"mb-3\">\r\n                                        <label>Fecha</label>\r\n                                        <div class=\"input-group\">\r\n                                            @Html.EditorFor(model => model.Fecha_asistencia, new { htmlAttributes = new { @class = \"form-control digits\", @type = \"text\", languaje = \"es\", @id = \"minMaxExample\", autocomplete = \"off\" } })\r\n\r\n                                        </div>\r\n                                        @Html.ValidationMessageFor(model => model.Fecha_asistencia, \"\", new { @class = \"text-danger\" })\r\n                                    </div>\r\n                                </div>\r\n                                <div class=\"col-sm-4\">\r\n                                    <div class=\"mb-3\">\r\n                                        <label>Hora Inicio</label>\r\n                                        <div class=\"input-group\">\r\n                                            @Html.EditorFor(model => model.Hora_inicio_asistencia, new { htmlAttributes = new { @class = \"form-control digits\", @type = \"time\" } })\r\n\r\n                                        </div>\r\n                                        @Html.ValidationMessageFor(model => model.Hora_inicio_asistencia, \"\", new { @class = \"text-danger\" })\r\n                                    </div>\r\n                                </div>\r\n                                <div class=\"col-sm-4\">\r\n                                    <div class=\"mb-3\">\r\n                                        <label>Hora Fin</label>\r\n                                        <div class=\"input-group\">\r\n                                            @Html.EditorFor(model => model.Hora_fin_asistencia, new { htmlAttributes = new { @class = \"form-control digits\", @type = \"time\" } })\r\n\r\n                                        </div>\r\n                                        @Html.ValidationMessageFor(model => model.Hora_fin_asistencia, \"\", new { @class = \"text-danger\" })\r\n\r\n                                    </div>\r\n                                </div>\r\n                                @Html.HiddenFor(model => model.Id_Instructor, new { Value = @Session[\"Idusuario\"].ToString() })\r\n                            </div>\r\n\r\n                            <div class=\"row\">\r\n                                <div class=\"col\">\r\n                                    <div class=\"mb-3\">\r\n                                        <label for=\"exampleFormControlTextarea4\">Ingresar algunos Detalles</label>\r\n                                        <div class=\"input-group\">\r\n                                            @Html.TextAreaFor(model => model.Detalles_asistencia, new { @class = \"form-control\", @id = \"exampleFormControlTextarea4\", @rows = \"3\" })\r\n                                        </div>\r\n                                        @Html.ValidationMessageFor(model => model.Detalles_asistencia, \"\", new { @class = \"text-danger\" })\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col\">\r\n                                    <div class=\"text-end\">\r\n                                        <button class=\"btn btn-success me-3\" type=\"submit\">Generar Qr</button>\r\n                                        <button class=\"btn btn-danger\" type=\"button\">\r\n                                            <a href=\"@Url.Action(\"Index\", \"Asistencias\")\">Cancelar</a>\r\n                                        </button>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n\r\n                            @Html.HiddenFor(model => model.Estado_Asistencia, new { Value = \"True\" })\r\n\r\n\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n}\r\n", "IdMeta", oferta.IdMetas);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "TipoDocumentoUsuario", oferta.IdUsuario);
            return View(oferta);
        }

        // GET: Planeacion/Edit/5
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
