using Application.Contracts.Dtos.Bill;
using Application.Contracts.Dtos.Payment;
using Application.Contracts.Dtos.Product;
using Application.Contracts.Services;
using AutoMapper;
using Domain.Entities.Bill;
using Domain.Entities.Product;
using Domain.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class CartService : ICartService
    {
        private readonly ICacheHelper _iCacheHelper;
        private readonly IProductRepository _iProductRepository;
        private readonly IMapper _iMapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBillRepository _iBillRepository;
        private readonly IBillDetailRepository _iBillDetailRepository;
        public CartService(ICacheHelper cacheHelper,
                           IProductRepository iProductRepository,
                           IMapper mapper,
                           UserManager<IdentityUser> userManager,
                           IBillRepository billRepository,
                           IBillDetailRepository billDetailRepository)
        {
            _iCacheHelper = cacheHelper;
            _iProductRepository = iProductRepository; 
            _iMapper = mapper;
            _userManager = userManager;
            _iBillRepository = billRepository;
            _iBillDetailRepository = billDetailRepository;
        }
        public async Task<bool> AddItemAsync(Guid id, ClaimsPrincipal input)
        {
            try
            {                
                var userId = _userManager.GetUserId(input);
                var dataCache = new ItemCacheDto()
                {
                    IdUser = !string.IsNullOrEmpty(userId) ? userId : Guid.Empty.ToString(),
                    ListProductCache = new List<ProductCacheDto>()
                };
                
                var product = await _iProductRepository.GetByIdAsync(id);
                if(product == null)
                {
                    return false;
                }
                var result = _iMapper.Map<ProductCacheDto>(product);
                var checkExistCache = _iCacheHelper.GetAsync<ItemCacheDto>(!string.IsNullOrEmpty(userId) ? userId : Guid.Empty.ToString());
                if(checkExistCache != null)
                {
                    dataCache = checkExistCache;
                    if(!checkExistCache.ListProductCache.Any(x =>x.Id == id))
                    {
                        result.Count += 1;
                        dataCache.ListProductCache.Add(result);
                    }
                    else
                    {
                        foreach(var item in dataCache.ListProductCache)
                        {
                            if(item.Id == id)
                            {
                                item.Count = checkExistCache.ListProductCache.First(x => x.Id == id).Count + 1;
                            }
                        }
                    }
                }
                else
                {
                    result.Count = 1;
                    dataCache.IdUser = userId != null ? userId.ToString() : Guid.Empty.ToString();
                    dataCache.ListProductCache = new List<ProductCacheDto> { result };
                }
                
                _iCacheHelper.CreateAsync(dataCache, dataCache.IdUser);
                return true;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<List<ProductCacheDto>> GetListProductCacheAysnc(ClaimsPrincipal input)
        {
            try
            {
                var userId = _userManager.GetUserId(input);
                var dataCache = _iCacheHelper.GetAsync<ItemCacheDto>(userId != null ? userId : Guid.Empty.ToString());
                if(dataCache == null)
                {
                    return await Task.FromResult(new List<ProductCacheDto>());
                }
                return await Task.FromResult(dataCache.ListProductCache);
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<List<BillDto>> HistoryBillAsync(ClaimsPrincipal input)
        {
            try
            {
                var userId = _userManager.GetUserId(input);
                var bill = (await _iBillRepository.GetAllAsync()).Where(x => x.UserId == userId).ToList();
                
                if (!bill.Any())
                {
                    return new List<BillDto>();
                }
                var billDetail = (await _iBillDetailRepository.GetAllAsync()).Where(x => bill.Select(a => a.Id).Contains(x.BillId)).ToList();
                var listBill = bill.Join(billDetail, b => b.Id, bd => bd.BillId,
                                                     (b, db) => new 
                                                     {
                                                         B = b,
                                                         BD = db
                                                     }  
                                         ).GroupBy(x => x.B).Select(x => new Bill
                                         {
                                             OrderDescription = x.Key.OrderDescription,
                                             OrderId = x.Key.OrderId,
                                             PaymentId = x.Key.PaymentId,
                                             PaymentMethod = x.Key.PaymentMethod,
                                             Success = x.Key.Success,
                                             Token = x.Key.Token,
                                             TransactionId = x.Key.TransactionId,
                                             VnPayResponseCode = x.Key.VnPayResponseCode,
                                             DetailBill = x.Select(a => a.BD).ToList()
                                         }).ToList();

                var result = _iMapper.Map<List<BillDto>>(listBill);
                return result;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<bool> InsertOrderAsync(PaymentResponseModel bill, ClaimsPrincipal input)
        {
            try
            {
                var userId = _userManager.GetUserId(input);
                var dataCache = _iCacheHelper.GetAsync<ItemCacheDto>(userId);
                if (dataCache == null)
                {
                    return true;
                }
                var billResult = _iMapper.Map<Bill>(bill);
                billResult.UserId = userId;
                var billInsert = await _iBillRepository.CreateAsync(billResult);
                foreach(var item in dataCache.ListProductCache)
                {
                    var detailBill = _iMapper.Map<DetailBill>(item);
                    detailBill.BillId = billInsert.Id;
                    await _iBillDetailRepository.CreateAsync(detailBill);
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<bool> RemoveCartAsync(ClaimsPrincipal input)
        {
            try
            {
                var userId = _userManager.GetUserId(input);
                _iCacheHelper.Remove(userId);
                return await Task.FromResult(true);
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<bool> SyncDataCacheWithDbAsync(ClaimsPrincipal input)
        {
            try
            {
                var userId = _userManager.GetUserId(input) != null ? _userManager.GetUserId(input) : Guid.Empty.ToString();
                var dataCache = _iCacheHelper.GetAsync<ItemCacheDto>(userId);
                if(dataCache == null)
                {
                    return await Task.FromResult(false);
                }
                var listIdItem = dataCache.ListProductCache.Select(x => x.Id).ToList();
                var listProductDb = (await _iProductRepository.GetAllAsync()).Where(x => listIdItem.Contains(x.Id)).ToList();
                var dataCacheNew = _iMapper.Map<List<ProductCacheDto>>(listProductDb);
                foreach(var item in dataCacheNew)
                {
                    item.Count = dataCache.ListProductCache.FirstOrDefault(x => x.Id == item.Id).Count;
                }
                dataCache.ListProductCache = dataCacheNew;
                _iCacheHelper.Remove(userId);
                _iCacheHelper.CreateAsync(dataCache, userId);
                return true;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<bool> UpdateUserIdCacheAsync(Guid userId)
        {
            try
            {
                var dataCacheUserIdEmpty = _iCacheHelper.GetAsync<ItemCacheDto>(Guid.Empty.ToString());
                var dataCacheUserLogin = _iCacheHelper.GetAsync<ItemCacheDto>(userId.ToString());
                if(dataCacheUserLogin != null)
                {
                    if(dataCacheUserIdEmpty != null)
                    {
                        var listIdDiff = dataCacheUserIdEmpty.ListProductCache.Select(x => x.Id).ToList()
                                        .Except(dataCacheUserLogin.ListProductCache.Select(x => x.Id).ToList()).ToList();
                        if(listIdDiff.Count < dataCacheUserIdEmpty.ListProductCache.Count)
                        {
                            foreach (var item in dataCacheUserLogin.ListProductCache)
                            {
                                if (dataCacheUserIdEmpty.ListProductCache.Any(x => x.Id == item.Id))
                                {
                                    //dataCacheUserLogin.IdUser = userId.ToString();
                                    item.Count += dataCacheUserIdEmpty.ListProductCache.First(x => x.Id == item.Id).Count;
                                    continue;
                                }
                            }
                            dataCacheUserLogin.ListProductCache.AddRange(dataCacheUserIdEmpty.ListProductCache.Where(x => listIdDiff.Contains(x.Id)).ToList());
                        }
                        if (listIdDiff.Count == dataCacheUserIdEmpty.ListProductCache.Count)
                        {
                            dataCacheUserLogin.ListProductCache.AddRange(dataCacheUserIdEmpty.ListProductCache);
                        }
                        
                        _iCacheHelper.Remove(Guid.Empty.ToString());
                    }
                    _iCacheHelper.CreateAsync(dataCacheUserLogin, userId.ToString());
                    return await Task.FromResult(true);

                }
                if (dataCacheUserIdEmpty != null)
                {
                    dataCacheUserIdEmpty.IdUser = userId.ToString();
                    _iCacheHelper.CreateAsync(dataCacheUserIdEmpty, userId.ToString());
                    _iCacheHelper.Remove(Guid.Empty.ToString());
                }
                return await Task.FromResult(true);
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
    }
}
