using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Services;
using AutoMapper;
using Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace Application.Applications
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IMapper _iMapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public ApplicationUserService(IMapper mapper,
                                      UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager)
        {
            _iMapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task ForgotPasswordAsync(ForgotPassworDto input)
        {
            try
            {

            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<LoginDto> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return loginDto;
                }
                throw new Exception("Login fail");
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<RegisterDto> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var register = _iMapper.Map<ApplicationUser>(registerDto);
                var result = await _userManager.CreateAsync(register, registerDto.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(register, isPersistent: false);
                    return registerDto;
                }
                return registerDto;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
    }
}
