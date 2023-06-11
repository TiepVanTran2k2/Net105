using Application.Contracts.Dtos.Product;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService _iCartService;
        public OrderController(ICartService cartService)
        {
            _iCartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _iCartService.ManagerBillAsync());
        }
        public async Task<ResponseChangeStatusBill> UpdateStatusBill(ResponseCheckStatusBillCart input)
        {
            return await _iCartService.CheckStatusBillAsync(input);
        }
    }
}
