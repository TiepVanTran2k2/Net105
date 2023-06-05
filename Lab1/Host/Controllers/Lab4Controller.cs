using Application.Contracts.Dtos.Lab4;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Authorize]
    public class Lab4Controller : Controller
    {
        public readonly ILab4Service _iLab4Service;
        public Lab4Controller(ILab4Service lab4Service)
        {
            _iLab4Service = lab4Service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _iLab4Service.GetListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Lab4Dto input)
        {
            await _iLab4Service.CreateAsync(input);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(Guid id)
        {
            await _iLab4Service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
