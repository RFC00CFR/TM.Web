using System;
using System.Collections.Generic;

namespace TM.Arquitecture.Models;

public partial class TicketsEmpleado
{
    public int Id { get; set; }

    public int EmpleadoId { get; set; }

    public int TicketId { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
