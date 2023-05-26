using Application.Contracts.Dtos.StudentInformation;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Host.Controllers
{
    [Authorize]
    public class StudentInformationController : Controller
    {
        private readonly IStudentInformationService _iStudentInformationService;
        public StudentInformationController(IStudentInformationService studentInformationService)
        {
            _iStudentInformationService = studentInformationService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _iStudentInformationService.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentInformationDto input)
        {
            await _iStudentInformationService.CreateAsync(input);
            return RedirectToAction(nameof(Index));
        }
    }
}
