using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TM.Arquitecture.Models;

namespace TM.Data.TicketRepository
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        bool AddTicket(Ticket ticket);
        bool DeleteTicket(int id);
        IEnumerable<Ticket> GetAllTickets();
        Ticket GetTicketById(int id);
        Ticket UpdateTicket(Ticket ticket);
        IEnumerable<Ticket> FindTickets(Expression<Func<Ticket, bool>> predicate);
        IEnumerable<Ticket> GetByEstado(string estado);
        IEnumerable<Ticket> GetAllTicketsWithIncludes();  // correccion al mostra nombres en el listado de tickets
    }

    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(TmDatabaseContext context) : base(context)
        {
        }

        public bool AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return true;
            //Add(ticket);
            //return Save();
        }

        public bool DeleteTicket(int id)
        {
            // Llama al método Delete de la clase base con el ID
            return Delete(id);
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return _context.Tickets.ToList();
        }

        public Ticket GetTicketById(int id)
        {
            var ticket = _context.Tickets
       .Include(t => t.AsignadoANavigation)
       .Include(t => t.AsignadoPorNavigation)
       .FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                throw new InvalidOperationException($"No se encontró un ticket con el ID {id}.");
            }

            return ticket;
        }

        public Ticket UpdateTicket(Ticket ticket)
        {
            Update(ticket);
            Save();
            return ticket;
        }

        public IEnumerable<Ticket> FindTickets(Expression<Func<Ticket, bool>> predicate)
        {
            return _context.Tickets
                .Include(t => t.AsignadoANavigation)
                .Include(t => t.AsignadoPorNavigation)
                .Where(predicate)
                .ToList();
        }

        public IEnumerable<Ticket> GetByEstado(string estado)
        {
            return _context.Tickets
                .Include(t => t.AsignadoANavigation)
                .Include(t => t.AsignadoPorNavigation)
                .Where(t => t.Estado == estado)
                .ToList();
        }

        public IEnumerable<Ticket> GetAllTicketsWithIncludes()
        {
            return _context.Tickets
                .Include(t => t.AsignadoANavigation)
                .Include(t => t.AsignadoPorNavigation)
                .ToList();
        }

        private bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}