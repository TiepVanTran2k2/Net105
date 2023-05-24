using Application.Contracts.Dtos.StudentInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IStudentInformationService
    {
        Task<List<StudentInformationResponseDto>> GetAllAsync();
        Task<bool> CreateAsync(StudentInformationDto input); 
    }
}
