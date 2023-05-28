using Application.Contracts.Dtos;
using Application.Contracts.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IProductService
    {
        Task<Paging<ProductDto>> GetListFilterProductAsync(RequestGetListFilterProductDto input);
    }
}
