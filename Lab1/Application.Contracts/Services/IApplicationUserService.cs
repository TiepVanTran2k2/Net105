using Application.Contracts.Dtos.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IApplicationUserService
    {
        Task<RegisterDto> RegisterAsync(RegisterDto registerDto);
    }
}
