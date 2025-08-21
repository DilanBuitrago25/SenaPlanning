using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClaseModelo;
using SenaPlanning.Helpers;

namespace SenaPlanning.Controllers
{
    public class UsuariosController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // ========================================
        // SECCIÓN: OPERACIONES CRUD BÁSICAS
        // ========================================

        // GET: Usuarios - Lista todos los usuarios
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        // GET: Usuarios/Details/5 - Muestra detalles de un usuario específico
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create - Formulario para crear nuevo usuario
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create - Procesa la creación de nuevo usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,TipoDocumentoUsuario,NombreUsuario,ApellidoUsuario,TipoUsuario,EstadoUsuario,DocumentoUsuario,TelefonoUsuario,ContraseñaUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(usuario.ContraseñaUsuario))
                {
                    usuario.ContraseñaUsuario = PasswordHelper.HashPassword(usuario.ContraseñaUsuario);
                }

                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5 - Formulario para editar usuario (Admin)
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5 - Procesa la edición de usuario (Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,TipoDocumentoUsuario,NombreUsuario,ApellidoUsuario,TipoUsuario,EstadoUsuario,DocumentoUsuario,TelefonoUsuario,ContraseñaUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario usuarioExistente = db.Usuario.Find(usuario.IdUsuario);
                if (usuarioExistente != null)
                {
                    // Actualizar campos editables
                    usuarioExistente.TipoDocumentoUsuario = usuario.TipoDocumentoUsuario;
                    usuarioExistente.NombreUsuario = usuario.NombreUsuario;
                    usuarioExistente.ApellidoUsuario = usuario.ApellidoUsuario;
                    usuarioExistente.TipoUsuario = usuario.TipoUsuario;
                    usuarioExistente.EstadoUsuario = usuario.EstadoUsuario;
                    usuarioExistente.DocumentoUsuario = usuario.DocumentoUsuario;
                    usuarioExistente.TelefonoUsuario = usuario.TelefonoUsuario;

                    // Solo actualizar contraseña si se proporciona una nueva
                    if (!string.IsNullOrEmpty(usuario.ContraseñaUsuario))
                    {
                        usuarioExistente.ContraseñaUsuario = PasswordHelper.HashPassword(usuario.ContraseñaUsuario);
                    }

                    db.Entry(usuarioExistente).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5 - Confirmación para eliminar usuario
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5 - Procesa la eliminación de usuario
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // ========================================
        // SECCIÓN: GESTIÓN DE PERFIL DE USUARIO
        // ========================================

        // GET: Usuarios/Ajustes - Formulario para que el usuario edite su propio perfil
        public ActionResult Ajustes()
        {
            // Verificar si hay una sesión activa
            if (Session["Idusuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Obtener el ID del usuario desde la sesión
            int usuarioId = (int)Session["Idusuario"];

            // Buscar el usuario en la base de datos
            Usuario usuario = db.Usuario.Find(usuarioId);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            // Limpiar la contraseña por seguridad (no mostrar en el formulario)
            usuario.ContraseñaUsuario = "";

            return View(usuario);
        }

        // POST: Usuarios/Ajustes - Procesa la actualización del perfil del usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ajustes(Usuario usuarioActualizado, string contrasenaActual, string nuevaContrasena, string confirmarContrasena)
        {
            // Verificar sesión activa
            if (Session["Idusuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int usuarioId = (int)Session["Idusuario"];

            // Obtener el usuario actual de la base de datos
            Usuario usuarioExistente = db.Usuario.Find(usuarioId);

            if (usuarioExistente == null)
            {
                return HttpNotFound();
            }

            // Validar que solo se actualicen los campos permitidos
            usuarioExistente.NombreUsuario = usuarioActualizado.NombreUsuario;
            usuarioExistente.ApellidoUsuario = usuarioActualizado.ApellidoUsuario;
            usuarioExistente.TelefonoUsuario = usuarioActualizado.TelefonoUsuario;

            // Manejar cambio de contraseña si se proporciona
            if (!string.IsNullOrEmpty(nuevaContrasena) || !string.IsNullOrEmpty(confirmarContrasena) || !string.IsNullOrEmpty(contrasenaActual))
            {
                // Validar que se proporcione la contraseña actual
                if (string.IsNullOrEmpty(contrasenaActual))
                {
                    ModelState.AddModelError("", "Debe ingresar su contraseña actual para cambiar la contraseña.");
                    usuarioActualizado.ContraseñaUsuario = "";
                    return View(usuarioActualizado);
                }

                // Verificar que la contraseña actual sea correcta
                if (!PasswordHelper.VerifyPassword(contrasenaActual, usuarioExistente.ContraseñaUsuario))
                {
                    ModelState.AddModelError("", "La contraseña actual es incorrecta.");
                    usuarioActualizado.ContraseñaUsuario = "";
                    return View(usuarioActualizado);
                }

                // Validar que se proporcionen ambos campos de nueva contraseña
                if (string.IsNullOrEmpty(nuevaContrasena) || string.IsNullOrEmpty(confirmarContrasena))
                {
                    ModelState.AddModelError("", "Debe completar todos los campos de contraseña.");
                    usuarioActualizado.ContraseñaUsuario = "";
                    return View(usuarioActualizado);
                }

                if (nuevaContrasena != confirmarContrasena)
                {
                    ModelState.AddModelError("", "Las contraseñas no coinciden.");
                    usuarioActualizado.ContraseñaUsuario = "";
                    return View(usuarioActualizado);
                }

                if (nuevaContrasena.Length < 6)
                {
                    ModelState.AddModelError("", "La contraseña debe tener al menos 6 caracteres.");
                    usuarioActualizado.ContraseñaUsuario = "";
                    return View(usuarioActualizado);
                }

                // Verificar que la nueva contraseña sea diferente a la actual
                if (PasswordHelper.VerifyPassword(nuevaContrasena, usuarioExistente.ContraseñaUsuario))
                {
                    ModelState.AddModelError("", "La nueva contraseña debe ser diferente a la actual.");
                    usuarioActualizado.ContraseñaUsuario = "";
                    return View(usuarioActualizado);
                }

                usuarioExistente.ContraseñaUsuario = PasswordHelper.HashPassword(nuevaContrasena);
            }

            // Validar modelo
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(usuarioExistente).State = EntityState.Modified;
                    db.SaveChanges();

                    // Actualizar datos de sesión
                    Session["NombreCompletoUsuario"] = usuarioExistente.NombreUsuario + " " + usuarioExistente.ApellidoUsuario;
                    Session["NombreUsuario"] = usuarioExistente.NombreUsuario.Split()[0] + " " + usuarioExistente.ApellidoUsuario.Split()[0];

                    TempData["MensajeExito"] = "Perfil actualizado correctamente.";
                    return RedirectToAction("Ajustes");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar el perfil: " + ex.Message);
                }
            }

            // Si hay errores, limpiar contraseña y mostrar formulario
            usuarioActualizado.ContraseñaUsuario = "";
            return View(usuarioActualizado);
        }

        // ========================================
        // SECCIÓN: LIMPIEZA DE RECURSOS
        // ========================================

        // Liberar recursos de la base de datos
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
