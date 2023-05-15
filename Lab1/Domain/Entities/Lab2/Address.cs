using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Lab2
{
    public class Address
    {
        //[Key]
        public Guid Addr_ID { get; set; }
        public string Home_addr { get; set; } 
        public string Office_addr { get; set; }
        public Client Client { get; set; }
    }
}
