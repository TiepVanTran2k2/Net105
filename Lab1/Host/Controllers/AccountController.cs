using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationUserService _iApplicationUserService;
        public AccountController(IApplicationUserService applicationUserService)
        {
            _iApplicationUserService = applicationUserService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
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
        [HttpGet]
        public IActionResult Login(string returnUrl= null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            await _iApplicationUserService.LoginAsync(loginDto);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _iApplicationUserService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPassworDto input)
        {
            await _iApplicationUserService.ForgotPasswordAsync(input);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<IActionResult> Information()
        {
            return View(await _iApplicationUserService.InformationUserAsync(User));
        }
        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUserDto input)
        {
            await _iApplicationUserService.UpdateAsync(input, User);
            return RedirectToAction("Information");
        }
    }
}
