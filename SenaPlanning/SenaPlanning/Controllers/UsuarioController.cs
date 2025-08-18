using System;
using System.Linq;
using System.Web.Mvc;
using ClaseModelo;
using System.ComponentModel.DataAnnotations;
using SenaPlanning.Helpers;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace SenaPlanning.Controllers
{
    public class UsuarioController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        // GET: Usuario/Ajustes
        public ActionResult Ajustes()
        {
            if (Session["IdUsuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
            var usuario = db.Usuario.Find(idUsuario);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var model = new UsuarioEditViewModel
            {
                IdUsuario = usuario.IdUsuario,
                TipoDocumentoUsuario = usuario.TipoDocumentoUsuario,
                DocumentoUsuario = usuario.DocumentoUsuario,
                NombreUsuario = usuario.NombreUsuario,
                ApellidoUsuario = usuario.ApellidoUsuario,
                TelefonoUsuario = usuario.TelefonoUsuario,
                TipoUsuario = usuario.TipoUsuario,
                EstadoUsuario = usuario.EstadoUsuario,
                ContraseñaUsuario = "", // Siempre vacía por seguridad
                ConfirmarContraseña = ""
            };

            return View(model);
        }

        // POST: Usuario/Ajustes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ajustes(UsuarioEditViewModel model)
        {
            if (Session["IdUsuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = db.Usuario.Find(model.IdUsuario);
                    if (usuario == null)
                    {
                        return HttpNotFound();
                    }

                    // Actualizar solo los campos editables
                    usuario.NombreUsuario = model.NombreUsuario;
                    usuario.ApellidoUsuario = model.ApellidoUsuario;
                    usuario.TelefonoUsuario = model.TelefonoUsuario;

                    // Solo actualizar contraseña si se proporcionó una nueva
                    if (!string.IsNullOrEmpty(model.ContraseñaUsuario))
                    {
                        usuario.ContraseñaUsuario = PasswordHelper.HashPassword(model.ContraseñaUsuario);
                    }

                    db.SaveChanges();

                    // Actualizar datos de sesión
                    Session["NombreUsuario"] = usuario.NombreUsuario;
                    Session["NombreCompletoUsuario"] = $"{usuario.NombreUsuario} {usuario.ApellidoUsuario}";

                    TempData["SuccessMessage"] = "Perfil actualizado correctamente.";
                    return RedirectToAction("Ajustes");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar el perfil: " + ex.Message);
                }
            }

            return View(model);
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

    // ViewModel para la edición de usuario
    public class UsuarioEditViewModel
    {
        public int IdUsuario { get; set; }

        [Display(Name = "Tipo de Documento")]
        public string TipoDocumentoUsuario { get; set; }

        [Display(Name = "Número de Documento")]
        public string DocumentoUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        [Display(Name = "Nombre")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder 50 caracteres")]
        [Display(Name = "Apellido")]
        public string ApellidoUsuario { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(15, ErrorMessage = "El teléfono no puede exceder 15 caracteres")]
        [RegularExpression(@"^\d{7,15}$", ErrorMessage = "El teléfono debe contener solo números y tener entre 7 y 15 dígitos")]
        [Display(Name = "Teléfono")]
        public string TelefonoUsuario { get; set; }

        [Display(Name = "Tipo de Usuario")]
        public string TipoUsuario { get; set; }

        public bool EstadoUsuario { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Display(Name = "Nueva Contraseña")]
        public string ContraseñaUsuario { get; set; }

        [Compare("ContraseñaUsuario", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmarContraseña { get; set; }
    }
}
