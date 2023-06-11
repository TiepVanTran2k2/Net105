using Application.Contracts.Dtos.Product;
using Application.Contracts.Dtos;
using Application.Contracts.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Domain.Shared.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Host.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _iCartService;
        private readonly INotyfService _iNotyfService;
        private readonly ICacheHelper _iCacheService;
        private readonly UserManager<IdentityUser> _userManager;
        public CartController(ICartService cartService,
                              INotyfService notyfService,
                              ICacheHelper cacheHelper,
                              UserManager<IdentityUser> userManager)
        {
            _iCartService = cartService;
            _iNotyfService = notyfService;
            _iCacheService = cacheHelper;
            _userManager = userManager;
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
        [HttpGet]
        public async Task<int> GetCountCartUser()
        {
            var userId = _userManager.GetUserId(User);
            var dataCache = _iCacheService.GetAsync<ItemCacheDto>(userId != null ? userId : Guid.Empty.ToString());
            if(dataCache == null)
            {
                return await Task.FromResult(0);
            }
            return await Task.FromResult(dataCache.ListProductCache.Count);
        }
        [HttpPost]
        public async Task<ItemCacheDto> ChangeCountProductCache(RequestChangeCountProductCacheDto input)
        {
            return await _iCartService.ChangeCountAsync(input, User);
        }
        [HttpPost]
        public async Task<List<ProductCacheDto>> RemoveProductCache(ResponseRemoveItemCart input)
        {
            return await _iCartService.RemoveItemCartAsync(input);
        }
    }
}
