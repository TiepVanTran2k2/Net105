using Application.Contracts.Dtos.Product;
using Application.Contracts.Dtos;
using Application.Contracts.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult> AddItem(string Id)
        {
            var result = await _iCartService.AddItemAsync(Guid.Parse(Id), User);
            if (!result)
            {
                //  Send "false"
                return Json(new { success = false, responseText = "The attached file is not supported." });
            }
            //  Send "Success"
            return Json(new { success = true, responseText = "Your message successfuly sent!" });
        }
        public async Task<IActionResult> GetListItemCache()
        {
            await _iCartService.SyncDataCacheWithDbAsync(User);
            return View(await _iCartService.GetListProductCacheAysnc(User));
        }
        [HttpGet]
        public async Task<IActionResult> HistoryPayment()
        {
            return View(await _iCartService.HistoryBillAsync(User));
        }
    }
}
