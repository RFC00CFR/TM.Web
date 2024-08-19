using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TM.Arquitecture.Models;

namespace TM.Data.EmpleadoRepository
{
    public interface IEmpleadoRepository : IRepository<Empleado>
    {
        bool AddEmpleado(Empleado empleado);
        bool DeleteEmpleado(int id);
        IEnumerable<Empleado> GetAllEmpleados();
        Empleado GetEmpleadoById(int id);
        Empleado UpdateEmpleado(Empleado empleado);
        IEnumerable<Empleado> FindEmpleados(Expression<Func<Empleado, bool>> predicate);
        IEnumerable<Empleado> GetByDepartamento(string departamento);
    }
    public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(TmDatabaseContext context) : base(context)
        {
        }

        public bool AddEmpleado(Empleado empleado)
        {
            return Add(empleado);
        }

        public bool DeleteEmpleado(int id)
        {
            return Delete(id);
        }

        public IEnumerable<Empleado> GetAllEmpleados()
        {
            return GetAll();
        }

        public Empleado GetEmpleadoById(int id)
        {
            return GetById(id);
        }

        public Empleado UpdateEmpleado(Empleado empleado)
        {
            return Update(empleado);
        }

        public IEnumerable<Empleado> FindEmpleados(Expression<Func<Empleado, bool>> predicate)
        {
            return Find(predicate);
        }

        public IEnumerable<Empleado> GetByDepartamento(string departamento)
        {
            return _context.Set<Empleado>().Where(e => e.Departamento == departamento).ToList();
        }
    }
}