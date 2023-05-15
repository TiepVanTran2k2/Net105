using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Lab2
{
    public class Client
    {
        public string ClientName { get; set; }
        //[Key, ForeignKey("Address")]
        public Guid Address_ID { get; set; }
        public string PhoneNO { get; set; }
        public Address Address { get; set; }
    }
}
