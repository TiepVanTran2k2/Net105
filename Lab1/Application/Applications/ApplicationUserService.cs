using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Services;
using AutoMapper;
using Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Applications
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IMapper _iMapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IApplicationUserRepository _userRepository;
        public ApplicationUserService(IMapper mapper,
                                      UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager,
                                      IApplicationUserRepository userRepository)
        {
            _iMapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
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

        public async Task<ApplicationUserDto> InformationUserAsync(ClaimsPrincipal input)
        {
            try
            {
                var informationUser = await _userManager.FindByIdAsync(_userManager.GetUserId(input));
                var informationMapper = _iMapper.Map<ApplicationUserDto>(informationUser);
                return informationMapper;
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

        public async Task<ApplicationUserDto> UpdateAsync(ApplicationUserDto input, ClaimsPrincipal claims)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(claims));
                user.Email = input.UserName;
                var result = await _userManager.UpdateAsync(user);
                return input;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
    }
}
