using AutoMapper;
using InventoryManagement.Core.DTOs.Brand;
using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.DTOs.CategorySub;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.DTOs.Model;
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
            #region Company
            CreateMap<CompanyDto, Company>().ReverseMap();
            CreateMap<CompanyUpdateDto, Company>();
            CreateMap<CompanyCreateDto, CompanyDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<Company, CompanyAllDto>();
            #endregion


            #region Category
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryUpdateDto, Category>();
            #endregion


            #region CategorySub
            CreateMap<CategorySubCreateDto, CategorySub>();
            CreateMap<CategorySub, CategorySubDto>();
            CreateMap<CategorySubUpdateDto, CategorySub>();
            #endregion


            #region Brand
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandUpdateDto, Brand>();
            #endregion

            #region Model
            CreateMap<ModelCreateDto, Model>();
            CreateMap<Model, ModelDto>();
            CreateMap<ModelUpdateDto, Model>();
            #endregion
        }
    }
}
