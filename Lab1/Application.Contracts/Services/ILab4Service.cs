using Application.Contracts.Dtos.Lab4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface ILab4Service
    {
        Task<List<Lab4Dto>> GetListAsync();
        Task<bool> CreateAsync(Lab4Dto input);
        Task<bool> DeleteAsync(Guid id);
    }
}
