using Application.Contracts.Dtos;
using Application.Contracts.Dtos.Product;
using Application.Contracts.Services;
using Host.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Host.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _iProductService;
        public HomeController(ILogger<HomeController> logger,
                              IProductService productService)
        {
            _logger = logger;
            _iProductService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _iProductService.GetListFilterProductAsync(new Paging<ProductDto, RequestGetListFilterProductDto>()));
        }
        [HttpPost]
        public async Task<IActionResult> Index(Paging<ProductDto, RequestGetListFilterProductDto> input)
        {
            return View(await _iProductService.GetListFilterProductAsync(input));
        }
    }
}