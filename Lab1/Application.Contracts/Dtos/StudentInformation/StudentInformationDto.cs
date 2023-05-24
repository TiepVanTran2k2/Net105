using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Dtos.StudentInformation
{
    public class StudentInformationDto
    {
        [Required]
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string Name { get; set; }
        [Required]
        public bool? Gender { get; set; }
        [Required]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
        [Required]
        [Display(Name = "Batch Time")]
        public TimeSpan? BatchTime { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(10), MaxLength(10), MinLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Website Url")]
        [Url]
        public string Url { get; set; }
        [Required, Compare(nameof(ComfirmPassword))]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Comfirm password")]
        public string ComfirmPassword { get; set; }
    }

    public class StudentInformationResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public TimeSpan? BatchTime { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
    }
}
