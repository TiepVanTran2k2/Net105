using Application.Contracts.Dtos.Bill;
using Application.Contracts.Dtos.Payment;
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
        Task<bool> UpdateUserIdCacheAsync(Guid userId);
        Task<bool> SyncDataCacheWithDbAsync(ClaimsPrincipal input);
        Task<bool> RemoveCartAsync(ClaimsPrincipal input);
        Task<bool> InsertOrderAsync(PaymentResponseModel bill, ClaimsPrincipal input);
        Task<List<BillDto>> HistoryBillAsync(ClaimsPrincipal input);
    }
}
