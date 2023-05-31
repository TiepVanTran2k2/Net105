using Application.Contracts.Dtos.Lab4;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
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
        [HttpPost]
        public IActionResult Update(Lab4Dto input)
        {
            return View();
        }
    }
}
