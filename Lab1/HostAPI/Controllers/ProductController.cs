using Application.Contracts.Services;
using Domain.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _iProductService;
        public ProductController(IProductRepository productService)
        {
            _iProductService = productService;
        }
        [HttpGet]
        public async Task<List<Product>> GetAllAsync([FromBody] int a)
        {
            return await _iProductService.GetAllAsync();
        }
    }
}
