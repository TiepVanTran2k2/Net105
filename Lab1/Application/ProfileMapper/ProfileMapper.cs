using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Dtos.Information;
using Application.Contracts.Dtos.StudentInformation;
using AutoMapper;
using Domain.Entities.ApplicationUser;
using Domain.Entities.Information;
using Domain.Entities.Lab3;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProfileMapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Information, InformationDto>().ReverseMap();
            CreateMap<Information, InformationInsertDto>().ReverseMap();
            CreateMap<StudentInformation, StudentInformationDto>().ReverseMap();
            CreateMap<StudentInformation, StudentInformationResponseDto>().ReverseMap();
            #region User
            CreateMap<ApplicationUser, RegisterDto>()
                .ForPath(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
            #endregion
        }
    }
}
