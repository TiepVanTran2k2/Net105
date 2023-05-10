using Application.Contracts.Dtos.Information;
using AutoMapper;
using Domain.Entities.Information;
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
        }
    }
}
