using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Lab4
{
    public class lab4 : BaseEntity
    {
        public string Name { get; set; }
        public string skill { get; set; }
        public int TotalStudent { get; set; }
        public decimal Salary { get; set; }
    }
}
