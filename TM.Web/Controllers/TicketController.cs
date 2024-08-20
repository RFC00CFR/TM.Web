using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.Arquitecture.Models;
using TM.Data.EmpleadoRepository;
using TM.Data.TicketRepository;

namespace TM.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEmpleadoRepository _empleadoRepository;

        public TicketController(ITicketRepository ticketRepository, IEmpleadoRepository empleadoRepository)
        {
            _ticketRepository = ticketRepository;
            _empleadoRepository = empleadoRepository;
        }

        // GET: Ticket
        public IActionResult Index()
        {
            //var tickets = _ticketRepository.GetAllTickets();
            var tickets = _ticketRepository.GetAllTicketsWithIncludes();
            return View(tickets);
        }

        // GET: Ticket/Details/5
        public IActionResult Details(int id)
        {
            var ticket = _ticketRepository.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // GET: Ticket/Create
        public IActionResult Create()
        {
            ViewData["AsignadoPor"] = new SelectList(_empleadoRepository.GetAllEmpleados(), "Id", "Nombre");
            ViewData["AsignadoA"] = new SelectList(_empleadoRepository.GetAllEmpleados(), "Id", "Nombre");
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Prioridad,FechaDeAsignado,FechaDeEntrega,Estado,AsignadoPor,AsignadoA")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _ticketRepository.AddTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _ticketRepository.AddTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignadoPor"] = new SelectList(_empleadoRepository.GetAllEmpleados(), "Id", "Nombre", ticket.AsignadoPor);
            ViewData["AsignadoA"] = new SelectList(_empleadoRepository.GetAllEmpleados(), "Id", "Nombre", ticket.AsignadoA);


            return View(ticket);
        }

        // GET: Ticket/Edit/5
        public IActionResult Edit(int id)
        {
            var ticket = _ticketRepository.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["AsignadoPor"] = new SelectList(_empleadoRepository.GetAllEmpleados(), "Id", "Nombre", ticket.AsignadoPor);
            ViewData["AsignadoA"] = new SelectList(_empleadoRepository.GetAllEmpleados(), "Id", "Nombre", ticket.AsignadoA);
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Prioridad,FechaDeAsignado,FechaDeEntrega,Estado,AsignadoPor,AsignadoA")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ticketRepository.UpdateTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_ticketRepository.GetAllTickets().Any(t => t.Id == ticket.Id))
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
            ViewData["AsignadoPor"] = new SelectList(_empleadoRepository.GetAllEmpleados(), "Id", "Nombre", ticket.AsignadoPor);
            ViewData["AsignadoA"] = new SelectList(_empleadoRepository.GetAllEmpleados(), "Id", "Nombre", ticket.AsignadoA);
            return View(ticket);
        }

        // GET: Ticket/Delete/5
        public IActionResult Delete(int id)
        {
            var ticket = _ticketRepository.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _ticketRepository.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
