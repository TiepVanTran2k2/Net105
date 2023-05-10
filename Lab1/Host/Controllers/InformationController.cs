using Application.Contracts.Dtos.Information;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Host.Controllers
{
    public class InformationController : Controller
    {
        private readonly IInformationService _informationService;
        public InformationController(IInformationService informationService)
        {
            _informationService = informationService;
        }
        [HttpGet("load-all")]
        public async Task<IActionResult> Index()
        {
            return View(await _informationService.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,License,Revenue")] InformationInsertDto information)
        {
            await _informationService.CreateAsync(information);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var information = await _informationService.GetAsync(id);
            return View(information);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _informationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            return View(await _informationService.GetAsync(id));
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditAsync([Bind("Id,Name,License,Establshed,Revenue")] InformationDto model)
        {
            await _informationService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
