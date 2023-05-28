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

        public async Task<Paging<ProductDto, RequestGetListFilterProductDto>> GetListFilterProductAsync(Paging<ProductDto, RequestGetListFilterProductDto> input)
        {
            try
            {
                var listProduct = (await _iProductRepository.GetAllAsync());
                if(input?.Payload?.Keyword != null)
                {
                    listProduct = listProduct?.Where(x => x.Name.Contains(input.Payload.Keyword)).ToList();
                }
                if (input?.Payload?.Type != null)
                {
                    listProduct = listProduct?.Where(x => x.Type == (int)input.Payload.Type).ToList();
                }
                if (!listProduct.Any())
                {
                    return new Paging<ProductDto, RequestGetListFilterProductDto>();
                }
                if(input?.Payload?.Skip != null)
                {
                    listProduct = listProduct.Skip((input.Payload.Skip.HasValue && input.Payload.Take.HasValue) ? (input.Payload.Skip.Value - 1) * input.Payload.Take.Value : 0)
                                             .Take(input.Payload.Take.HasValue ? input.Payload.Take.Value : 10)
                                             .ToList();
                }
                else
                {
                    listProduct = listProduct.Skip(0)
                                              .Take(12)
                                              .ToList();
                }
                var result = _iMapper.Map<List<Product>, List<ProductDto>>(listProduct);
                double pageCount = (double)((decimal)(await _iProductRepository.GetAllAsync()).Count() / Convert.ToDecimal(12));
                return new Paging<ProductDto, RequestGetListFilterProductDto>
                {
                    Payload = input.Payload,
                    PageCount = (int)Math.Ceiling(pageCount),
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
