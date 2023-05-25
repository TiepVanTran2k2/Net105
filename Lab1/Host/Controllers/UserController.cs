using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class UserController : Controller
    {
        private readonly IApplicationUserService _iApplicationUserService;
        public UserController(IApplicationUserService applicationUserService)
        {
            _iApplicationUserService = applicationUserService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto input)
        {
            await _iApplicationUserService.RegisterAsync(input);
            return RedirectToAction("Index", "Home");
        }

    }
}
