using Application.Contracts.Dtos.ApplicationUser;
using AutoMapper;
using Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace Application.ProfileMapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            #region User
            CreateMap<ApplicationUser, RegisterDto>()
                .ForPath(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            CreateMap<ApplicationUser, IdentityUser>().ReverseMap();
            CreateMap<ApplicationUserDto, IdentityUser>().ReverseMap();

            #endregion
        }
    }
}
