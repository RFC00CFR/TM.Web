using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Arquitecture.Models;

namespace TM.Data.Repository
{
    public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(ApDatabaseContext context) : base(context)
        {
        }

        // Métodos específicos de Empleado pueden ser sobreescritos aquí si es necesario
    }
}