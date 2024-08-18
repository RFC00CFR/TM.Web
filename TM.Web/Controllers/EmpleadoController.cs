using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TM.Arquitecture.Models;
using TM.Data.Repository;

namespace TM.Web.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoController(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        public IActionResult Index()
        {
            var empleados = _empleadoRepository.GetAll();
            return View(empleados);
        }

        public IActionResult Details(int id)
        {
            var empleado = _empleadoRepository.GetById(id);
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
        public IActionResult Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _empleadoRepository.Add(empleado);
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public IActionResult Edit(int id)
        {
            var empleado = _empleadoRepository.GetById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _empleadoRepository.Update(empleado);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_empleadoRepository.GetById(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public IActionResult Delete(int id)
        {
            var empleado = _empleadoRepository.GetById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _empleadoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}