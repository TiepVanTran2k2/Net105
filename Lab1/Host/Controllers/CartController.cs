using Application.Contracts.Dtos.Product;
using Application.Contracts.Dtos;
using Application.Contracts.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _iCartService;
        private readonly INotyfService _iNotyfService;
        public CartController(ICartService cartService,
                              INotyfService notyfService)
        {
            _iCartService = cartService;
            _iNotyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(string Id)
        {
            var result = await _iCartService.AddItemAsync(Guid.Parse(Id), User);
            if (result)
            {
                _iNotyfService.Success("Add success", 4);
                return View();
            }
            _iNotyfService.Warning("Add fail", 4);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetListItemCache()
        {
            return View(await _iCartService.GetListProductCacheAysnc(User));
        }
    }
}
