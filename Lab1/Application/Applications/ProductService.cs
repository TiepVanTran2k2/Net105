using Application.Contracts.Dtos;
using Application.Contracts.Dtos.Product;
using Application.Contracts.Services;
using AutoMapper;
using Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _iProductRepository;
        private readonly IMapper _iMapper;
        public ProductService(IProductRepository productRepository,
                              IMapper mapper)
        {
            _iProductRepository = productRepository;
            _iMapper = mapper;
        }

        public async Task<Paging<ProductDto>> GetListFilterProductAsync(RequestGetListFilterProductDto input)
        {
            try
            {
                var listProduct = (await _iProductRepository.GetAllAsync());
                if(input.Keyword != null)
                {
                    listProduct = listProduct?.Where(x => x.Name.Contains(input.Keyword)).ToList();
                }
                if (input.Type != null)
                {
                    listProduct = listProduct?.Where(x => x.Type == (int)input.Type).ToList();
                }
                if (!listProduct.Any())
                {
                    return new Paging<ProductDto>();
                }
                var resultFilter = listProduct.Skip((input.Skip.HasValue && input.Take.HasValue) ? (input.Skip.Value - 1) * input.Take.Value : 0)
                                              .Take(input.Take.HasValue ? input.Take.Value : 10)
                                              .ToList();
                var result = _iMapper.Map<List<Product>, List<ProductDto>>(resultFilter);
                return new Paging<ProductDto>
                {
                    Total = (await _iProductRepository.GetAllAsync()).Count(),
                    Items = result
                };

            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
    }
}
