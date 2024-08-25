using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TM.Data.EmpleadoRepository;
using TM.Arquitecture.Models;
using TM.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

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
                // Crear claims y principal
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, empleado.Nombre),
            new Claim(ClaimTypes.NameIdentifier, empleado.Id.ToString())
            
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Almacenar el ID del empleado en la sesión
                HttpContext.Session.SetInt32("EmpleadoId", empleado.Id);

                return RedirectToAction("TicketsList", "Ticket");
            }
            else
            {
                ViewData["Error"] = "Credenciales incorrectas.";
                return View();
            }
        }


        // Para el logout
        public async Task<IActionResult> Logout()
        {
            // Cierra la sesión de autenticación
            await HttpContext.SignOutAsync();

            // Elimina la información del usuario de la sesión
            HttpContext.Session.Remove("EmpleadoId");

            // Redirige a la página de login o a otra página específica
            return RedirectToAction("Index","Home");
        }

    }
}

