using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.BrandModel;
using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace InventoryManagement.Services.Mapping
{

    public class AutoMapperProfile : Profile
    {
        //public T GetValueOrDefault(T defaultValue)
        //{
        //    return HasValue ? value : defaultValue;
        //}

        public AutoMapperProfile()
        {
            #region Company DTO
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, CompanyCreateDto>().ReverseMap();
            CreateMap<Company, CompanyUpdateDto>().ReverseMap();

            //CreateMap<Company, CompanyUpdateDto>().ReverseMap().ForAllMembers(x => x.Condition((source, dest, inMember) => Filter(inMember)));

            //CreateMap<Company, CompanyUpdateDto>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //CreateMap<Company, CompanyUpdateDto>().ReverseMap().ForMember(dest => dest.Description, opt => opt.Ignore());
            //CreateMap<Company, CompanyUpdateDto>().ReverseMap().ForMember(dest => dest.Description.GetValueOrDefault(), opt => opt.MapFrom(src => src.Description));
            //CreateMap<Company, CompanyUpdateDto>().ReverseMap().ForAllMembers(x => x.Condition((source, destination, arg3, arg4, resolutionContext) =>
            //{
            //    Console.WriteLine($"Mapping to {destination.GetType().Name}.{x.DestinationMember.Name}");
            //    return true;
            //})); ;


            //
            //CreateMap<Company, CompanyUpdateDto>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region Category DTO
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            #endregion


            #region BrandModel DTO
            CreateMap<BrandModel, BrandModelGetDto>().ReverseMap();
            CreateMap<BrandModel, BrandModelCreateDto>().ReverseMap();
            CreateMap<BrandModel, BrandModelUpdateDto>().ReverseMap();
            #endregion
        }
    }
}
