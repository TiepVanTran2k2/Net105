using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Department
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public List<Employee.Employee> Employees { get; set; }
    }
}
