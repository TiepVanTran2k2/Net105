using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Lab2
{
    public class PersonCompanies
    {
        public Guid Id { get; set; }
        public DateTime FromYear { get; set; }
        public DateTime? ToYear { get; set; }
        public DateTime? Current { get; set; }
        public string Position { get; set; }
        public Guid Company_Id { get; set; }
        public Guid Person_Id { get; set; }
        public Person Person { get; set; }
        public Company Company { get; set; }
    }
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<PersonCompanies> Companies { get; set; }
    }
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<PersonCompanies> PersonCompanies { get; set; }
    }
}
