using Application.Contracts.Dtos.Product;
using Application.Contracts.Services;
using AutoMapper;
using Domain.Entities.Product;
using Domain.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        public CartService(ICacheHelper cacheHelper,
                           IProductRepository iProductRepository,
                           IMapper mapper,
                           UserManager<IdentityUser> userManager)
        {
            _iCacheHelper = cacheHelper;
            _iProductRepository = iProductRepository; 
            _iMapper = mapper;
            _userManager = userManager;
        }
        public async Task<bool> AddItemAsync(Guid id, ClaimsPrincipal input)
        {
            try
            {
                var dataCache = new ItemCacheDto();
                var userId = _userManager.GetUserId(input);

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
                    dataCache.IdUser = userId != null ? userId.ToString() : string.Empty.ToString();
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
                var dataCache = _iCacheHelper.GetAsync<ItemCacheDto>(userId != null ? userId : string.Empty.ToString());
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
    }
}
