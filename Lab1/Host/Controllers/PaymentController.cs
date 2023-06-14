using Application.Applications;
using Application.Contracts.Dtos.Payment;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IVnPayService _iVnPayService;
        private readonly ICartService _iCartService;
        public PaymentController(IVnPayService vnPayService,
                                 ICartService cartService)
        {
            _iVnPayService = vnPayService;
            _iCartService = cartService;
        }
        public async Task<IActionResult> CreateUrl(PaymentInformationModel model)
        {
            var url = _iVnPayService.CreatePaymentUrl(model, HttpContext);
            if (!string.IsNullOrEmpty(url))
            {
                return Json(new { success = true, responseText = url });
            }
            return Json(new { success = false});
        }
        public async Task<IActionResult> PaymentCallback()
        {
            var response = await _iVnPayService.PaymentExecute(Request.Query);
            await _iCartService.InsertOrderAsync(response, User);
            await _iCartService.RemoveCartAsync(User);
            return RedirectToAction("Index", "Home");
        }
    }
}
