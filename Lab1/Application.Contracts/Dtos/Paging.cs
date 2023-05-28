using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Dtos
{
    public class Paging<T,D>
    {
        public D Payload { get; set; }
        public int PageCount{ get; set; }
        public List<T>? Items { get; set; }
    }
}
