using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Employee
{
    public class Employee : BaseEntity
    {
        [ForeignKey("DepartmentId")]
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public Department.Department Departments { get; set; } 
    }
}
