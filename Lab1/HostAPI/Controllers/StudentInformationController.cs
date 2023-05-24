using Application.Contracts.Dtos.StudentInformation;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentInformationController : ControllerBase
    {
        private readonly IStudentInformationService _iStudentInformationService;
        public StudentInformationController(IStudentInformationService studentInformationService)
        {
            _iStudentInformationService = studentInformationService;
        }
        [HttpPost("get-all-information")]
        public async Task<List<StudentInformationResponseDto>> GetAllAsync()
        {
            return await _iStudentInformationService.GetAllAsync();
        }
        [HttpGet]
        public async Task<bool> CreateAsync(StudentInformationDto input)
        {
            return await _iStudentInformationService.CreateAsync(input);
        }
    }
}
