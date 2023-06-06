using Application.Contracts.Dtos.Payment;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IVnPayService _iVnPayService;
        public PaymentController(IVnPayService vnPayService)
        {
            _iVnPayService = vnPayService;
        }
        public IActionResult CreateUrl(PaymentInformationModel model)
        {
            var url = _iVnPayService.CreatePaymentUrl(model, HttpContext);
            return View(url);
        }
    }
}
