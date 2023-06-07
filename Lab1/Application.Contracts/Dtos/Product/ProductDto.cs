using Domain.EnumStatus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Dtos.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StatusProductEnum Status { get; set; }
        public string UrlImg { get; set; }
        public decimal Price { get; set; }
    }
    public class ItemCacheDto
    {
        public string IdUser { get; set; }
        public List<ProductCacheDto> ListProductCache { get; set; }
    }
    public class ProductCacheDto
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public StatusProductEnum Status { get; set; }
        public string UrlImg { get; set; }
        public decimal Price { get; set; }
    }
    public class RequestGetListFilterProductDto
    {
        public string Keyword { get; set; }
        public TypeProductEnum? Type { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
    public class RequestCreateProductDto
    {
        [Required(ErrorMessage = "Please input product name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please choose file image")]
        public IFormFile GetFile { get; set; }
        public string UrlImg { get; set; }
        public string ContainerName { get; set; }
        [Required(ErrorMessage = "Please input price")]
        public decimal? Price { get; set; }
        public StatusProductEnum Status { get; set; }
        [Required(ErrorMessage = "Please choose type product")]
        public TypeProductEnum? Type { get; set; }
        public string Description { get; set; }
    }

    public class RequestUpdateProductDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please input product name")]
        public string Name { get; set; }
        public IFormFile? GetFile { get; set; }
        public string UrlImg { get; set; }
        public string ContainerName { get; set; }
        [Required(ErrorMessage = "Please input price")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Please choose Status")]
        public StatusProductEnum? Status { get; set; }
        [Required(ErrorMessage = "Please choose type product")]
        public TypeProductEnum? Type { get; set; }
        public string Description { get; set; }
    }
}
