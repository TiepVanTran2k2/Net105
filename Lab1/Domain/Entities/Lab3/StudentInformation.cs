using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Lab3
{
    public class StudentInformation : BaseEntity
    {
        [Display(Name = "The field Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="The field Gender is Required")]
        public bool? Gender { get; set; }
        [Required(ErrorMessage ="The field Birthdate is Required")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage ="The field BatchTime is Required")]
        public TimeSpan BatchTime { get; set; }
        [Required(ErrorMessage = "The field PhoneNumber is Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "The field Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The field Url is Required")]
        [Url]
        public string Url { get; set; }
        [Required(ErrorMessage = "The field Password is Required") , Compare(nameof(ComfirmPassword))]
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
    }
}
