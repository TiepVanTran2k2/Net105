using Application.Contracts.Dtos.ApplicationUser;
using Application.Contracts.Dtos.Bill;
using Application.Contracts.Dtos.Lab4;
using Application.Contracts.Dtos.Payment;
using Application.Contracts.Dtos.Product;
using AutoMapper;
using Domain.Entities.ApplicationUser;
using Domain.Entities.Bill;
using Domain.Entities.Lab4;
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
            CreateMap<Product, RequestCreateProductDto>().ReverseMap();
            CreateMap<Product, RequestUpdateProductDto>().ReverseMap();
            #endregion
            CreateMap<lab4, Lab4Dto>().ReverseMap();
            #region Cart
            CreateMap<Product, ProductCacheDto>().ReverseMap();
            CreateMap<Bill, BillDto>().ReverseMap();
            CreateMap<ProductCacheDto, DetailBill>()
                .ForPath(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<PaymentResponseModel, Bill>().ReverseMap();
            #endregion
        }
    }
}
