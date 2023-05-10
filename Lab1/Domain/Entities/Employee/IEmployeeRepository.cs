using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Employee
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
    }
}
