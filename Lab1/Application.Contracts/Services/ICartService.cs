using Application.Contracts.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface ICartService
    {
        Task<bool> AddItemAsync(Guid id, ClaimsPrincipal input);
        Task<List<ProductCacheDto>> GetListProductCacheAysnc(ClaimsPrincipal input);
    }
}
