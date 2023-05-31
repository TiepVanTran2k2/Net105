using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Dtos.Lab4
{
    public class Lab4Dto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is not required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Skill is not required")]
        public string Skill { get; set; }
        [Required(ErrorMessage = "Total student is not required")]
        public int? TotalStudent { get; set; }
        [Required(ErrorMessage = "Salary is not required")]
        public decimal? Salary { get; set; }
    }
}
