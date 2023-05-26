using Application.Contracts.Dtos.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IApplicationUserService
    {
        Task<RegisterDto> RegisterAsync(RegisterDto registerDto);
        Task<LoginDto> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
        Task ForgotPasswordAsync(ForgotPassworDto input);
        Task<ApplicationUserDto> InformationUserAsync(ClaimsPrincipal input);
        Task<ApplicationUserDto> UpdateAsync(ApplicationUserDto input, ClaimsPrincipal claims);
    }
}
