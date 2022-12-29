using AutoMapper;
using InventoryManagement.Core.DTOs.BrandModel;
using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.DTOs.CategorySub;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;

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
            #endregion

            #region Category DTO
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            #endregion


            #region CategorySub DTO
            CreateMap<CategorySub, CategorySubDto>().ReverseMap();
            CreateMap<CategorySub, CategorySubCreateDto>().ReverseMap();
            CreateMap<CategorySub, CategorySubUpdateDto>().ReverseMap();
            #endregion

            #region BrandModel DTO
            CreateMap<BrandModel, BrandModelDto>().ReverseMap();
            CreateMap<BrandModel, BrandModelCreateDto>().ReverseMap();
            CreateMap<BrandModel, BrandModelUpdateDto>().ReverseMap();
            #endregion
        }
    }
}
