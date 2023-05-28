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
            return View(await _iProductService.GetListFilterProductAsync(new RequestGetListFilterProductDto()));
        }
        [HttpPost]
        public async Task<IActionResult> Index(RequestGetListFilterProductDto input)
        {
            return View(await _iProductService.GetListFilterProductAsync(input));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}