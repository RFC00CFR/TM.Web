using System;
using System.Collections.Generic;

namespace TM.Arquitecture.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Prioridad { get; set; } = null!;

    public DateOnly FechaDeAsignado { get; set; }

    public DateOnly FechaDeEntrega { get; set; }

    public string Estado { get; set; } = null!;

    public int AsignadoPor { get; set; }

    public int AsignadoA { get; set; }

    public virtual Empleado AsignadoANavigation { get; set; } = null!;

    public virtual Empleado AsignadoPorNavigation { get; set; } = null!;

    public virtual ICollection<TicketsEmpleado> TicketsEmpleados { get; set; } = new List<TicketsEmpleado>();
}
