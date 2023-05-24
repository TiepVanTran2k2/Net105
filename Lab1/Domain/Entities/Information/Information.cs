using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Information
{
    public class Information : BaseEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        public string Name { get; set; }
        public string License { get; set; }
        public DateTime Establshed { get; set; } = DateTime.Now;
        public decimal Revenue { get; set; }
    }
}
