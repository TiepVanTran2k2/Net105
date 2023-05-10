using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Dtos.Information
{
    public class InformationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string License { get; set; }
        public DateTime Establshed { get; set; }
        public decimal Revenue { get; set; }
    }
    public class InformationInsertDto
    {
        public string Name { get; set; }
        public string License { get; set; }
        public decimal Revenue { get; set; }
    }
}
