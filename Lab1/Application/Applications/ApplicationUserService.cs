using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Dtos.User;
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
        private readonly ICartService _iCartService;
        public ApplicationUserService(IMapper mapper,
                                      UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager,
                                      IApplicationUserRepository userRepository,
                                      ICartService iCartService)
        {
            _iMapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _iCartService = iCartService;
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
                    var userId = (_userRepository.GetList()).First(x => x.Email == loginDto.UserName).Id;
                    await _iCartService.UpdateUserIdCacheAsync(Guid.Parse(userId));
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
                var roleResult = await _userManager.AddToRoleAsync(user, "customer");
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

        public async Task<List<UserDto>> GetListAsync()
        {
            try
            {
                var listUser = _userRepository.GetList().ToList();
                if (!listUser.Any())
                {
                    return new List<UserDto>();
                }
                var result = _iMapper.Map<List<UserDto>>(listUser);
                List<Task<IList<string>>> taskGetUser = new List<Task<IList<string>>>();
                
                for (var i = 0; i < result.Count; i++)
                {
                    result[i].Role = (await _userManager.GetRolesAsync(listUser[i])).First();
                }
                return result;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<bool> EditAsync(RequestUpdateUserDto input)
        {
            try
            {
                var user = (_userRepository.GetList()).Where(x => x.Id == input.Id).FirstOrDefault();
                if (user == null)
                    return false;
                var result = _iMapper.Map(input, user);
                switch (input.Role)
                {
                    case "0":
                        _userRepository.Update(result, "customer");
                        break;
                    default:
                        _userRepository.Update(result, "employee");
                        break;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<RequestUpdateUserDto> GetAsync(Guid id, ClaimsPrincipal identity)
        {
            var user = (_userRepository.GetList()).Where(x => x.Id == id.ToString()).FirstOrDefault();
            var result = _iMapper.Map<RequestUpdateUserDto>(user);
            result.Role = (await _userManager.GetRolesAsync(await _userManager.GetUserAsync(identity))).First();
            return result;
        }
    }
}
