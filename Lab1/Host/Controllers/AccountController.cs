using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Dtos.User;
using Application.Contracts.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using EntityFrameworkCore.Entity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Host.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationUserService _iApplicationUserService;
        private readonly INotyfService _notyf;
        public AccountController(IApplicationUserService applicationUserService,
                                 INotyfService notyf)
        {
            _iApplicationUserService = applicationUserService;
            _notyf = notyf;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _iApplicationUserService.GetListAsync();
            return View(result);
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
            try
            {
                var result = await _iApplicationUserService.LoginAsync(loginDto);
                if (result)
                {
                    _notyf.Success("Login success", 4);
                    return RedirectToAction("Index", "Home");
                }
                _notyf.Warning("Login fail");
                return View();
            }
            catch(Exception ex)
            {
                _notyf.Error(ex.Message, 4);
                return View();
            }
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
        public async Task<IActionResult> Information(ApplicationUserDto input)
        {
            try
            {
                var mess = await _iApplicationUserService.UpdateAsync(input, User);
                if (mess == "Update success")
                {
                    _notyf.Success(mess, 4);
                    return RedirectToAction("Information");
                }
                _notyf.Warning(mess, 4);
                return RedirectToAction("Information");
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message, 4);
                return RedirectToAction("Information");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            return View(await _iApplicationUserService.GetAsync(id, User));
        }
        [HttpPost]
        public async Task<IActionResult> Update(RequestUpdateUserDto input)
        {
            await _iApplicationUserService.EditAsync(input);
            return RedirectToAction(nameof(Index));
        }
    }
}
