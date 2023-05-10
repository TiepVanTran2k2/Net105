using Application.Contracts.Dtos.Information;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IInformationService
    {
        Task<InformationDto> GetAsync(Guid id);
        Task<List<InformationDto>> GetAllAsync();
        Task<InformationInsertDto> CreateAsync(InformationInsertDto dto);
        Task<InformationDto> DeleteAsync(Guid id);
        Task<InformationDto> EditAsync(InformationDto id);
    }
}
