using Application.Contracts.Dtos;
using Application.Contracts.Dtos.Product;
using Application.Contracts.Services;
using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _iProductRepository;
        private readonly IMapper _iMapper;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        public ProductService(IProductRepository productRepository,
                              IMapper mapper,
                              BlobServiceClient blobServiceClient,
                              IConfiguration configuration)
        {
            _iProductRepository = productRepository;
            _iMapper = mapper;
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public async Task<bool> CreateAsync(RequestCreateProductDto input)
        {
            try
            {
                if(input.GetFile != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(input.GetFile.FileName)+"_"+Guid.NewGuid()+Path.GetExtension(input.GetFile.FileName);
                    var resultUpLoadFile = await UpLoadBlob(fileName, _configuration["ContainerName"], input.GetFile);
                    switch (resultUpLoadFile)
                    {
                        case true:
                            input.UrlImg = GetBlob(fileName, _configuration["ContainerName"]);
                            break;
                        case false:
                            input.UrlImg = GetBlob(_configuration["ContainerName"], "testimage.jpg");
                            break;
                    }
                }
                else
                {
                    input.UrlImg = GetBlob(_configuration["ContainerName"], "testimage.jpg");
                }
                var resutl = _iMapper.Map<Product>(input);
                await _iProductRepository.CreateAsync(resutl);
                return true;

            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
        public async Task<bool> UpLoadBlob(string name, string containerName, IFormFile file)
        {
            try
            {
                BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var blobClient = blobContainerClient.GetBlobClient(name);

                var httpHeaders = new BlobHttpHeaders()
                {
                    ContentType = file.ContentType
                };

                IDictionary<string, string> metaData = new Dictionary<string, string>();
                metaData.Add("title", "Image Product");
                metaData["comment"] = "Image Product";

                var result = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders, metaData);
                if (result != null)
                    return true;
                return false;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
        public string GetBlob(string name, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(name);
            return blobClient.Uri.AbsoluteUri;
        }
        public async Task<Paging<ProductDto, RequestGetListFilterProductDto>> GetListFilterProductAsync(Paging<ProductDto, RequestGetListFilterProductDto> input)
        {
            try
            {
                var listProduct = (await _iProductRepository.GetAllAsync());
                if(input?.Payload?.Keyword != null)
                {
                    listProduct = listProduct?.Where(x => x.Name.ToLower().Contains(input.Payload.Keyword.ToLower())).ToList();
                }
                if (input?.Payload?.Type != null)
                {
                    listProduct = listProduct?.Where(x => x.Type == (int)input.Payload.Type).ToList();
                }
                if (!listProduct.Any())
                {
                    return new Paging<ProductDto, RequestGetListFilterProductDto>();
                }
                var countAll = listProduct.Count();
                if(input?.Payload?.Skip != null)
                {
                    listProduct = listProduct.Skip((input.Payload.Skip.HasValue) ? (input.Payload.Skip.Value - 1) * 12 : 0)
                                             .Take(12)
                                             .ToList();
                }
                else
                {
                    listProduct = listProduct.Skip(0)
                                              .Take(12)
                                              .ToList();
                }
                var result = _iMapper.Map<List<Product>, List<ProductDto>>(listProduct.OrderByDescending(x => x.CreationDate).ToList());
                double pageCount = (double)((decimal)countAll/ Convert.ToDecimal(12)); ;
                
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
