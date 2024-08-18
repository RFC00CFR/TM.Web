using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TM.Arquitecture.Models;
using TM.Data.Repository;

namespace TM.Data.Repository
{
    public interface IEmpleadoRepository : IRepository<Empleado>
    {
        // Métodos específicos de Empleado pueden ser agregados aquí si es necesario
    }
}