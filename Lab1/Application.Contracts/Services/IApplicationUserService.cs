using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Dtos.User;
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
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<bool> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
        Task ForgotPasswordAsync(ForgotPassworDto input);
        Task<ApplicationUserDto> InformationUserAsync(ClaimsPrincipal input);
        Task<string> UpdateAsync(ApplicationUserDto input, ClaimsPrincipal claims);
        Task<List<UserDto>> GetListAsync();
        Task<bool> EditAsync(RequestUpdateUserDto input);
        Task<RequestUpdateUserDto> GetAsync(Guid id, ClaimsPrincipal identity);
    }
}
