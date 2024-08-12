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
    public class LoginController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int documento, string contrasena)
        {
            var usuario = db.Usuario.FirstOrDefault(u => u.DocumentoUsuario == documento && u.ConstraseñaUsuario == contrasena);

            if (usuario != null)
            {
                Session["Idusuario"] = usuario.IdUsuario; // Almacenar el ID en sesión
                Session["TipoUsuario"] = usuario.TipoUsuario;
                Session["NombreUsuario"] = usuario.NombreUsuario + " " + usuario.ApellidoUsuario;
                ViewBag.TipoUsuario = usuario.TipoUsuario;

                if (usuario.TipoUsuario == "Administrador")
                {
                    return RedirectToAction("Index", "Home"); // Vista para Administradores
                }
                else if (usuario.TipoUsuario == "Coordinador")
                {
                    return RedirectToAction("Contact", "Home"); // Vista para Coordinadores
                }
            }
            if (usuario == null)
            {
                ViewData["Mensaje"] = "Correo o contraseña incorrectos";
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no Activo";
            }

            ModelState.AddModelError("", "Credenciales inválidas.");
            return View();

        }

        public ActionResult Logout()
        {
            Session.Abandon(); // Cerrar sesión
            return RedirectToAction("Index");
        }

        public ActionResult EditarPerfil()
        {
            if (Session["Idusuario"] != null)
            {
                int usuarioId = (int)Session["Idusuario"];
                var usuario = db.Usuario.Find(usuarioId);

                if (usuario != null)
                {
                    return View(usuario);
                }
            }

            return RedirectToAction("Index"); // O a donde quieras redirigir si no hay sesión o usuario
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutorizarTipoUsuario("Administrador", "Coordinador")]
        public ActionResult EditarPerfil([Bind(Include = "Id_usuario,Tipo_Documento_usuario,Documento_usuario,Nombre_usuario,Apellido_usuario,Telefono_usuario,Correo_usuario,Contrasena_usuario,Tipo_usuario,Tipo_instructor,Id_ficha,Estado_usuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();

                if (usuario.TipoUsuario == "Administrador")
                {
                    return RedirectToAction("Index", "Administrador");
                }
                else if (usuario.TipoUsuario == "Coordinador")
                {
                    return RedirectToAction("Index", "Coordinador");
                }
                else
                {
                    // Manejar otros tipos de usuario si es necesario
                    return RedirectToAction("Index"); // Redirigir al índice actual por defecto
                }
            }

            return View(usuario);
        }


        public class AutorizarTipoUsuarioAttribute : AuthorizeAttribute
        {
            private readonly string[] _tiposPermitidos;
            private readonly string _vistaNoAutorizado = "~/Views/Home/Error401.cshtml";

            public AutorizarTipoUsuarioAttribute(params string[] tiposPermitidos)
            {
                _tiposPermitidos = tiposPermitidos;
            }

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                var tipoUsuario = httpContext.Session["TipoUsuario"] as string;
                return tipoUsuario != null && _tiposPermitidos.Contains(tipoUsuario);
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                var url = urlHelper.Action("Error401", "Home");
                filterContext.Result = new RedirectResult(url);
            }
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
