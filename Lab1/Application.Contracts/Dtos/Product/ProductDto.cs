using Domain.EnumStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Dtos.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
    public class RequestGetListFilterProductDto
    {
        public string Keyword { get; set; }
        public TypeProductEnum? Type { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
