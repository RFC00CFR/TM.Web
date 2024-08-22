using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TM.Data.EmpleadoRepository;
using TM.Arquitecture.Models;
using TM.Web.Models;

namespace TM.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public AccountController(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(AccountModel model)
        {
            if (string.IsNullOrEmpty(model.Nombre) || string.IsNullOrEmpty(model.Apellido))
            {
                ViewData["Error"] = "Nombre y apellido son obligatorios.";
                return View();
            }

            var empleado = _empleadoRepository.FindEmpleados(e => e.Nombre == model.Nombre && e.Apellido == model.Apellido).FirstOrDefault();

            if (empleado != null)
            {
                // Almacenar aquí la información del usuario en la sesión o en una cookie
                // Por simplicidad, se redirige a la vista de tickets
                // Guarda el ID del empleado en la sesión para futuras referencias
                HttpContext.Session.SetInt32("EmpleadoId", empleado.Id);
                return RedirectToAction("Index", "Ticket");
            }
            else
            {
                ViewData["Error"] = "Credenciales incorrectas.";
                return View();
            }
        }

        // Para el logout
        public IActionResult Logout()
        {
            // Elimina la información del usuario de la sesión
            HttpContext.Session.Remove("EmpleadoId");
            return RedirectToAction("Login");
        }
    }
}
