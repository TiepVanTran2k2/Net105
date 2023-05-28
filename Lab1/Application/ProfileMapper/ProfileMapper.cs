﻿using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Dtos.Product;
using AutoMapper;
using Domain.Entities.ApplicationUser;
using Domain.Entities.Product;
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
            #region Product
            CreateMap<Product, ProductDto>().ReverseMap();
            #endregion
        }
    }
}
