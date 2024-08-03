using System;
using System.Collections.Generic;

namespace TM.Arquitecture.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Puesto { get; set; } = null!;

    public string Departamento { get; set; } = null!;

    public virtual ICollection<Ticket> TicketAsignadoANavigations { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketAsignadoPorNavigations { get; set; } = new List<Ticket>();

    public virtual ICollection<TicketsEmpleado> TicketsEmpleados { get; set; } = new List<TicketsEmpleado>();
}
