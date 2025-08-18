using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClaseModelo;
using SenaPlanning.Helpers; // Agregando referencia al helper de contraseñas

namespace SenaPlanning.Controllers
{
    public class LoginController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string documento, string contrasena)
        {
            var usuario = db.Usuario.FirstOrDefault(u => u.DocumentoUsuario == documento);

            if (usuario != null)
            {
                bool contraseñaValida = false;

                if (usuario.ContraseñaUsuario.StartsWith("$2"))
                {
                    // Contraseña ya está cifrada con BCrypt
                    contraseñaValida = PasswordHelper.VerifyPassword(contrasena, usuario.ContraseñaUsuario);

                    if (contraseñaValida && PasswordHelper.NeedsRehash(usuario.ContraseñaUsuario))
                    {
                        usuario.ContraseñaUsuario = PasswordHelper.HashPassword(contrasena);
                        db.Entry(usuario).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (usuario.ContraseñaUsuario == contrasena)
                    {
                        contraseñaValida = true;
                        // Actualizar a formato cifrado
                        usuario.ContraseñaUsuario = PasswordHelper.HashPassword(contrasena);
                        db.Entry(usuario).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                if (contraseñaValida)
                {
                    Session["Idusuario"] = usuario.IdUsuario;
                    Session["TipoUsuario"] = usuario.TipoUsuario;
                    Session["NombreCompletoUsuario"] = usuario.NombreUsuario + " " + usuario.ApellidoUsuario;
                    Session["NombreUsuario"] = (usuario.NombreUsuario).Split()[0] + " " + usuario.ApellidoUsuario.Split()[0];
                    ViewBag.TipoUsuario = usuario.TipoUsuario;

                    if (usuario.TipoUsuario == "Administrador" && usuario.EstadoUsuario == true)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (usuario.TipoUsuario == "Coordinador" && usuario.EstadoUsuario == true)
                    {
                        return RedirectToAction("Contact", "Home");
                    }
                }
            }

            if (usuario == null)
            {
                ViewData["Mensaje"] = "Correo o contraseña incorrectos";
            }
            else if (usuario.EstadoUsuario == false)
            {
                ViewData["Mensaje"] = "Usuario no Activo";
            }
            else
            {
                ViewData["Mensaje"] = "Correo o contraseña incorrectos";
            }

            ModelState.AddModelError("", "Credenciales inválidas.");
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutorizarTipoUsuario("Administrador", "Coordinador")]
        public ActionResult EditarPerfil([Bind(Include = "Id_usuario,Tipo_Documento_usuario,Documento_usuario,Nombre_usuario,Apellido_usuario,Telefono_usuario,Correo_usuario,Contrasena_usuario,Tipo_usuario,Tipo_instructor,Id_ficha,Estado_usuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(usuario.ContraseñaUsuario))
                {
                    usuario.ContraseñaUsuario = PasswordHelper.HashPassword(usuario.ContraseñaUsuario);
                }

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
                    return RedirectToAction("Index");
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
                var url = urlHelper.Action("Index", "Login");
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
