using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TM.Arquitecture.Models;
using TM.Data.EmpleadoRepository;

namespace TM.Web.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoController(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        // GET: Empleado
        public IActionResult Index()
        {
            var empleados = _empleadoRepository.GetAllEmpleados();
            return View(empleados);
        }

        // GET: Empleado/Details/5
        public IActionResult Details(int id)
        {
            var empleado = _empleadoRepository.GetEmpleadoById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Puesto,Departamento")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _empleadoRepository.AddEmpleado(empleado);
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public IActionResult Edit(int id)
        {
            var empleado = _empleadoRepository.GetEmpleadoById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Puesto,Departamento")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _empleadoRepository.UpdateEmpleado(empleado);
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public IActionResult Delete(int id)
        {
            var empleado = _empleadoRepository.GetEmpleadoById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = _empleadoRepository.DeleteEmpleado(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

