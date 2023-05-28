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
                var role = (await _userManager.GetRolesAsync(informationUser))?.OrderBy(x => x).FirstOrDefault();
                var informationMapper = _iMapper.Map<ApplicationUserDto>(informationUser);
                informationMapper.Role = role;
                return informationMapper;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
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

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var register = _iMapper.Map<ApplicationUser>(registerDto);
                var result = await _userManager.CreateAsync(register, registerDto.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(register, isPersistent: false);
                    var resultRole = await AddRoleUserAsync(register.Id);
                    if (!resultRole)
                    {
                        return "Add role fail";
                    }
                    return "Success";
                }
                var message = result.Errors.FirstOrDefault()?.Description;
                return message; 
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
        public async Task<bool> AddRoleUserAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var roleResult = await _userManager.AddToRoleAsync(user, "employee");
                if (roleResult.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<string> UpdateAsync(ApplicationUserDto input, ClaimsPrincipal claims)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(claims));
                var userList = (_userRepository.GetList()).Where(x => x.Email != user.Email || x.PhoneNumber != user.PhoneNumber);                

                if(user.Email == input.Email && user.PhoneNumber == input.PhoneNumber)
                {
                    user.Email = input.Email;
                    user.PhoneNumber = input.PhoneNumber;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return "Update success";
                    }
                }
                else
                {
                    if(userList.Any(x => x.Email == input.Email || x.PhoneNumber == input.PhoneNumber))
                    {
                        if(userList.Any(x => x.Email == input.Email))
                        {
                            return "Email exist";
                        }
                        return "Phone number exist";
                    }
                    user.Email = input.Email;
                    user.PhoneNumber = input.PhoneNumber;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return "Update success";
                    }
                }                
                return "Update fail";
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
    }
}
